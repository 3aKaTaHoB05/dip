namespace Cursach
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtCaptchaInput = new System.Windows.Forms.TextBox();
            this.сheckPassword = new System.Windows.Forms.Button();
            this.NewCaptchaButton = new System.Windows.Forms.Button();
            this.ImageCaptcha = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.hidePass = new System.Windows.Forms.Button();
            this.Input = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCaptchaInput
            // 
            this.txtCaptchaInput.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtCaptchaInput.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCaptchaInput.ForeColor = System.Drawing.Color.White;
            this.txtCaptchaInput.Location = new System.Drawing.Point(501, 239);
            this.txtCaptchaInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCaptchaInput.Name = "txtCaptchaInput";
            this.txtCaptchaInput.Size = new System.Drawing.Size(341, 40);
            this.txtCaptchaInput.TabIndex = 24;
            this.txtCaptchaInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCaptchaInput_KeyPress);
            // 
            // сheckPassword
            // 
            this.сheckPassword.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.сheckPassword.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.сheckPassword.FlatAppearance.BorderSize = 3;
            this.сheckPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.сheckPassword.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.сheckPassword.ForeColor = System.Drawing.Color.White;
            this.сheckPassword.Location = new System.Drawing.Point(501, 295);
            this.сheckPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.сheckPassword.Name = "сheckPassword";
            this.сheckPassword.Size = new System.Drawing.Size(261, 69);
            this.сheckPassword.TabIndex = 23;
            this.сheckPassword.Text = "Проверить";
            this.сheckPassword.UseVisualStyleBackColor = false;
            this.сheckPassword.Click += new System.EventHandler(this.CapButton_Click);
            // 
            // NewCaptchaButton
            // 
            this.NewCaptchaButton.BackColor = System.Drawing.Color.White;
            this.NewCaptchaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NewCaptchaButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.NewCaptchaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewCaptchaButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewCaptchaButton.ForeColor = System.Drawing.Color.White;
            this.NewCaptchaButton.Image = ((System.Drawing.Image)(resources.GetObject("NewCaptchaButton.Image")));
            this.NewCaptchaButton.Location = new System.Drawing.Point(769, 296);
            this.NewCaptchaButton.Margin = new System.Windows.Forms.Padding(4);
            this.NewCaptchaButton.Name = "NewCaptchaButton";
            this.NewCaptchaButton.Size = new System.Drawing.Size(73, 68);
            this.NewCaptchaButton.TabIndex = 22;
            this.NewCaptchaButton.UseVisualStyleBackColor = false;
            this.NewCaptchaButton.Click += new System.EventHandler(this.NewCaptchaButton_Click);
            // 
            // ImageCaptcha
            // 
            this.ImageCaptcha.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ImageCaptcha.Location = new System.Drawing.Point(501, 87);
            this.ImageCaptcha.Margin = new System.Windows.Forms.Padding(4);
            this.ImageCaptcha.Name = "ImageCaptcha";
            this.ImageCaptcha.Size = new System.Drawing.Size(343, 146);
            this.ImageCaptcha.TabIndex = 21;
            this.ImageCaptcha.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(480, 23);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(381, 364);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 25;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 140);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Password.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Password.ForeColor = System.Drawing.Color.White;
            this.Password.Location = new System.Drawing.Point(77, 193);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(317, 40);
            this.Password.TabIndex = 2;
            this.Password.Text = "medic";
            this.Password.UseSystemPasswordChar = true;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Login.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Login.ForeColor = System.Drawing.Color.White;
            this.Login.Location = new System.Drawing.Point(77, 140);
            this.Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(317, 40);
            this.Login.TabIndex = 1;
            this.Login.Text = "medic";
            this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged);
            // 
            // hidePass
            // 
            this.hidePass.BackColor = System.Drawing.Color.White;
            this.hidePass.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.hidePass.FlatAppearance.BorderSize = 0;
            this.hidePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hidePass.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hidePass.ForeColor = System.Drawing.Color.White;
            this.hidePass.Image = global::Cursach.Properties.Resources.eye_open;
            this.hidePass.Location = new System.Drawing.Point(410, 194);
            this.hidePass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hidePass.Name = "hidePass";
            this.hidePass.Size = new System.Drawing.Size(40, 39);
            this.hidePass.TabIndex = 6;
            this.hidePass.UseVisualStyleBackColor = false;
            this.hidePass.Click += new System.EventHandler(this.hidePass_Click);
            // 
            // Input
            // 
            this.Input.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Input.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Input.FlatAppearance.BorderSize = 3;
            this.Input.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Input.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Input.ForeColor = System.Drawing.Color.White;
            this.Input.Location = new System.Drawing.Point(75, 280);
            this.Input.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(319, 52);
            this.Input.TabIndex = 0;
            this.Input.Text = "Войти";
            this.Input.UseVisualStyleBackColor = false;
            this.Input.Click += new System.EventHandler(this.LogPas_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(31, 194);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 39);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Location = new System.Drawing.Point(12, 23);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(449, 364);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 26;
            this.pictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(31, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 39);
            this.label6.TabIndex = 138;
            this.label6.Text = "Авторизация";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(494, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 39);
            this.label1.TabIndex = 139;
            this.label1.Text = "Защита от ботов";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(474, 408);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hidePass);
            this.Controls.Add(this.txtCaptchaInput);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.сheckPassword);
            this.Controls.Add(this.NewCaptchaButton);
            this.Controls.Add(this.ImageCaptcha);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCaptchaInput;
        private System.Windows.Forms.Button сheckPassword;
        private System.Windows.Forms.Button NewCaptchaButton;
        private System.Windows.Forms.PictureBox ImageCaptcha;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.Button hidePass;
        private System.Windows.Forms.Button Input;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
    }
}

