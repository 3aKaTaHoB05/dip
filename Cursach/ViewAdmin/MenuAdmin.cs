using Cursach.ViewAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach
{
    public partial class MenuAdmin : Form
    {
        private string fullName;
        public MenuAdmin(string fio)
        {
            InitializeComponent();
            this.fullName = $"{fio}";
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            label1.Text = $"Удачного дня,\n{fullName}!😎";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminFullName = fullName;

            this.Hide();
            VaccinesAdmin vaccinesAdmin= new VaccinesAdmin();
            vaccinesAdmin.AdminFullName = adminFullName;
            vaccinesAdmin.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string adminFullName = fullName;

            this.Hide();
            GuideAdmin guideAdmin = new GuideAdmin();
            guideAdmin.AdminFullName = adminFullName;
            guideAdmin.ShowDialog();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            string adminFullName = fullName;

            this.Hide();
            Users users = new Users();
            users.AdminFullName = adminFullName;
            users.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string adminFullName = fullName;

            this.Hide();
            Otchot otchot = new Otchot();
            otchot.AdminFullName = adminFullName;
            otchot.ShowDialog();
        }
        private void RestoreImport_Click(object sender, EventArgs e)
        {
            
        }
        private void MenuAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }

       
    }
}
