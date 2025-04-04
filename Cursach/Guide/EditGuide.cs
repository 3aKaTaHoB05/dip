using Cursach.ViewAdmin;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach.Guide
{
  
    public partial class EditGuide : Form
    {
       
        private string _tableName;
        private int _recordId; // Добавлено поле для ID
        string idColumnName = "";
        private GuideAdmin _guideForm; // Ссылка на родительскую форму

        public EditGuide(string tableName, int recordId, GuideAdmin guideForm) // Измененный конструктор
        {
            InitializeComponent();
            _tableName = tableName;
            _recordId = recordId;
            _guideForm = guideForm;
        }

        private void EditGuide_Load(object sender, EventArgs e)
        {
          
            // Загружаем данные из базы данных на основе TableName и RecordId
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string columnName = "";
            string query = "";

            switch (_tableName)
            {
                case "Unit":
                    columnName = "UnitName";
                    idColumnName = "UnitID"; // Добавлено имя столбца ID
                    break;
                case "SocialStatus":
                    columnName = "SocialStatusName";
                    idColumnName = "SocialStatusID"; // Добавлено имя столбца ID
                    break;
                case "Packaging":
                    columnName = "PackagingName";
                    idColumnName = "PackagingID"; // Добавлено имя столбца ID
                    break;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    query = $"SELECT {columnName} FROM {_tableName} WHERE {idColumnName} = @RecordId"; // Использовано имя столбца ID
                   
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RecordId", _recordId);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            NewUnitNameTextBox.Text = result.ToString(); // Загружаем значение в текстовое поле
                        }
                        else
                        {
                            MessageBox.Show("Запись не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Edit_Click(object sender, EventArgs e)
        {
            // Получаем новое имя (NewUnitNameTextBox)
            string newName = NewUnitNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Пожалуйста, введите новое значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string columnName = "";
            string idColumnName = ""; // Добавлено имя столбца ID

            // Определяем имя столбца в зависимости от таблицы
            switch (_tableName)
            {
                case "Unit":
                    columnName = "UnitName";
                    idColumnName = "UnitID"; // Добавлено имя столбца ID
                    break;
                case "SocialStatus":
                    columnName = "SocialStatusName";
                    idColumnName = "SocialStatusID"; // Добавлено имя столбца ID
                    break;
                case "Packaging":
                    columnName = "PackagingName";
                    idColumnName = "PackagingID"; // Добавлено имя столбца ID
                    break;
            }

            // 1. Проверяем на дубликат
            if (IsDuplicate(newName, columnName, idColumnName))
            {
                MessageBox.Show("Запись с таким названием уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прерываем выполнение, если дубликат найден
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Обновляем данные в базе данных
                    string query = $"UPDATE {_tableName} SET {columnName} = @NewName WHERE {idColumnName} = @RecordId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewName", newName);
                        command.Parameters.AddWithValue("@RecordId", _recordId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Обновляем данные в родительской форме
                            _guideForm.LoadUnitData(); // Загружаем данные для Unit
                            _guideForm.LoadSocialStatusData(); // Загружаем данные для SocialStatus
                            _guideForm.LoadPackagingData(); // Загружаем данные для Packaging

                            this.DialogResult = DialogResult.OK; // Указываем, что данные были успешно сохранены
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Метод для проверки на дубликат
        private bool IsDuplicate(string newName, string columnName, string idColumnName)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            bool isDuplicate = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM {_tableName} WHERE {columnName} = @NewName AND {idColumnName} != @RecordId"; // Исключаем текущую запись
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewName", newName);
                        command.Parameters.AddWithValue("@RecordId", _recordId);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        isDuplicate = count > 0; // Если count > 0, значит дубликат найден
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при проверке на дубликат: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // В случае ошибки считаем, что дубликат может быть (защита)
                isDuplicate = true;
            }

            return isDuplicate;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewUnitNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // 1. Ограничение максимальной длины до 30 символов
            if (NewUnitNameTextBox.Text.Length > 30)
            {
                NewUnitNameTextBox.Text = NewUnitNameTextBox.Text.Substring(0, 30);
                NewUnitNameTextBox.SelectionStart = NewUnitNameTextBox.Text.Length; // Устанавливаем курсор в конец
                return; // Прерываем обработку, чтобы не выполнять дальнейшие проверки
            }

            // 2. Удаление цифр
            string input = NewUnitNameTextBox.Text; //  Получаем текущий текст

            // 3. Удаление запрещенных символов (цифры, другие символы кроме русских букв, пробелов, дефисов и слешей)
            string sanitizedInput = Regex.Replace(input, @"[^а-яА-ЯёЁ\s\-//]", ""); //Разрешаем только русские буквы, пробелы, дефисы и слеши
            if (sanitizedInput != input)
            {
                NewUnitNameTextBox.Text = sanitizedInput;
                NewUnitNameTextBox.SelectionStart = NewUnitNameTextBox.Text.Length; // Устанавливаем курсор в конец
                input = sanitizedInput; // Обновляем input после удаления запрещенных символов
            }

            // 4. Удаление более одного пробела, дефиса или слеша
            sanitizedInput = Regex.Replace(input, @"[\s\-/]{2,}", " "); // Заменяем несколько пробелов, дефисов или слешей на один пробел
            if (sanitizedInput != input)
            {
                NewUnitNameTextBox.Text = sanitizedInput;
                NewUnitNameTextBox.SelectionStart = NewUnitNameTextBox.Text.Length;
                input = sanitizedInput; // Обновляем input
            }

            // 5. Удаление пробела, дефиса или слеша в начале строки
            sanitizedInput = input.TrimStart(' ', '-', '/'); // Удаляем пробелы, дефисы или слеши в начале строки
            if (sanitizedInput != input)
            {
                NewUnitNameTextBox.Text = sanitizedInput;
                NewUnitNameTextBox.SelectionStart = NewUnitNameTextBox.Text.Length;
            }

        }
    }
}
