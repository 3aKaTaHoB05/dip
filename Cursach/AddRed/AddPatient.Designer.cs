namespace Cursach.DB_Management
{
    partial class AddPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPatient));
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SocialStatusComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Age = new System.Windows.Forms.TextBox();
            this.FIO = new System.Windows.Forms.TextBox();
            this.Phone = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GenderCC = new System.Windows.Forms.ComboBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(699, 458);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(265, 55);
            this.button1.TabIndex = 85;
            this.button1.Text = "Создать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Add_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(64, 458);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 55);
            this.button4.TabIndex = 84;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SocialStatusComboBox
            // 
            this.SocialStatusComboBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.SocialStatusComboBox.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SocialStatusComboBox.ForeColor = System.Drawing.Color.White;
            this.SocialStatusComboBox.FormattingEnabled = true;
            this.SocialStatusComboBox.IntegralHeight = false;
            this.SocialStatusComboBox.Location = new System.Drawing.Point(391, 288);
            this.SocialStatusComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SocialStatusComboBox.Name = "SocialStatusComboBox";
            this.SocialStatusComboBox.Size = new System.Drawing.Size(572, 39);
            this.SocialStatusComboBox.TabIndex = 133;
            this.SocialStatusComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Gender_KeyDown);
            this.SocialStatusComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Gender_KeyPress);
            this.SocialStatusComboBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SocialStatusComboBox_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 32);
            this.label4.TabIndex = 132;
            this.label4.Text = "Социальный статус";
            // 
            // Age
            // 
            this.Age.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Age.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Age.ForeColor = System.Drawing.Color.White;
            this.Age.Location = new System.Drawing.Point(391, 341);
            this.Age.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Age.Name = "Age";
            this.Age.Size = new System.Drawing.Size(572, 40);
            this.Age.TabIndex = 131;
            this.Age.TextChanged += new System.EventHandler(this.Age_TextChanged);
            this.Age.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Age_KeyPress);
            // 
            // FIO
            // 
            this.FIO.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.FIO.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FIO.ForeColor = System.Drawing.Color.White;
            this.FIO.Location = new System.Drawing.Point(391, 130);
            this.FIO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FIO.Name = "FIO";
            this.FIO.Size = new System.Drawing.Size(572, 40);
            this.FIO.TabIndex = 130;
            this.FIO.TextChanged += new System.EventHandler(this.FIO_TextChanged);
            // 
            // Phone
            // 
            this.Phone.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Phone.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Phone.ForeColor = System.Drawing.Color.White;
            this.Phone.Location = new System.Drawing.Point(391, 236);
            this.Phone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Phone.Mask = "+7(000) 000-00-00";
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(572, 35);
            this.Phone.TabIndex = 129;
            this.Phone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.Phone_MaskInputRejected);
            this.Phone.Click += new System.EventHandler(this.Phone_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 341);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 32);
            this.label5.TabIndex = 128;
            this.label5.Text = "Возраст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 32);
            this.label3.TabIndex = 127;
            this.label3.Text = "Телефон";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 32);
            this.label2.TabIndex = 126;
            this.label2.Text = "Пол";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 32);
            this.label1.TabIndex = 125;
            this.label1.Text = "ФИО";
            // 
            // GenderCC
            // 
            this.GenderCC.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.GenderCC.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenderCC.ForeColor = System.Drawing.Color.White;
            this.GenderCC.FormattingEnabled = true;
            this.GenderCC.IntegralHeight = false;
            this.GenderCC.Location = new System.Drawing.Point(391, 185);
            this.GenderCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GenderCC.Name = "GenderCC";
            this.GenderCC.Size = new System.Drawing.Size(572, 39);
            this.GenderCC.TabIndex = 134;
            this.GenderCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Gender_KeyPress);
            this.GenderCC.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Gender_MouseClick);
            this.GenderCC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GenderCC_MouseDown);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(35, 94);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(974, 434);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 136;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(51, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 39);
            this.label6.TabIndex = 137;
            this.label6.Text = "Пациент";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(35, 16);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(974, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 138;
            this.pictureBox1.TabStop = false;
            // 
            // AddPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1043, 542);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GenderCC);
            this.Controls.Add(this.SocialStatusComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Age);
            this.Controls.Add(this.FIO);
            this.Controls.Add(this.Phone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить нового пациента";
            this.Load += new System.EventHandler(this.Add_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        
        private System.Windows.Forms.ComboBox SocialStatusComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Age;
        private System.Windows.Forms.TextBox FIO;
        private System.Windows.Forms.MaskedTextBox Phone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox GenderCC;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}