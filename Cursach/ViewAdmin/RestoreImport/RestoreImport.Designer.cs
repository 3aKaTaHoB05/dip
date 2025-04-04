
namespace Cursach.ViewAdmin
{
    partial class RestoreImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreImport));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.nameTable = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Import = new System.Windows.Forms.Button();
            this.Exit2 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Restore = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.nameTable);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.Import);
            this.tabPage4.Controls.Add(this.Exit2);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(433, 185);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Импортировать";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // nameTable
            // 
            this.nameTable.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.nameTable.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTable.ForeColor = System.Drawing.Color.White;
            this.nameTable.FormattingEnabled = true;
            this.nameTable.Location = new System.Drawing.Point(5, 43);
            this.nameTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameTable.Name = "nameTable";
            this.nameTable.Size = new System.Drawing.Size(411, 39);
            this.nameTable.TabIndex = 43;
            this.nameTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameTable_KeyDown);
            this.nameTable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTable_KeyPress);
            this.nameTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nameTable_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(5, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 29);
            this.label4.TabIndex = 42;
            this.label4.Text = "Название таблицы:";
            // 
            // Import
            // 
            this.Import.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Import.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Import.FlatAppearance.BorderSize = 3;
            this.Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Import.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Import.ForeColor = System.Drawing.Color.White;
            this.Import.Location = new System.Drawing.Point(117, 111);
            this.Import.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(300, 52);
            this.Import.TabIndex = 36;
            this.Import.Text = "Импорт";
            this.Import.UseVisualStyleBackColor = false;
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // Exit2
            // 
            this.Exit2.BackColor = System.Drawing.Color.White;
            this.Exit2.FlatAppearance.BorderSize = 0;
            this.Exit2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit2.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit2.Image = ((System.Drawing.Image)(resources.GetObject("Exit2.Image")));
            this.Exit2.Location = new System.Drawing.Point(5, 111);
            this.Exit2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Exit2.Name = "Exit2";
            this.Exit2.Size = new System.Drawing.Size(60, 55);
            this.Exit2.TabIndex = 39;
            this.Exit2.UseVisualStyleBackColor = false;
            this.Exit2.Click += new System.EventHandler(this.Exit2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Restore);
            this.tabPage1.Controls.Add(this.Exit);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(433, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Восстановить структуру БД";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Restore
            // 
            this.Restore.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Restore.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Restore.FlatAppearance.BorderSize = 3;
            this.Restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restore.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Restore.ForeColor = System.Drawing.Color.White;
            this.Restore.Location = new System.Drawing.Point(117, 111);
            this.Restore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(300, 52);
            this.Restore.TabIndex = 37;
            this.Restore.Text = "Восстановить";
            this.Restore.UseVisualStyleBackColor = false;
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.White;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit.ForeColor = System.Drawing.Color.White;
            this.Exit.Image = ((System.Drawing.Image)(resources.GetObject("Exit.Image")));
            this.Exit.Location = new System.Drawing.Point(5, 111);
            this.Exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(60, 55);
            this.Exit.TabIndex = 25;
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Comic Sans MS", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(15, 14);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(441, 223);
            this.tabControl1.TabIndex = 1;
            // 
            // RestoreImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 242);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RestoreImport";
            this.Text = "Восстановить и импортировать";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RestoreImport_FormClosed);
            this.Load += new System.EventHandler(this.RestoreImport_Load);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.Button Exit2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox nameTable;
        private System.Windows.Forms.Button Restore;
    }
}