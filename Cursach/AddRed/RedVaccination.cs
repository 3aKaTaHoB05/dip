using MySql.Data.MySqlClient;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static Cursach.ViewMedic.VaccinationPatient;

namespace Cursach.DB_Management
{
    public partial class RedVaccination : Form
    {
        private Form _parentForm; // Ссылка на родительскую форму (например, главную форму с DataGridView)
        private int _vaccinationId;  // ID редактируемой вакцинации
        private int _patientId;
        private string _patientFIO;
        private string _executorFIO;
        private DateTime? _dateOfExecution; //  DateTime? позволяет хранить null
        private string _vaccineName;
        private string _methodName;
        private string _status;
        private List<string> statuses = new List<string> { "Отменено", "Выполнено", "Запланировано" };

        // Конструктор формы RedVaccination (с параметрами)
        public RedVaccination(Form parentForm, int vaccinationId,int patientId, string executorFIO, DateTime? dateOfExecution, string vaccineName, string methodName, string status, List<Patient> patients, bool isFromVaccinationPatient)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _patientId = patientId;
            _vaccinationId = vaccinationId;
            _executorFIO = executorFIO;
            _dateOfExecution = dateOfExecution;
            _vaccineName = vaccineName;
            _methodName = methodName;
            _status = status;
            StatusVaccination.DataSource = statuses;
            LoadDataToControls();

            #region Автовыбор ComboBox
            PatientComboBox.Enabled = !isFromVaccinationPatient;

            VaccineComboBox.DataSource = GetVaccine();
            VaccineComboBox.DisplayMember = "DisplayText";
            VaccineComboBox.ValueMember = "VaccineSeries";

            ExecutorComboBox.DataSource = GetUser();
            ExecutorComboBox.DisplayMember = "FIO";
            ExecutorComboBox.ValueMember = "UserID";

            MethodComboBox.DataSource = GetMethodOfVaccineAdministration();
            MethodComboBox.DisplayMember = "MethodOfVaccineAdministrationName";
            MethodComboBox.ValueMember = "MethodOfVaccineAdministrationID";

            PatientComboBox.DataSource = patients;
            PatientComboBox.DisplayMember = "FIO";
            PatientComboBox.ValueMember = "PatientID";

            int unitIndex = MethodComboBox.FindStringExact(methodName.Trim());
            if (unitIndex != -1)
            {
                MethodComboBox.SelectedIndex = unitIndex;
            }
            else
            {
                MessageBox.Show("Метод ввода вакцины не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            List<Vaccine> vaccines = VaccineComboBox.DataSource as List<Vaccine>;
            if (vaccines != null)
            {
                Vaccine selectedVaccine = vaccines.FirstOrDefault(v => v.VaccineName == _vaccineName); // Ищем по имени, а не по DisplayText
                if (selectedVaccine != null)
                {
                    VaccineComboBox.SelectedValue = selectedVaccine.VaccineSeries; // Устанавливаем VaccineSeries
                }
                else
                {
                    MessageBox.Show($"Вакцина '{_vaccineName}' не найдена.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("DataSource для VaccineComboBox не является List<Vaccine>.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int unitIndex2 = ExecutorComboBox.FindStringExact(executorFIO.Trim());
            if (unitIndex2 != -1)
            {
                ExecutorComboBox.SelectedIndex = unitIndex2;
            }
            else
            {
                MessageBox.Show("Исполнитель не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (_dateOfExecution.HasValue)
            {
                DateVaccination.Value = _dateOfExecution.Value; 
            }
            else
            {
                DateVaccination.Value = DateTime.MinValue; 
            }

            if (PatientComboBox.DataSource != null)
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
            else
            {
                MessageBox.Show("DataSource PatientComboBox еще не установлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            #endregion

            #region Подписка на блокирование ввода

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

        #region Блокирование ввода

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

        private void LoadDataToControls()
        {
            
            PatientComboBox.Text = _patientFIO; 
            ExecutorComboBox.Text = _executorFIO; 
            DateVaccination.Value = _dateOfExecution ?? DateTime.Now; 
            VaccineComboBox.Text = _vaccineName; 
            StatusVaccination.SelectedItem = _status; 

        }

        
        private void buttonSave_Click(object sender, EventArgs e) 
        {
            int updatedPatientID = Convert.ToInt32(PatientComboBox.SelectedValue);
            int updatedExecutorID = Convert.ToInt32(ExecutorComboBox.SelectedValue);
            int updatedMethodID = Convert.ToInt32(MethodComboBox.SelectedValue);
            DateTime updatedDateOfExecution = DateVaccination.Value;
            int vaccineSeries = Convert.ToInt32(VaccineComboBox.SelectedValue);
            string updatedStatus = StatusVaccination.Text;

            string errorMessage = CanVaccinate(updatedPatientID, vaccineSeries, updatedDateOfExecution, _vaccinationId);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            string updateQuery = @"
UPDATE Vaccination
SET
    Patient = @Patient,
    Executor = @Executor,
    DateOfExecution = @DateOfExecution,
    VaccineName = @VaccineName,
    MethodOfVaccineAdministration = @MethodOfVaccineAdministration,
    Status = @Status
WHERE
    VaccinationSeries = @VaccinationId;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@VaccinationId", _vaccinationId);
                        command.Parameters.AddWithValue("@Patient", updatedPatientID);
                        command.Parameters.AddWithValue("@Executor", updatedExecutorID);
                        command.Parameters.AddWithValue("@DateOfExecution", updatedDateOfExecution);
                        command.Parameters.AddWithValue("@VaccineName", MySqlDbType.Int32).Value = vaccineSeries;
                        command.Parameters.AddWithValue("@MethodOfVaccineAdministration", updatedMethodID);
                        command.Parameters.AddWithValue("@Status", updatedStatus);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CanVaccinate(int patientID, int vaccineSeries, DateTime selectedDate, int vaccinationId)
        {
            DateTime? lastVaccinationDate = GetLastVaccinationDate(patientID, vaccineSeries, vaccinationId);

            // Проверка на дубликаты (такая же вакцина в тот же день, исключая текущую запись)
            if (lastVaccinationDate.HasValue && lastVaccinationDate.Value.Date == selectedDate.Date)
            {
                return "Эта вакцина уже была назначена на этот день.";
            }

            // Получаем название последней вакцины
            string lastVaccineName = GetLastVaccineName(patientID);

            // Если это первая вакцина
            if (lastVaccineName == null)
            {
                return null; // Нет ошибки
            }

            // Получаем название текущей вакцины
            string currentVaccineName = GetVaccineName(vaccineSeries);

            // Если вакцины одинаковые, проверяем год
            if (lastVaccineName == currentVaccineName)
            {
                if (lastVaccinationDate.HasValue && selectedDate < lastVaccinationDate.Value.AddYears(1))
                {
                    return "Повторную вакцинацию можно делать только через год.";
                }
            }
            else // Если вакцины разные, проверяем неделю
            {
                if (lastVaccinationDate.HasValue && selectedDate < lastVaccinationDate.Value.AddDays(7))
                {
                    return "Разные вакцины можно делать только через неделю.";
                }
            }

            return null; // Нет ошибки
        }

        private DateTime? GetLastVaccinationDate(int patientID, int vaccineSeries, int vaccinationId)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            DateTime? lastVaccinationDate = null;

            string selectQuery = @"SELECT MAX(DateOfExecution) FROM Vaccination WHERE Patient = @PatientID AND VaccineName = @VaccineSeries AND VaccinationSeries != @VaccinationId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    command.Parameters.AddWithValue("@VaccineSeries", vaccineSeries);
                    command.Parameters.AddWithValue("@VaccinationId", vaccinationId); // Exclude current record
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

        // Метод для получения названия последней вакцины
        private string GetLastVaccineName(int patientID)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            string vaccineName = null;

            string selectQuery = @"SELECT V.VaccineName 
                       FROM Vaccination VA
                       JOIN Vaccine V ON VA.VaccineName = V.VaccineSeries
                       WHERE VA.Patient = @PatientID
                       ORDER BY VA.DateOfExecution DESC
                       LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        vaccineName = result.ToString();
                    }
                }
            }

            return vaccineName;
        }

        // Метод для получения названия вакцины по её ID
        private string GetVaccineName(int vaccineSeries)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            string vaccineName = null;

            string selectQuery = "SELECT VaccineName FROM Vaccine WHERE VaccineSeries = @VaccineSeries";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@VaccineSeries", vaccineSeries);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        vaccineName = result.ToString();
                    }
                }
            }

            return vaccineName;
        }

        private void buttonCancel_Click(object sender, EventArgs e) 
        {
            this.DialogResult = DialogResult.Cancel;  
            this.Close(); 
        }

        private void RedVaccination_Load(object sender, EventArgs e)
        {
            PatientComboBox.DropDownHeight = 300;
            VaccineComboBox.DropDownHeight = 300;
            ExecutorComboBox.DropDownHeight = 300;
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
