using Cursach.DB_Management;
using Cursach.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cursach.DB_Management.AddVaccine;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Configuration;
using Application = System.Windows.Forms.Application;

namespace Cursach
{
    public partial class VaccinesAdmin : Form
    {
        public string AdminFullName { get; set; }
        public VaccinesAdmin()
        {
            InitializeComponent();
            InitializeSortComboBox();
            
            Filtr.GotFocus += Focus_GotFocus;
            Sort.GotFocus += Focus_GotFocus;
            Vaccines_Load();
        }

        private void VaccinesAdmin_Load(object sender, EventArgs e)
        {
            FillPackagingComboBox();

            #region ЗаголовкиПолей1
            Search.Text = "Поиск";
            Sort.Text = "Сортировка";
            Filtr.Text = "Фильтрация";
            #endregion

            Vaccines_Load();
        }
            
    private void Vaccines_Load()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(@"
                SELECT v.VaccineName, 
                       v.Volume, 
                       c.UnitName, 
                       m.PackagingName,  
                       v.Image,
                       v.VaccineSeries
                FROM Vaccine v
                INNER JOIN Unit c ON c.UnitID = v.Unit 
                INNER JOIN Packaging m ON m.PackagingID = v.Packaging
 ORDER BY v.VaccineSeries ASC", connection);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                #region Выделение строк
                ShowMedic.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                ShowMedic.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                ShowMedic.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                ShowMedic.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                ShowMedic.ReadOnly = true; // Запрет редактирования ячеек
                ShowMedic.MultiSelect = false; // Запрет выделения нескольких строк
                ShowMedic.AllowUserToAddRows = false; //Пустые строка снизу

                ShowMedic.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in ShowMedic.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                #region Столбцы и строки

                ShowMedic.EnableHeadersVisualStyles = false; // Отключаем стили заголовков по умолчанию

                ShowMedic.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                ShowMedic.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;    // Цвет текста заголовков
                ShowMedic.ColumnHeadersDefaultCellStyle.Font = new Font(ShowMedic.Font, FontStyle.Bold); // Жирный шрифт заголовков

                ShowMedic.DefaultCellStyle.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
                ShowMedic.RowTemplate.Height = 80; //Ширина вроде

                ShowMedic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn column in ShowMedic.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                // Привязываем DataTable к DataGridView
                ShowMedic.DataSource = dataTable;

                // Настройка заголовков столбцов
                ShowMedic.Columns["VaccineSeries"].HeaderText = "Серия";
                if (ShowMedic.Columns.Contains("VaccineSeries"))
                {
                    ShowMedic.Columns["VaccineSeries"].Visible = false;
                }
                ShowMedic.Columns["VaccineName"].HeaderText = "Название вакцины";
                ShowMedic.Columns["Volume"].HeaderText = "Объем";
                ShowMedic.Columns["UnitName"].HeaderText = "Единица измерения";
                ShowMedic.Columns["PackagingName"].HeaderText = "Упаковка";
                ShowMedic.Columns["Image"].HeaderText = "Изображение";



                #region Фото-заглушка
                // Получаем столбец изображения 
                if (ShowMedic.Columns.Contains("Image"))
                {
                    DataGridViewImageColumn imgCol = ShowMedic.Columns["Image"] as DataGridViewImageColumn; // Важно: as DataGridViewImageColumn
                    if (imgCol != null) // Проверяем, что столбец действительно является столбцом изображения
                    {
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        imgCol.DefaultCellStyle.NullValue = Resources.noPhoto;

                        // Итерируемся по строкам DataGridView
                        foreach (DataGridViewRow row in ShowMedic.Rows)
                        {
                            // Получаем значение из DataTable (непосредственно из dataTable, а не из DataGridView)
                            DataRow dataRow = (dataTable.Rows.Count > row.Index) ? dataTable.Rows[row.Index] : null;

                            if (dataRow != null)
                            {
                                // Проверяем наличие данных о фотографии в DataTable
                                if (dataRow["Image"] != DBNull.Value && dataRow["Image"] is byte[])
                                {
                                    byte[] imgData = (byte[])dataRow["Image"];

                                    if (imgData.Length > 0)
                                    {
                                        try
                                        {
                                            using (var stream = new System.IO.MemoryStream(imgData))
                                            {
                                                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                                                row.Cells["Image"].Value = image;
                                            }
                                        }
                                        catch (Exception imageEx)
                                        {
                                            // Обрабатываем ошибки при создании изображения из потока.  Важно для обработки поврежденных данных.
                                            Console.WriteLine($"Ошибка при создании изображения: {imageEx.Message}");
                                            row.Cells["Image"].Value = Resources.noPhoto; // Отображаем заглушку в случае ошибки.
                                        }
                                    }
                                    else
                                    {
                                        // Если imgData.Length == 0, отображаем заглушку.  Добавлено для обработки случая пустых изображений.
                                        row.Cells["Image"].Value = Resources.noPhoto;
                                    }
                                }
                                else
                                {
                                    // Если в базе данных нет данных об изображении, показываем заглушку.  Добавлено для обработки DBNull.Value
                                    row.Cells["Image"].Value = Resources.noPhoto;
                                }
                            }
                            else
                            {
                                // Если соответствующая строка в DataTable не найдена, отображаем заглушку.
                                row.Cells["Image"].Value = Resources.noPhoto;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            #endregion

            #region ПКМ
            ShowMedic.MouseDown += (s, mouseEventArgs) => // Переименовали "e" в "mouseEventArgs"
            {
                // Проверяем, является ли нажатие правой кнопкой мыши
                if (mouseEventArgs.Button == MouseButtons.Right)
                {
                    // Получаем позицию курсора мыши относительно DataGridView
                    var hit = ShowMedic.HitTest(mouseEventArgs.X, mouseEventArgs.Y);

                    // Проверяем, что кликнули по ячейке
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        // Снимаем выделение, если оно было
                        ShowMedic.ClearSelection();

                        // Выделяем всю строку
                        ShowMedic.Rows[hit.RowIndex].Selected = true;

                        // Устанавливаем фокус на первую ячейку выделенной строки
                        ShowMedic.CurrentCell = ShowMedic.Rows[hit.RowIndex].Cells[0];
                    }
                }
                else if (mouseEventArgs.Button == MouseButtons.Left)
                {
                    // Отменяем выделение при нажатии левой кнопкой мыши
                    ShowMedic.ClearSelection();
                }

            };


            #endregion
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


        #region ЗаголовкиПолей2
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

        private void Sort_DropDown(object sender, EventArgs e)
        {
            if (Sort.Text == "Сброс")
            {
                Sort.Text = "Сортировка";
                Sort.ForeColor = Color.Black;
            }
        }

        private void Sort_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Sort.Text))
            {
                Sort.Text = "Сортировка";
                Sort.ForeColor = Color.Black;
            }
        }
        #endregion

        #region ЗаполнениеФильтр
        private string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;"; 
        private List<Packaging> GetPackaging()
        {
            List<Packaging> packagings = new List<Packaging>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT PackagingID, PackagingName FROM Packaging";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            packagings.Add(new Packaging
                            {
                                PackagingID = reader.GetInt32("PackagingID"),
                                PackagingName = reader.GetString("PackagingName")
                            });
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return packagings;
        }

        public class Packaging
        {
            public int PackagingID { get; set; }
            public string PackagingName { get; set; }

            public override string ToString()
            {
                return PackagingName; // Чтобы в ComboBox отображалось имя
            }
        }
        #endregion




        #region Фильтрация
            private void FillPackagingComboBox()
        {
            List<Packaging> packagings = GetPackaging();

            // Создаем специальный объект для пункта "Все"
            Packaging allPackaging = new Packaging { PackagingID = -1, PackagingName = "Все" }; // ID -1 для отличия

            // Добавляем пункт "Все" в список
            packagings.Insert(0, allPackaging);

            // Привязываем список к ComboBox
            Filtr.DataSource = packagings;
            Filtr.DisplayMember = "PackagingName";
            Filtr.ValueMember = "PackagingID";

            Filtr.SelectedIndex = 0; // Выбираем пункт "Все" по умолчанию
            

            Filtr.KeyPress += Sort_KeyPress;
            Filtr.MouseClick += Sort_MouseClick;





        }
        private void Filtr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }
        private void Filtr_MouseClick(object sender, MouseEventArgs e)
        {
            Filtr.Select(0, 0); // Снимаем выделение с поля ввода
        }
        private string _selectedPackaging = ""; // Сохраняем выбранную упаковку

        private void Filtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Filtr.SelectedItem != null)
            {
                Packaging selectedPackaging = (Packaging)Filtr.SelectedItem;
                if (selectedPackaging.PackagingID == -1)
                {
                    _selectedPackaging = ""; // Если выбрано "Все", сбрасываем фильтр
                }
                else
                {
                    _selectedPackaging = selectedPackaging.PackagingName;
                }
            }
            else
            {
                _selectedPackaging = ""; // Сбрасываем фильтр по упаковке
            }
            ApplyFilters(); // Применяем фильтры при изменении выбранной упаковки
        }

        private void ApplyFilters()
        {
            if (ShowMedic.DataSource is DataTable dataTable)
            {
                string filterExpression = "";

                // Фильтр по упаковке
                if (!string.IsNullOrEmpty(_selectedPackaging))
                {
                    // Экранирование кавычек внутри фильтра
                    string escapedPackaging = _selectedPackaging.Replace("'", "''");
                    filterExpression = $"PackagingName = '{escapedPackaging}'";
                }

                // Применяем фильтр к DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
                dataTable.AcceptChanges();
            }
        }

        #endregion

        #region Поиск
        private string _lastSearchText = ""; // Сохраняем текст последнего поиска
        private void Search_TextChanged(object sender, EventArgs e)
        {
            string text = Search.Text;

            // Ограничение длины текста
            if (text.Length > 20)
            {
                Search.Text = text.Substring(0, 20);
                Search.SelectionStart = Search.Text.Length;
                return;
            }

            // Ограничение количества дефисов
            int hyphenCount = text.Split('-').Length - 2;
            if (hyphenCount > 1)
            {
                int lastHyphenIndex = text.LastIndexOf('-');
                if (lastHyphenIndex >= 0)
                {
                    text = text.Remove(lastHyphenIndex, 1);
                }
            }

            // Фильтрация символов (только русские буквы, цифры, дефисы и пробелы)
            string filteredText = new string(text.Where(c => (c >= 'А' && c <= 'я') || char.IsDigit(c) || c == '-' || c == ' ').ToArray());

            // Регулярное выражение для удаления повторяющихся пробелов и дефисов
            filteredText = System.Text.RegularExpressions.Regex.Replace(filteredText, @"[- ]{2,}", m => m.Value.Substring(0, 1));


            // Всегда начинается с заглавной буквы
            if (!string.IsNullOrEmpty(filteredText))
            {
                filteredText = char.ToUpper(filteredText[0]) + filteredText.Substring(1);
            }

            filteredText = filteredText.TrimStart(' ', '-');

            // Обновляем текстовое поле (если были изменения)
            if (text != filteredText)
            {
                Search.Text = filteredText;
                Search.SelectionStart = Search.Text.Length;
            }


            _lastSearchText = Search.Text; // Обновляем текст последнего поиска
            HighlightRows(_lastSearchText);
        }

        private string placeholderText = ""; // Убедитесь, что placeholderText инициализировано
        private void HighlightRows(string searchText)
        {
            // Сбрасываем цвет всех строк к исходному
            foreach (DataGridViewRow row in ShowMedic.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Или цвет по умолчанию для нечетных строк (укажите свой цвет)
            }

            if (string.IsNullOrEmpty(searchText) || searchText == placeholderText || searchText.Length < 2) return;  // Ничего не делаем, если поле поиска пустое, содержит текст-подсказку или меньше 2 символов

            foreach (DataGridViewRow row in ShowMedic.Rows)
            {
                if (row.Cells["VaccineName"].Value != null)
                {
                    string vaccineName = row.Cells["VaccineName"].Value.ToString().ToLower();
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
            Sort.Items.Add("⭣ Объем");
            Sort.Items.Add("⭡ Объем");
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
                    sortExpression = "Volume ASC"; // Сортировка по возрастанию
                    break;
                case 1:
                    sortExpression = "Volume DESC"; // Сортировка по убыванию
                    break;
                case 2:
                    if (ShowMedic.DataSource is DataTable localDataTable)
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

            if (ShowMedic.DataSource is DataTable localDataTable2)
            {
                localDataTable2.DefaultView.Sort = sortExpression;
                localDataTable2.AcceptChanges();
                HighlightRows(_lastSearchText); // Восстанавливаем подсветку
            }
        }
        #endregion

       

        private void AddVaccineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VaccinesAdmin_Load(this, EventArgs.Empty); // Перезагружаем данные
            
        }

        private void Add_Click(object sender, EventArgs e)
        {
            
            AddVaccine vaccinesAdd = new AddVaccine(this, "", 0.0, "", "", "");
            vaccinesAdd.FormClosed += AddVaccineForm_FormClosed;
            vaccinesAdd.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowMedic.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = ShowMedic.SelectedRows[0];

                // Получаем значения из ячеек
                int series = Convert.ToInt32(selectedRow.Cells["VaccineSeries"].Value);
                string vaccineName = selectedRow.Cells["VaccineName"].Value?.ToString() ?? string.Empty;
                double volume = selectedRow.Cells["Volume"].Value != null && double.TryParse(selectedRow.Cells["Volume"].Value.ToString(), out double parsedVolume)
                                ? parsedVolume
                                : 0;

                string packagingName = (selectedRow.Cells["PackagingName"].Value?.ToString() ?? string.Empty).Trim();
                string unitName = (selectedRow.Cells["UnitName"].Value?.ToString() ?? string.Empty).Trim();
                byte[] imageBytes = selectedRow.Cells["Image"].Value as byte[];
                System.Drawing.Image image = null;

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                    }
                }

               

                // Создаем и отображаем форму редактирования
                RedVaccine editForm = new RedVaccine(vaccineName, volume, unitName, packagingName,series, image);

                try
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        VaccinesAdmin_Load(this, EventArgs.Empty);
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

    
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowMedic.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowMedic.SelectedRows[0]; // Берем первую выбранную строку

                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID удаляемой записи (предполагаем, что ID хранится в столбце "VaccineSeries")
                        int vaccineSeries = Convert.ToInt32(selectedRow.Cells["VaccineSeries"].Value);

                        // Выполняем удаление из базы данных
                        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
                        string query = "DELETE FROM Vaccine WHERE VaccineSeries = @VaccineSeries";

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@VaccineSeries", vaccineSeries);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Удаляем строку из DataGridView
                        ShowMedic.Rows.Remove(selectedRow);

                        MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VaccinesAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }
    }
    }

