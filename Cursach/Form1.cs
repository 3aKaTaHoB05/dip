using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Configuration;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Office.Interop.Word;

namespace Cursach
{
    public partial class Form1 : Form
    {

        private int loginAttempts = 0;
        private bool isPasswordVisible = false; // Переменная для отслеживания состояния видимости пароля
        private int failedAttempts = 0;

        private string captchaText;
        private Random random = new Random();
        private DateTime lockoutEndTime;
        private const int lockoutDuration = 10; // Время блокировки
        public Form1()
        {
            InitializeComponent();
            GenerateCaptcha();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void LogPas_Click(object sender, EventArgs e)
        {
            string username = Login.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прерываем выполнение, если поля пустые
            }

            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password.Equals("admin", StringComparison.Ordinal))
            {
                this.Hide();
                admin admin = new admin();
                admin.ShowDialog();
                Login.Text = "admin";
                Password.Text = "admin";
            }

            // Проверяем соединение с базой данных
            if (!CheckDatabaseConnection())
            {
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Предположим, что у вас есть ссылка на текущую форму
            Form1 currentForm = this; // Сохраняем ссылку на текущую форму

            // Проверяем пользователя
            if (ValidateUser(username, password, out int role, out string fio))
            {
                loginAttempts = 0; // Сбрасываем счетчик попыток при успешной авторизации
                switch (role)
                {
                    case 1: // Админ
                        currentForm.Hide();
                        MenuAdmin adminMenu = new MenuAdmin(fio);
                        adminMenu.ShowDialog();
                        Login.Text = "";
                        Password.Text = "";
                        break;
                    case 2: // Мед сотрудник
                        currentForm.Hide();
                        MenuMedic medicMenu = new MenuMedic(fio);
                        medicMenu.ShowDialog();
                        Login.Text = "";
                        Password.Text = "";
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                failedAttempts++;
                loginAttempts++;
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (failedAttempts >= 1)
                {
                    Login.Text = "";
                    Password.Text = "";

                    // Изменяем размеры текущей формы
                    currentForm.Width = 672;  
                    currentForm.Height = 375; 

                    Input.Enabled = false;
                    Login.Enabled = false;
                    Password.Enabled = false;
                    hidePass.Enabled = false;

                }
            }
        }

        #region Капча

        // Обработчик для проверки капчи
        private void CapButton_Click(object sender, EventArgs e)
            {
                // Проверка блокировки
                if (DateTime.Now < lockoutEndTime)
                {
                    MessageBox.Show($"Попробуйте снова через {lockoutEndTime.Subtract(DateTime.Now).Seconds} секунд.", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            if (txtCaptchaInput.Text.Equals(captchaText))
            {
                    MessageBox.Show("CAPTCHA введена правильно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Разблокируем элементы на форме
                    Input.Enabled = true;
                    Login.Enabled = true;
                    Password.Enabled = true;
                    hidePass.Enabled = true;

                    Form1 currentForm = this;
                    currentForm.Width = 365; 
                    currentForm.Height = 375;

                GenerateCaptcha();
                txtCaptchaInput.Text = "";

            }
                else
                {
                    MessageBox.Show("Неверная CAPTCHA. Пожалуйста, попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCaptchaInput.Text = "";
                GenerateCaptcha();
                    lockoutEndTime = DateTime.Now.AddSeconds(lockoutDuration);
                }
        }

        private void GenerateCaptcha()
        {
            captchaText = GenerateRandomText(4);
            ImageCaptcha.Image = GenerateCaptchaImage(captchaText);
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        private Bitmap GenerateCaptchaImage(string text)
        {
            Bitmap bitmap = new Bitmap(257, 159); // Размер изображения
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                System.Drawing.Font font = new System.Drawing.Font("Arial", 24, FontStyle.Bold);
                Pen pen = new Pen(Color.Red, 2); // Перечеркивающая линия

                // Начальная позиция по X
                float xOffset = 10;
                float baseY = 40; // Базовая позиция по Y для всех символов

                // Рисуем буквы в строку
                for (int i = 0; i < text.Length; i++)
                {
                    // Случайное смещение по Y для каждой буквы
                    float yOffset = (float)(random.NextDouble() * 10 - 5); // Смещение от -5 до 5
                    float y = baseY + yOffset; // Новая позиция по Y для буквы

                    float x = xOffset + i * 50; // Расстояние между символами
                    g.DrawString(text[i].ToString(), font, Brushes.Black, new PointF(x, y));

                    float lineStartX = xOffset; // Начало линии
                    float lineEndX = xOffset + (text.Length * 50) - 10; // Конец линии
                    float lineYStart = baseY + 30; // начало
                    float lineYEnd = baseY + 5; // конец

                    // Рисуем линию
                    g.DrawLine(pen, lineStartX, lineYStart, lineEndX, lineYEnd);
                }

                // Добавление графического шума
                for (int i = 0; i < 3900; i++)
                {
                    bitmap.SetPixel(random.Next(bitmap.Width), random.Next(bitmap.Height),
                                    Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                }
            }
            return bitmap;
        }


        private void NewCaptchaButton_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        #endregion

        private bool CheckDatabaseConnection()
        {
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
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private string HashPassword(string password) // Оставьте этот метод как есть
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

        private bool ValidateUser(string username, string password, out int userRole, out string userName)
        {
            userRole = 0;
            userName = string.Empty;

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" +
                                      $"database={ConfigurationManager.AppSettings["DbName"]};" +
                                      $"uid={ConfigurationManager.AppSettings["DbUserName"]};" +
                                      $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" +
                                      "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Role, FIO, Password FROM User WHERE Login = @username"; // Запрос, чтобы получить хеш

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader.GetString(2); // Получаем хэш из базы данных
                                string hashedPassword = HashPassword(password);  // Хешируем введенный пароль

                                if (storedHash == hashedPassword)  // Сравниваем хеши
                                {
                                    userRole = reader.GetInt32(0);
                                    userName = reader.GetString(1);
                                    return true;
                                }
                            }
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок (например, логирование)
                        MessageBox.Show($"Ошибка при проверке пользователя: {ex.Message}");
                        return false; // Ошибка при выполнении запроса
                    }
                    
                }
            }
        }

        private void hidePass_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            Password.UseSystemPasswordChar = !isPasswordVisible;


            if (isPasswordVisible)
            {
                hidePass.Image = Properties.Resources.eye_close;
                
            }
            else
            {
                hidePass.Image = Properties.Resources.eye_open;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                e.Cancel = true;
        }

        private void Login_TextChanged(object sender, EventArgs e)
        {
            string text = Login.Text;
            if (text.Length > 15)
            {
                Login.Text = text.Substring(0, 15);
                Login.SelectionStart = Login.Text.Length;
                return; 
            }
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            string text = Password.Text;

            // Ограничение длины (максимум 15 символов)
            if (text.Length > 15)
            {
                Password.Text = text.Substring(0, 15);
                Password.SelectionStart = Password.Text.Length;
                return; // Завершаем обработку
            }
        }

        private void txtCaptchaInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, сколько символов уже введено
            if (txtCaptchaInput.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                // Если длина уже 4 и вводится не управляющий символ (например, Backspace), отменяем ввод
                e.Handled = true;
            }

            // Проверяем, разрешён ли символ (только латинские буквы и цифры)
            if (!char.IsControl(e.KeyChar) &&
                !("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Contains(e.KeyChar)))
            {
                // Если символ не разрешён, отменяем ввод
                e.Handled = true;
            }
        }
    }
}
