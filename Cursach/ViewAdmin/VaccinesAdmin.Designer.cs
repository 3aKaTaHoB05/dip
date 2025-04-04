namespace Cursach
{
    partial class VaccinesAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VaccinesAdmin));
            this.Search = new System.Windows.Forms.TextBox();
            this.Sort = new System.Windows.Forms.ComboBox();
            this.Filtr = new System.Windows.Forms.ComboBox();
            this.ShowMedic = new System.Windows.Forms.DataGridView();
            this.RedDel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ShowMedic)).BeginInit();
            this.RedDel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Search.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(35, 28);
            this.Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(379, 40);
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
            this.Sort.Location = new System.Drawing.Point(820, 30);
            this.Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(248, 39);
            this.Sort.TabIndex = 18;
            this.Sort.DropDown += new System.EventHandler(this.Sort_DropDown);
            this.Sort.DropDownClosed += new System.EventHandler(this.Sort_DropDownClosed);
            this.Sort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseDown);
            // 
            // Filtr
            // 
            this.Filtr.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Filtr.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Filtr.ForeColor = System.Drawing.Color.White;
            this.Filtr.FormattingEnabled = true;
            this.Filtr.Location = new System.Drawing.Point(565, 30);
            this.Filtr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtr.Name = "Filtr";
            this.Filtr.Size = new System.Drawing.Size(248, 39);
            this.Filtr.TabIndex = 17;
            this.Filtr.SelectedIndexChanged += new System.EventHandler(this.Filtr_SelectedIndexChanged);
            this.Filtr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Filtr_MouseDown);
            // 
            // ShowMedic
            // 
            this.ShowMedic.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.ShowMedic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowMedic.ContextMenuStrip = this.RedDel;
            this.ShowMedic.Location = new System.Drawing.Point(35, 90);
            this.ShowMedic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowMedic.Name = "ShowMedic";
            this.ShowMedic.RowHeadersWidth = 51;
            this.ShowMedic.RowTemplate.Height = 24;
            this.ShowMedic.Size = new System.Drawing.Size(1035, 489);
            this.ShowMedic.TabIndex = 16;
            // 
            // RedDel
            // 
            this.RedDel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RedDel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.RedDel.Name = "RedDel";
            this.RedDel.Size = new System.Drawing.Size(181, 52);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.EditToolStripMenuItem.Text = "Редактировать";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(35, 603);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 55);
            this.button4.TabIndex = 20;
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
            this.Add.Location = new System.Drawing.Point(831, 607);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(239, 52);
            this.Add.TabIndex = 21;
            this.Add.Text = "Создать";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1075, 672);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 123;
            this.pictureBox1.TabStop = false;
            // 
            // VaccinesAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 694);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Sort);
            this.Controls.Add(this.Filtr);
            this.Controls.Add(this.ShowMedic);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VaccinesAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вакцины";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VaccinesAdmin_FormClosed);
            this.Load += new System.EventHandler(this.VaccinesAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowMedic)).EndInit();
            this.RedDel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.ComboBox Sort;
        private System.Windows.Forms.ComboBox Filtr;
        private System.Windows.Forms.DataGridView ShowMedic;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.ContextMenuStrip RedDel;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}