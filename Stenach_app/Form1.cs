using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Stenach_app
{
    public partial class FormStenach : Form
    {

        static HttpListener httpListener = new HttpListener();

        static string token = "";

        static string captchaSid = "";

        static HttpClient client = new HttpClient();


        static string groupId = "-122271186";
        static string postId = "10261";

        public FormStenach()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            httpListener.Prefixes.Add("http://localhost:5000/");
            httpListener.Start();
            if (File.Exists("config.cfg"))
            {
                byte[] file = File.ReadAllBytes("config.cfg");
                StringBuilder str = new StringBuilder();
                foreach (byte b in file)
                {
                    str.Append((char)(b));
                }
                this.textBoxToken.Text = str.ToString();
                token = str.ToString();

            }
            else
            {
                System.Diagnostics.Process.Start("https://oauth.vk.com/authorize?client_id=6413942&display=page&" +
                    "redirect_uri=https://oauth.vk.com/blank.html&response_type=token&v=5.73&state=stenach&scope=wall+groups+offline" +
                    "&revoke=1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                buttonPost.Enabled = false;

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("owner_id", groupId);
                parameters.Add("post_id", postId);
                parameters.Add("from_group", "0");

                if (checkBox.Checked || dataGrid.Rows.Count == 0)
                {
                    parameters.Add("message", "опа опа опа па");
                }
                else
                {
                    if (dataGrid.SelectedRows.Count == 0)
                    {
                        dataGrid.Rows[0].Selected = true;
                    }
                    string id = dataGrid.SelectedRows[0].Cells[0].Value.ToString();
                    string userId = dataGrid.SelectedRows[0].Cells[1].Value.ToString();
                    string name = dataGrid.SelectedRows[0].Cells[2].Value.ToString();
                    parameters.Add("reply_to_comment", id);
                    parameters.Add("reply_to_user", userId);
                    parameters.Add("message", "[id" + userId + "|" + name + "], опа опа опа па");
                }


                string text = MakeRequest("wall.createComment", parameters);

                if (!captchaText.Enabled)
                {
                    updateComments();
                }
                buttonPost.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            updateComments();
        }


        private string MakeRequest(string method, Dictionary<string, string> parameters)
        {
            string query = "";
            if (!parameters.ContainsKey("access_token"))
            {
                parameters.Add("access_token", token);
            }
            if (!parameters.ContainsKey("v"))
            {
                parameters.Add("v", "5.74");
            }

            if (captchaText.Enabled)
            {
                parameters.Add("captcha_sid", captchaSid);
                parameters.Add("captcha_key", captchaText.Text);
            }

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
                    captchaPicture.ImageLocation = result["error"]["captcha_img"].ToString();
                    captchaPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    MessageBox.Show("Надо ввести капчу");
                }
                else if (result["error"]["error_code"].ToString() == "6")
                {
                    MessageBox.Show("Слишком много запросов в секунду");
                }
            }
            else
            {
                captchaText.Clear();
                captchaPicture.ImageLocation = "";
                captchaText.Enabled = false;
            }
            return text;
        }

        private void textBoxToken_TextChanged(object sender, EventArgs e)
        {
            try
            {
                token = this.textBoxToken.Text;
                string json = MakeRequest("account.getProfileInfo", new Dictionary<string, string>());
                JObject profile = JObject.Parse(json);
                labelUser.Text = profile["response"]["first_name"] + " " + profile["response"]["last_name"];

                using (System.IO.FileStream file = File.OpenWrite("config.cfg"))
                {
                    for (int i = 0; i < this.textBoxToken.Text.Length; i++)
                    {
                        file.WriteByte((byte)(this.textBoxToken.Text[i]));
                    }
                    file.Close();
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Неверный токен");
                labelUser.Text = "Нет регистрации";
            }
        }

        private void updateComments()
        {
            if (captchaText.Enabled)
            {
                MessageBox.Show("Надо ввести капчу");
                return;
            }
            try
            {
                dataGrid.Rows.Clear();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("owner_id", groupId);
                parameters.Add("post_id", postId);
                parameters.Add("count", "10");
                parameters.Add("sort", "desc");
                parameters.Add("extended", "1");

                string json = MakeRequest("wall.getComments", parameters);
                var result = JObject.Parse(json)["response"];
                var comments = result["items"];
                var jsonProfiles = result["profiles"];
                Dictionary<string, string> profiles = new Dictionary<string, string>();


                foreach (var profile in jsonProfiles)
                {
                    profiles.Add(Convert.ToString(profile["id"]), profile["first_name"].ToString());
                }

                foreach (JObject comment in comments)
                {
                    dataGrid.Rows.Add(
                        comment["id"],
                        comment["from_id"],
                        profiles[comment["from_id"].ToString()],
                        comment["text"]
                    );
                }
                dataGrid.ClearSelection();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                dataGrid.ClearSelection();
            }
            else
            {
                if (dataGrid.Rows.Count > 0)
                {
                    dataGrid.Rows[0].Selected = true;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(antifa() + "\n\n\n\n                      ©dgufan 2018");
        }

        private string antifa()
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
    }
}
