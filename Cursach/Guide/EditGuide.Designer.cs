namespace Cursach.Guide
{
    partial class EditGuide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGuide));
            this.label2 = new System.Windows.Forms.Label();
            this.NewUnitNameTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.EditNewName = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 29);
            this.label2.TabIndex = 30;
            this.label2.Text = "Новое название:";
            // 
            // NewUnitNameTextBox
            // 
            this.NewUnitNameTextBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.NewUnitNameTextBox.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewUnitNameTextBox.ForeColor = System.Drawing.Color.White;
            this.NewUnitNameTextBox.Location = new System.Drawing.Point(27, 84);
            this.NewUnitNameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NewUnitNameTextBox.Name = "NewUnitNameTextBox";
            this.NewUnitNameTextBox.Size = new System.Drawing.Size(656, 40);
            this.NewUnitNameTextBox.TabIndex = 29;
            this.NewUnitNameTextBox.TextChanged += new System.EventHandler(this.NewUnitNameTextBox_TextChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(12, 7);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(679, 209);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 33;
            this.pictureBox3.TabStop = false;
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.White;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancel.Location = new System.Drawing.Point(27, 153);
            this.Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(184, 55);
            this.Cancel.TabIndex = 88;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // EditNewName
            // 
            this.EditNewName.BackColor = System.Drawing.SystemColors.Highlight;
            this.EditNewName.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.EditNewName.FlatAppearance.BorderSize = 3;
            this.EditNewName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditNewName.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditNewName.ForeColor = System.Drawing.Color.White;
            this.EditNewName.Location = new System.Drawing.Point(499, 153);
            this.EditNewName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditNewName.Name = "EditNewName";
            this.EditNewName.Size = new System.Drawing.Size(184, 55);
            this.EditNewName.TabIndex = 89;
            this.EditNewName.Text = "Изменить";
            this.EditNewName.UseVisualStyleBackColor = false;
            this.EditNewName.Click += new System.EventHandler(this.Edit_Click);
            // 
            // EditGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(704, 228);
            this.Controls.Add(this.EditNewName);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewUnitNameTextBox);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditGuide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование записи справочника";
            this.Load += new System.EventHandler(this.EditGuide_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NewUnitNameTextBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button EditNewName;
    }
}