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

namespace Cursach.DB_Management
{
    public partial class AddPatient : Form
    {
        private int patientId;
        private string fio;
        private string phoneNumber;
        private int age;
        private string genderName;

        private Patients patientsForm;
        private string previousValidAge = "";
        public AddPatient(Patients patientsForm, string patientId, string fio, string phoneNumber, int age, string genderName, string socialStatusName)
        {
            InitializeComponent();
            this.patientsForm = patientsForm;

            Phone.Text = "+7";
            Phone.SelectionStart = Phone.Text.Length;

            Age.KeyPress += Age_KeyPress; // Подписываемся на событие KeyPress
            Age.TextChanged += Age_TextChanged; // Подписываемся на событие TextChanged

        
            GenderCC.KeyPress += Gender_KeyPress;
            GenderCC.MouseClick += Gender_MouseClick;
            GenderCC.GotFocus += Focus_GotFocus;
        }

        #region Заполнение ComboBox
        private void Add_Load(object sender, EventArgs e)
        {
            GenderCC.DataSource = GetGender();
            GenderCC.DisplayMember = "GenderName";
            GenderCC.ValueMember = "GenderID";

            SocialStatusComboBox.DataSource = GetSocialStatus();
            SocialStatusComboBox.DisplayMember = "SocialStatusName";
            SocialStatusComboBox.ValueMember = "SocialStatusID";

        }
        #endregion

        #region ЗаполнениеДаннымиРоль
        private List<Gender> GetGender()
        {
            List<Gender> genderName = new List<Gender>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT GenderID, GenderName FROM Gender";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genderName.Add(new Gender
                            {
                                GenderID = reader.GetInt32("GenderID"),
                                GenderName = reader.GetString("GenderName")
                            });
                        }
                    }
                }
                connection.Close();
                return genderName;
                
            }
            
        }

        public class Gender
        {
            public int GenderID { get; set; }
            public string GenderName { get; set; }
        }
        #endregion

        #region ЗаполнениеДаннымиСоциальныйСтатус
        private List<SocialStatus> GetSocialStatus()
        {
            List<SocialStatus> SocialStatus = new List<SocialStatus>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT SocialStatusID, SocialStatusName FROM SocialStatus";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SocialStatus.Add(new SocialStatus
                            {
                                SocialStatusID = reader.GetInt32("SocialStatusID"),
                                SocialStatusName = reader.GetString("SocialStatusName")
                            });
                        }
                    }
                }
                connection.Close();
                return SocialStatus;
            }
        }

        public class SocialStatus
        {
            public int SocialStatusID { get; set; }
            public string SocialStatusName { get; set; }
        }
        #endregion


        #region ОграничениеВвода

        private void FIO_TextChanged(object sender, EventArgs e)
        {
            string text = FIO.Text;

            // Ограничение длины строки
            if (text.Length > 50)
            {
                FIO.Text = text.Substring(0, 50);
                FIO.SelectionStart = FIO.Text.Length;
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
            if (FIO.Text != result)
            {
                FIO.Text = result;
                FIO.SelectionStart = FIO.Text.Length;
            }

        }



        private void Age_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Age_TextChanged(object sender, EventArgs e)
        {

            // 1. Пытаемся преобразовать текст в число
            if (int.TryParse(Age.Text, out int age))
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
                    Age.Text = (string.IsNullOrEmpty(previousValidAge) ? "" : previousValidAge);
                    Age.SelectionStart = Age.Text.Length; // Восстанавливаем положение курсора
                }
            }
            else
            {
                // 4. Преобразование не удалось (введен не числовой символ или пустая строка)
                // Сбрасываем текст до предыдущего валидного значения
                // или до пустой строки, если предыдущего значения нет.

                if (!string.IsNullOrEmpty(Age.Text))
                {
                    Age.Text = (string.IsNullOrEmpty(previousValidAge) ? "" : previousValidAge);
                    Age.SelectionStart = Age.Text.Length; // Восстанавливаем положение курсора

                }
            }

            // 5. Сохраняем текущее валидное значение для следующей проверки
            if (int.TryParse(Age.Text, out int validAge) && validAge >= 1 && validAge <= 100)
            {

                previousValidAge = Age.Text;

            }
        }

        private void Phone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            Phone.SelectionStart = 3; // Устанавливаем курсор на позицию после +7(
            Phone.SelectionLength = 0; // Снимаем выделение, если оно было
        }

        private void Phone_Click(object sender, EventArgs e)
        {
            Phone.SelectionStart = 3; // Устанавливаем курсор на позицию после +7(
            Phone.SelectionLength = 0; // Снимаем выделение, если оно было

        }

        private void Gender_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }

        private void Gender_MouseClick(object sender, MouseEventArgs e)
        {
            GenderCC.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private void Gender_KeyDown(object sender, KeyEventArgs e)
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

        private void SocialStatus_MouseClick(object sender, MouseEventArgs e)
        {
            GenderCC.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private void Focus_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }



        #endregion

        #region КнопкаСоздать
        private void Add_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(FIO.Text) ||
                string.IsNullOrWhiteSpace(Phone.Text) ||
                string.IsNullOrWhiteSpace(Age.Text) ||
                SocialStatusComboBox.SelectedValue == null ||
                GenderCC.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если есть пустые поля
            }
            string fio = FIO.Text;
            string ageText = Age.Text;
            string phone = Phone.Text;
            //Проверка ФИО
            string[] fioParts = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (fioParts.Length != 3)
            {
                MessageBox.Show("ФИО должно содержать 3 инициала (Фамилия Имя Отчество)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Phone.MaskCompleted)
            {
                MessageBox.Show("Пожалуйста, заполните номер телефона полностью.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            // Проверка возраста (старше 18 лет)
            if (!int.TryParse(ageText, out int age) || age < 18)
            {
                MessageBox.Show("Возраст должен быть числом больше или равным 18.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем значения SocialStatus и Gender
            int socialStatusId = Convert.ToInt32(SocialStatusComboBox.SelectedValue);
            int genderId = Convert.ToInt32(GenderCC.SelectedValue);

            // Проверяем на дубликат
            if (IsDuplicatePatient(fio, age, socialStatusId, genderId, phone))
            {
                DialogResult result = MessageBox.Show("Запись с такими ФИО, возрастом, социальным статусом и полом уже существует. Вы уверены, что хотите добавить эту запись как нового пациента (возможно, близнеца)?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // Пользователь решил не добавлять запись
                }
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";


            // SQL-запрос для вставки данных
            string query = @"INSERT INTO Patient (FIO, PhoneNumber, Age, SocialStatus, Gender) 
         VALUES (@FIO, @PhoneNumber, @Age, @SocialStatus, @Gender)";

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
                        command.Parameters.AddWithValue("@SocialStatus", socialStatusId);
                        command.Parameters.AddWithValue("@Gender", genderId);

                        // Выполняем запрос
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Данные успешно добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (MySqlException ex)
            {
                // Обработка ошибок при работе с базой данных MySQL
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка других возможных ошибок
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsDuplicatePatient(string fio, int age, int socialStatusId, int genderId, string phoneNumber)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = "SELECT COUNT(*) FROM Patient WHERE FIO = @FIO AND Age = @Age AND SocialStatus = @SocialStatus AND Gender = @Gender AND PhoneNumber = @PhoneNumber";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FIO", fio);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@SocialStatus", socialStatusId);
                        command.Parameters.AddWithValue("@Gender", genderId);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber); 

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; 
                    }
                }
            }
            catch (MySqlException ex)
            {
              
                Console.WriteLine("Ошибка при проверке дубликата: " + ex.Message);
                return false; 
            }

        }


        #endregion

        private void SocialStatusComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!SocialStatusComboBox.DroppedDown)
            {
                SocialStatusComboBox.DroppedDown = true;
            }
        }

        private void GenderCC_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GenderCC.DroppedDown)
            {
                GenderCC.DroppedDown = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

    }
}
