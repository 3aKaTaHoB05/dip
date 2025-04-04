using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach.DB_Management
{

    public partial class RedUser : Form
    {
        private int userId;
        private string fio;
        private string phoneNumber;
        private int age;
        private string roleComboBox;
        private string role;
        private string login;
        private string passwordd;
        private Users usersForm;
        private string previousValidAge = "";

  
        public RedUser(Users usersForm, string fio, string phoneNumber, int age, string role, string login, string passwor, int userID)
        {
            InitializeComponent();
            this.usersForm = usersForm;


            AddPhone.Text = "+7";
            AddPhone.SelectionStart = AddPhone.Text.Length;

            AddAge.KeyPress += AddAge_KeyPress; // Подписываемся на событие KeyPress
            AddAge.TextChanged += AddAge_TextChanged; // Подписываемся на событие TextChanged

            UserRole.MouseDown += Role_MouseDown;
            UserRole.KeyPress += Role_KeyPress;
            UserRole.MouseClick += Role_MouseClick;
            UserRole.GotFocus += Focus_GotFocus;

            UserRole.DataSource = GetRole();
            UserRole.DisplayMember = "RoleName";
            UserRole.ValueMember = "RoleID";


            AddPhone.Text = phoneNumber;
            roleComboBox = role;
             userId = userID;
            passwordd = passwor;
        
            // Устанавливаем значения в элементы управления
            AddFIO.Text = fio;
            AddAge.Text = age.ToString();

            AddLogin.Text = login;


            // Автоматически выбираем элемент в ComboBox для единицы измерения
            int unitIndex = UserRole.FindStringExact(roleComboBox.Trim());
            if (unitIndex != -1)
            {
                UserRole.SelectedIndex = unitIndex;
            }
            else
            {
                MessageBox.Show("Единица измерения не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        
        }

        #region ЗаполнениеДаннымиРоль
        private List<Role> GetRole()
        {
            List<Role> role = new List<Role>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
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
                return role;
            }
        }

        public class Role
        {
            public int RoleID { get; set; }
            public string RoleName { get; set; }
        }
        #endregion

        #region Заполнение ComboBox
        private void AddUser_Load(object sender, EventArgs e)
        {
            UserRole.DataSource = GetRole();
            UserRole.DisplayMember = "RoleName";
            UserRole.ValueMember = "RoleID";


            this.AddPassword.MouseEnter += new EventHandler(AddPassword_MouseEnter);
            this.AddPassword.MouseLeave += new EventHandler(AddPassword_MouseLeave);

            this.toolTip1.SetToolTip(this.AddPassword, "Введите новый пароль, чтобы изменить текущий. Пустое поле не изменит пароль.");

        }
        #endregion


        #region ОграничениеВвода
        private void UserRole_MouseDown(object sender, MouseEventArgs e)
        {
            if (!UserRole.DroppedDown)
            {
                UserRole.DroppedDown = true;
            }
        }
        private void Focus_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        private void AddFIO_TextChanged(object sender, EventArgs e)
        {
            string text = AddFIO.Text;

            // Ограничение длины строки
            if (text.Length > 50)
            {
                AddFIO.Text = text.Substring(0, 50);
                AddFIO.SelectionStart = AddFIO.Text.Length;
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
            if (AddFIO.Text != result)
            {
                AddFIO.Text = result;
                AddFIO.SelectionStart = AddFIO.Text.Length;
            }

        }



        private void AddAge_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddAge_TextChanged(object sender, EventArgs e)
        {

            // 1. Пытаемся преобразовать текст в число
            if (int.TryParse(AddAge.Text, out int age))
            {
                // 2. Проверяем, находится ли число в диапазоне от 1 до 100
                if (age >= 0 && age <= 100)
                {
                    // Число валидно, ничего не делаем
                }
                else
                {
                    // 3. Число невалидно, сбрасываем текст до предыдущего валидного значения
                    // или до пустой строки, если предыдущего значения нет.
                    AddAge.Text = (string.IsNullOrEmpty(previousValidAge) ? "" : previousValidAge);
                    AddAge.SelectionStart = AddAge.Text.Length; // Восстанавливаем положение курсора
                }
            }
            else
            {
                // 4. Преобразование не удалось (введен не числовой символ или пустая строка)
                // Сбрасываем текст до предыдущего валидного значения
                // или до пустой строки, если предыдущего значения нет.

                if (!string.IsNullOrEmpty(AddAge.Text))
                {
                    AddAge.Text = (string.IsNullOrEmpty(previousValidAge) ? "" : previousValidAge);
                    AddAge.SelectionStart = AddAge.Text.Length; // Восстанавливаем положение курсора

                }
            }

            // 5. Сохраняем текущее валидное значение для следующей проверки
            if (int.TryParse(AddAge.Text, out int validAge) && validAge >= 1 && validAge <= 100)
            {

                previousValidAge = AddAge.Text;

            }
        }

        private void AddPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            AddPhone.SelectionStart = 3; // Устанавливаем курсор на позицию после +7(
            AddPhone.SelectionLength = 0; // Снимаем выделение, если оно было
        }

        private void AddPhone_Click(object sender, EventArgs e)
        {
            AddPhone.SelectionStart = 3; // Устанавливаем курсор на позицию после +7(
            AddPhone.SelectionLength = 0; // Снимаем выделение, если оно было

        }

        private void AddLogin_TextChanged(object sender, EventArgs e)
        {
            string text = AddLogin.Text;

            // Ограничение длины (максимум 15 символов)
            if (text.Length > 15)
            {
                AddLogin.Text = text.Substring(0, 15);
                AddLogin.SelectionStart = AddLogin.Text.Length;
                return; // Завершаем обработку
            }

            // Фильтрация: разрешаем только латинские буквы, цифры, подчеркивания, точки и @
            string filteredText = new string(text.Where(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_' || c == '.' || c == '@').ToArray());

            // Запрет символов @ и . в начале строки (при вводе)
            if (filteredText.Length > 0 && (filteredText[0] == '@' || filteredText[0] == '.'))
            {
                filteredText = ""; // Очищаем строку, если @ или . стоит первым символом
            }

            // Ограничение на количество символов @ (не более одного)
            int atCount = filteredText.Count(c => c == '@');
            if (atCount > 1)
            {
                // Удаляем все @, кроме первого
                int firstAtIndex = filteredText.IndexOf('@');
                filteredText = filteredText.Substring(0, firstAtIndex + 1);
            }

            // Запрет @ и . подряд
            filteredText = filteredText.Replace("@.", "@");
            filteredText = filteredText.Replace(".@", ".");

            // Ограничение на количество символов . подряд (не более одного)
            filteredText = System.Text.RegularExpressions.Regex.Replace(filteredText, @"\.+", ".");

            // Обновляем текстовое поле
            AddLogin.Text = filteredText;
            AddLogin.SelectionStart = AddLogin.Text.Length;

            // Проверка минимальной длины (после фильтрации)
            if (filteredText.Length < 6)
            {
                // Если длина меньше 6, ничего не делаем и ждем дальнейшего ввода
                return;
            }
        }

        private void AddPassword_TextChanged(object sender, EventArgs e)
        {
            string text = AddPassword.Text;

            // Ограничение длины (максимум 15 символов)
            if (text.Length > 15)
            {
                AddPassword.Text = text.Substring(0, 15);
                AddPassword.SelectionStart = AddLogin.Text.Length;
                return; // Завершаем обработку
            }

            string allowedSpecialChars = "!@#$%&*()_+=-`~[]\\{}|;':,./?";
            string filteredText = new string(text.Where(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || allowedSpecialChars.Contains(c)).ToArray());


            // Обновляем текстовое поле
            AddPassword.Text = filteredText;
            AddPassword.SelectionStart = AddPassword.Text.Length;

            // Проверка минимальной длины (после фильтрации)
            if (filteredText.Length < 6)
            {
                // Если длина меньше 6, ничего не делаем и ждем дальнейшего ввода
                return;
            }
        }
        private void Role_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                return;
            }
        }

        private void Role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }
        private void Role_MouseClick(object sender, MouseEventArgs e)
        {
            UserRole.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private void Role_KeyDown(object sender, KeyEventArgs e)
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



        #endregion

        #region Подсказка
        private void AddPassword_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Введите новый пароль, чтобы изменить текущий. Пустое поле не изменит пароль.", AddPassword, 5000);
        }

        private void AddPassword_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(AddPassword);
        }
        #endregion



        private void LoadUserData(int userId)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT FIO, PhoneNumber, Age, RoleName, Login, Password FROM User WHERE UserID = @UserID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AddFIO.Text = reader["FIO"].ToString();
                                AddPhone.Text = reader["PhoneNumber"].ToString();
                                AddAge.Text = reader["Age"].ToString();
                                UserRole.SelectedValue = reader["RoleName"].ToString();
                                AddLogin.Text = reader["Login"].ToString();
                                AddPassword.Text = reader["Password"].ToString();




                            }
                            else
                            {
                                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }




        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Конвертируем байты в шестнадцатеричную строку
                }
                return builder.ToString();
            }
        }

        #region Дубликат
        // Вспомогательный метод для проверки пароля
        private bool IsDuplicateUser(string fio, string phoneNumber, string login, int userId)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = @"SELECT COUNT(*) FROM User 
                     WHERE (FIO = @FIO AND PhoneNumber = @PhoneNumber AND Login = @Login) 
                     AND UserID <> @UserID"; // Исключаем текущую запись

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@FIO", fio);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@UserID", userId); // Исключаем текущую запись

                    // Выполняем запрос и получаем количество дубликатов
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Возвращаем true, если найдены дубликаты
                }
            }
        }

      

        #endregion

        #region КнопкаИзменить
        private void Edit_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(AddFIO.Text) ||
                string.IsNullOrWhiteSpace(AddPhone.Text) ||
                string.IsNullOrWhiteSpace(AddAge.Text) ||
                string.IsNullOrWhiteSpace(UserRole.Text) ||
                string.IsNullOrWhiteSpace(AddLogin.Text))

            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если есть пустые поля
            }
            string fio = AddFIO.Text;
            string password = AddPassword.Text;
            string phoneNumber = AddPhone.Text;
            string ageText = AddAge.Text;
            string login = AddLogin.Text;

         


            //Проверка ФИО
            string[] fioParts = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (fioParts.Length != 3)
            {
                MessageBox.Show("ФИО должно содержать 3 инициала (Фамилия Имя Отчество)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!AddPhone.MaskCompleted)
            {
                MessageBox.Show("Пожалуйста, заполните номер телефона полностью.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                // Проверяем пароль, только если он не пустой
                if (!IsValidPassword(password))
                {
                    MessageBox.Show("Пароль должен содержать минимум 6 символов, одну заглавную букву и один специальный символ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Проверка возраста (старше 18 лет)
            if (!int.TryParse(ageText, out int age) || age < 18)
            {
                MessageBox.Show("Возраст должен быть числом больше или равным 18.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Возможно будет ошибка
            if (IsDuplicateUser(fio, phoneNumber, login, userId))
            {
                //MessageBox.Show("Пользователь с такими ФИО, телефоном или логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            // SQL-запрос для обновления данных
            string query = @"UPDATE User 
                 SET FIO = @FIO, 
                     Age = @Age, 
                     PhoneNumber = @PhoneNumber, 
                     Role = @Role,
                     Login = @Login, 
                     Password = @Password
                 WHERE UserID = @UserID";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@FIO", fio);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Role", Convert.ToInt32(UserRole.SelectedValue.ToString()));
                        command.Parameters.AddWithValue("@Login", login);

                        if (!string.IsNullOrWhiteSpace(password))
                        {
                            command.Parameters.AddWithValue("@Password", HashPassword(password));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Password", passwordd);
                        }

                        command.Parameters.AddWithValue("@UserID", userId);

                        // Выполняем запрос
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные пользователя успешно изменены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK; // Возвращаем DialogResult.OK
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось изменить данные пользователя. Возможно, запись не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении вакцины: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private bool IsValidPassword(string password)
        {
            if (password.Length < 6) return false;
            bool hasUpper = false;
            bool hasSpecial = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (!char.IsLetterOrDigit(c)) hasSpecial = true;
            }
            return hasUpper && hasSpecial;
        }

        #region Вспомогательные методы для получения ID
        private int GetRoleId(string roleName)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = "SELECT RoleID FROM Role WHERE RoleName = @RoleName";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return -1; // Возвращаем -1, если ID не найден
                        }
                    }
                }
            }
            catch (Exception ex)  // Ловим все остальные исключения
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return -1;
            }
        }
        #endregion

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}



