using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cursach.ViewAdmin
{
    public partial class RestoreImport : Form
    {
        public string AdminFullName { get; set; }
        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" +
                                     $"database={ConfigurationManager.AppSettings["DbName"]};" +
                                     $"uid={ConfigurationManager.AppSettings["DbUserName"]};" +
                                     $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" +
                                     "charset=utf8mb4;";

        string connectionBase = $"server={ConfigurationManager.AppSettings["DbHost"]};" +
                                    $"database={ConfigurationManager.AppSettings["DbName"]};" +
                                    $"uid={ConfigurationManager.AppSettings["DbUserName"]};" +
                                    $"pwd={ConfigurationManager.AppSettings["DbPassword"]};";
        public RestoreImport()
        {
            InitializeComponent();
            

        }
        private void RestoreImport_Load(object sender, EventArgs e)
        {
            LoadTableNamesToComboBox();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
        }

        private void RestoreImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }

        private void Exit2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            try
            {
                string databaseName = "vaccination_center";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем существование базы данных
                    MySqlCommand checkDbCommand = new MySqlCommand($"SHOW DATABASES LIKE '{databaseName}'", connection);
                    object result = checkDbCommand.ExecuteScalar();

                    if (result != null)
                    {
                        MessageBox.Show("База данных уже существует. Выполнение SQL файла отменено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Если базы данных нет, выполняем SQL файл
                    string sqlScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViewAdmin", "RestoreImport", "restore_vaccination_center.sql");
                    string[] sqlCommands = File.ReadAllText(sqlScript).Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string command in sqlCommands)
                    {
                        if (!string.IsNullOrWhiteSpace(command))
                        {
                            MySqlCommand sqlCommand = new MySqlCommand(command.Trim(), connection);
                            sqlCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Содержимое SQL файла успешно выполнено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении SQL файла: " + ex.Message);
            }
        }
        #region Импорт
       
        private void Import_Click(object sender, EventArgs e)
        {
            int importedRecords = 0;
            // Открываем диалог для выбора CSV файла
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        // Проверяем, что в comboBox что-то выбрано
                        if (nameTable.SelectedItem == null)
                        {
                            MessageBox.Show("Пожалуйста, выберите таблицу из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;  // Прекращаем выполнение метода
                        }

                        string selectedTable = nameTable.SelectedItem.ToString();

                        if (ImportCsvData(filePath, selectedTable, out importedRecords))
                        {
                            if (importedRecords > 0)
                            {
                                MessageBox.Show($"Импорт завершен успешно! Занесено записей: {importedRecords}", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            // else - Ничего не делаем, importedRecords остается равным 0
                        }
                        else
                        {
                            // Сообщение об ошибке (если ImportCsvData вернул false)
                            MessageBox.Show("Ошибка при импорте данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Изменено сообщение
                            importedRecords = -1;
                        }
                    }
                    else
                    {
                        importedRecords = -2; // Файл не был выбран
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте данных: {ex.Message}", "Ошибка импорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                importedRecords = -1; // Аналогично, устанавливаем importedRecords = -1
                                      // return false;  Метод, который вызывает этот код, должен обрабатывать return false
            }
            finally
            {
                if (importedRecords == 0) // Если импортировано 0 записей и не было ошибки
                {
                    MessageBox.Show("Импортировано 0 записей. Возможно, файл пуст или не содержит новых данных.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (importedRecords == -1)
                {
                    //Не нужно ничего делать, сообщение об ошибке уже показано в catch
                }
                else if (importedRecords == -2)
                {
                    // Файл не был выбран, ничего не делаем
                }
            }
        }
        private void LoadTableNamesToComboBox()
        {
           

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionBase))
                {
                    connection.Open();

                    string dbName = ConfigurationManager.AppSettings["DbName"];
                    string query = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '{dbName}'";

                    // Получаем список таблиц, используя GetSchema
            DataTable schemaTable = connection.GetSchema("Tables");

                    List<string> tableNames = new List<string>();
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        // Добавляем имя таблицы в список
                        tableNames.Add(row["TABLE_NAME"].ToString());
                    }

                    // Заполняем ComboBox названиями таблиц
                    nameTable.Items.Clear(); // Очищаем существующие элементы
                            nameTable.Items.AddRange(tableNames.ToArray());

                            // Выбираем первый элемент, если таблицы есть
                            if (nameTable.Items.Count > 0)
                            {
                                nameTable.SelectedIndex = 0;
                            }
                            else
                            {
                                // Сообщаем пользователю, что таблиц нет
                                MessageBox.Show("В базе данных нет таблиц.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке названий таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ImportCsvData(string filePath, string selectedTable, out int importedRecords)
        {
            importedRecords = 0;
            string selectedDelimiter = ";"; // Устанавливаем разделитель

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" +
                                      $"database={ConfigurationManager.AppSettings["DbName"]};" +
                                      $"uid={ConfigurationManager.AppSettings["DbUserName"]};" +
                                      $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" +
                                      "charset=utf8mb4;";
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Получаем количество столбцов в таблице (исключая столбец с автоинкрементом)
                    string queryColumnCount = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{selectedTable}' AND TABLE_SCHEMA = '{ConfigurationManager.AppSettings["DbName"]}' AND EXTRA <> 'auto_increment'";
                    using (MySqlCommand commandColumnCount = new MySqlCommand(queryColumnCount, connection))
                    {
                        int columnCount = Convert.ToInt32(commandColumnCount.ExecuteScalar());

                        // Получаем названия столбцов для динамической генерации запроса
                        string queryColumnNames = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{selectedTable}' AND TABLE_SCHEMA = '{ConfigurationManager.AppSettings["DbName"]}' AND EXTRA <> 'auto_increment'";
                        using (MySqlCommand commandColumnNames = new MySqlCommand(queryColumnNames, connection))
                        {
                            MySqlDataReader readerColumnNames = commandColumnNames.ExecuteReader();
                            List<string> columnNames = new List<string>();
                            while (readerColumnNames.Read())
                            {
                                columnNames.Add(readerColumnNames.GetString(0));
                            }
                            readerColumnNames.Close();


                            // Читаем CSV файл построчно
                            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("Windows-1251")))
                            {
                                // Пропускаем строку заголовка (если она есть)
                                sr.ReadLine();

                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (string.IsNullOrWhiteSpace(line))
                                    {
                                        continue; // Пропускаем пустую строку
                                    }

                                    // Разделяем строку на значения, используя указанный разделитель
                                    string[] values = line.Split(new string[] { selectedDelimiter }, StringSplitOptions.None);

                                    // Проверяем, соответствует ли количество значений количеству столбцов в таблице (с учетом исключения автоинкремента)
                                    if (values.Length != columnCount)
                                    {
                                        MessageBox.Show($"Ошибка: Количество значений в строке не соответствует структуре таблицы '{selectedTable}'. Ожидалось: {columnCount}, получено: {values.Length}.", "Ошибка импорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue; // Пропускаем эту строку и переходим к следующей
                                    }

                                    // Формируем запрос для проверки наличия записи
                                    StringBuilder checkExistingQuery = new StringBuilder($"SELECT COUNT(*) FROM `{selectedTable}` WHERE ");
                                    for (int i = 0; i < columnCount; i++)
                                    {
                                        checkExistingQuery.Append($"`{columnNames[i]}` = @value{i}");
                                        if (i < columnCount - 1)
                                        {
                                            checkExistingQuery.Append(" AND ");
                                        }
                                    }

                                    using (MySqlCommand checkExistingCommand = new MySqlCommand(checkExistingQuery.ToString(), connection))
                                    {
                                        // Добавляем параметры в запрос проверки
                                        for (int i = 0; i < values.Length; i++)
                                        {
                                            checkExistingCommand.Parameters.AddWithValue($"@value{i}", values[i]);
                                        }

                                        int existingCount = Convert.ToInt32(checkExistingCommand.ExecuteScalar());

                                        if (existingCount == 0) // Если запись не существует, добавляем ее
                                        {
                                            // Динамически создаем запрос INSERT
                                            StringBuilder insertQuery = new StringBuilder($"INSERT INTO `{selectedTable}` (");
                                            for (int i = 0; i < columnNames.Count; i++)
                                            {
                                                insertQuery.Append($"`{columnNames[i]}`");
                                                if (i < columnNames.Count - 1)
                                                {
                                                    insertQuery.Append(", ");
                                                }
                                            }
                                            insertQuery.Append(") VALUES (");
                                            for (int i = 0; i < columnCount; i++)
                                            {
                                                insertQuery.Append($"@value{i}");
                                                if (i < columnCount - 1)
                                                {
                                                    insertQuery.Append(", ");
                                                }
                                            }
                                            insertQuery.Append(")");

                                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery.ToString(), connection))
                                            {
                                                // Добавляем параметры
                                                for (int i = 0; i < values.Length; i++)
                                                {
                                                    MySqlParameter param = new MySqlParameter($"@value{i}", MySqlDbType.VarChar);
                                                    param.Value = values[i];
                                                    insertCommand.Parameters.Add(param);
                                                }

                                                insertCommand.ExecuteNonQuery();
                                                importedRecords++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return true; // Импорт успешно завершен
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте данных: {ex.Message}", "Ошибка импорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Произошла ошибка
                }
                
            }
        }
        #endregion


        private void nameTable_MouseDown(object sender, MouseEventArgs e)
        {
                if (!nameTable.DroppedDown)
                {
                     nameTable.DroppedDown = true;
                }
        }
        private void nameTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }
        private void nameTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.Insert))
            {
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void nameTable_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

       
    }
}
