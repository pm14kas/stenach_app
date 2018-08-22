using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;

namespace Stenach_app
{
    public partial class FormStenach : Form
    {
        string token = "";

        string captchaSid = "";

        string userId = "";

        //используемая версия api
        const string apiVersion = "5.80";

        //адрес стенача
        const string groupId = "-122271186";
        const string postId = "10261";

        //шорткаты
        Dictionary<string, string> methods = new Dictionary<string, string>
        {
            ["post"] = "wall.createComment",
            ["get"] = "wall.getComments",
            ["users"] = "users.get"
        };

        //последний отправленный перед капчей запрос
        Dictionary<string, string> lastRequest = new Dictionary<string, string>();

        // список обработанных в режиме турели постов
        List<string> answeredPosts = new List<string>();

        static HttpClient client = new HttpClient();

        const string message = "опа опа опа па";

        //флаг для показа капчи только один раз
        bool isCaptchaShown = false;

        /*
         * settings section
         */
        int commentAmount = 10;
        bool selfAnswerAllowed = false;
        int timerTurretInterval = 3000;


        public FormStenach()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.cfg"))
            {
                try
                {
                    JObject file = JObject.Parse(File.ReadAllText("config.cfg"));
                    if (file["token"] == null)
                    {
                        this.tokenRequest();
                        return;
                    }
                    this.token = file["token"].ToString();
                    captchaPicture.BackColor = Color.FromName((file["captchaColor"] ?? "Transparent").ToString());
                    //advanced coding of mature people
                    checkBoxCheapPosts.Checked = (bool)((file["cheapPosts"] ?? "True").ToString() == "True");

                    commentAmount = Math.Max(Math.Min(Convert.ToInt32((file["commentAmount"] ?? 10)), 100), 5);
                    timerTurretInterval = Math.Max(Math.Min(Convert.ToInt32((file["timerTurretInterval"] ?? 3000)), 60000), 500);
                    selfAnswerAllowed = Convert.ToBoolean((file["selfAnswerAllowed"] ?? false));

                    this.timerUpdate.Interval = timerTurretInterval;
                    this.textBoxToken.Text = this.token;
                    updateComments();
                }
                catch
                {
                    this.tokenRequest();
                }
            }
            else
            {
                //если нет конфиг файла, то нужно залогиниться
                this.tokenRequest();
            }
        }

        private void textBoxToken_TextChanged(object sender, EventArgs e)
        {
            try
            {
                token = this.textBoxToken.Text;
                string result = MakeRequest(methods["users"]);
                JObject profile = JObject.Parse(result);
                labelUser.Text = profile["response"][0]["first_name"]
                    + " "
                    + profile["response"][0]["last_name"]
                    + " ["
                    + profile["response"][0]["id"]
                    + "]";

                this.userId = profile["response"][0]["id"].ToString();

                saveSettings();

                checkBoxTurret.Enabled = true;
                updateComments();
            }
            catch (NullReferenceException)
            {
                dataGrid.Rows.Clear();
                labelUser.Text = "Нет регистрации";
                showError("Неверный токен");
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            try
            {
                buttonPost.Enabled = false;

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("owner_id", groupId);
                parameters.Add("post_id", postId);
                parameters.Add("from_group", "0");

                if (checkBoxCheapPosts.Checked || dataGrid.Rows.Count == 0)
                {
                    parameters.Add("message", message);
                }
                else
                {
                    if (dataGrid.SelectedRows.Count == 0)
                    {
                        dataGrid.Rows[0].Selected = true;
                    }
                    string id = dataGrid.SelectedRows[0].Cells[0].Value.ToString();
                    string commentUserId = dataGrid.SelectedRows[0].Cells[1].Value.ToString();
                    string name = dataGrid.SelectedRows[0].Cells[2].Value.ToString();

                    if ((commentUserId == this.userId) && !selfAnswerAllowed)
                    {
                        showError("Нельзя отправлять сообщения самому себе");
                        buttonPost.Enabled = true;
                        return;
                    }
                    parameters.Add("reply_to_comment", id);
                    parameters.Add("reply_to_user", commentUserId);
                    parameters.Add("message", "[id" + commentUserId + "|" + name + "], " + message);
                }

                string text = MakeRequest(methods["post"], parameters);

                if (!captchaText.Enabled)
                {
                    updateComments();
                }
                buttonPost.Enabled = true;
            }
            catch
            {
                showError("Ошибка");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            updateComments();
        }

        private string MakeRequest(string method, Dictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();

            if (parameters.ContainsKey("method"))
            {
                parameters.Remove("method");
            }

            parameters["access_token"] = token;
            parameters["v"] = apiVersion;

            if (captchaText.Enabled)
            {
                parameters["captcha_sid"] = captchaSid;
                parameters["captcha_key"] = captchaText.Text;
            }

            string query = "";
            foreach (var pair in parameters)
            {
                query = query + pair.Key + "=" + pair.Value + "&";
            }
            query = query.Substring(0, query.Length - 1);
            var response = client.GetAsync("https://api.vk.com/method/" + method + "?" + query).Result;
            string text = response.Content.ReadAsStringAsync().Result;
            JObject result = JObject.Parse(text);
            if (result["error"] != null)
            {
                if (result["error"]["error_code"].ToString() == "14")
                {
                    captchaSid = result["error"]["captcha_sid"].ToString();
                    captchaText.Enabled = true;
                    captchaText.Text = "";
                    captchaPicture.ImageLocation = result["error"]["captcha_img"].ToString();
                    captchaPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                    parameters.Add("method", method);
                    lastRequest = parameters;

                    showCaptcha();
                }
                else if (result["error"]["error_code"].ToString() == "6")
                {
                    showError("Слишком много запросов в секунду");
                }
                else if (result["error"]["error_code"].ToString() == "8")
                {
                    showError("Неверный запрос");
                }
                else if (result["error"]["error_code"].ToString() == "9")
                {
                    showError("Слишком много однотипных действий");
                }
                else if (result["error"]["error_code"].ToString() == "10")
                {
                    showError("Ошибка сервера");
                }
                else if (result["error"]["error_code"].ToString() == "15")
                {
                    showError("ВАМ БАНН");
                }
                else if (result["error"]["error_code"].ToString() == "17")
                {
                    showError("Требуется подтверждение (не реализовано");
                }
                else if (result["error"]["error_code"].ToString() == "24")
                {
                    showError("Требуется подтверждение (не реализовано)");
                }
                else if (result["error"]["error_code"].ToString() == "29")
                {
                    showError("Достигнут количественный лимит на вызов метода");
                }
                else if (result["error"]["error_code"].ToString() == "203")
                {
                    showError("ВАМ БАНН");
                }
            }
            else
            {
                captchaText.Clear();
                captchaPicture.ImageLocation = "";
                captchaText.Enabled = false;
                if (lastRequest.Count > 0)
                {
                    lastRequest = new Dictionary<string, string>();
                }
            }
            return text;
        }

        private void updateComments()
        {
            if (captchaText.Enabled)
            {
                this.showCaptcha();
                return;
            }
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("owner_id", groupId);
                parameters.Add("post_id", postId);
                parameters.Add("count", commentAmount.ToString());
                parameters.Add("sort", "desc"); //выбирает последние комментарии
                parameters.Add("extended", "1");

                string json = MakeRequest(methods["get"], parameters);
                var result = JObject.Parse(json)["response"];
                var postCount = result["count"];
                var comments = result["items"];
                var jsonProfiles = result["profiles"];
                Dictionary<string, string> profiles = new Dictionary<string, string>();

                labelCount.Text = "Количество постов: " + postCount.ToString();

                //Сохранение пользователей для функционала ответа
                foreach (var profile in jsonProfiles)
                {
                    profiles.Add(
                        Convert.ToString(profile["id"]), 
                        profile["first_name"].ToString()
                    );
                }

                dataGrid.Rows.Clear();
                foreach (JObject comment in comments)
                {
                    dataGrid.Rows.Add(
                        comment["id"],
                        comment["from_id"],
                        profiles[comment["from_id"].ToString()],
                        comment["text"]
                    );
                    if (comment["from_id"].ToString() == this.userId)
                    { 
                        dataGrid.Rows[dataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
                dataGrid.ClearSelection();

                if (dataGrid.Rows.Count > 0)
                {
                    if (timerUpdate.Enabled)
                    {
                        //за один проход необходимо отправлять только одно сообщение
                        bool messageSent = false;
                        for (int i = dataGrid.Rows.Count - 1; i >= 0 && !messageSent; i--)
                        {
                            string id = dataGrid.Rows[i].Cells[0].Value.ToString();
                            string commentUserId = dataGrid.Rows[i].Cells[1].Value.ToString();
                            if (commentUserId == this.userId)
                            {
                                continue;
                            }
                            else if (answeredPosts.Contains(id))
                            {
                                continue;
                            }
                            else
                            {
                                string name = dataGrid.Rows[i].Cells[2].Value.ToString();
                                parameters.Add("reply_to_comment", id);
                                parameters.Add("reply_to_user", commentUserId);
                                parameters.Add("message", "[id" + commentUserId + "|" + name + "], " + message);

                                string text = MakeRequest(methods["post"], parameters);

                                answeredPosts.Add(id);

                                messageSent = true;
                            }
                        }
                    }
                    else
                    {
                        if (checkBoxSetSelection.Checked)
                        {
                            dataGrid.CurrentRow.Selected = false;
                            dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;
                        }
                        else
                        {
                            dataGrid.Rows[0].Selected = true;
                        }
                    }
                }
            }
            catch
            {
                showError("Ошибка");
            }
        }

        private void checkBoxCheapPosts_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCheapPosts.Checked)
            {
                dataGrid.ClearSelection();
                checkBoxSetSelection.Enabled = false;
            }
            else
            {
                checkBoxSetSelection.Enabled = true;
                if (dataGrid.Rows.Count > 0)
                {
                    if (checkBoxSetSelection.Checked)
                    {
                        dataGrid.CurrentRow.Selected = false;
                        dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;
                    }
                    else
                    {
                        dataGrid.Rows[0].Selected = true;
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this.asciiArt() 
                + "\n\n\n\n                      ©dgufan 2018"
                + "\n\n                        v1.0.0.2"
                );
        }

        private string asciiArt()
        {
            return
            "                /´¯/)                      (\\¯`\\ \n" +
            "               /   //    ЗДОХНИ    \\\\   \\ \n" +
            "              /   //     ФАШИСТ     \\\\   \\ \n" +
            "         /´¯/    /´¯\\   ЕБАНЫй   /¯` \\    \\¯`\\ \n" +
            "      / /   /    /    / | _          _ | \\    \\    \\   \\ \\ \n" +
            "    ( (    (    (    /  )  )            (  ( \\    )    )    ) )\n" +
            "     \\                \\/   /         \\   \\/                /\n" +
            "      \\                   /            \\                  / ";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count > 0)
            {
                if (checkBoxSetSelection.Checked)
                {
                    dataGrid.CurrentRow.Selected = false;
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;
                }
                else
                {
                    dataGrid.CurrentRow.Selected = false;
                    dataGrid.Rows[0].Selected = true;
                }
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (captchaText.Enabled)
            {
                showCaptcha();
                return;
            }
            try
            {
                updateComments();
            }
            catch
            {
                showError("Ошибка");
            }
        }

        private void checkBoxTurret_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTurret.Checked)
            {
                if (MessageBox.Show("Включить режим ТЕХНОЛОГИЧЕСКОГО ДОМИНИРОВАНИЯ?", "опа опа опа па intensifies",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != System.Windows.Forms.DialogResult.Yes)
                {
                    checkBoxTurret.Checked = false;
                }
                else
                {
                    timerUpdate.Enabled = true;
                    updateComments();
                }
            }
            else
            {
                timerUpdate.Enabled = false;
            }
        }
        private void showError(string text)
        {
            timerUpdate.Enabled = false;
            checkBoxTurret.Checked = false;
            MessageBox.Show(text);
        }

        private void showCaptcha()
        {
            if (!isCaptchaShown)
            {
                isCaptchaShown = true;
                timerUpdate.Enabled = false;
                checkBoxTurret.Checked = false;
                MessageBox.Show("Надо ввести капчу");
                isCaptchaShown = false;
            }
        }

        private void saveSettings()
        {
            JObject file = new JObject(
                new JProperty("token", this.textBoxToken.Text),
                new JProperty("captchaColor", captchaPicture.BackColor.Name),
                new JProperty("cheapPosts", checkBoxCheapPosts.Checked),
                new JProperty("commentAmount", commentAmount),
                new JProperty("selfAnswerAllowed", selfAnswerAllowed),
                new JProperty("timerTurretInterval", timerTurretInterval)
            );
            File.WriteAllText("config.cfg", file.ToString());
        }

        private void tokenRequest()
        {
            System.Diagnostics.Process.Start("https://oauth.vk.com/authorize?client_id=6413942&display=page&" +
                "redirect_uri=https://oauth.vk.com/blank.html&response_type=token&v=" + apiVersion + "&state=stenach&scope=wall+groups+offline" +
                "&revoke=1");
            MessageBox.Show("Вставьте токен из адресной строки");
        }

        //отправка капчи по нажатию Enter;
        private void captchaText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = MakeRequest(lastRequest["method"], lastRequest);
                if (!captchaText.Enabled)
                {
                    updateComments();
                }
            }
        }

        private void FormStenach_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string commentId = dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            string commentUserId = dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            string commentUserName = dataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            string comment = dataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();

            if ((commentUserId == this.userId) && !selfAnswerAllowed)
            {
                MessageBox.Show(comment);
            }
            else
            {
                FormReply form = new FormReply(commentUserName, comment);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("owner_id", groupId);
                        parameters.Add("post_id", postId);
                        parameters.Add("from_group", "0");
                        parameters.Add("reply_to_comment", commentId);
                        parameters.Add("reply_to_user", commentUserId);
                        parameters.Add("message", "[id" + commentUserId + "|" + commentUserName + "], " + form.getTextBoxInputValue());

                        string text = MakeRequest(methods["post"], parameters);
                        updateComments();
                    }
                    catch
                    {
                        showError("Ошибка");
                    }
                }
                form.Dispose();
            }
        }
    }
}
