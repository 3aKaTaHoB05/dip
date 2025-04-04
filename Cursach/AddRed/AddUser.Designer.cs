namespace Cursach.DB_Management
{
    partial class AddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUser));
            this.AddFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AddAge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserRole = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AddLogin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AddPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddPhone = new System.Windows.Forms.MaskedTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AddFIO
            // 
            this.AddFIO.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddFIO.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddFIO.ForeColor = System.Drawing.Color.White;
            this.AddFIO.Location = new System.Drawing.Point(267, 146);
            this.AddFIO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddFIO.Name = "AddFIO";
            this.AddFIO.Size = new System.Drawing.Size(411, 40);
            this.AddFIO.TabIndex = 21;
            this.AddFIO.TextChanged += new System.EventHandler(this.AddFIO_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "ФИО";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 32);
            this.label3.TabIndex = 25;
            this.label3.Text = "Телефон";
            // 
            // AddAge
            // 
            this.AddAge.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddAge.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddAge.ForeColor = System.Drawing.Color.White;
            this.AddAge.Location = new System.Drawing.Point(267, 250);
            this.AddAge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddAge.Name = "AddAge";
            this.AddAge.Size = new System.Drawing.Size(411, 40);
            this.AddAge.TabIndex = 29;
            this.AddAge.TextChanged += new System.EventHandler(this.AddAge_TextChanged);
            this.AddAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddAge_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(72, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 32);
            this.label5.TabIndex = 28;
            this.label5.Text = "Возраст";
            // 
            // UserRole
            // 
            this.UserRole.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.UserRole.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserRole.ForeColor = System.Drawing.Color.White;
            this.UserRole.FormattingEnabled = true;
            this.UserRole.Location = new System.Drawing.Point(267, 300);
            this.UserRole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserRole.Name = "UserRole";
            this.UserRole.Size = new System.Drawing.Size(411, 39);
            this.UserRole.TabIndex = 33;
            this.UserRole.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Role_KeyDown);
            this.UserRole.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Role_KeyPress);
            this.UserRole.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Role_MouseClick);
            this.UserRole.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserRole_MouseDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(72, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 32);
            this.label7.TabIndex = 32;
            this.label7.Text = "Роль";
            // 
            // AddLogin
            // 
            this.AddLogin.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddLogin.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddLogin.ForeColor = System.Drawing.Color.White;
            this.AddLogin.Location = new System.Drawing.Point(267, 347);
            this.AddLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddLogin.Name = "AddLogin";
            this.AddLogin.Size = new System.Drawing.Size(411, 40);
            this.AddLogin.TabIndex = 35;
            this.AddLogin.TextChanged += new System.EventHandler(this.AddLogin_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(72, 350);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 32);
            this.label8.TabIndex = 34;
            this.label8.Text = "Логин";
            // 
            // AddPassword
            // 
            this.AddPassword.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddPassword.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPassword.ForeColor = System.Drawing.Color.White;
            this.AddPassword.Location = new System.Drawing.Point(267, 398);
            this.AddPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddPassword.Name = "AddPassword";
            this.AddPassword.PasswordChar = '*';
            this.AddPassword.Size = new System.Drawing.Size(411, 40);
            this.AddPassword.TabIndex = 37;
            this.AddPassword.TextChanged += new System.EventHandler(this.AddPassword_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(73, 400);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 32);
            this.label9.TabIndex = 36;
            this.label9.Text = "Пароль";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(77, 505);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 55);
            this.button4.TabIndex = 38;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Add.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Add.FlatAppearance.BorderSize = 3;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Add.ForeColor = System.Drawing.Color.White;
            this.Add.Location = new System.Drawing.Point(413, 505);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(265, 55);
            this.Add.TabIndex = 39;
            this.Add.Text = "Создать";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(33, 114);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(703, 478);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 90;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(51, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 39);
            this.label2.TabIndex = 91;
            this.label2.Text = "Пользователь";
            // 
            // AddPhone
            // 
            this.AddPhone.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddPhone.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPhone.ForeColor = System.Drawing.Color.White;
            this.AddPhone.Location = new System.Drawing.Point(267, 201);
            this.AddPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddPhone.Mask = "+7(000) 000-00-00";
            this.AddPhone.Name = "AddPhone";
            this.AddPhone.Size = new System.Drawing.Size(409, 35);
            this.AddPhone.TabIndex = 92;
            this.AddPhone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.AddPhone_MaskInputRejected);
            this.AddPhone.Click += new System.EventHandler(this.AddPhone_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(33, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(703, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 127;
            this.pictureBox1.TabStop = false;
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(771, 625);
            this.Controls.Add(this.AddPhone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.AddPassword);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.AddLogin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.UserRole);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AddAge);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddFIO);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить нового пользователя";
            this.Load += new System.EventHandler(this.AddUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AddFIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AddAge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox UserRole;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AddLogin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox AddPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox AddPhone;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}