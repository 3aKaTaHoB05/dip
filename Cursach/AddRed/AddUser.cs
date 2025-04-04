using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cursach.DB_Management.AddUser;
using static Cursach.Users;

namespace Cursach.DB_Management
{

    public partial class AddUser : Form
    {

        private string fio;
        private string phoneNumber;
        private int age;
        private string role;
        private string login;
        private string password;
        private Users usersForm;
        private string previousValidAge = "";


        public AddUser(Users usersForm, string fio, string phoneNumber, int age, string role, string login, string password)
        {
            InitializeComponent();
            this.usersForm = usersForm;

            AddPhone.Text = "+7";
            AddPhone.SelectionStart = AddPhone.Text.Length;

            AddAge.KeyPress += AddAge_KeyPress; // Подписываемся на событие KeyPress
            AddAge.TextChanged += AddAge_TextChanged; // Подписываемся на событие TextChanged

            UserRole.KeyPress += Role_KeyPress;
            UserRole.MouseClick += Role_MouseClick;
            UserRole.GotFocus += Focus_GotFocus;
        }

        #region Заполнение ComboBox
        private void AddUser_Load(object sender, EventArgs e)
        {
            UserRole.DataSource = GetRole();
            UserRole.DisplayMember = "RoleName";
            UserRole.ValueMember = "RoleID";
        }
        #endregion

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

        #region ОграничениеВвода
        private void UserRole_MouseDown(object sender, MouseEventArgs e)
        {
            if (!UserRole.DroppedDown)
            {
                UserRole.DroppedDown = true;
            }
        }
        private void AddFIO_TextChanged(object sender, EventArgs e)
        {
            string text = AddFIO.Text;

         
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

            // Обновление поля 
            if (AddFIO.Text != result)
            {
                AddFIO.Text = result;
                AddFIO.SelectionStart = AddFIO.Text.Length;
            }

        }

        private void Focus_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
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
                AddPassword.SelectionStart = AddPassword.Text.Length;
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
        private int GetRoleId(string role)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = "SELECT RoleID FROM Roles WHERE RoleName = @RoleName";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", role);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении ID роли: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
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


        #region КнопкаСоздать
        private void Add_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(AddFIO.Text) ||
                string.IsNullOrWhiteSpace(AddPhone.Text) ||
                string.IsNullOrWhiteSpace(AddAge.Text) ||
                UserRole.SelectedValue == null ||
                string.IsNullOrWhiteSpace(AddLogin.Text) ||
                string.IsNullOrWhiteSpace(AddPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если есть пустые поля
            }

            string fio = AddFIO.Text;
            string password = AddPassword.Text;
            string ageText = AddAge.Text;

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

            // Проверка пароля (минимум 6 символов, одна заглавная и один спец. символ)
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов, одну заглавную букву и один специальный символ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка возраста (старше 18 лет)
            if (!int.TryParse(ageText, out int age) || age < 18)
            {
                MessageBox.Show("Возраст должен быть числом больше или равным 18.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка на дубликаты
            if (IsDuplicateUser())
            {
               
                return; // Прекращаем выполнение, если запись уже существует
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string phone = AddPhone.Text;  // Получаем телефон
            string login = AddLogin.Text; // Получаем логин



            string hashedPassword = HashPassword(password);

            // SQL-запрос для вставки данных
            string query = @"INSERT INTO User (FIO, PhoneNumber, Age, Role, Login, Password) 
                 VALUES (@FIO, @PhoneNumber, @Age, @Role, @Login, @Password)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры с использованием SqlParameter
                        command.Parameters.AddWithValue("@FIO", fio);
                        command.Parameters.AddWithValue("@PhoneNumber", phone);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Role", UserRole.SelectedValue);
                        command.Parameters.AddWithValue("@Login", login);
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Используем хешированный пароль

                        // Выполняем запрос
                        command.ExecuteNonQuery();
                    }
                }
                    MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Дубликат
        // Вспомогательный метод для проверки пароля
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


        private bool IsDuplicateUser()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string login = AddLogin.Text;
            string fio = AddFIO.Text;
            string phone = AddPhone.Text;
            string password = AddPassword.Text; // Получаем пароль

            // Проверяем, существует ли пользователь с таким логином
            string loginQuery = "SELECT COUNT(*) FROM User WHERE Login = @Login";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(loginQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Login", login);

                        int loginCount = Convert.ToInt32(command.ExecuteScalar());
                        if (loginCount > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true; // Возвращаем true (дубликат)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // В случае ошибки считаем, что не дубликат (чтобы не блокировать добавление)
            }
            string passwordQuery = "SELECT COUNT(*) FROM User WHERE Password = @Password";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(passwordQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Password", password);

                        int passwordCount = Convert.ToInt32(command.ExecuteScalar());
                        if (passwordCount > 0)
                        {
                            MessageBox.Show("Пользователь с таким паролем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true; // Возвращаем true (дубликат)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // В случае ошибки считаем, что не дубликат (чтобы не блокировать добавление)
            }

            // Если логин уникален, проверяем, существует ли пользователь с такими же ФИО, телефоном и паролем
            string query = @"SELECT COUNT(*) FROM User
        WHERE PhoneNumber = @PhoneNumber"; // Убрали Login, так как уже проверили его уникальность

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@PhoneNumber", phone);
                        

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Пользователь с таким номером телефона  уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true; // Возвращаем true (дубликат)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке на дубликат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // В случае ошибки считаем, что не дубликат (чтобы не блокировать добавление)
            }

            

            // Если все проверки пройдены, возвращаем false (не дубликат)
            return false;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
