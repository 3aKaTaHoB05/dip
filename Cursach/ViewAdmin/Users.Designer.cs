namespace Cursach
{
    partial class Users
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.Search = new System.Windows.Forms.TextBox();
            this.Sort = new System.Windows.Forms.ComboBox();
            this.Filtr = new System.Windows.Forms.ComboBox();
            this.ShowUsers = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.RedDel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Add = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ShowUsers)).BeginInit();
            this.RedDel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Search.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(29, 28);
            this.Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(725, 40);
            this.Search.TabIndex = 14;
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
            this.Sort.Location = new System.Drawing.Point(1320, 30);
            this.Sort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(248, 39);
            this.Sort.TabIndex = 13;
            this.Sort.SelectedIndexChanged += new System.EventHandler(this.Sort_SelectedIndexChanged);
            this.Sort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sort_KeyDown);
            this.Sort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseClick);
            this.Sort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseDown);
            // 
            // Filtr
            // 
            this.Filtr.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Filtr.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Filtr.ForeColor = System.Drawing.Color.White;
            this.Filtr.FormattingEnabled = true;
            this.Filtr.Location = new System.Drawing.Point(939, 30);
            this.Filtr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filtr.Name = "Filtr";
            this.Filtr.Size = new System.Drawing.Size(376, 39);
            this.Filtr.TabIndex = 12;
            this.Filtr.SelectedIndexChanged += new System.EventHandler(this.Filtr_SelectedIndexChanged);
            this.Filtr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filtr_KeyDown);
            this.Filtr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Filtr_MouseDown);
            // 
            // ShowUsers
            // 
            this.ShowUsers.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.ShowUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowUsers.Location = new System.Drawing.Point(29, 94);
            this.ShowUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowUsers.Name = "ShowUsers";
            this.ShowUsers.RowHeadersWidth = 51;
            this.ShowUsers.RowTemplate.Height = 24;
            this.ShowUsers.Size = new System.Drawing.Size(1539, 489);
            this.ShowUsers.TabIndex = 11;
            this.ShowUsers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ShowUsers_CellFormatting);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(29, 590);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 55);
            this.button4.TabIndex = 15;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
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
            // Add
            // 
            this.Add.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Add.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.Add.FlatAppearance.BorderSize = 3;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Add.ForeColor = System.Drawing.Color.White;
            this.Add.Location = new System.Drawing.Point(1331, 590);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(239, 52);
            this.Add.TabIndex = 22;
            this.Add.Text = "Создать";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(12, 11);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1575, 643);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1599, 665);
            this.ContextMenuStrip = this.RedDel;
            this.Controls.Add(this.Add);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Sort);
            this.Controls.Add(this.Filtr);
            this.Controls.Add(this.ShowUsers);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пользователи";

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Users_FormClosed);
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowUsers)).EndInit();
            this.RedDel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.ComboBox Sort;
        private System.Windows.Forms.ComboBox Filtr;
        private System.Windows.Forms.DataGridView ShowUsers;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip RedDel;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}