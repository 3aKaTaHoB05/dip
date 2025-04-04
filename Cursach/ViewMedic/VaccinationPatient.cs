using Cursach.DB_Management;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cursach.DB_Management.AddVaccination;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cursach.ViewMedic
{
    public partial class VaccinationPatient : Form
    {
        private string fio;
        private string phoneNumber;
        private int patientID;
        private int age;
        private string genderName;
        private string socialStatusName;
        private Patients patientsForm;
        private string previousValidAge = "";
        public VaccinationPatient(Patients patientsForm, int patientId, string FIOO, string phoneNumber, int age, string genderName, string socialStatusName)
        {
            InitializeComponent();
            this.patientsForm = patientsForm;
           

            FIO.Text = FIOO;
            Phone.Text = phoneNumber;
            Age.Text = age.ToString(); // Преобразуем int в string
            Gender.Text = genderName;
            SocialStatus.Text = socialStatusName;
            patientID = patientId;

            #region БлокированиеВвода1

            FIO.ReadOnly = true;
            // Предотвращаем вставку текста (Ctrl+V, контекстное меню)
            FIO.ShortcutsEnabled = false;
            // Предотвращаем ввод текста с клавиатуры
            FIO.KeyPress += FIO_KeyPress;
            // Предотвращаем выделение текста мышью
            FIO.MouseDown += FIO_MouseDown;
            // Предотвращаем выделение текста клавиатурой
            FIO.GotFocus += FIO_GotFocus;


            Age.ShortcutsEnabled = false;
            Age.KeyPress += Age_KeyPress;
            Age.MouseDown += Age_MouseDown;
            Age.GotFocus += Age_GotFocus;

            Phone.ShortcutsEnabled = false;
            Phone.KeyPress += Phone_KeyPress;
            Phone.MouseDown += Phone_MouseDown;
            Phone.GotFocus += Phone_GotFocus;

            Gender.ShortcutsEnabled = false;
            Gender.KeyPress += Gender_KeyPress;
            Gender.MouseDown += Gender_MouseDown;
            Gender.GotFocus += Gender_GotFocus;

            SocialStatus.ShortcutsEnabled = false;
            SocialStatus.KeyPress += SocialStatus_KeyPress;
            SocialStatus.MouseDown += SocialStatus_MouseDown;
            SocialStatus.GotFocus += SocialStatus_GotFocus;

            #endregion

            LoadVaccinationData();
          

        }

        #region БлокированиеВвода2
        private void FIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void FIO_MouseDown(object sender, MouseEventArgs e)
        {
            // Отменяем выделение текста
            FIO.SelectionStart = FIO.Text.Length; // Устанавливаем курсор в конец текста
            FIO.SelectionLength = 0;
        }

        private void FIO_GotFocus(object sender, EventArgs e)
        {
            // Снимаем фокус с TextBox, чтобы нельзя было выделять текст клавиатурой
            this.ActiveControl = null; // Передаем фокус другому элементу управления (например, форме)
        }



        private void Age_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void Age_MouseDown(object sender, MouseEventArgs e)
        {
            // Отменяем выделение текста
            Age.SelectionStart = Age.Text.Length; // Устанавливаем курсор в конец текста
            Age.SelectionLength = 0;
        }

        private void Age_GotFocus(object sender, EventArgs e)
        {
            // Снимаем фокус с TextBox, чтобы нельзя было выделять текст клавиатурой
            this.ActiveControl = null; // Передаем фокус другому элементу управления (например, форме)
        }


        private void Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Phone_MouseDown(object sender, MouseEventArgs e)
        {
            Phone.SelectionStart = Phone.Text.Length;
            Phone.SelectionLength = 0;
        }

        private void Phone_GotFocus(object sender, EventArgs e)
        {

            this.ActiveControl = null;
        }


        private void Gender_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Gender_MouseDown(object sender, MouseEventArgs e)
        {
            Gender.SelectionStart = Gender.Text.Length;
            Gender.SelectionLength = 0;
        }

        private void Gender_GotFocus(object sender, EventArgs e)
        {

            this.ActiveControl = null;
        }


        private void SocialStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void SocialStatus_MouseDown(object sender, MouseEventArgs e)
        {
            SocialStatus.SelectionStart = SocialStatus.Text.Length;
            SocialStatus.SelectionLength = 0;
        }

        private void SocialStatus_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        private void VaccinationPatient_Load(object sender, EventArgs e)
        {
            LoadVaccinationData();
            // Устанавливаем индивидуальную ширину для столбцов ФИО и Возраст
            ShowVaccination.Columns["ExecutorFIO"].Width = 350; // Ширина для столбца ФИО
            ShowVaccination.Columns["VaccineName"].Width = 200; // Ширина для столбца ФИО
        }

        private void LoadVaccinationData()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(@"
                SELECT
                    e.FIO AS ExecutorFIO,
                    c.MethodOfVaccineAdministrationName AS MethodOfAdministration,
                    vn.VaccineName AS VaccineName,
                    v.DateOfExecution,
                    v.Status,
                    v.VaccinationSeries,
                    v.Patient
                FROM
                    Vaccination v
                LEFT JOIN
                     User e ON e.UserID = v.Executor  
                LEFT JOIN
                    MethodOfVaccineAdministration c ON c.MethodOfVaccineAdministrationID = v.MethodOfVaccineAdministration
                LEFT JOIN
                    Vaccine vn ON vn.VaccineSeries = v.VaccineName
                WHERE
                    v.Patient = @PatientID  
                ORDER BY
                    v.VaccinationSeries ASC;", connection);

                    command.Parameters.AddWithValue("@PatientID", patientID); // Добавляем параметр PatientID

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                #region Выделение строк
                ShowVaccination.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                ShowVaccination.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                ShowVaccination.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                ShowVaccination.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                ShowVaccination.ReadOnly = true; // Запрет редактирования ячеек
                ShowVaccination.MultiSelect = false; // Запрет выделения нескольких строк
                ShowVaccination.AllowUserToAddRows = false; //Пустые строка снизу

                ShowVaccination.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in ShowVaccination.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion

                ShowVaccination.DataSource = dataTable; // Присваиваем DataSource после настройки DataGridView

                #region Столбцы и строки

                ShowVaccination.EnableHeadersVisualStyles = false; // Отключаем стили заголовков по умолчанию

                // Настройка стиля заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;    // Цвет текста заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.Font = new Font(ShowVaccination.Font, FontStyle.Bold); // Жирный шрифт заголовков

                // Настройка стиля ячеек
                ShowVaccination.DefaultCellStyle.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
                ShowVaccination.RowTemplate.Height = 45; // Высота строки






                ShowVaccination.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Отключаем сортировку для всех столбцов
                foreach (DataGridViewColumn column in ShowVaccination.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                // Привязываем DataTable к DataGridView
                ShowVaccination.DataSource = dataTable;

                // Настройка заголовков столбцов
                ShowVaccination.Columns["VaccinationSeries"].HeaderText = "Идинтификатор";
                if (ShowVaccination.Columns.Contains("VaccinationSeries"))
                {
                    ShowVaccination.Columns["VaccinationSeries"].Visible = false;
                }
                ShowVaccination.Columns["ExecutorFIO"].HeaderText = "Исполнитель";
                ShowVaccination.Columns["DateOfExecution"].HeaderText = "Дата выполнения";
                ShowVaccination.Columns["VaccineName"].HeaderText = "Название вакцины";
                ShowVaccination.Columns["MethodOfAdministration"].HeaderText = "Способ введения";
                ShowVaccination.Columns["Status"].HeaderText = "Статус";
                ShowVaccination.Columns["Patient"].HeaderText = "ПацентID";
                if (ShowVaccination.Columns.Contains("Patient"))
                {
                    ShowVaccination.Columns["Patient"].Visible = false;
                }

                #region ПКМ
                ShowVaccination.MouseDown += (s, mouseEventArgs) => // Переименовали "e" в "mouseEventArgs"
                {
                    // Проверяем, является ли нажатие правой кнопкой мыши
                    if (mouseEventArgs.Button == MouseButtons.Right)
                    {
                        // Получаем позицию курсора мыши относительно DataGridView
                        var hit = ShowVaccination.HitTest(mouseEventArgs.X, mouseEventArgs.Y);

                        // Проверяем, что кликнули по ячейке
                        if (hit.Type == DataGridViewHitTestType.Cell)
                        {
                            // Снимаем выделение, если оно было
                            ShowVaccination.ClearSelection();

                            // Выделяем всю строку
                            ShowVaccination.Rows[hit.RowIndex].Selected = true;

                            // Устанавливаем фокус на первую ячейку выделенной строки
                            ShowVaccination.CurrentCell = ShowVaccination.Rows[hit.RowIndex].Cells[0];
                        }
                    }
                    else if (mouseEventArgs.Button == MouseButtons.Left)
                    {
                        // Отменяем выделение при нажатии левой кнопкой мыши
                        ShowVaccination.ClearSelection();
                    }

                };
            }



            #endregion

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        #region Удаление
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowVaccination.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowVaccination.SelectedRows[0];

                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID удаляемой записи (предполагаем, что ID хранится в столбце "VaccineSeries")
                        int vaccinationId = Convert.ToInt32(selectedRow.Cells["VaccinationSeries"].Value);

                        // Выполняем удаление из базы данных
                        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

                        string query = "DELETE FROM Vaccination WHERE VaccinationSeries = @VaccinationSeries";

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@VaccinationSeries", vaccinationId);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Удаляем строку из DataGridView
                        ShowVaccination.Rows.Remove(selectedRow);

                        MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region Редактирование
         private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowVaccination.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowVaccination.SelectedRows[0];



                try
                {
                    List<Patient> patients = GetPatient();
                    int vaccinationId = Convert.ToInt32(selectedRow.Cells[5].Value);
                    int patientId = Convert.ToInt32(selectedRow.Cells["Patient"].Value);
                    string executorFIO = (selectedRow.Cells["ExecutorFIO"].Value?.ToString() ?? string.Empty).Trim();
                    DateTime? dateOfExecution = (selectedRow.Cells["DateOfExecution"].Value != null && !string.IsNullOrEmpty(selectedRow.Cells["DateOfExecution"].Value.ToString()))
                    ? Convert.ToDateTime(selectedRow.Cells["DateOfExecution"].Value)
                    : (DateTime?)null;
                    string vaccineName = (selectedRow.Cells["VaccineName"].Value?.ToString() ?? string.Empty).Trim();

                    // Получаем RoleID из DataGridView
                    string methodName = (selectedRow.Cells["MethodOfAdministration"].Value?.ToString() ?? string.Empty).Trim();

                    string status = selectedRow.Cells["Status"].Value?.ToString() ?? string.Empty;

                    RedVaccination editForm = new RedVaccination(this, vaccinationId,patientId, executorFIO, dateOfExecution, vaccineName, methodName, status, patients,true);

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadVaccinationData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при редактировании: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для редактирования.");
            }
        }
        #endregion

        #region Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            List<Patient> patients = GetPatient();

            AddVaccination vaccinationAdd = new AddVaccination(this, patientID, "", "", "", "", "", patients,true);
            vaccinationAdd.FormClosed += VaccinationPatient_FormClosed;
            vaccinationAdd.ShowDialog();
        }

        private void VaccinationPatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            VaccinationPatient_Load(this, EventArgs.Empty);
        }
        #endregion
     
        #region ЗаполнениеДаннымиПациент
        public List<Patient> GetPatient()
        {
            List<Patient> Patient = new List<Patient>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT PatientID, FIO FROM Patient";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient.Add(new Patient
                            {
                                PatientID = reader.GetInt32("PatientID"),
                                FIO = reader.GetString("FIO")
                            });
                        }
                    }
                }
                return Patient;
            }
        }

        public class Patient
        {
            public int PatientID { get; set; }
            public string FIO { get; set; }


        }
        #endregion

       
    }
}


