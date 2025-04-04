using Cursach.ViewMedic;
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
using static Cursach.ViewMedic.VaccinationPatient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cursach.DB_Management
{
    public partial class AddVaccination : Form
    {
        private int patientId;
        private string vaccineName;
        private string executorFIO;
        private string dateOfExecution;
        private string methodOfAdministration;
        private string status;

        private List<string> statuses = new List<string> { "Отменено", "Выполнено", "Запланировано" };
        private Form _parentForm;

        public AddVaccination(Form parentForm, int patientId, string vaccineName, string executorFIO, string dateOfExecution, string methodOfAdministration, string status, 
            List<Patient> patients, bool isFromVaccinationPatient)
        {
            InitializeComponent();
            this.patientId = patientId;
            _parentForm = parentForm;
            StatusVaccination.DataSource = statuses;
            //Устанавливаем DataSource
            PatientComboBox.DataSource = patients;
            PatientComboBox.DisplayMember = "FIO";
            PatientComboBox.ValueMember = "PatientID";

            #region АвтовыборПациента
            //Убеждаемся, что источник данных установлен
            PatientComboBox.Enabled = !isFromVaccinationPatient;
            if (PatientComboBox.DataSource != null)
            {
                if (patientId > 0) // Если PatientID больше 0, пытаемся выбрать его
                {
                    PatientComboBox.SelectedValue = patientId;

                    // Если SelectedValue не сработало (потому что значение еще не загружено), попробуем другой способ
                    if ((int)PatientComboBox.SelectedValue != patientId)
                    {
                        List<Patient> patientsList = PatientComboBox.DataSource as List<Patient>;
                        if (patientsList != null)
                        {
                            Patient selectedPatient = patientsList.FirstOrDefault(p => p.PatientID == patientId);

                            if (selectedPatient != null)
                            {
                                PatientComboBox.SelectedItem = selectedPatient;
                            }
                            else
                            {
                                MessageBox.Show($"Пациент с ID '{patientId}' не найден в списке.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("DataSource не является List<Patient>.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else // Если PatientID равен 0 или меньше, выбираем первого пациента
                {
                    if (PatientComboBox.Items.Count > 0)
                    {
                        PatientComboBox.SelectedIndex = 0; // Выбираем первого пациента
                    }
                }
            }
            else
            {
                MessageBox.Show("DataSource PatientComboBox еще не установлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region Блокирование ввода 1

            PatientComboBox.KeyPress += PatientComboBox_KeyPress;
            PatientComboBox.MouseDown += PatientComboBox_MouseDown;
            PatientComboBox.GotFocus += PatientComboBox_GotFocus;

            ExecutorComboBox.KeyPress += ExecutorComboBox_KeyPress;
            ExecutorComboBox.MouseDown += ExecutorComboBox_MouseDown;
            ExecutorComboBox.GotFocus += ExecutorComboBox_GotFocus;

            MethodComboBox.KeyPress += MethodComboBox_KeyPress;
            MethodComboBox.MouseDown += MethodComboBox_MouseDown;
            MethodComboBox.GotFocus += MethodComboBox_GotFocus;

            VaccineComboBox.KeyPress += VaccineComboBox_KeyPress;
            VaccineComboBox.MouseDown += VaccineComboBox_MouseDown;
            VaccineComboBox.GotFocus += VaccineComboBox_GotFocus;

            StatusVaccination.KeyPress += StatusVaccination_KeyPress;
            StatusVaccination.MouseDown += StatusVaccination_MouseDown;
            StatusVaccination.GotFocus += StatusVaccination_GotFocus;

            #endregion
        }

        #region Блокирование ввода 2
        private void PatientComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void PatientComboBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (!PatientComboBox.DroppedDown)
            {
                PatientComboBox.DroppedDown = true;
            }
        }

        private void PatientComboBox_GotFocus(object sender, EventArgs e)
        {

            this.ActiveControl = null;
        }

        private void ExecutorComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ExecutorComboBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (!ExecutorComboBox.DroppedDown)
            {
                ExecutorComboBox.DroppedDown = true;
            }
        }

        private void ExecutorComboBox_GotFocus(object sender, EventArgs e)
        {

            this.ActiveControl = null;
        }


        private void MethodComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void MethodComboBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (!MethodComboBox.DroppedDown)
            {
                MethodComboBox.DroppedDown = true;
            }
        }

        private void MethodComboBox_GotFocus(object sender, EventArgs e)
        {

            this.ActiveControl = null;
        }

        private void VaccineComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void VaccineComboBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (!VaccineComboBox.DroppedDown)
            {
                VaccineComboBox.DroppedDown = true;
            }
        }

        private void VaccineComboBox_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void StatusVaccination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void StatusVaccination_MouseDown(object sender, MouseEventArgs e)
        {

            if (!StatusVaccination.DroppedDown)
            {
                StatusVaccination.DroppedDown = true;
            }
        }

        private void StatusVaccination_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        #endregion

        private void AddVaccination_Load(object sender, EventArgs e)
        {



            VaccineComboBox.DataSource = GetVaccine();
            VaccineComboBox.DisplayMember = "DisplayText";
            VaccineComboBox.ValueMember = "VaccineSeries";

            PatientComboBox.DropDownHeight = 300;
            VaccineComboBox.DropDownHeight = 300;


            ExecutorComboBox.DataSource = GetUser();
            ExecutorComboBox.DisplayMember = "FIO";
            ExecutorComboBox.ValueMember = "UserID";

            ExecutorComboBox.DropDownHeight = 300;

            MethodComboBox.DataSource = GetMethodOfVaccineAdministration();
            MethodComboBox.DisplayMember = "MethodOfVaccineAdministrationName";
            MethodComboBox.ValueMember = "MethodOfVaccineAdministrationID";



        }

        #region ЗаполнениеДаннымиВакцина
        private List<Vaccine> GetVaccine()
        {
            List<Vaccine> vaccines = new List<Vaccine>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" +
                                      $"database={ConfigurationManager.AppSettings["DbName"]};" +
                                      $"uid={ConfigurationManager.AppSettings["DbUserName"]};" +
                                      $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" +
                                      "charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    Vaccine.VaccineSeries, 
                    Vaccine.VaccineName, 
                    Vaccine.Volume, 
                    Packaging.PackagingName  AS PackagingName 
                FROM Vaccine
                INNER JOIN Packaging ON Vaccine.Packaging = Packaging.PackagingID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vaccines.Add(new Vaccine
                            {
                                VaccineSeries = reader.GetInt32("VaccineSeries"),
                                VaccineName = reader.GetString("VaccineName"),
                                Volume = reader.GetDouble("Volume").ToString(),
                                Packaging = reader.GetString("PackagingName")
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show($"Ошибка при получении списка вакцин: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return vaccines;
        }

        public class Vaccine
        {
            public int VaccineSeries { get; set; }
            public string VaccineName { get; set; }
            public string Volume { get; set; }
            public string Packaging { get; set; } // Теперь храним описание упаковки (String)

            public string DisplayText
            {
                get
                {
                    return $"{VaccineName} - Объем: {Volume}, Упаковка: {Packaging}";
                }
            }
        }
        #endregion

        #region ЗаполнениеДаннымиИсполнитель
        private List<User> GetUser()
        {
            List<User> User = new List<User>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT UserID, FIO FROM User WHERE Role = 2";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User.Add(new User
                            {
                                UserID = reader.GetInt32("UserID"),
                                FIO = reader.GetString("FIO")
                            });
                        }
                    }
                }
                return User;
            }
        }

        public class User
        {
            public int UserID { get; set; }
            public string FIO { get; set; }
        }
        #endregion

        #region ЗаполнениеДаннымиМетодВведения
        private List<MethodOfVaccineAdministration> GetMethodOfVaccineAdministration()
        {
            List<MethodOfVaccineAdministration> MethodOfVaccineAdministration = new List<MethodOfVaccineAdministration>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT MethodOfVaccineAdministrationID, MethodOfVaccineAdministrationName FROM MethodOfVaccineAdministration";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MethodOfVaccineAdministration.Add(new MethodOfVaccineAdministration
                            {
                                MethodOfVaccineAdministrationID = reader.GetInt32("MethodOfVaccineAdministrationID"),
                                MethodOfVaccineAdministrationName = reader.GetString("MethodOfVaccineAdministrationName")
                            });
                        }
                    }
                }
                return MethodOfVaccineAdministration;
            }
        }

        public class MethodOfVaccineAdministration
        {
            public int MethodOfVaccineAdministrationID { get; set; }
            public string MethodOfVaccineAdministrationName { get; set; }
        }

        #endregion

        #region Кнопка добавить
        private void Add_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DateVaccination.Value.Date;
            DateTime today = DateTime.Today;
            int scheduledStatusIndex = statuses.IndexOf("Запланировано");
            int patientId = Convert.ToInt32(PatientComboBox.SelectedValue);
            int vaccineSeries = Convert.ToInt32(VaccineComboBox.SelectedValue);
            int statusId;

            if (selectedDate > today)
            {
                statusId = (StatusVaccination.SelectedIndex == scheduledStatusIndex) ? scheduledStatusIndex : StatusVaccination.SelectedIndex;
            }
            else
            {
                if (StatusVaccination.SelectedIndex == scheduledStatusIndex)
                {
                    MessageBox.Show("Нельзя установить статус 'Запланировано' для даты в прошлом.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                statusId = StatusVaccination.SelectedIndex;
            }

            if (StatusVaccination.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите статус.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CanVaccinateResult canVaccinateResult = CanVaccinate(patientId, vaccineSeries, selectedDate);

            if (!canVaccinateResult.CanVaccinate)
            {
                MessageBox.Show(canVaccinateResult.ErrorMessage, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveVaccinationData(patientId, vaccineSeries, statusId, selectedDate);
        }
        #endregion

        #region Добавление данных
        private void SaveVaccinationData(int patientId, int vaccineSeries, int statusId, DateTime dateOfExecution)
        {
            int executorId = Convert.ToInt32(ExecutorComboBox.SelectedValue);
            int methodOfAdministration = Convert.ToInt32(MethodComboBox.SelectedValue);
            string statusText = statuses[statusId];

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            string query = @"INSERT INTO Vaccination (Patient, VaccineName, Executor, DateOfExecution, MethodOfVaccineAdministration, Status) VALUES (@Patient, @VaccineName, @Executor, @DateOfExecution, @MethodOfVaccineAdministration, @Status)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Patient", MySqlDbType.Int32).Value = patientId;
                        command.Parameters.Add("@VaccineName", MySqlDbType.Int32).Value = vaccineSeries;
                        command.Parameters.Add("@Executor", MySqlDbType.Int32).Value = executorId;
                        command.Parameters.Add("@DateOfExecution", MySqlDbType.DateTime).Value = dateOfExecution;
                        command.Parameters.Add("@MethodOfVaccineAdministration", MySqlDbType.Int32).Value = methodOfAdministration;
                        command.Parameters.Add("@Status", MySqlDbType.VarChar, 50).Value = statusText;
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Данные о вакцинации успешно добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show($"Общая при добавлении вакцинации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class CanVaccinateResult
        {
            public bool CanVaccinate { get; set; }
            public string ErrorMessage { get; set; }
        }

        private CanVaccinateResult CanVaccinate(int patientID, int vaccineSeries, DateTime selectedDate)
        {
            // 1. Получаем последнюю вакцинацию пациента
            DateTime? lastVaccinationDate = GetLastVaccinationDate(patientID);
            int? lastVaccineSeries = GetLastVaccineSeries(patientID);

            // 2. Проверка на дубликаты
            if (lastVaccinationDate.HasValue && lastVaccinationDate.Value.Date == selectedDate.Date &&
                lastVaccineSeries == vaccineSeries)
            {
                return new CanVaccinateResult { CanVaccinate = false, ErrorMessage = "Эта вакцина уже была назначена на этот день." };
            }

            // 3. Проверка интервалов
            if (lastVaccinationDate.HasValue)
            {
                if (lastVaccineSeries == vaccineSeries && selectedDate < lastVaccinationDate.Value.AddYears(1))
                {
                    return new CanVaccinateResult { CanVaccinate = false, ErrorMessage = "Повторную вакцинацию можно выполнять только через год." };
                }
                else if (lastVaccineSeries != vaccineSeries && selectedDate < lastVaccinationDate.Value.AddDays(7))
                {
                    return new CanVaccinateResult { CanVaccinate = false, ErrorMessage = "Для минимизирования риска побочных реакций рекомендовано проводить вакцинацию минимум через неделю!" };
                }
            }

            return new CanVaccinateResult { CanVaccinate = true, ErrorMessage = null };
        }

        private DateTime? GetLastVaccinationDate(int patientID)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            DateTime? lastVaccinationDate = null;

            string selectQuery = "SELECT MAX(DateOfExecution) FROM Vaccination WHERE Patient = @PatientID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        lastVaccinationDate = Convert.ToDateTime(result);
                    }
                }
            }

            return lastVaccinationDate;
        }

        private int? GetLastVaccineSeries(int patientID)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            int? lastVaccineSeries = null;

            string selectQuery = "SELECT VaccineName FROM Vaccination WHERE Patient = @PatientID ORDER BY DateOfExecution DESC LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        lastVaccineSeries = Convert.ToInt32(result);
                    }
                }
            }

            return lastVaccineSeries;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

