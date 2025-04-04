namespace Cursach
{
    partial class Otchot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Otchot));
            this.button4 = new System.Windows.Forms.Button();
            this.AddVaccination = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.TextBox();
            this.ShowVaccination = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Report = new System.Windows.Forms.ToolStripMenuItem();
            this.DateVaccinationStart = new System.Windows.Forms.DateTimePicker();
            this.DateVaccinationEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ShowVaccination)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.DarkGray;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(21, 612);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 55);
            this.button4.TabIndex = 25;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // AddVaccination
            // 
            this.AddVaccination.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddVaccination.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.AddVaccination.FlatAppearance.BorderSize = 3;
            this.AddVaccination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddVaccination.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddVaccination.ForeColor = System.Drawing.Color.White;
            this.AddVaccination.Location = new System.Drawing.Point(1063, 612);
            this.AddVaccination.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddVaccination.Name = "AddVaccination";
            this.AddVaccination.Size = new System.Drawing.Size(265, 55);
            this.AddVaccination.TabIndex = 92;
            this.AddVaccination.Text = "Экспорт";
            this.AddVaccination.UseVisualStyleBackColor = false;
            this.AddVaccination.Click += new System.EventHandler(this.ReporotVaccination_Click);
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Search.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(21, 30);
            this.Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(553, 40);
            this.Search.TabIndex = 89;
            this.Search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            this.Search.Enter += new System.EventHandler(this.Search_Enter);
            this.Search.Leave += new System.EventHandler(this.Search_Leave);
            // 
            // ShowVaccination
            // 
            this.ShowVaccination.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.ShowVaccination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowVaccination.ContextMenuStrip = this.contextMenuStrip1;
            this.ShowVaccination.Location = new System.Drawing.Point(21, 110);
            this.ShowVaccination.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowVaccination.Name = "ShowVaccination";
            this.ShowVaccination.RowHeadersWidth = 51;
            this.ShowVaccination.RowTemplate.Height = 24;
            this.ShowVaccination.Size = new System.Drawing.Size(1307, 479);
            this.ShowVaccination.TabIndex = 88;
            this.ShowVaccination.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ShowVaccination_CellFormatting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Report});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 28);
            // 
            // Report
            // 
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(192, 24);
            this.Report.Text = "Создать справку";
            this.Report.Click += new System.EventHandler(this.Report_Click);
            // 
            // DateVaccinationStart
            // 
            this.DateVaccinationStart.Font = new System.Drawing.Font("Comic Sans MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DateVaccinationStart.Location = new System.Drawing.Point(779, 30);
            this.DateVaccinationStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateVaccinationStart.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.DateVaccinationStart.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.DateVaccinationStart.Name = "DateVaccinationStart";
            this.DateVaccinationStart.Size = new System.Drawing.Size(245, 38);
            this.DateVaccinationStart.TabIndex = 117;
            this.DateVaccinationStart.ValueChanged += new System.EventHandler(this.StartDate_ValueChanged);
            // 
            // DateVaccinationEnd
            // 
            this.DateVaccinationEnd.Font = new System.Drawing.Font("Comic Sans MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DateVaccinationEnd.Location = new System.Drawing.Point(1081, 30);
            this.DateVaccinationEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateVaccinationEnd.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.DateVaccinationEnd.Name = "DateVaccinationEnd";
            this.DateVaccinationEnd.Size = new System.Drawing.Size(245, 38);
            this.DateVaccinationEnd.TabIndex = 118;
            this.DateVaccinationEnd.ValueChanged += new System.EventHandler(this.EndDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(736, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 31);
            this.label1.TabIndex = 119;
            this.label1.Text = "от";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(1036, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 31);
            this.label2.TabIndex = 120;
            this.label2.Text = "до";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 98);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1326, 580);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 122;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(12, 20);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1326, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 121;
            this.pictureBox2.TabStop = false;
            // 
            // Otchot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1353, 702);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateVaccinationEnd);
            this.Controls.Add(this.DateVaccinationStart);
            this.Controls.Add(this.AddVaccination);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.ShowVaccination);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Otchot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспорт данных";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Otchot_FormClosed);
            this.Load += new System.EventHandler(this.Otchot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowVaccination)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button AddVaccination;
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.DataGridView ShowVaccination;
        private System.Windows.Forms.DateTimePicker DateVaccinationStart;
        private System.Windows.Forms.DateTimePicker DateVaccinationEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Report;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}