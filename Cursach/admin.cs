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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void RestoreImport_Click(object sender, EventArgs e)
        {

            this.Hide();
            RestoreImport restoreImport = new RestoreImport();
            restoreImport.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }

       
    }
}
