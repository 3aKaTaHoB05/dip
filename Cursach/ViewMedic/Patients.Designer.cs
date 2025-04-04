namespace Cursach
{
    partial class Patients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patients));
            this.button4 = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.TextBox();
            this.Sort = new System.Windows.Forms.ComboBox();
            this.Filtr = new System.Windows.Forms.ComboBox();
            this.ShowPatient = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вакцинацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPatient = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ShowPatient)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(23, 500);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 45);
            this.button4.TabIndex = 20;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Search.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(23, 26);
            this.Search.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(416, 33);
            this.Search.TabIndex = 19;
            this.Search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            this.Search.Enter += new System.EventHandler(this.Search_Enter);
            this.Search.Leave += new System.EventHandler(this.Search_Leave);
            // 
            // Sort
            // 
            this.Sort.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Sort.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sort.ForeColor = System.Drawing.Color.White;
            this.Sort.FormattingEnabled = true;
            this.Sort.Location = new System.Drawing.Point(776, 27);
            this.Sort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(187, 32);
            this.Sort.TabIndex = 18;
            this.Sort.SelectedIndexChanged += new System.EventHandler(this.Sort_SelectedIndexChanged);
            this.Sort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sort_KeyPress);
            this.Sort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseClick);
            this.Sort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseDown);
            // 
            // Filtr
            // 
            this.Filtr.BackColor = System.Drawing.SystemColors.Highlight;
            this.Filtr.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Filtr.ForeColor = System.Drawing.Color.White;
            this.Filtr.FormattingEnabled = true;
            this.Filtr.Location = new System.Drawing.Point(585, 26);
            this.Filtr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Filtr.Name = "Filtr";
            this.Filtr.Size = new System.Drawing.Size(187, 32);
            this.Filtr.TabIndex = 17;
            this.Filtr.SelectedIndexChanged += new System.EventHandler(this.Filtr_SelectedIndexChanged);
            this.Filtr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Filtr_KeyPress);
            this.Filtr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Filtr_MouseClick);
            this.Filtr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Filtr_MouseDown);
            // 
            // ShowPatient
            // 
            this.ShowPatient.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.ShowPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowPatient.ContextMenuStrip = this.contextMenuStrip1;
            this.ShowPatient.Location = new System.Drawing.Point(23, 79);
            this.ShowPatient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ShowPatient.Name = "ShowPatient";
            this.ShowPatient.RowHeadersWidth = 51;
            this.ShowPatient.RowTemplate.Height = 24;
            this.ShowPatient.Size = new System.Drawing.Size(940, 397);
            this.ShowPatient.TabIndex = 16;
            this.ShowPatient.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ShowPatient_CellFormatting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вакцинацияToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 70);
            // 
            // вакцинацияToolStripMenuItem
            // 
            this.вакцинацияToolStripMenuItem.Name = "вакцинацияToolStripMenuItem";
            this.вакцинацияToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.вакцинацияToolStripMenuItem.Text = "Вакцинация";
            this.вакцинацияToolStripMenuItem.Click += new System.EventHandler(this.VaccinationToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.EditToolStripMenuItem.Text = "Редактировать";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // AddPatient
            // 
            this.AddPatient.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddPatient.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.AddPatient.FlatAppearance.BorderSize = 3;
            this.AddPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPatient.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPatient.ForeColor = System.Drawing.Color.White;
            this.AddPatient.Location = new System.Drawing.Point(764, 500);
            this.AddPatient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddPatient.Name = "AddPatient";
            this.AddPatient.Size = new System.Drawing.Size(199, 45);
            this.AddPatient.TabIndex = 86;
            this.AddPatient.Text = "Создать";
            this.AddPatient.UseVisualStyleBackColor = false;
            this.AddPatient.Click += new System.EventHandler(this.AddPatient_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(11, 11);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(965, 560);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 87;
            this.pictureBox3.TabStop = false;
            // 
            // Patients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 582);
            this.Controls.Add(this.AddPatient);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Sort);
            this.Controls.Add(this.Filtr);
            this.Controls.Add(this.ShowPatient);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Patients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пациенты";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Patients_FormClosed);
            this.Load += new System.EventHandler(this.Patients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowPatient)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.ComboBox Sort;
        private System.Windows.Forms.ComboBox Filtr;
        private System.Windows.Forms.DataGridView ShowPatient;
        private System.Windows.Forms.Button AddPatient;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вакцинацияToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}