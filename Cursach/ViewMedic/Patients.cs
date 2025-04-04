using Cursach.DB_Management;
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

namespace Cursach
{
    public partial class Patients : Form
    {
        public string MedicFullName;
        public Patients()
        {

            InitializeComponent();
            InitializeSortComboBox();
            FillSocialStatusComboBox();
            LoadData();
            Filtr.GotFocus += Focus_GotFocus;
            Sort.GotFocus += Focus_GotFocus;
         
        }

        private void Patients_Load(object sender, EventArgs e)
        {
           
            #region ЗаголовкиПолей1
            Search.Text = "Поиск";
            Search.ForeColor = Color.White;

            Sort.Text = "Сортировка";
            Sort.ForeColor = Color.White;

            Filtr.Text = "Фильтрация";
            Filtr.ForeColor = Color.White;
            #endregion

            LoadData();
            ShowPatient.Columns["FIO"].Width = 400; // Ширина для столбца ФИО
            ShowPatient.Columns["Age"].Width = 80; // Ширина для столбца ФИО
            ShowPatient.Columns["PhoneNumber"].Width = 180; // Ширина для столбца ФИО

        }
        #region Блокировка Сортировка и Фильтрация
        private void Filtr_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Filtr.DroppedDown)
            {
                Filtr.DroppedDown = true;
            }
        }
        private void Sort_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Sort.DroppedDown)
            {
                Sort.DroppedDown = true;
            }
        }
       
        private void Focus_GotFocus(object sender, EventArgs e)
        {
        this.ActiveControl = null;
        }

        #endregion

        #region ЗаполнениеФильтр
        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

        private List<SocialStatus> GetSocialStatus()
        {
            List<SocialStatus> SocialStatus = new List<SocialStatus>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SocialStatusID, SocialStatusName FROM SocialStatus";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SocialStatus.Add(new SocialStatus
                            {
                                SocialStatusID = reader.GetInt32("SocialStatusID"),
                                SocialStatusName = reader.GetString("SocialStatusName")
                            });
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return SocialStatus;
        }

        public class SocialStatus
        {
            public int SocialStatusID { get; set; }
            public string SocialStatusName { get; set; }

            public override string ToString()
            {
                return SocialStatusName; // Чтобы в ComboBox отображалось имя
            }
        }
        #endregion

        #region Фильтрация
        private void FillSocialStatusComboBox()
        {
            List<SocialStatus> SocialStatuss = GetSocialStatus();

            // Создаем специальный объект для пункта "Все"
            SocialStatus allSocialStatus = new SocialStatus { SocialStatusID = -1, SocialStatusName = "Все" }; // ID -1 для отличия

            // Добавляем пункт "Все" в список
            SocialStatuss.Insert(0, allSocialStatus);

            // Привязываем список к ComboBox
            Filtr.DataSource = SocialStatuss;
            Filtr.DisplayMember = "SocialStatusName";
            Filtr.ValueMember = "SocialStatusID";

            Filtr.SelectedIndex = 0; // Выбираем пункт "Все" по умолчанию

            Filtr.KeyPress += Filtr_KeyPress;
            Filtr.MouseClick += Filtr_MouseClick;
        }

        private void Filtr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void Filtr_MouseClick(object sender, MouseEventArgs e)
        {
            Filtr.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private string _selectedSocialStatus = ""; // Сохраняем выбранную упаковку

        private void Filtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Filtr.SelectedItem != null)
            {
                SocialStatus selectedSocialStatus = (SocialStatus)Filtr.SelectedItem;
                if (selectedSocialStatus.SocialStatusID == -1)
                {
                    _selectedSocialStatus = ""; // Если выбрано "Все", сбрасываем фильтр
                }
                else
                {
                    _selectedSocialStatus = selectedSocialStatus.SocialStatusName;
                }
            }
            else
            {
                _selectedSocialStatus = ""; // Сбрасываем фильтр по упаковке
            }
            ApplyFilters(); // Применяем фильтры при изменении выбранной упаковки
        }

        private void ApplyFilters()
        {
            if (ShowPatient.DataSource is DataTable dataTable)
            {
                string filterExpression = "";

                // Фильтр по упаковке
                if (!string.IsNullOrEmpty(_selectedSocialStatus))
                {
                    // Экранирование кавычек внутри фильтра
                    string escapedSocialStatus = _selectedSocialStatus.Replace("'", "''");
                    filterExpression = $"SocialStatusName = '{escapedSocialStatus}'";
                }

                // Применяем фильтр к DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
                dataTable.AcceptChanges();
            }

            // Применяем поиск и сортировку после фильтрации
            HighlightRows(_lastSearchText);
            SortDataTable();
        }
        #endregion

        #region Поиск

        private string _lastSearchText = ""; // Сохраняем текст последнего поиска

        private void Search_TextChanged(object sender, EventArgs e)
        {
            string text = Search.Text;


            if (text.Length > 50)
            {
                Search.Text = text.Substring(0, 50);
                Search.SelectionStart = Search.Text.Length;
                return;
            }

            StringBuilder filteredText = new StringBuilder();

            foreach (char c in text)
            {
                if ((c >= 'А' && c <= 'я') || c == ' ' || c == '-')
                {
                    filteredText.Append(c);
                }
            }

            string result = filteredText.ToString();

            //Удаление нескольких пробелов подряд.
            result = System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ");
            // Удаление нескольких дефисов подряд.
            result = System.Text.RegularExpressions.Regex.Replace(result, @"[-]+", "-");

            //Запрет более трех слов
            string[] words = result.Split(' ');
            if (words.Length > 3)
            {
                result = string.Join(" ", words.Take(3));
            }

            // Форматирование текста (заглавная первая буква)
            result = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());

            // Обновление поля 
            if (Search.Text != result)
            {
                Search.Text = result;
                Search.SelectionStart = Search.Text.Length;
            }

            _lastSearchText = Search.Text; // Обновляем текст последнего поиска
            ApplyFilters(); // Применяем фильтры и сортировку
        }

        private string placeholderText = ""; // Убедитесь, что placeholderText инициализировано

        private void HighlightRows(string searchText)
        {
            // Сбрасываем цвет всех строк к исходному
            foreach (DataGridViewRow row in ShowPatient.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Или цвет по умолчанию для нечетных строк (укажите свой цвет)
            }

            if (string.IsNullOrEmpty(searchText) || searchText == placeholderText || searchText.Length < 2) return;  // Ничего не делаем, если поле поиска пустое, содержит текст-подсказку или меньше 2 символов

            foreach (DataGridViewRow row in ShowPatient.Rows)
            {
                if (row.Cells["FIO"].Value != null)
                {
                    string vaccineName = row.Cells["FIO"].Value.ToString().ToLower();
                    string searchTextLower = searchText.ToLower();

                    // Используем Contains для поиска совпадений подстроки
                    if (vaccineName.Contains(searchTextLower))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
        }


        #endregion

        #region Сортировка

        private void InitializeSortComboBox()
        {
            // Заполняем ComboBox элементами для выбора варианта сортировки
            Sort.Items.Add("⭣ Возраст");
            Sort.Items.Add("⭡ Возраст");
            Sort.Items.Add("Сброс");
            Sort.SelectedIndexChanged += Sort_SelectedIndexChanged;

            Sort.KeyPress += Sort_KeyPress;
            Sort.MouseClick += Sort_MouseClick;
        }

        private void Sort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void Sort_MouseClick(object sender, MouseEventArgs e)
        {
            Sort.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private void Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortDataTable(); // Вызываем метод сортировки при изменении выбора
        }

        private void SortDataTable()
        {
            string sortExpression = "";

            switch (Sort.SelectedIndex)
            {
                case 0:
                    sortExpression = "Age ASC"; // Сортировка по возрастанию
                    break;
                case 1:
                    sortExpression = "Age DESC"; // Сортировка по убыванию
                    break;
                case 2:
                    if (ShowPatient.DataSource is DataTable localDataTable)
                    {
                        localDataTable.DefaultView.Sort = "";
                        localDataTable.AcceptChanges();
                        HighlightRows(_lastSearchText); // Восстанавливаем подсветку
                        break;
                    }

                    else
                    {
                        MessageBox.Show("Ошибка");
                        break;
                    }
                default:
                    break;
            }

            if (ShowPatient.DataSource is DataTable localDataTable2)
            {
                localDataTable2.DefaultView.Sort = sortExpression;
                localDataTable2.AcceptChanges();
                HighlightRows(_lastSearchText); // Восстанавливаем подсветку
            }
        }
        #endregion

        private void Search_Enter(object sender, EventArgs e)
        {
            if (Search.Text == "Поиск")
            {
                Search.Text = "";
                Search.ForeColor = Color.White; // Меняем цвет текста, когда пользователь начинает вводить
            }
        }
        private void Search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                Search.Text = "Поиск";
                Search.ForeColor = Color.LightGray; // Меняем цвет текста, чтобы отличить от обычного ввода
            }
        }
        public void LoadData()
        {

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(@"
               SELECT v.FIO, 
       v.PhoneNumber, 
       v.Age, 
       c.SocialStatusName, 
       b.GenderName,
       v.PatientID
       FROM Patient v
       LEFT JOIN SocialStatus c ON c.SocialStatusID = v.SocialStatus
       LEFT JOIN Gender b ON b.GenderID = v.Gender
       ORDER BY v.PatientID ASC", connection);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                #region Выделение строк
                ShowPatient.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                ShowPatient.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                ShowPatient.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                ShowPatient.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                ShowPatient.ReadOnly = true; // Запрет редактирования ячеек
                ShowPatient.MultiSelect = false; // Запрет выделения нескольких строк
                ShowPatient.AllowUserToAddRows = false; //Пустые строка снизу

                ShowPatient.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in ShowPatient.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                #region Столбцы и строки

                ShowPatient.EnableHeadersVisualStyles = false; // Отключаем стили заголовков по умолчанию

                // Настройка стиля заголовков
                ShowPatient.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                ShowPatient.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;    // Цвет текста заголовков
                ShowPatient.ColumnHeadersDefaultCellStyle.Font = new Font(ShowPatient.Font, FontStyle.Bold); // Жирный шрифт заголовков

                // Настройка стиля ячеек
                ShowPatient.DefaultCellStyle.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
                ShowPatient.RowTemplate.Height = 80; // Высота строки






                ShowPatient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Отключаем сортировку для всех столбцов
                foreach (DataGridViewColumn column in ShowPatient.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }






                #endregion

                // Привязываем DataTable к DataGridView
                ShowPatient.DataSource = dataTable;

                // Настройка заголовков столбцов
                ShowPatient.Columns["PatientID"].HeaderText = "Идинтификатор";
                if (ShowPatient.Columns.Contains("PatientID"))
                {
                    ShowPatient.Columns["PatientID"].Visible = false;
                }
                ShowPatient.Columns["FIO"].HeaderText = "ФИО";
                ShowPatient.Columns["GenderName"].HeaderText = "Пол";
                ShowPatient.Columns["Age"].HeaderText = "Возраст";
                ShowPatient.Columns["PhoneNumber"].HeaderText = "Номер телефона";
                ShowPatient.Columns["SocialStatusName"].HeaderText = "Социальный статус";

                #region ПКМ
                ShowPatient.MouseDown += (s, mouseEventArgs) => // Переименовали "e" в "mouseEventArgs"
                {
                    // Проверяем, является ли нажатие правой кнопкой мыши
                    if (mouseEventArgs.Button == MouseButtons.Right)
                    {
                        // Получаем позицию курсора мыши относительно DataGridView
                        var hit = ShowPatient.HitTest(mouseEventArgs.X, mouseEventArgs.Y);

                        // Проверяем, что кликнули по ячейке
                        if (hit.Type == DataGridViewHitTestType.Cell)
                        {
                            // Снимаем выделение, если оно было
                            ShowPatient.ClearSelection();

                            // Выделяем всю строку
                            ShowPatient.Rows[hit.RowIndex].Selected = true;

                            // Устанавливаем фокус на первую ячейку выделенной строки
                            ShowPatient.CurrentCell = ShowPatient.Rows[hit.RowIndex].Cells[0];
                        }
                    }
                    else if (mouseEventArgs.Button == MouseButtons.Left)
                    {
                        // Отменяем выделение при нажатии левой кнопкой мыши
                        ShowPatient.ClearSelection();
                    }

                };
            }


            #endregion

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        #region Редактирование
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowPatient.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowPatient.SelectedRows[0];

                try
                {
                    int patientId = Convert.ToInt32(selectedRow.Cells[5].Value);
                    string fio = selectedRow.Cells["FIO"].Value?.ToString() ?? string.Empty;
                    string phoneNumber = selectedRow.Cells["PhoneNumber"].Value?.ToString() ?? string.Empty;
                    int age = selectedRow.Cells["Age"].Value != null && int.TryParse(selectedRow.Cells["Age"].Value.ToString(), out int parsedAge) ? parsedAge : 0;

                    // Получаем RoleID из DataGridView
                    string socialStatusName = (selectedRow.Cells["SocialStatusName"].Value?.ToString() ?? string.Empty).Trim();
                    string genderName = (selectedRow.Cells["GenderName"].Value?.ToString() ?? string.Empty).Trim();

                    RedPatient editForm = new RedPatient(this, patientId, fio, phoneNumber, age, genderName,socialStatusName);

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Обновляем данные в DataGridView после редактирования
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

        #region Удаление
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowPatient.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowPatient.SelectedRows[0];

                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись и все связанные с ней вакцинации?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID удаляемого пациента
                        int patientId = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);

                        // Строка подключения к базе данных
                        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

                        // SQL-запросы для удаления: сначала вакцинации, потом пациент
                        string deleteVaccinationsQuery = "DELETE FROM Vaccination WHERE Patient = @Patient";
                        string deletePatientQuery = "DELETE FROM Patient WHERE PatientID = @PatientID";

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            // Начинаем транзакцию, чтобы гарантировать, что удаление выполнится полностью или не выполнится вообще
                            MySqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                // 1. Удаляем связанные вакцинации
                                using (MySqlCommand command = new MySqlCommand(deleteVaccinationsQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Patient", patientId);
                                    command.ExecuteNonQuery();
                                }

                                // 2. Удаляем пациента
                                using (MySqlCommand command = new MySqlCommand(deletePatientQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@PatientID", patientId);
                                    command.ExecuteNonQuery();
                                }

                                // Подтверждаем транзакцию
                                transaction.Commit();

                                // Удаляем строку из DataGridView
                                ShowPatient.Rows.Remove(selectedRow);

                                MessageBox.Show("Запись и все связанные с ней вакцинации успешно удалены.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                // Откатываем транзакцию в случае ошибки
                                transaction.Rollback();
                                MessageBox.Show($"Ошибка при удалении записи и связанных вакцинаций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при получении PatientID: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void AddPatient_Click(object sender, EventArgs e)
        {
            AddPatient patientAdd = new AddPatient(this, "", "", "", 0, "","");
            patientAdd.FormClosed += AddPatientForm_FormClosed;
            patientAdd.ShowDialog();
        }

        private void AddPatientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Patients_Load(this, EventArgs.Empty);

        }

        private void VaccinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowPatient.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowPatient.SelectedRows[0];



                try
                {
                    int patientId = Convert.ToInt32(selectedRow.Cells[5].Value);
                    string fio = selectedRow.Cells["FIO"].Value?.ToString() ?? string.Empty;
                    string phoneNumber = selectedRow.Cells["PhoneNumber"].Value?.ToString() ?? string.Empty;
                    int age = selectedRow.Cells["Age"].Value != null && int.TryParse(selectedRow.Cells["Age"].Value.ToString(), out int parsedAge) ? parsedAge : 0;

                    // Получаем RoleID из DataGridView
                    string socialStatusName = (selectedRow.Cells["SocialStatusName"].Value?.ToString() ?? string.Empty).Trim();
                    string genderName = (selectedRow.Cells["GenderName"].Value?.ToString() ?? string.Empty).Trim();

                    VaccinationPatient vaccination = new VaccinationPatient(this, patientId, fio, phoneNumber, age, genderName, socialStatusName);

                    if (vaccination.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); 
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

        private void Patients_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuMedic menu = new MenuMedic(MedicFullName);
            menu.ShowDialog();
        }

        private void ShowPatient_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            // Форматирование для столбца "PhoneNumber"
            if (ShowPatient.Columns[e.ColumnIndex].Name == "PhoneNumber")
            {
                if (e.Value != null)
                {
                    string phoneNumber = e.Value.ToString();
                    if (phoneNumber.Length > 4)
                    {
                        // Заменяем последние 4 цифры на ****
                        e.Value = phoneNumber.Substring(0, phoneNumber.Length - 4) + "****";
                    }
                    else if (phoneNumber.Length > 0)
                    {
                        // Заменяем все цифры на * в случае, если меньше 4 цифр
                        e.Value = new string('*', phoneNumber.Length);
                    }
                    e.FormattingApplied = true; // Указываем, что форматирование было применено
                }
            }



            // Форматирование для столбца "FIO"
            if (ShowPatient.Columns[e.ColumnIndex].Name == "FIO")
            {
                if (e.Value != null)
                {
                    string[] names = e.Value.ToString().Split(' ');
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].Length > 4)
                        {
                            names[i] = names[i].Substring(0, names[i].Length - 4) + "****"; // Закрываем последние 4 буквы
                        }
                        else if (names[i].Length == 4)
                        {
                            names[i] = names[i].Substring(0, 1) + "***"; // Закрываем последние 3 буквы
                        }
                        else if (names[i].Length == 3)
                        {
                            names[i] = names[i].Substring(0, 1) + "**"; // Закрываем последние 2 буквы
                        }
                        else if (names[i].Length == 2)
                        {
                            names[i] = names[i].Substring(0, 1) + "*"; // Закрываем последнюю букву
                        }
                    }
                    e.Value = string.Join(" ", names); // Объединяем обратно в строку
                    e.FormattingApplied = true; // Указываем, что форматирование было применено
                }
            }
        }
    }
}
