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
using System.Windows.Forms.VisualStyles;
using static Cursach.ViewMedic.VaccinationPatient;

namespace Cursach
{
    public partial class AllVaccination : Form
    {
        public string MedicFullName { get; set; }

        private string fio;
        private string phoneNumber;
        private int patientID;
        private int age;
        private string genderName;
        private string socialStatusName;
        private AllVaccination allVaccination;
        private string previousValidAge = "";

        private int currentPage = 1; // Текущая страница
        private int pageSize = 20; // Количество записей на странице
        private int totalRecords = 0; // Общее количество записей в базе данных
        private DataTable allData; // Храним все данные из базы данных

        private DataTable _filteredData;
        private bool _dataLoaded = false;


        private const string VaccinationSeriesColumnName = "VaccinationSeries";
        private const string PatientFIOColumnName = "PatientFIO";
        private const string ExecutorFIOColumnName = "ExecutorFIO";
        private const string DateOfExecutionColumnName = "DateOfExecution";
        private const string VaccineNameColumnName = "VaccineName";
        private const string MethodOfAdministrationColumnName = "MethodOfAdministration";
        private const string StatusColumnName = "Status";
        private const string PatientColumnName = "Patient";

        public AllVaccination()
        {
            InitializeComponent(); // Инициализация компонентов (обязательно первой)
            LoadData();
            InitializeSortComboBox();
            FillFiltrComboBox();
            Filtr.GotFocus += Focus_GotFocus; // Регистрация обработчиков событий
            Sort.GotFocus += Focus_GotFocus;

           

            UpdatePagination();
            // Загрузка данных будет происходить в Vaccination_Load
        }

        private void Vaccination_Load(object sender, EventArgs e)
        {
            #region ЗаголовкиПолей1
            Search.Text = "Поиск";
            Search.ForeColor = Color.White;

            Sort.Text = "Сортировка";
            Sort.ForeColor = Color.White;

            Filtr.Text = "Фильтрация";
            Filtr.ForeColor = Color.White;
            #endregion

            
            _dataLoaded = false; // Данные еще не загружены
            LoadData(); // Загружаем данные
            

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
            #endregion

            UpdatePagination();

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
        #endregion

        private void LoadData()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            allData = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Убираем LIMIT и OFFSET из запроса - загружаем все!
                    MySqlCommand command = new MySqlCommand(@"
                 SELECT
    p.FIO AS PatientFIO, 
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
    Patient p ON p.PatientID = v.Patient
LEFT JOIN
    User e ON e.UserID = v.Executor  
LEFT JOIN
    MethodOfVaccineAdministration c ON c.MethodOfVaccineAdministrationID = v.MethodOfVaccineAdministration
LEFT JOIN
    Vaccine vn ON vn.VaccineSeries = v.VaccineName
ORDER BY
    p.FIO ASC;", connection);

                    command.Parameters.AddWithValue("@PatientID", patientID);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(allData); // Заполняем allData ВСЕМИ данными
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

                totalRecords = allData.Rows.Count;
                 DisplayData();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            _filteredData = allData.Copy(); // Инициализируем _filteredData копией allData
            _dataLoaded = true; // Данные загружены
        }


        #region Фильтрация

        private void FillFiltrComboBox()
        {
            // Заполняем комбобокс вариантами фильтрации
            Filtr.Items.Add("Все");
            Filtr.Items.Add("Отменено");
            Filtr.Items.Add("Выполнено");
            Filtr.Items.Add("Не пришел");

            Filtr.Text = "Фильтрация"; // Устанавливаем текст по умолчанию

            // Подписываемся на события
            Filtr.KeyPress += Filtr_KeyPress;
            Filtr.MouseClick += Filtr_MouseClick;
            Filtr.SelectedIndexChanged += Filtr_SelectedIndexChanged;

            Filtr.SelectedIndex = 0; // Выбираем пункт "Все" по умолчанию
        }

        private void Filtr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void Filtr_MouseClick(object sender, MouseEventArgs e)
        {
            Filtr.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private string _selectedVaccinationStatus = ""; // Сохраняем выбранный статус вакцинации

        private void Filtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный текст из ComboBox
            _selectedVaccinationStatus = Filtr.SelectedItem?.ToString() ?? ""; // Сбрасываем фильтр, если ничего не выбрано
            ApplyFilters(); // Применяем фильтры при изменении выбранного статуса
        }

        private void ApplyFilters()
        {
            if (ShowVaccination.DataSource is DataTable dataTable)
            {
                string filterExpression = string.Empty;

                // Фильтр по статусу вакцинации
                if (!string.IsNullOrEmpty(_selectedVaccinationStatus) && _selectedVaccinationStatus != "Все")
                {
                    // Экранирование кавычек внутри фильтра
                    string escapedVaccinationStatus = _selectedVaccinationStatus.Replace("'", "''");
                    filterExpression = $"Status = '{escapedVaccinationStatus}'"; // Предполагается, что в DataTable столбец называется "Status"
                }

                // Применяем фильтр к DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
                dataTable.AcceptChanges();
            }

            // Применяем поиск и сортировку после фильтрации
            //HighlightRows(_lastSearchText);
            SortDataTable();
        }

        #endregion


        #region Сортировка

        private void InitializeSortComboBox()
        {
            // Заполняем ComboBox элементами для выбора варианта сортировки
            Sort.Items.Add("⭣ Дата");
            Sort.Items.Add("⭡ Дата");
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

        private DataTable currentDataTable; // Переменная для хранения текущей таблицы данных
        private void SortDataTable()
        {
            string sortExpression = "";

            switch (Sort.SelectedIndex)
            {
                case 0:
                    sortExpression = "DateOfExecution ASC"; // Сортировка по возрастанию
                    break;
                case 1:
                    sortExpression = "DateOfExecution DESC"; // Сортировка по убыванию
                    break;
                case 2:
                    sortExpression = "PatientFIO ASC"; // Сортировка по убыванию
                    break;
                default:
                    break;
            }

            if (ShowVaccination.DataSource is DataTable localDataTable2)
            {
                localDataTable2.DefaultView.Sort = sortExpression;
                localDataTable2.AcceptChanges();
            }

            // Сортируем _filteredData
            if (_filteredData != null)
            {
                _filteredData.DefaultView.Sort = sortExpression; // Применяем сортировку к _filteredData
                _filteredData = _filteredData.DefaultView.ToTable(); // Обновляем _filteredData
            }

            DisplayData(); // Обновляем отображение данных
        }

        #endregion


        private void DisplayData()
        {
            // Проверяем, что _filteredData не равно null
            if (_filteredData != null)
            {
                DataTable pageDataTable = _filteredData.Clone();

                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, _filteredData.Rows.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    pageDataTable.ImportRow(_filteredData.Rows[i]);
                }

                ShowVaccination.DataSource = pageDataTable;
                ConfigureColumns();
                UpdatePagination();
                UpdateRecordCountLabel(pageDataTable.Rows.Count);

            }
            else
            {
                // Обрабатываем случай, когда _filteredData равно null
                ShowVaccination.DataSource = null; // Очищаем DataGridView
                MessageBox.Show("Невозможно отобразить данные, так как отфильтрованные данные отсутствуют.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigureColumns()
        {
            ShowVaccination.Columns[VaccinationSeriesColumnName].HeaderText = "Идинтификатор";
            if (ShowVaccination.Columns.Contains(VaccinationSeriesColumnName))
            {
                ShowVaccination.Columns[VaccinationSeriesColumnName].Visible = false;
            }
            ShowVaccination.Columns[PatientFIOColumnName].HeaderText = "ФИО пациента";
            ShowVaccination.Columns[ExecutorFIOColumnName].HeaderText = "Исполнитель";
            ShowVaccination.Columns[DateOfExecutionColumnName].HeaderText = "Дата выполнения";
            ShowVaccination.Columns[VaccineNameColumnName].HeaderText = "Название вакцины";
            ShowVaccination.Columns[MethodOfAdministrationColumnName].HeaderText = "Способ введения";
            ShowVaccination.Columns[StatusColumnName].HeaderText = "Статус";
            ShowVaccination.Columns[PatientColumnName].HeaderText = "ПацентID";
            if (ShowVaccination.Columns.Contains(PatientColumnName))
            {
                ShowVaccination.Columns[PatientColumnName].Visible = false;
            }
        }

        #region Кнопки и номер страницы
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (currentPage < (int)Math.Ceiling((double)totalRecords / pageSize))
            {
                currentPage++;
                DisplayData();

            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayData();

            }
        }

        private void UpdatePagination()
        {
            // Удаляем существующие LinkLabel'ы пагинации
            foreach (Control control in this.Controls.OfType<LinkLabel>().Where(ll => ll.Name.StartsWith("page")).ToList())
            {
                this.Controls.Remove(control);
                control.Dispose(); // Освобождаем ресурсы
            }

            int maxDisplayedPages = 3;
            int startX = 115; // Фиксированная координата X
            int startY = 635; // Фиксированная координата Y

            // Рассчитываем количество страниц
            int totalRecords = (_filteredData != null) ? _filteredData.Rows.Count : 0;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        
            // Диапозон страниц
            int startPage = Math.Max(1, currentPage - maxDisplayedPages / 2); // Начальная страница для отображения
            int endPage = Math.Min(totalPages, startPage + maxDisplayedPages - 1); // Конечная страница для отображения

            // Если конечная страница - последняя, корректируем начальную, чтобы всегда отображалось maxDisplayedPages
            if (endPage == totalPages)
            {
                startPage = Math.Max(1, totalPages - maxDisplayedPages + 1);
            }

            int step = 25; // Расстояние между номерами страниц

           
            LinkLabel[] pageLinks = new LinkLabel[totalPages];
            for (int i = 0; i < totalPages; ++i)
            {
                pageLinks[i] = new LinkLabel();
                pageLinks[i].Text = Convert.ToString(i + 1);
                pageLinks[i].Name = "page" + i;
                pageLinks[i].AutoSize = true;
                pageLinks[i].Location = new Point(startX + (i * step), startY); // Равномерное расстояние
                pageLinks[i].Click += new EventHandler(PageLink_Click); // Обработчик для переключения страниц

                pageLinks[i].BackColor = Color.White; // Белый фон
                pageLinks[i].ForeColor = Color.Black; // Черный шрифт
                pageLinks[i].Font = new Font("Comic Sans MS", 14);
                                                                   

                this.Controls.Add(pageLinks[i]);
                pageLinks[i].BringToFront(); 
            }

            // Устанавливаем стиль для текущей страницы
            if (totalPages > 0)
            {
                // Находим LinkLabel для текущей страницы (currentPage начинается с 1, а индексы с 0)
                LinkLabel currentLink = this.Controls.Find("page" + (currentPage - 1), true).FirstOrDefault() as LinkLabel;
                if (currentLink != null)
                {
                    currentLink.LinkBehavior = LinkBehavior.NeverUnderline;
                }
            }
        }

        private void PageLink_Click(object sender, EventArgs e)
        {
            LinkLabel clickedLink = (LinkLabel)sender;
            int pageNumber = int.Parse(clickedLink.Text);

            // Обновляем currentPage и отображаем данные для выбранной страницы
            currentPage = pageNumber;
            DisplayData();

            // После отображения данных, обновляем пагинацию, чтобы подсветить текущую страницу
            UpdatePagination();
        }

        private void UpdateRecordCountLabel(int recordCount)
        {
           // Получаем общее количество записей(из _filteredData)
            int totalRecords = (_filteredData != null) ? _filteredData.Rows.Count : 0;

            // Рассчитываем номер первой и последней отображаемой записи
            int firstRecord = (currentPage - 1) * pageSize + 1;
            int lastRecord = Math.Min(currentPage * pageSize, totalRecords);

            // Формируем текст для Label
            label1.Text = $"Количество записей: {firstRecord}-{lastRecord} из {totalRecords}";
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

              #region ВыделениеСтатуса
          
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Проверка на валидность индекса
            {
                if (ShowVaccination.Columns[e.ColumnIndex].Name == "Status")
                {
                    string status = e.Value?.ToString()?.Trim() ?? ""; // Получаем значение из ячейки, обрабатываем null

                    Color backgroundColor;
                    switch (status)
                    {
                        case "Выполнено":
                            backgroundColor = Color.LightGreen;
                            break;
                        case "Отменено":
                            backgroundColor = ColorTranslator.FromHtml("#F19CBB");
                            break;
                        default:
                            backgroundColor = ShowVaccination.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                            break;
                    }


                    ShowVaccination.Rows[e.RowIndex].DefaultCellStyle.BackColor = backgroundColor;
                
            }
            }
            #endregion
            

        }
        #endregion


        #region Добавление
        private void AddVaccination_Click(object sender, EventArgs e)
        {
            List<Patient> patients = GetPatient();

            AddVaccination vaccinationAdd = new AddVaccination(this, patientID, "", "", "", "", "", patients, false);
            vaccinationAdd.FormClosed += VaccinationPatient_FormClosed;
            vaccinationAdd.ShowDialog();
        }

        private void VaccinationPatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            

           
            LoadData(); // Перезагружаем данные из базы
            DisplayData(); // Обновляем DataGridView
            UpdatePagination(); // Обновляем пагинацию
        }
        // Убираем один из методов GetPatient(), оставляем только тот, который нужен
        private List<Patient> GetPatient()
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
        #endregion

        #region Удаление
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Проверяем, что в DataGridView есть выбранные строки
            if (ShowVaccination.SelectedRows.Count > 0)
            {
                // 2. Получаем выбранную строку
                DataGridViewRow selectedRow = ShowVaccination.SelectedRows[0];

                // 3. Получаем ID вакцинации из выбранной строки
                //  Замени "VaccinationID" на фактическое имя столбца
                int vaccinationID = Convert.ToInt32(selectedRow.Cells["VaccinationSeries"].Value);

                // 4. Отображаем окно подтверждения удаления
                DialogResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить эту запись?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // 5. Если пользователь подтвердил удаление
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // 6. Удаляем запись из базы данных
                        DeleteVaccination(vaccinationID);

                        // 7. Обновляем DataGridView (перезагружаем данные)
                        // Vaccination_Load(this, EventArgs.Empty); // ЗАМЕНЯЕМ ЭТУ СТРОКУ

                        //Перезагружаем данные из базы данных
                        LoadData();

                        //Обновляем отображение данных в DataGridView
                        DisplayData();

                        //Обновляем пагинацию
                        UpdatePagination();


                        // 8. Отображаем сообщение об успешном удалении
                        MessageBox.Show("Запись успешно удалена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // 9. Обрабатываем ошибки
                        MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // 10. Сообщаем пользователю, что нужно выбрать строку для удаления
                    MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Предполагаемый метод для удаления вакцинации из базы данных
        private void DeleteVaccination(int vaccinationID)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            string deleteQuery = "DELETE FROM Vaccination WHERE VaccinationSeries = @VaccinationSeries";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@VaccinationSeries", vaccinationID);

                    connection.Open();
                    command.ExecuteNonQuery();
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
                    int vaccinationId = Convert.ToInt32(selectedRow.Cells[6].Value);
                    int patientId = Convert.ToInt32(selectedRow.Cells["Patient"].Value);
                    string executorFIO = (selectedRow.Cells["ExecutorFIO"].Value?.ToString() ?? string.Empty).Trim();
                    DateTime? dateOfExecution = (selectedRow.Cells["DateOfExecution"].Value != null && !string.IsNullOrEmpty(selectedRow.Cells["DateOfExecution"].Value.ToString()))
                        ? Convert.ToDateTime(selectedRow.Cells["DateOfExecution"].Value)
                        : (DateTime?)null;
                    string vaccineName = (selectedRow.Cells["VaccineName"].Value?.ToString() ?? string.Empty).Trim();

                    // Получаем RoleID из DataGridView
                    string methodName = (selectedRow.Cells["MethodOfAdministration"].Value?.ToString() ?? string.Empty).Trim();

                    string status = selectedRow.Cells["Status"].Value?.ToString() ?? string.Empty;

                    RedVaccination editForm = new RedVaccination(this, vaccinationId, patientId, executorFIO, dateOfExecution, vaccineName, methodName, status, patients, false); // или как назовешь форму

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // LoadData(); // ЗАМЕНЯЕМ ЭТУ СТРОКУ
                        LoadData(); // Перезагружаем данные из базы
                        DisplayData(); // Обновляем DataGridView
                        UpdatePagination(); // Обновляем пагинацию
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

        private void AllVaccination_FormClosed(object sender, FormClosedEventArgs e)
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

    }
}
