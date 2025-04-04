using Cursach.Guide;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach.ViewAdmin
{
    public partial class GuideAdmin : Form
    {
        public string AdminFullName { get; set; }
        private string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
        private DataTable dataTable = new DataTable(); // Храним DataTable как член класса
        private DataTable socialStatusDataTable = new DataTable();
        private DataTable packagingDataTable = new DataTable();
        public GuideAdmin()
        {
            InitializeComponent();
            UnitNameTextBox.TextChanged += UnitNameTextBox_TextChanged;

        }

        private void GuideAdmin_Load(object sender, EventArgs e)
        {

           
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            DataTable unitDataTable = new DataTable();
            DataTable socialStatusDataTable = new DataTable();
            DataTable roleDataTable = new DataTable();
            DataTable packagingDataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Получаем данные из таблицы Unit
                    MySqlCommand unitCommand = new MySqlCommand(@"
            SELECT v.UnitName
            FROM Unit v", connection);

                    using (var adapter = new MySqlDataAdapter(unitCommand))
                    {
                        adapter.Fill(unitDataTable);
                    }

                    // Получаем данные из таблицы SocialStatus
                    MySqlCommand socialStatusCommand = new MySqlCommand(@"
            SELECT s.SocialStatusName
            FROM SocialStatus s", connection);

                    using (var adapter = new MySqlDataAdapter(socialStatusCommand))
                    {
                        adapter.Fill(socialStatusDataTable);
                    }

                    MySqlCommand roleCommand = new MySqlCommand(@"
            SELECT s.RoleName
            FROM Role s", connection);

                    using (var adapter = new MySqlDataAdapter(roleCommand))
                    {
                        adapter.Fill(roleDataTable);
                    }

                    MySqlCommand packagingCommand = new MySqlCommand(@"
            SELECT s.PackagingName
            FROM Packaging s", connection);

                    using (var adapter = new MySqlDataAdapter(packagingCommand))
                    {
                        adapter.Fill(packagingDataTable);
                    }
                }

               
                

                #region Столбцы и строки для UnitView
                UnitView.EnableHeadersVisualStyles = false;
                UnitView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                UnitView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                UnitView.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 15, FontStyle.Bold);
                UnitView.DefaultCellStyle.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                UnitView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn column in UnitView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion

                #region Выделение строк для UnitView
                UnitView.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                UnitView.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                UnitView.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                UnitView.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                UnitView.ReadOnly = true; // Запрет редактирования ячеек
                UnitView.MultiSelect = false; // Запрет выделения нескольких строк
                UnitView.AllowUserToAddRows = false; //Пустые строка снизу

                UnitView.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in UnitView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion

                UnitView.DataSource = unitDataTable;
                
                UnitView.Columns["UnitName"].HeaderText = "Название единицы измерения";

                SocialStatusView.DataSource = socialStatusDataTable; 

                #region Столбцы и строки для SocialStatusView
                SocialStatusView.EnableHeadersVisualStyles = false;
                SocialStatusView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                SocialStatusView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // Цвет текста заголовков
                SocialStatusView.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 15, FontStyle.Bold);
                SocialStatusView.DefaultCellStyle.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                SocialStatusView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn column in SocialStatusView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion

                #region Выделение строк для SocialStatusView
                SocialStatusView.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                SocialStatusView.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                SocialStatusView.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                SocialStatusView.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                SocialStatusView.ReadOnly = true; // Запрет редактирования ячеек
                SocialStatusView.MultiSelect = false; // Запрет выделения нескольких строк
                SocialStatusView.AllowUserToAddRows = false; //Пустые строка снизу

                SocialStatusView.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in SocialStatusView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion
                
                SocialStatusView.Columns["SocialStatusName"].HeaderText = "Название социального статуса";

                PackagingView.DataSource = packagingDataTable;

                PackagingView.Columns["PackagingName"].HeaderText = "Название упаковки";

                #region Столбцы и строки для PackagingView
                PackagingView.EnableHeadersVisualStyles = false;
                PackagingView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                PackagingView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // Цвет текста заголовков
                PackagingView.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 15, FontStyle.Bold);
                PackagingView.DefaultCellStyle.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                PackagingView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn column in PackagingView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion

                #region Выделение строк для PackagingView
                PackagingView.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                PackagingView.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                PackagingView.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                PackagingView.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                PackagingView.ReadOnly = true; // Запрет редактирования ячеек
                PackagingView.MultiSelect = false; // Запрет выделения нескольких строк
                PackagingView.AllowUserToAddRows = false; //Пустые строка снизу

                PackagingView.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in PackagingView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                #endregion







                LoadUnitData(); // Если это необходимо для загрузки данных
                LoadPackagingData();
                LoadSocialStatusData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }


        }


        private void UnitView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что клик был по строке, а не по заголовку
            {
                DataGridViewRow row = UnitView.Rows[e.RowIndex];
                string unitName = row.Cells["UnitName"].Value.ToString();
                UnitNameTextBox.Text = unitName;
            }
        }
        #region СоциальныйСтатус
        private void SocialStatusView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что клик был по строке, а не по заголовку
            {
                DataGridViewRow row = SocialStatusView.Rows[e.RowIndex];
                string socialStatusName = row.Cells["SocialStatusName"].Value.ToString();
                SocialStatusTextBox.Text = socialStatusName;
            }
        }
        private void SocialStatusTextBox_TextChanged(object sender, EventArgs e)
        {
            // Ограничение максимальной длины до 20 символов
            if (SocialStatusTextBox.Text.Length > 15)
            {
                SocialStatusTextBox.Text = SocialStatusTextBox.Text.Substring(0, 15);
                SocialStatusTextBox.SelectionStart = SocialStatusTextBox.Text.Length; // Устанавливаем курсор в конец
            }


            // Удаление лишних пробелов и дефисов
            string input = SocialStatusTextBox.Text;
            string sanitizedInput = Regex.Replace(input, @"[ -]{2,}", " "); // Заменяем более одного пробела или дефиса на один пробел

            // Удаление пробела в начале строки
            if (sanitizedInput.StartsWith(" "))
            {
                sanitizedInput = sanitizedInput.TrimStart();
            }

            sanitizedInput = Regex.Replace(sanitizedInput, @"//+", "/", RegexOptions.None);
            if (sanitizedInput != input)
            {
                SocialStatusTextBox.Text = sanitizedInput;
                SocialStatusTextBox.SelectionStart = SocialStatusTextBox.Text.Length; // Устанавливаем курсор в конец
            }
            sanitizedInput = Regex.Replace(sanitizedInput, @"[^а-яА-ЯёЁ]", ""); // Разрешаем только русские буквы, цифры, пробелы и дефисы

            if (sanitizedInput != input)
            {
                SocialStatusTextBox.Text = sanitizedInput;
                SocialStatusTextBox.SelectionStart = SocialStatusTextBox.Text.Length; // Устанавливаем курсор в конец
            }
        }
        private void EditSocialStatus_Click(object sender, EventArgs e)
        {
            int recordId = -1; // Инициализируем ID значением по умолчанию

            if (SocialStatusView.SelectedRows.Count > 0)
            {
                // Получаем ID из DataGridView, если строка выбрана
                recordId = Convert.ToInt32(SocialStatusView.SelectedRows[0].Cells["SocialStatusID"].Value);
            }
            else if (!string.IsNullOrWhiteSpace(SocialStatusTextBox.Text))
            {
                // Пытаемся найти ID по имени, если строка не выбрана
                recordId = GetSocialStatusIdByName(SocialStatusTextBox.Text);
            }

            if (recordId > 0)
            {
                // Создаем и открываем форму EditGuide
                EditGuide editGuideForm = new EditGuide("SocialStatus", recordId, this);
                if (editGuideForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUnitData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для редактирования или введите название единицы измерения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int GetSocialStatusIdByName(string unitName)
        {
            int unitId = -1; // Значение по умолчанию - запись не найдена

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT SocialStatusID FROM SocialStatus WHERE SocialStatusName = @SocialStatusName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocialStatusName", unitName);
                        object result = command.ExecuteScalar(); // ExecuteScalar возвращает первое значение из запроса

                        if (result != null && result != DBNull.Value)
                        {
                            unitId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске ID по имени: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return unitId;
        }
       

        private void DelSocialStatus_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string socialStatusName = SocialStatusTextBox.Text.Trim();


            if (string.IsNullOrEmpty(socialStatusName))
            {
                MessageBox.Show("Введите название для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить '{socialStatusName}'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open(); // Открываем соединение синхронно

                        string query = "DELETE FROM SocialStatus WHERE SocialStatusName = @SocialStatusName";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SocialStatusName", socialStatusName);
                            int rowsAffected = command.ExecuteNonQuery(); // Выполняем запрос синхронно

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadSocialStatusData();
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
               
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Общая ошибка
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                   
                    SocialStatusTextBox.Text = string.Empty;
                }
            }
        }
        private void AddSocialStatus_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string socialStatusName = SocialStatusTextBox.Text.Trim(); // Получаем текст из TextBox и убираем пробелы

            // Проверка на пустое поле
            if (string.IsNullOrEmpty(socialStatusName))
            {
                MessageBox.Show("Пожалуйста, введите название единицы измерения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Вы уверены, что хотите добавить '{socialStatusName}'?", "Подтверждение добавления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Проверка на дублирование
                        MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM SocialStatus WHERE SocialStatusName = @SocialStatusName", connection);
                        checkCommand.Parameters.AddWithValue("@SocialStatusName", socialStatusName);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Такая единица измерения уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Если все проверки пройдены, добавляем запись
                        MySqlCommand insertCommand = new MySqlCommand("INSERT INTO SocialStatus (SocialStatusName) VALUES (@SocialStatusName)", connection);
                        insertCommand.Parameters.AddWithValue("@SocialStatusName", socialStatusName);

                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Социальный статус успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSocialStatusData();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла общая ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SocialStatusTextBox.Text = string.Empty; // Очистка поля ввода
                }
            }

        }
        

        #endregion

        #region ЕдиницыИзмерения
        private void Add_Click(object sender, EventArgs e)
        {
            string unitName = UnitNameTextBox.Text.Trim(); // Получаем текст из TextBox и убираем пробелы


            // Проверка на пустое поле
            if (string.IsNullOrEmpty(unitName))
            {
                MessageBox.Show("Пожалуйста, введите название единицы измерения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите добавить '{unitName}'?", "Подтверждение добавления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Проверка на дублирование
                string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM Unit WHERE UnitName = @UnitName", connection);
                    checkCommand.Parameters.AddWithValue("@UnitName", unitName);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Такая единица измерения уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Если все проверки пройдены, добавляем запись
                    MySqlCommand insertCommand = new MySqlCommand("INSERT INTO Unit (UnitName) VALUES (@UnitName)", connection);
                    insertCommand.Parameters.AddWithValue("@UnitName", unitName);

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Единица измерения успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUnitData();
                }
            }
        }

        private void UnitNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Ограничение максимальной длины до 20 символов
            if (UnitNameTextBox.Text.Length > 15)
            {
                UnitNameTextBox.Text = UnitNameTextBox.Text.Substring(0, 15);
                UnitNameTextBox.SelectionStart = UnitNameTextBox.Text.Length; // Устанавливаем курсор в конец
            }


            // Удаление лишних пробелов и дефисов
            string input = UnitNameTextBox.Text;
            string sanitizedInput = Regex.Replace(input, @"[ -]{2,}", " "); // Заменяем более одного пробела или дефиса на один пробел

            // Удаление пробела в начале строки
            if (sanitizedInput.StartsWith(" "))
            {
                sanitizedInput = sanitizedInput.TrimStart();
            }

            sanitizedInput = Regex.Replace(sanitizedInput, @"//+", "/", RegexOptions.None);
            if (sanitizedInput != input)
            {
                UnitNameTextBox.Text = sanitizedInput;
                UnitNameTextBox.SelectionStart = UnitNameTextBox.Text.Length; // Устанавливаем курсор в конец
            }
            sanitizedInput = Regex.Replace(sanitizedInput, @"[^а-яА-ЯёЁ0-9\s-\s/]", ""); // Разрешаем только русские буквы, цифры, пробелы и дефисы

            if (sanitizedInput != input)
            {
                UnitNameTextBox.Text = sanitizedInput;
                UnitNameTextBox.SelectionStart = UnitNameTextBox.Text.Length; // Устанавливаем курсор в конец
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string unitName = UnitNameTextBox.Text.Trim();


            if (string.IsNullOrEmpty(unitName))
            {
                MessageBox.Show("Введите название для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить '{unitName}'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open(); // Открываем соединение синхронно

                        string query = "DELETE FROM Unit WHERE UnitName = @UnitName";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UnitName", unitName);
                            int rowsAffected = command.ExecuteNonQuery(); // Выполняем запрос синхронно

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadUnitData(); // Обновляем DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Общая ошибка
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Edit_Click(object sender, EventArgs e)
        {
            int recordId = -1; // Инициализируем ID значением по умолчанию

            if (UnitView.SelectedRows.Count > 0)
            {
                // Получаем ID из DataGridView, если строка выбрана
                recordId = Convert.ToInt32(UnitView.SelectedRows[0].Cells["UnitID"].Value);
            }
            else if (!string.IsNullOrWhiteSpace(UnitNameTextBox.Text))
            {
                // Пытаемся найти ID по имени, если строка не выбрана
                recordId = GetUnitIdByName(UnitNameTextBox.Text);
            }

            if (recordId > 0)
            {
                // Создаем и открываем форму EditGuide
                EditGuide editGuideForm = new EditGuide("Unit", recordId, this);
                if (editGuideForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUnitData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для редактирования или введите название единицы измерения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetUnitIdByName(string unitName)
        {
            int unitId = -1; // Значение по умолчанию - запись не найдена
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT UnitID FROM Unit WHERE UnitName = @UnitName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UnitName", unitName);
                        object result = command.ExecuteScalar(); // ExecuteScalar возвращает первое значение из запроса

                        if (result != null && result != DBNull.Value)
                        {
                            unitId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске ID по имени: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return unitId;
        }
        #endregion

        #region Упаковка
        private void PackagingView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что клик был по строке, а не по заголовку
            {
                DataGridViewRow row = PackagingView.Rows[e.RowIndex];
                string PackagingName = row.Cells["PackagingName"].Value.ToString();
                PackagingTextBox.Text = PackagingName;
            }
        }
        private void PackagingTextBox_TextChanged(object sender, EventArgs e)
        {
            // Ограничение максимальной длины до 20 символов
            if (PackagingTextBox.Text.Length > 30)
            {
                PackagingTextBox.Text = PackagingTextBox.Text.Substring(0, 30);
                PackagingTextBox.SelectionStart = PackagingTextBox.Text.Length; // Устанавливаем курсор в конец
            }


            // Удаление лишних пробелов и дефисов
            string input = PackagingTextBox.Text;
            string sanitizedInput = Regex.Replace(input, @"[ ]{2,}", " "); // Заменяем более одного пробела или дефиса на один пробел

            // Удаление пробела в начале строки
            if (sanitizedInput.StartsWith(" "))
            {
                sanitizedInput = sanitizedInput.TrimStart();
            }

            sanitizedInput = Regex.Replace(sanitizedInput, @"//+", "/", RegexOptions.None);
            if (sanitizedInput != input)
            {
                PackagingTextBox.Text = sanitizedInput;
                PackagingTextBox.SelectionStart = PackagingTextBox.Text.Length; // Устанавливаем курсор в конец
            }
            sanitizedInput = Regex.Replace(sanitizedInput, @"[^а-яА-ЯёЁ]", " "); // Разрешаем только русские буквы, цифры, пробелы и дефисы

            if (sanitizedInput != input)
            {
                PackagingTextBox.Text = sanitizedInput;
                PackagingTextBox.SelectionStart = PackagingTextBox.Text.Length; // Устанавливаем курсор в конец
            }
        }
        private void EditPackaging_Click(object sender, EventArgs e)
        {
            int recordId = -1; // Инициализируем ID значением по умолчанию

            if (PackagingView.SelectedRows.Count > 0)
            {
                // Получаем ID из DataGridView, если строка выбрана
                recordId = Convert.ToInt32(PackagingView.SelectedRows[0].Cells["PackagingID"].Value);
            }
            else if (!string.IsNullOrWhiteSpace(PackagingTextBox.Text))
            {
                // Пытаемся найти ID по имени, если строка не выбрана
                recordId = GetPackagingIdByName(PackagingTextBox.Text);
            }

            if (recordId > 0)
            {
                // Создаем и открываем форму EditGuide
                EditGuide editGuideForm = new EditGuide("Packaging", recordId, this);
                if (editGuideForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUnitData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для редактирования или введите название упаковки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int GetPackagingIdByName(string unitName)
        {
            int unitId = -1; // Значение по умолчанию - запись не найдена
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT PackagingID FROM Packaging WHERE PackagingName = @PackagingName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PackagingName", unitName);
                        object result = command.ExecuteScalar(); // ExecuteScalar возвращает первое значение из запроса

                        if (result != null && result != DBNull.Value)
                        {
                            unitId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске ID по имени: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return unitId;
        }

        private void DelPackaging_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string packagingName = PackagingTextBox.Text.Trim();


            if (string.IsNullOrEmpty(packagingName))
            {
                MessageBox.Show("Введите название для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить '{packagingName}'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open(); // Открываем соединение синхронно

                        string query = "DELETE FROM Packaging WHERE PackagingName = @PackagingName";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PackagingName", packagingName);
                            int rowsAffected = command.ExecuteNonQuery(); // Выполняем запрос синхронно

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPackagingData();
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Общая ошибка
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                    PackagingTextBox.Text = string.Empty;
                }
            }
        }
        private void AddPackaging_Click(object sender, EventArgs e)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string packagingName = PackagingTextBox.Text.Trim(); // Получаем текст из TextBox и убираем пробелы

            // Проверка на пустое поле
            if (string.IsNullOrEmpty(packagingName))
            {
                MessageBox.Show("Пожалуйста, введите название упаковки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Вы уверены, что хотите добавить '{packagingName}'?", "Подтверждение добавления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Проверка на дублирование
                        MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM  Packaging WHERE  PackagingName = @PackagingName", connection);
                        checkCommand.Parameters.AddWithValue("@PackagingName", packagingName);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Такая упаковка уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Если все проверки пройдены, добавляем запись
                        MySqlCommand insertCommand = new MySqlCommand("INSERT INTO  Packaging (PackagingName) VALUES (@PackagingName)", connection);
                        insertCommand.Parameters.AddWithValue("@PackagingName", packagingName);

                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Упаковка успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPackagingData();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла общая ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    PackagingTextBox.Text = string.Empty; 
                }
            }

        }


        #endregion

        #region ЗагрузкаДанных

        public void UpdateUnitList()
        {
            LoadUnitData();
        }


        internal void LoadUnitData()
        {
            dataTable.Clear(); // Очищаем таблицу перед загрузкой

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT UnitID, UnitName FROM Unit"; // Запрос для получения данных (ВКЛЮЧАЕМ ID!)
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable); // Заполняем DataTable
                        }
                    }
                }
                UnitView.DataSource = dataTable; // Привязываем DataTable к DataGridView
                                                 // Настройка заголовков столбцов (если необходимо)
                if (UnitView.Columns.Count > 0)
                {
                    UnitView.Columns["UnitName"].HeaderText = "Название единицы измерения";
                    UnitView.Columns["UnitID"].Visible = false; // Скрываем столбец ID от пользователя
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateSocialStatusList()
        {
            LoadSocialStatusData();
        }
        internal void LoadSocialStatusData()
            {
                socialStatusDataTable.Clear();

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT SocialStatusID, SocialStatusName FROM SocialStatus"; // ВКЛЮЧАЕМ SocialStatusID
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                adapter.Fill(socialStatusDataTable);
                            }
                        }
                    }
                    SocialStatusView.DataSource = socialStatusDataTable;
                    if (SocialStatusView.Columns.Count > 0)
                    {
                        SocialStatusView.Columns["SocialStatusName"].HeaderText = "Название социального статуса";
                        SocialStatusView.Columns["SocialStatusID"].Visible = false; // Скрываем SocialStatusID
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных (SocialStatus): " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        public void UpdatePackagingList()
        {
            LoadPackagingData();
        }


        internal void LoadPackagingData()
        {
            packagingDataTable.Clear(); // Очищаем таблицу перед загрузкой

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT PackagingID, PackagingName FROM Packaging"; // Запрос для получения данных (ВКЛЮЧАЕМ ID!)
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(packagingDataTable); // Заполняем DataTable
                        }
                    }
                }
                PackagingView.DataSource = packagingDataTable; // Привязываем DataTable к DataGridView
                                                 // Настройка заголовков столбцов (если необходимо)
                if (PackagingView.Columns.Count > 0)
                {
                    PackagingView.Columns["PackagingName"].HeaderText = "Название упаковки";
                    PackagingView.Columns["PackagingID"].Visible = false; // Скрываем столбец ID от пользователя
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion





        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
            Application.Exit();
        }

        
    }
    }
    


