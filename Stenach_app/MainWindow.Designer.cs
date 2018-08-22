namespace Stenach_app
{
    partial class FormStenach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonPost = new System.Windows.Forms.Button();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.ColumnPostId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.checkBoxCheapPosts = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.captchaPicture = new System.Windows.Forms.PictureBox();
            this.captchaText = new System.Windows.Forms.TextBox();
            this.checkBoxSetSelection = new System.Windows.Forms.CheckBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.checkBoxTurret = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPost
            // 
            this.buttonPost.Location = new System.Drawing.Point(16, 83);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(246, 23);
            this.buttonPost.TabIndex = 0;
            this.buttonPost.Text = "опа опа опа па";
            this.buttonPost.UseVisualStyleBackColor = true;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
            // 
            // textBoxToken
            // 
            this.textBoxToken.Location = new System.Drawing.Point(6, 32);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(238, 20);
            this.textBoxToken.TabIndex = 1;
            this.textBoxToken.TextChanged += new System.EventHandler(this.textBoxToken_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "вставить свой access token";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPostId,
            this.ColumnUserId,
            this.ColumnName,
            this.ColumnPost});
            this.dataGrid.Location = new System.Drawing.Point(5, 48);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(464, 272);
            this.dataGrid.TabIndex = 3;
            this.dataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellDoubleClick);
            // 
            // ColumnPostId
            // 
            this.ColumnPostId.HeaderText = "ID";
            this.ColumnPostId.MinimumWidth = 10;
            this.ColumnPostId.Name = "ColumnPostId";
            this.ColumnPostId.ReadOnly = true;
            this.ColumnPostId.Visible = false;
            this.ColumnPostId.Width = 50;
            // 
            // ColumnUserId
            // 
            this.ColumnUserId.HeaderText = "ID стеначера";
            this.ColumnUserId.Name = "ColumnUserId";
            this.ColumnUserId.ReadOnly = true;
            this.ColumnUserId.Visible = false;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Имя стеначера";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 150;
            // 
            // ColumnPost
            // 
            this.ColumnPost.HeaderText = "Текст";
            this.ColumnPost.Name = "ColumnPost";
            this.ColumnPost.ReadOnly = true;
            this.ColumnPost.Width = 265;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(6, 19);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(240, 23);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Обновить данные";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxToken);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 65);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Логин";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelCount);
            this.groupBox2.Controls.Add(this.buttonUpdate);
            this.groupBox2.Controls.Add(this.dataGrid);
            this.groupBox2.Location = new System.Drawing.Point(16, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 326);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Данные о комментах";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(253, 24);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(156, 13);
            this.labelCount.TabIndex = 5;
            this.labelCount.Text = "Количество опа опа опа па: 0";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(268, 28);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(13, 13);
            this.labelUser.TabIndex = 6;
            this.labelUser.Text = "_";
            // 
            // checkBoxCheapPosts
            // 
            this.checkBoxCheapPosts.AutoSize = true;
            this.checkBoxCheapPosts.Location = new System.Drawing.Point(16, 112);
            this.checkBoxCheapPosts.Name = "checkBoxCheapPosts";
            this.checkBoxCheapPosts.Size = new System.Drawing.Size(153, 17);
            this.checkBoxCheapPosts.TabIndex = 7;
            this.checkBoxCheapPosts.Text = "Дешевые опа опа опа па";
            this.checkBoxCheapPosts.UseVisualStyleBackColor = true;
            this.checkBoxCheapPosts.CheckedChanged += new System.EventHandler(this.checkBoxCheapPosts_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Пользователь";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // captchaPicture
            // 
            this.captchaPicture.BackColor = System.Drawing.Color.Transparent;
            this.captchaPicture.Location = new System.Drawing.Point(274, 56);
            this.captchaPicture.Name = "captchaPicture";
            this.captchaPicture.Size = new System.Drawing.Size(210, 50);
            this.captchaPicture.TabIndex = 9;
            this.captchaPicture.TabStop = false;
            // 
            // captchaText
            // 
            this.captchaText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.captchaText.Location = new System.Drawing.Point(274, 120);
            this.captchaText.Name = "captchaText";
            this.captchaText.Size = new System.Drawing.Size(210, 13);
            this.captchaText.TabIndex = 10;
            this.captchaText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.captchaText_KeyDown);
            // 
            // checkBoxSetSelection
            // 
            this.checkBoxSetSelection.AutoSize = true;
            this.checkBoxSetSelection.Location = new System.Drawing.Point(16, 131);
            this.checkBoxSetSelection.Name = "checkBoxSetSelection";
            this.checkBoxSetSelection.Size = new System.Drawing.Size(217, 17);
            this.checkBoxSetSelection.TabIndex = 11;
            this.checkBoxSetSelection.Text = "Перенести курсор на последний пост";
            this.checkBoxSetSelection.UseVisualStyleBackColor = true;
            this.checkBoxSetSelection.Visible = false;
            this.checkBoxSetSelection.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 3000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // checkBoxTurret
            // 
            this.checkBoxTurret.AutoSize = true;
            this.checkBoxTurret.Enabled = false;
            this.checkBoxTurret.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxTurret.Location = new System.Drawing.Point(16, 139);
            this.checkBoxTurret.Name = "checkBoxTurret";
            this.checkBoxTurret.Size = new System.Drawing.Size(431, 28);
            this.checkBoxTurret.TabIndex = 12;
            this.checkBoxTurret.Text = "ТЕХНОЛОГИЧЕСКОЕ ДОМИНИРОВАНИЕ";
            this.checkBoxTurret.UseVisualStyleBackColor = true;
            this.checkBoxTurret.CheckedChanged += new System.EventHandler(this.checkBoxTurret_CheckedChanged);
            // 
            // FormStenach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 511);
            this.Controls.Add(this.checkBoxTurret);
            this.Controls.Add(this.checkBoxSetSelection);
            this.Controls.Add(this.captchaText);
            this.Controls.Add(this.captchaPicture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxCheapPosts);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPost);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 550);
            this.MinimumSize = new System.Drawing.Size(520, 550);
            this.Name = "FormStenach";
            this.Text = "опа опа опа па";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStenach_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.CheckBox checkBoxCheapPosts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox captchaPicture;
        private System.Windows.Forms.TextBox captchaText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPostId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPost;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.CheckBox checkBoxSetSelection;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.CheckBox checkBoxTurret;
    }
}

