using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Cursach
{
    
    public partial class MenuMedic : Form
    {
       
        private string fullName;
        public MenuMedic(string fio)
        {
            InitializeComponent();
            this.fullName = $"{fio}";
        }

        private void MenuMedic_Load(object sender, EventArgs e)
        {
            label2.Text = $"Удачного дня,\n{fullName}!😎";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string medicFullName = fullName;

            this.Hide(); 
            VaccinesMedic vaccinesMedic = new VaccinesMedic();
            vaccinesMedic.MedicFullName = medicFullName;
            vaccinesMedic.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
        private void Input_Click(object sender, EventArgs e)
        {
            string medicFullName = fullName;

            this.Hide(); 
            Patients patients = new Patients();
            patients.MedicFullName = medicFullName;
            patients.ShowDialog(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string medicFullName = fullName;

            this.Hide();
            AllVaccination vaccination = new AllVaccination();
            vaccination.MedicFullName = medicFullName;
            vaccination.ShowDialog();
        }

        private void MenuMedic_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }
    }
}
