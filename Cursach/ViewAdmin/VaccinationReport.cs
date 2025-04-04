//using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;
using static Cursach.ViewMedic.VaccinationPatient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;
using Table = System.Windows.Documents.Table;

namespace Cursach
{

    public partial class Otchot : Form
    {
        private int patientID;
        private HashSet<int> _searchResultRows = new HashSet<int>(); // Сохраняем индексы строк, соответствующих результатам поиска
        public string AdminFullName { get; internal set; }

        public Otchot()
        {
            InitializeComponent();
        }

        private void Otchot_Load(object sender, EventArgs e)
        {
            #region ЗаголовкиПолей1
            Search.Text = "Поиск";
            #endregion

            DateTime minDate = GetMinDateFromDatabase();

            // Устанавливаем MinDate для обоих DateTimePicker
            DateVaccinationStart.MinDate = minDate;
            DateVaccinationEnd.MinDate = minDate;

            // Устанавливаем начальные значения
            DateVaccinationStart.Value = minDate; // Начальная дата - минимальная
            DateVaccinationEnd.Value = DateTime.Now;   // Конечная дата - сегодня

            UpdateDataDisplay();

            ShowVaccination.Columns["PatientFIO"].Width = 200;
            ShowVaccination.Columns["ExecutorFIO"].Width = 200;
            ShowVaccination.Columns["VaccineName"].Width = 200;
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            if (Search.Text == "Поиск")
            {
                Search.Text = "";
                Search.ForeColor = Color.White;
            }
        }

        private void Search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                Search.Text = "Поиск";
                Search.ForeColor = Color.LightGray;
            }
        }

        private void DisplayDataForPeriod(DateTime startDate, DateTime endDate)
        {
            LoadData(startDate, endDate);
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateDataDisplay();
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateDataDisplay();
        }

        private void UpdateDataDisplay()
        {
            DateTime startDate = DateVaccinationStart.Value.Date;
            DateTime endDate = DateVaccinationEnd.Value.Date;

            DisplayDataForPeriod(startDate, endDate);
            ApplyFilters();
        }

        // Метод для получения минимальной даты из базы данных
        private DateTime GetMinDateFromDatabase()
        {
            DateTime minDate = DateTime.MinValue; // Значение по умолчанию, если в базе данных нет данных

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};database={ConfigurationManager.AppSettings["DbName"]};uid={ConfigurationManager.AppSettings["DbUserName"]};pwd={ConfigurationManager.AppSettings["DbPassword"]};charset=utf8mb4;";
            string selectQuery = "SELECT MIN(DateOfExecution) FROM Vaccination";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            // Проверяем, является ли результат строкой
                            if (result is string dateString)
                            {
                                // Преобразуем строку в DateTime
                                if (DateTime.TryParse(dateString, out DateTime parsedDate))
                                {
                                    minDate = parsedDate;
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось преобразовать строку даты из базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (result is DateTime dateTime)
                            {
                                // Если результат уже является DateTime, используем его напрямую
                                minDate = dateTime;
                            }
                            else
                            {
                                MessageBox.Show("Неожиданный тип данных из базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении минимальной даты из базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return minDate;
        }

        private void LoadData(DateTime startDate, DateTime endDate)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            System.Data.DataTable dataTable = new System.Data.DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(@"
    SELECT
        p.FIO AS PatientFIO,
        e.FIO AS ExecutorFIO,
        c.MethodOfVaccineAdministrationName AS MethodOfAdministration,
        vn.VaccineName AS VaccineName,
        v.DateOfExecution,
        v.Status,
        v.VaccinationSeries,
        v.Patient,
        g.GenderName AS PatientGender
    FROM
        Vaccination v
    LEFT JOIN
        Patient p ON p.PatientID = v.Patient
    LEFT JOIN
        User e ON e.UserID = v.Executor
    LEFT JOIN
        MethodOfVaccineAdministration c ON c.MethodOfVaccineAdministrationID = v.MethodOfVaccineAdministration
    LEFT JOIN
        Vaccine vn ON vn.VaccineSeries = v.VaccineName
LEFT JOIN
    Gender g ON p.Gender = g.GenderID
    WHERE CAST(v.DateOfExecution AS DATE) >= @StartDate AND CAST(v.DateOfExecution AS DATE) <= @EndDate"
        , connection);

                    command.Parameters.AddWithValue("@StartDate", startDate.Date); // Используем Date, чтобы убрать время
                    command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddTicks(-1)); // Включаем конец дня
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    using (var adapter = new MySqlDataAdapter(command))
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
                ShowVaccination.DataSource = dataTable;
                #endregion

                #region Столбцы и строки

                ShowVaccination.EnableHeadersVisualStyles = false; // Отключаем стили заголовков по умолчанию

                // Настройка стиля заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;    // Цвет текста заголовков
                ShowVaccination.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(ShowVaccination.Font, FontStyle.Bold); // Жирный шрифт заголовков

                // Настройка стиля ячеек
                ShowVaccination.DefaultCellStyle.Font = new System.Drawing.Font("Comic Sans MS", 12, FontStyle.Regular);
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
                ShowVaccination.Columns["PatientFIO"].HeaderText = "ФИО пациента";
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
                ShowVaccination.Columns["PatientGender"].HeaderText = "ГендерID";
                if (ShowVaccination.Columns.Contains("PatientGender"))
                {
                    ShowVaccination.Columns["PatientGender"].Visible = false;
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
            ApplyFilters();
        }

        private string placeholderText = ""; // Убедитесь, что placeholderText инициализировано

        private void HighlightRows(string searchText)
        {
            _searchResultRows.Clear();

            if (string.IsNullOrEmpty(searchText) || searchText == placeholderText || searchText.Length < 2)
            {
                ShowVaccination.Refresh();
                return;
            }

            foreach (DataGridViewRow row in ShowVaccination.Rows)
            {
                if (row.Cells["PatientFIO"].Value != null)
                {
                    string patientFIO = row.Cells["PatientFIO"].Value.ToString().ToLower();
                    string searchTextLower = searchText.ToLower();

                    if (patientFIO.Contains(searchTextLower))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        _searchResultRows.Add(row.Index);
                    }
                }
            }
        }


        private void ResetRowColors()
        {
            foreach (DataGridViewRow row in ShowVaccination.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Или цвет по умолчанию
            }
        }

        private void ApplyFilters()
        {
            if (ShowVaccination.DataSource is System.Data.DataTable dataTable)
            {
                string filterExpression = "";


                // Применяем фильтр к DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
                dataTable.AcceptChanges();
            }

            // Применяем поиск и сортировку после фильтрации
            HighlightRows(_lastSearchText);
        }

        #endregion

        #region СокращениеФИО и ВыделениеСтатуса
        private string FormatFIO(string fio)
        {
            string[] parts = fio.Split(' ');
            if (parts.Length == 3)
            {
                return $"{parts[0]} {parts[1][0]}.{parts[2][0]}.";
            }
            else
            {
                return fio;
            }
        }

        private void ShowVaccination_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ShowVaccination.Columns[e.ColumnIndex].Name == "PatientFIO" || ShowVaccination.Columns[e.ColumnIndex].Name == "ExecutorFIO")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    e.Value = FormatFIO(e.Value.ToString());
                    e.FormattingApplied = true;
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (ShowVaccination.Columns[e.ColumnIndex].Name == "Status")
                {
                    // Проверяем, соответствует ли строка результатам поиска
                    if (!_searchResultRows.Contains(e.RowIndex))
                    {
                        string status = e.Value?.ToString()?.Trim() ?? "";

                        Color backgroundColor;
                        switch (status)
                        {
                            case "Выполнено":
                                backgroundColor = ColorTranslator.FromHtml("#90ee90");
                                break;

                            default:
                                backgroundColor = ShowVaccination.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                                break;
                        }

                        // Применяем цвет ко всей строке
                        ShowVaccination.Rows[e.RowIndex].DefaultCellStyle.BackColor = backgroundColor;
                    }
                }
            }

            #endregion

        }

        private void ReporotVaccination_Click(object sender, EventArgs e)
        {
            if (ShowVaccination.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отчета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Data.DataTable dataTable = (ShowVaccination.DataSource as BindingSource)?.DataSource as System.Data.DataTable ?? ShowVaccination.DataSource as System.Data.DataTable;
            if (dataTable == null)
            {
                MessageBox.Show("Ошибка при получении данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Подготовка данных для отчета
            var mostVaccinations = dataTable.AsEnumerable().GroupBy(row => row.Field<string>("PatientFIO"))
                .Select(group => new
                {
                    PatientFIO = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count).FirstOrDefault();

            var mostUsedVaccine = dataTable.AsEnumerable().GroupBy(row => row.Field<string>("VaccineName"))
                .Select(group => new
                {
                    VaccineName = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count).FirstOrDefault();

            var vaccinesByGender = dataTable.AsEnumerable().GroupBy(row => row.Field<string>("PatientGender"))
                .ToDictionary(g => g.Key, g => g.Count());

            var vaccinesByStatus = dataTable.AsEnumerable().GroupBy(row => row.Field<string>("Status"))
                .ToDictionary(g => g.Key, g => g.Count());

            var vaccinesByMethod = dataTable.AsEnumerable().GroupBy(row => row.Field<string>("MethodOfAdministration"))
                .ToDictionary(g => g.Key, g => g.Count());


            // 3. Создание Word-файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx|All files (*.*)|*.*";

            DateTime startDate = DateVaccinationStart.Value; 
            DateTime endDate = DateVaccinationEnd.Value;


            string startDateString = startDate.ToString("dd.MM.yyyy");
            string endDateString = endDate.ToString("dd.MM.yyyy");

            // Формируем имя файла с указанием периода
            saveFileDialog.FileName = $"Cтатистика вакцинации с {startDateString} по {endDateString}.docx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Document doc = wordApp.Documents.Add();

                try
                {
                    #region Шапка

                    // Параграф "Приложение 1"
                    Microsoft.Office.Interop.Word.Paragraph paraApp = doc.Content.Paragraphs.Add();
                    paraApp.Range.Text = "Федеральное статистическое наблюдение";
                    paraApp.Range.Font.Size = 12;
                    paraApp.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    paraApp.Format.SpaceAfter = 6;
                    paraApp.Range.Font.Name = "Times New Roman";
                    paraApp.Range.InsertParagraphAfter();

                    // Параграф "к Письму Роспотребнадзора"
                    Microsoft.Office.Interop.Word.Paragraph paraLetter = doc.Content.Paragraphs.Add();
                    paraLetter.Range.Text = "Утверждено приказом Росстата\n";
                    paraLetter.Range.Font.Size = 12;
                    paraLetter.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    paraLetter.Format.SpaceAfter = 6;
                    paraLetter.Range.Font.Name = "Times New Roman";
                    paraLetter.Range.InsertParagraphAfter();

                    // Параграф "ФОРМА"
                    Microsoft.Office.Interop.Word.Paragraph paraForm = doc.Content.Paragraphs.Add();
                    paraForm.Range.Text = "ФОРМА";
                    paraForm.Range.Font.Size = 14;
                    paraForm.Range.Font.Bold = 1;
                    paraForm.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraForm.Format.SpaceAfter = 6;
                    paraForm.Range.Font.Name = "Times New Roman";
                    paraForm.Range.InsertParagraphAfter();

                    // Параграф "ОТЧЕТА О ВЫПОЛНЕНИИ ВАКЦИНЦИИ НАСЕЛЕНИЯ"
                    Microsoft.Office.Interop.Word.Paragraph paraReport = doc.Content.Paragraphs.Add();
                    paraReport.Range.Text = "ОТЧЕТА О ВЫПОЛНЕНИИ ВАКЦИНЦИИ НАСЕЛЕНИЯ";
                    paraReport.Range.Font.Size = 14;
                    paraReport.Range.Font.Bold = 1;
                    paraReport.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraReport.Format.SpaceAfter = 10;
                    paraReport.Range.InsertParagraphAfter();
                    paraApp.Format.SpaceAfter = 6;
                    paraApp.Range.Font.Name = "Times New Roman";
                    paraApp.Range.InsertParagraphAfter();

                    #endregion

                    #region Таблица

                    // Добавляем параграф, ПОСЛЕ которого будет вставлена таблица
                    Microsoft.Office.Interop.Word.Paragraph paraBeforeTable = doc.Content.Paragraphs.Add();

                    // Вычисляем количество строк
                    int numRows = 1 // Строка для "Характеристика" и "Количество"
                        + 1 // Строка для заголовка "Вакцинации по полу"
                        + vaccinesByGender.Count
                        + 1 // Строка для заголовка "Вакцинации по статусу"
                        + vaccinesByStatus.Count
                        + 1 // Строка для заголовка "Вакцинации по методу"
                        + vaccinesByMethod.Count;

                    int numCols = 2;

                    // Создаем таблицу ПОСЛЕ параграфа paraBeforeTable
                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(paraBeforeTable.Range, numRows, numCols);
                    table.Borders.Enable = 1; // Включаем границы таблицы

                    // Добавляем параграф, в котором будет начинаться текст ПОСЛЕ таблицы
                    Microsoft.Office.Interop.Word.Paragraph paraAfterTable = doc.Content.Paragraphs.Add();


                    // Заголовки таблицы
                    table.Rows[1].Cells[1].Range.Text = "Характеристика";
                    table.Rows[1].Cells[1].Range.Font.Bold = 1;
                    table.Rows[1].Cells[1].Range.Font.Size = 14;
                    table.Rows[1].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                    table.Rows[1].Cells[2].Range.Text = "Количество";
                    table.Rows[1].Cells[2].Range.Font.Bold = 1;
                    table.Rows[1].Cells[2].Range.Font.Size = 14;
                    table.Rows[1].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                    int currentRow = 2; // Начинаем заполнять данные со второй строки

                    // Данные по полу
                    table.Rows[currentRow].Cells[1].Merge(table.Rows[currentRow].Cells[2]);
                    table.Rows[currentRow].Cells[1].Range.Text = "Вакцинации по полу:";
                    table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                    currentRow++;

                    foreach (var gender in vaccinesByGender)
                    {
                        table.Rows[currentRow].Cells[1].Range.Text = gender.Key;
                        table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[1].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[1].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                        table.Rows[currentRow].Cells[2].Range.Text = gender.Value.ToString();
                        table.Rows[currentRow].Cells[2].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[2].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[2].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        currentRow++;
                    }

                    // Данные по статусу
                    table.Rows[currentRow].Cells[1].Merge(table.Rows[currentRow].Cells[2]);
                    table.Rows[currentRow].Cells[1].Range.Text = "Вакцинации по статусу:";
                    table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                    currentRow++;

                    foreach (var status in vaccinesByStatus)
                    {
                        table.Rows[currentRow].Cells[1].Range.Text = status.Key;
                        table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[1].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[1].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                        table.Rows[currentRow].Cells[2].Range.Text = status.Value.ToString();
                        table.Rows[currentRow].Cells[2].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[2].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[2].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        currentRow++;
                    }

                    // Данные по методу
                    table.Rows[currentRow].Cells[1].Merge(table.Rows[currentRow].Cells[2]);
                    table.Rows[currentRow].Cells[1].Range.Text = "Вакцинации по методу:";
                    table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                    currentRow++;

                    foreach (var method in vaccinesByMethod)
                    {
                        // Заполняем данные в отдельные ячейки
                        table.Rows[currentRow].Cells[1].Range.Text = method.Key;
                        table.Rows[currentRow].Cells[1].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[1].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[1].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                        table.Rows[currentRow].Cells[2].Range.Text = method.Value.ToString();
                        table.Rows[currentRow].Cells[2].Range.Font.Size = 14;
                        table.Rows[currentRow].Cells[2].Range.Font.Bold = 0;
                        table.Rows[currentRow].Cells[2].Range.Font.Name = "Times New Roman";
                        table.Rows[currentRow].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        currentRow++;
                    }

                    #endregion

                    #region Нижняя часть страницы
                    Microsoft.Office.Interop.Word.Paragraph end11 = doc.Content.Paragraphs.Add();
                    end11.Range.Text = "";
                    end11.Range.Font.Size = 14;
                    end11.Range.Font.Name = "Times New Roman";
                    end11.Format.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceMultiple;
                    end11.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify; // Выравнивание по ширине
                    end11.Format.FirstLineIndent = doc.Application.CentimetersToPoints(1.5f); // Отступ первой строки 1.5 см
                    end11.Range.Font.Bold = 0;


                    // Получаем информацию о наиболее часто используемой вакцине
                    string mostUsedVaccineName = (mostUsedVaccine?.VaccineName ?? "Нет данных");
                    int mostUsedVaccineCount = dataTable.AsEnumerable() //Переделываем функцию в запросе!
                            .Count(row => row.Field<string>("VaccineName") == mostUsedVaccineName);

                    // Получаем информацию о пациенте, получившем наибольшее количество вакцинаций
                    string mostVaccinatedPatientName = (mostVaccinations?.PatientFIO ?? "Нет данных");
                    int mostVaccinatedPatientVaccinationCount = mostVaccinations?.Count ?? 0;

                    Microsoft.Office.Interop.Word.Paragraph end1 = doc.Content.Paragraphs.Add();
                    if (mostUsedVaccineCount > 1)
                    {
                        end1.Range.Text = $"Согласно проведенному анализу, наиболее часто используемой вакциной является \"{mostUsedVaccineName}\". Общее количество применений составило {mostUsedVaccineCount} раз(а).";
                    }

                    if (mostVaccinatedPatientVaccinationCount > 1)
                    {
                        end1.Range.Text += $"Особое внимание привлекает пациент {mostVaccinatedPatientName}, выполнив {mostVaccinatedPatientVaccinationCount}-ю вакцинацию. Данный факт свидетельствует о высокой степени доверия к программе вакцинации.\n\n";
                    }

                    //Параграф с датой
                    Microsoft.Office.Interop.Word.Paragraph paraDate = doc.Content.Paragraphs.Add();
                    paraDate.Range.Text = "Дата создания: " + DateTime.Now.ToString("dd.MM.yyyy");
                    paraDate.Range.Font.Size = 12;
                    paraDate.Format.SpaceAfter = 6;
                    paraDate.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    paraDate.Range.Font.Name = "Times New Roman";
                    paraDate.Range.InsertParagraphAfter();

                    Microsoft.Office.Interop.Word.Paragraph end2 = doc.Content.Paragraphs.Add();
                    end2.Range.Text = "Подпись_____________";
                    end2.Range.Font.Size = 12;
                    end2.Range.Font.Name = "Times New Roman";
                    end2.Format.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceMultiple;
                    end2.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    end2.Range.Font.Bold = 0;
                    end2.Format.SpaceAfter = 0;


                    #endregion

                    // Сохранение файла
                    doc.SaveAs(filePath);
                    wordApp.Visible = true; ;
                    MessageBox.Show("Отчет успешно создан.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    wordApp.Quit();
                }
            }
        }

        private void Report_Click(object sender, EventArgs e)
        {

            if (ShowVaccination.SelectedRows.Count == 0)
            {
                MessageBox.Show("Не выбрана запись вакцинации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем данные о выбранной вакцинации
            DataGridViewRow selectedRow = ShowVaccination.SelectedRows[0];

            // Проверяем статус
            string status = selectedRow.Cells["Status"].Value?.ToString();
            if (string.IsNullOrEmpty(status) || status.ToLower() != "выполнено")
            {
                MessageBox.Show("Справка формируется только для пациентов со статусом \"Выполнено\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем данные о пациенте и вакцинации из выбранной строки
            string patientFIO = selectedRow.Cells["PatientFIO"].Value?.ToString() ?? "Не указано";
            string vaccineName = selectedRow.Cells["VaccineName"].Value?.ToString() ?? "Не указано";
            string executorFIO = selectedRow.Cells["ExecutorFIO"].Value?.ToString() ?? "Не указано";
            string methodOfAdministration = selectedRow.Cells["MethodOfAdministration"].Value?.ToString() ?? "Не указано";
            DateTime? dateValue = selectedRow.Cells["DateOfExecution"].Value as DateTime?;
            string dateOfExecution = dateValue?.ToString("dd.MM.yyyy") ?? "Не указано";


            // 3. Создание Word-файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx|All files (*.*)|*.*";
            saveFileDialog.FileName = $"Справка о вакцинации - {patientFIO}.docx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Document doc = wordApp.Documents.Add();

                try
                {

                    #region Шапка

                    // Параграф "Приложение 1"
                    Microsoft.Office.Interop.Word.Paragraph paraApp = doc.Content.Paragraphs.Add();
                    paraApp.Range.Text = "Медицинская документация";
                    paraApp.Range.Font.Size = 12;
                    paraApp.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    paraApp.Format.SpaceAfter = 6;
                    paraApp.Range.Font.Name = "Times New Roman";
                    paraApp.Range.InsertParagraphAfter();

                    // Параграф "к Письму Роспотребнадзора"
                    Microsoft.Office.Interop.Word.Paragraph paraLetter = doc.Content.Paragraphs.Add();
                    paraLetter.Range.Text = "Свидетельство о вакцинации\n";
                    paraLetter.Range.Font.Size = 12;
                    paraLetter.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    paraLetter.Format.SpaceAfter = 6;
                    paraLetter.Range.Font.Name = "Times New Roman";
                    paraLetter.Range.InsertParagraphAfter();


                    // Параграф "ФОРМА"
                    Microsoft.Office.Interop.Word.Paragraph paraForm = doc.Content.Paragraphs.Add();
                    paraForm.Range.Text = "СПРАВКА О ВАКЦИНАЦИИ";
                    paraForm.Range.Font.Size = 14;
                    paraForm.Range.Font.Bold = 1;
                    paraForm.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraForm.Format.SpaceAfter = 6;
                    paraForm.Range.Font.Name = "Times New Roman";
                    paraForm.Range.InsertParagraphAfter();

                    // Параграф "ОТЧЕТА О ВЫПОЛНЕНИИ ВАКЦИНЦИИ НАСЕЛЕНИЯ"
                    Microsoft.Office.Interop.Word.Paragraph paraReport = doc.Content.Paragraphs.Add();
                    paraReport.Range.Text = "ВЫДАЕТСЯ ПРИ НЕВОЗМОЖНОСТИ ОФОРМЛЕНИИ ЭЛЕКТРОННОГО СЕРТИФИКАТА НА ГОСУСЛУГАХ";
                    paraReport.Range.Font.Size = 14;
                    paraReport.Range.Font.Bold = 1;
                    paraReport.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraReport.Format.SpaceAfter = 10;
                    paraReport.Range.InsertParagraphAfter();
                    paraApp.Format.SpaceAfter = 6;
                    paraApp.Range.Font.Name = "Times New Roman";
                    paraApp.Range.InsertParagraphAfter();

                    #endregion

                    #region Информация о вакцинации
                    Microsoft.Office.Interop.Word.Paragraph end11 = doc.Content.Paragraphs.Add();
                    end11.Range.Text = "";
                    end11.Range.Font.Size = 14;
                    end11.Range.Font.Name = "Times New Roman";
                    end11.Format.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceMultiple;
                    end11.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    end11.Range.Font.Bold = 0;

                    Microsoft.Office.Interop.Word.Paragraph paraVaccinationInfo = doc.Content.Paragraphs.Add();
                    paraVaccinationInfo.Range.Text = $"Выдана: {patientFIO}\n" +
                                                      $"В том, что он(она) прошла вакцинацию с  использованием препарата: {vaccineName}\n" +
                                                      $"Введено: {methodOfAdministration}\n" +
                                                      $"Исполнитель: {executorFIO}\n" +
                                                      $"Дата вакцинации: {dateOfExecution}\n" +
                                                      $"Оптимальная дата для введения второго компонента вакцины(для двухкомпонентных вакцин)______________20___ г\n" +
                                                      $"Выдана для предоставления: по месту требования.\n\n" +
                                                      $"Дата: " + DateTime.Now.ToString("dd.MM.yyyy") +
                                                      $"\nПодпись: ___________________";
                    paraVaccinationInfo.Range.Font.Size = 14;
                    paraVaccinationInfo.Range.Font.Name = "Times New Roman";
                    paraVaccinationInfo.Format.SpaceAfter = 14;
                    paraVaccinationInfo.Range.Font.Bold = 0;
                    paraVaccinationInfo.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    paraVaccinationInfo.Range.InsertParagraphAfter();

                    #endregion

                    doc.SaveAs(filePath);
                    wordApp.Visible = true; // Открываем документ

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Otchot_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form)
                form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
        }
    }
}


