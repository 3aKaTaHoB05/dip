using Cursach.DB_Management;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach
{
    public partial class Users : Form
    {
        public string AdminFullName { get; set; }
        private Timer TimerExit;
        private int TimeOut;
        public Users()
        {
            InitializeComponent();
            InitializeSortComboBox();
            Filtr.GotFocus += Focus_GotFocus;
            Sort.GotFocus += Focus_GotFocus;

            LoadTimeOut();
            InitializeTimerExit();
            SubscribeToUserActivity();

        }

        private void Users_Load(object sender, EventArgs e)
        {
            FillPackagingComboBox();
            ShowUsers.CellFormatting += ShowUsers_CellFormatting;

            #region ЗаголовкиПолей1
            Search.Text = "Поиск";

            Sort.Text = "Сортировка";
            Sort.ForeColor = Color.White;

            Filtr.Text = "Фильтрация";
            Filtr.ForeColor = Color.White;
            #endregion



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
                       c.RoleName,  
                       v.Login,
                       v.Password,
                       v.UserID
                FROM User v
                INNER JOIN Role c ON c.RoleID = v.Role
                ORDER BY v.UserID ASC", connection);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                #region Выделение строк
                ShowUsers.AllowUserToAddRows = false; // Запрет добавления новых строк пользователем
                ShowUsers.AllowUserToDeleteRows = false; // Запрет удаления строк пользователем
                ShowUsers.AllowUserToResizeColumns = false; // Запрет изменения размера столбцов пользователем
                ShowUsers.AllowUserToResizeRows = false; // Запрет изменения размера строк пользователем
                ShowUsers.ReadOnly = true; // Запрет редактирования ячеек
                ShowUsers.MultiSelect = false; // Запрет выделения нескольких строк
                ShowUsers.AllowUserToAddRows = false; //Пустые строка снизу

                ShowUsers.RowHeadersVisible = false; // Стобец справа

                foreach (DataGridViewColumn column in ShowUsers.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                #region Столбцы и строки

                ShowUsers.EnableHeadersVisualStyles = false; // Отключаем стили заголовков по умолчанию

                // Настройка стиля заголовков
                ShowUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Цвет фона заголовков
                ShowUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;    // Цвет текста заголовков
                ShowUsers.ColumnHeadersDefaultCellStyle.Font = new Font(ShowUsers.Font, FontStyle.Bold); // Жирный шрифт заголовков

                // Настройка стиля ячеек
                ShowUsers.DefaultCellStyle.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
                ShowUsers.RowTemplate.Height = 80; // Высота строки

             

                

          
                ShowUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Отключаем сортировку для всех столбцов
                foreach (DataGridViewColumn column in ShowUsers.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                #endregion

                // Привязываем DataTable к DataGridView
                ShowUsers.DataSource = dataTable;

                // Настройка заголовков столбцов
                ShowUsers.Columns["UserID"].HeaderText = "Идинтификатор";
                if (ShowUsers.Columns.Contains("UserID"))
                {
                    ShowUsers.Columns["UserID"].Visible = false;
                }
                ShowUsers.Columns["FIO"].HeaderText = "ФИО";
                ShowUsers.Columns["Age"].HeaderText = "Возраст";
                ShowUsers.Columns["PhoneNumber"].HeaderText = "Номер телефона";
                ShowUsers.Columns["RoleName"].HeaderText = "Роль";
                ShowUsers.Columns["Login"].HeaderText = "Логин";
                ShowUsers.Columns["Password"].HeaderText = "Пароль";


                // Устанавливаем индивидуальную ширину для столбцов ФИО и Возраст
                ShowUsers.Columns["FIO"].Width = 400; // Ширина для столбца ФИО
                ShowUsers.Columns["Age"].Width = 80; // Ширина для столбца ФИО
                ShowUsers.Columns["PhoneNumber"].Width = 180; // Ширина для столбца ФИО

                #region ПКМ
                ShowUsers.MouseDown += (s, mouseEventArgs) => // Переименовали "e" в "mouseEventArgs"
                {
                    // Проверяем, является ли нажатие правой кнопкой мыши
                    if (mouseEventArgs.Button == MouseButtons.Right)
                    {
                        // Получаем позицию курсора мыши относительно DataGridView
                        var hit = ShowUsers.HitTest(mouseEventArgs.X, mouseEventArgs.Y);

                        // Проверяем, что кликнули по ячейке
                        if (hit.Type == DataGridViewHitTestType.Cell)
                        {
                            // Снимаем выделение, если оно было
                            ShowUsers.ClearSelection();

                            // Выделяем всю строку
                            ShowUsers.Rows[hit.RowIndex].Selected = true;

                            // Устанавливаем фокус на первую ячейку выделенной строки
                            ShowUsers.CurrentCell = ShowUsers.Rows[hit.RowIndex].Cells[0];
                        }
                    }
                    else if (mouseEventArgs.Button == MouseButtons.Left)
                    {
                        // Отменяем выделение при нажатии левой кнопкой мыши
                        ShowUsers.ClearSelection();
                    }

                };
            }


            #endregion

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        #region Скрытие полей
        private void ShowUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ShowUsers.Columns[e.ColumnIndex].Name == "Password")
            {
                if (e.Value != null)
                {
                    e.Value = "🔒"; // Заменяем пароль шестью звездочками
                    e.FormattingApplied = true; // Указываем, что форматирование было применено
                }
            }

            // Форматирование для столбца "FIO"
            if (ShowUsers.Columns[e.ColumnIndex].Name == "FIO")
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

            // Форматирование для столбца "PhoneNumber"
            if (ShowUsers.Columns[e.ColumnIndex].Name == "PhoneNumber")
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

            // Форматирование для столбца "Login"
            if (ShowUsers.Columns[e.ColumnIndex].Name == "Login")
            {
                if (e.Value != null)
                {
                    string login = e.Value.ToString();
                    if (login.Length > 3)
                    {
                        // Заменяем последние 3 символа на ***
                        e.Value = login.Substring(0, login.Length - 3) + "***";
                    }
                    else if (login.Length > 0)
                    {
                        // Заменяем все символы на * в случае, если меньше 3 символов
                        e.Value = new string('*', login.Length);
                    }
                    e.FormattingApplied = true; // Указываем, что форматирование было применено
                }
            }

        }

        #endregion

        #region ВремяСчетчик

        private void LoadTimeOut()
        {
            TimeOut = int.TryParse(ConfigurationManager.AppSettings["TimeOut"], out int timeout) ? timeout * 1000 : 30000; // 30 секунд по умолчанию
        }

        private void InitializeTimerExit()
        {
            TimerExit = new Timer
            {
                Interval = TimeOut
            };
            TimerExit.Tick += TimerExit_Tick;
            TimerExit.Start();
        }

        private void SubscribeToUserActivity()
        {
            this.MouseMove += UserActivityDetected;
            this.KeyPress += UserActivityDetected;
        }

        private void UserActivityDetected(object sender, EventArgs e)
        {
            // Сброс таймера при активности пользователя
            TimerExit.Stop();
            TimerExit.Start();
        }

        private void TimerExit_Tick(object sender, EventArgs e)
        {
            // Блокировка системы
            TimerExit.Stop();
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            // Открытие формы авторизации
            var loginForm = new Form1(); // Переименуйте в соответствии с вашей формой авторизации
            loginForm.Show();
            this.Hide(); // Скрыть основную форму
        }
        #endregion

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

       

      
     
        #endregion

        #region ЗаполнениеФильтр
        private string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
        private List<Role> GetRole()
        {
            List<Role> role = new List<Role>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT RoleID, RoleName FROM Role";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            role.Add(new Role
                            {
                                RoleID = reader.GetInt32("RoleID"),
                                RoleName = reader.GetString("RoleName")
                            });
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return role;
        }

        public class Role
        {
            public int RoleID { get; set; }
            public string RoleName { get; set; }

            public override string ToString()
            {
                return RoleName; // Чтобы в ComboBox отображалось имя
            }
        }
        #endregion

        #region Фильтрация
        private void FillPackagingComboBox()
        {
            List<Role> role = GetRole();

            // Создаем специальный объект для пункта "Все"
            Role allPackaging = new Role { RoleID = -1, RoleName = "Все" }; // ID -1 для отличия

            // Добавляем пункт "Все" в список
            role.Insert(0, allPackaging);

            // Привязываем список к ComboBox
            Filtr.DataSource = role;
            Filtr.DisplayMember = "RoleName";
            Filtr.ValueMember = "RoleID";

            Filtr.SelectedIndex = 0; // Выбираем пункт "Все" по умолчанию

            Filtr.MouseClick += Sort_MouseClick;
        }
        private void Filtr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true; // Предотвращаем удаление выбранного элемента
                e.Handled = true;           // Указываем, что событие обработано
            }
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
                Role selectedPackaging = (Role)Filtr.SelectedItem;
                if (selectedPackaging.RoleID == -1)
                {
                    _selectedPackaging = ""; // Если выбрано "Все", сбрасываем фильтр
                }
                else
                {
                    _selectedPackaging = selectedPackaging.RoleName;
                }
            }
            else
            {
                _selectedPackaging = ""; // Сбрасываем фильтр по упаковке
            }

            ApplyFiltersAndSearch(); // Применяем фильтры и поиск
        }


        #endregion

        #region Поиск
        private string _lastSearchText = ""; // Сохраняем текст последнего поиска
        private void Search_TextChanged(object sender, EventArgs e)
        {
            string text = Search.Text;

            // Ограничение длины строки
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

            // Обновление поля (если были изменения)
            if (Search.Text != result)
            {
                Search.Text = result;
                Search.SelectionStart = Search.Text.Length;
            }

            _lastSearchText = Search.Text; // Обновляем текст последнего поиска
            ApplyFiltersAndSearch();
        }

        private string placeholderText = ""; // Убедитесь, что placeholderText инициализировано
        private void HighlightRows(string searchText)
        {
            // Сбрасываем цвет всех строк к исходному
            foreach (DataGridViewRow row in ShowUsers.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Или цвет по умолчанию для нечетных строк (укажите свой цвет)
            }

            if (string.IsNullOrEmpty(searchText) || searchText == placeholderText || searchText.Length < 2) return;  // Ничего не делаем, если поле поиска пустое, содержит текст-подсказку или меньше 2 символов

            foreach (DataGridViewRow row in ShowUsers.Rows)
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

        private void ApplyFilters() //Переименовали, но пусть пока останется
        {
            if (ShowUsers.DataSource is DataTable dataTable)
            {
                string filterExpression = "";

                // Фильтр по упаковке
                if (!string.IsNullOrEmpty(_selectedPackaging))
                {
                    // Экранирование кавычек внутри фильтра
                    string escapedPackaging = _selectedPackaging.Replace("'", "''");
                    filterExpression = $"RoleName = '{escapedPackaging}'";
                }

                // Применяем фильтр к DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
                dataTable.AcceptChanges();
            }
        }

        private void ApplyFiltersAndSearch()
        {
            ApplyFilters(); // Сначала применяем фильтр

            // Затем подсвечиваем результаты поиска
            HighlightRows(_lastSearchText);
        }

        #region Сортировка
        private void InitializeSortComboBox()
        {
           
            Sort.Items.Add("⭣ ФИО");
            Sort.Items.Add("⭡ ФИО");
            Sort.Items.Add("Сброс");
            Sort.SelectedIndexChanged += Sort_SelectedIndexChanged;

            
            Sort.MouseClick += Sort_MouseClick;

            if (Sort.SelectedItem?.ToString() == "Сброс")
            {
                Sort.Text = "Сортировка";
                Sort.ForeColor = Color.Black;
            }
        }

       private void Sort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true; // Предотвращаем удаление выбранного элемента
                e.Handled = true;           // Указываем, что событие обработано
            }
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
                    sortExpression = "FIO ASC"; // Сортировка по возрастанию
                    break;
                case 1:
                    sortExpression = "FIO DESC"; // Сортировка по убыванию
                    break;
                case 2:
                    if (ShowUsers.DataSource is DataTable localDataTable)
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

            if (ShowUsers.DataSource is DataTable localDataTable2)
            {
                localDataTable2.DefaultView.Sort = sortExpression;
                localDataTable2.AcceptChanges();
                HighlightRows(_lastSearchText); // Восстанавливаем подсветку
            }
        }
        #endregion

        private void Add_Click(object sender, EventArgs e)
        {
            AddUser userAdd = new AddUser(this, "","", 0,"", "", "");
            userAdd.FormClosed += AddUserForm_FormClosed;
            userAdd.ShowDialog();

        }
        private void AddUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Users_Load(this, EventArgs.Empty); 

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuAdmin menu = new MenuAdmin(AdminFullName);
            menu.ShowDialog();
        }

      

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowUsers.SelectedRows[0];

              

                try
                {
                    int userId = Convert.ToInt32(selectedRow.Cells[6].Value);
                    string fio = selectedRow.Cells["FIO"].Value?.ToString() ?? string.Empty;
                    string phoneNumber = selectedRow.Cells["PhoneNumber"].Value?.ToString() ?? string.Empty;
                    int age = selectedRow.Cells["Age"].Value != null && int.TryParse(selectedRow.Cells["Age"].Value.ToString(), out int parsedAge) ? parsedAge : 0;

                    // Получаем RoleID из DataGridView
                    string roleName = (selectedRow.Cells["RoleName"].Value?.ToString() ?? string.Empty).Trim();



                    string login = selectedRow.Cells["Login"].Value?.ToString() ?? string.Empty;
                    string password = selectedRow.Cells["Password"].Value?.ToString() ?? string.Empty;

                    RedUser editForm = new RedUser(this, fio, phoneNumber, age, roleName, login, password, userId); // передаем ссылку на текущую форму

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

        // Метод для получения RoleName по RoleID
        private string GetRoleNameById(int roleId)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string roleName = string.Empty;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT RoleName FROM Role WHERE RoleID = @RoleID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleID", roleId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            roleName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении RoleName: {ex.Message}");
            }

            return roleName;
        }

        // Метод для получения UserID по данным пользователя
        private int GetUserId(string fio, string phoneNumber, int age, string login)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            int userId = -1; // По умолчанию, UserID не найден

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT UserID 
                FROM User
                WHERE FIO = @FIO
                  AND PhoneNumber = @PhoneNumber
                  AND Age = @Age
                  AND Login = @Login"; // Добавлено условие по логину

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FIO", fio);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Login", login); // Добавлено условие по логину

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
            }
           
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении UserID: {ex.Message}");
            }

            return userId;
        }


        // Метод для загрузки данных в DataGridView
        private void LoadData()
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
                       c.RoleName,  
                       v.Login,
                       v.Password,
                       v.UserID
                FROM User v
                INNER JOIN Role c ON c.RoleID = v.Role
                ORDER BY v.UserID ASC", connection);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                ShowUsers.DataSource = dataTable;

                // Настройка заголовков столбцов
                ShowUsers.Columns["UserID"].HeaderText = "Идинтификатор";
                if (ShowUsers.Columns.Contains("UserID"))
                {
                    ShowUsers.Columns["UserID"].Visible = false;
                }
                ShowUsers.Columns["FIO"].HeaderText = "ФИО";
                ShowUsers.Columns["Age"].HeaderText = "Возраст";
                ShowUsers.Columns["PhoneNumber"].HeaderText = "Номер телефона";
                ShowUsers.Columns["RoleName"].HeaderText = "Роль";
                ShowUsers.Columns["Login"].HeaderText = "Логин";
                ShowUsers.Columns["Password"].HeaderText = "Пароль";

                // Устанавливаем индивидуальную ширину для столбцов ФИО и Возраст
                ShowUsers.Columns["FIO"].Width = 400; // Ширина для столбца ФИО
                ShowUsers.Columns["Age"].Width = 80; // Ширина для столбца ФИО
                ShowUsers.Columns["PhoneNumber"].Width = 180; // Ширина для столбца ФИО

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        #region Удаление
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ShowUsers.SelectedRows[0]; // Берем первую выбранную строку

                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID удаляемой записи (предполагаем, что ID хранится в столбце "VaccineSeries")
                        int userId = Convert.ToInt32(selectedRow.Cells["UserID"].Value);

                        // Выполняем удаление из базы данных
                        string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
                        string query = "DELETE FROM User WHERE UserID = @UserID";

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserID", userId);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Удаляем строку из DataGridView
                        ShowUsers.Rows.Remove(selectedRow);

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
        #endregion


        

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
        if (Application.OpenForms["Form1"] is Form1 form)
            form.Show();
    }
    }

}

