using Cursach.Properties;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach.DB_Management
{
    public partial class AddVaccine : Form
    {
        private string vaccineName;
        private double volume;
        private string unitName;
        private string packagingName;
        private string imagePath;
        private VaccinesAdmin vaccinesAdminForm;
        private byte[] imageBytes;

        public AddVaccine(VaccinesAdmin vaccinesAdminForm,string vaccineName, double volume, string unitName, string packagingName, string imagePath)
        {
            InitializeComponent();
            this.vaccinesAdminForm = vaccinesAdminForm;
            LoadDefaultImage(); // Загружаем фото-заглушку
            imageBytes = null; // Пустое изображение по умолчанию
            VaccineUnit.GotFocus += Focus_GotFocus;
            VaccinePackaging.GotFocus += Focus_GotFocus;


        } 
        #region Заполнение ComboBox
        private void AddVaccine_Load(object sender, EventArgs e)
        {
            
            VaccinePackaging.DataSource = GetPackaging();
            VaccinePackaging.DisplayMember = "PackagingName";
            VaccinePackaging.ValueMember = "PackagingID";

            VaccineUnit.DataSource = GetUnit();
            VaccineUnit.DisplayMember = "UnitName";
            VaccineUnit.ValueMember = "UnitID";
            

        }
        #endregion

        #region ЗаполнениеДаннымиУпаковка
        private List<Packaging> GetPackaging()
        {
            List<Packaging> packagings = new List<Packaging>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
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
                return packagings;
            }
        }
       
        public class Packaging
        {
            public int PackagingID { get; set; }
            public string PackagingName { get; set; }
        }
        #endregion

        #region ЗаполнениеДаннымиЕдиницаИзмерения
        private List<Unit> GetUnit()
        {
            List<Unit> units = new List<Unit>();
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    connection.Open();
                    string query = "SELECT UnitID, UnitName FROM Unit";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            units.Add(new Unit
                            {
                                UnitID = reader.GetInt32("UnitID"),
                                UnitName = reader.GetString("UnitName")
                            });
                        }
                    }
                }
                return units;
            }
        }
       

        public class Unit
        {
            public int UnitID { get; set; }
            public string UnitName { get; set; }
        }
        #endregion

        #region ОграничениеВвода
        private void VaccinePackaging_MouseDown(object sender, MouseEventArgs e)
        {
            if (!VaccinePackaging.DroppedDown)
            {
                VaccinePackaging.DroppedDown = true;
            }
        }
        private void VaccineUnit_MouseDown(object sender, MouseEventArgs e)
        {
            if (!VaccineUnit.DroppedDown)
            {
                VaccineUnit.DroppedDown = true;
            }
        }
        private void Focus_GotFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        private void NameVaccine_TextChanged(object sender, EventArgs e)
        {
            string text = NameVaccine.Text;

            // Ограничение длины текста
            if (text.Length > 20)
            {
                NameVaccine.Text = text.Substring(0, 20);
                NameVaccine.SelectionStart = NameVaccine.Text.Length;
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
                NameVaccine.Text = filteredText;
                NameVaccine.SelectionStart = NameVaccine.Text.Length;
            }
        }

        private void Vol_TextChanged(object sender, EventArgs e)
        {
            string input = Regex.Replace(Vol.Text, @"[^0-9,]", ""); // Удаляем все, кроме цифр и запятой

            // Проверяем, чтобы была только одна запятая
            if (input.Count(c => c == ',') > 1)
            {
                input = input.Replace(",", ""); // Удаляем все запятые, если их больше одной
            }

            // Если начинается не с "0,", добавляем "0,"
            if (!input.StartsWith("0,"))
            {
                input = "0," + input;
            }

            // Если только "0,", больше ничего не добавляем
            if (input == "0,")
            {
                Vol.Text = "0,";
                Vol.SelectionStart = Vol.Text.Length;
                return;
            }

            // Убираем лишние символы после запятой
            int commaIndex = input.IndexOf(',');
            if (commaIndex >= 0 && input.Length > commaIndex + 4) // Запятая + 3 символа
            {
                input = input.Substring(0, commaIndex + 4);
            }

            // Убираем лишние символы до запятой
            if (commaIndex > 1)
            {
                input = "0," + input.Substring(commaIndex + 1);
            }
            // Обновляем текстовое поле
            Vol.Text = input;
            Vol.SelectionStart = Vol.Text.Length;
        }

        private void VaccineUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }
        private void VaccineUnit_MouseClick(object sender, MouseEventArgs e)
        {
            VaccineUnit.Select(0, 0); // Снимаем выделение с поля ввода
        }

        private void VaccineUnit_KeyDown(object sender, KeyEventArgs e)
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

        private void VaccinePackaging_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Блокируем ввод символа
        }
        private void VaccinePackaging_MouseClick(object sender, MouseEventArgs e)
        {
            VaccinePackaging.Select(0, 0); // Снимаем выделение с поля ввода
        }

        #endregion

        #region ФотоЗаглушка
        private void LoadDefaultImage()
        {
            try
            {
                BoxImage.Image = Resources.noPhoto;
                BoxImage.SizeMode = PictureBoxSizeMode.Zoom;

                // Преобразуем изображение по умолчанию в массив байтов и сохраняем.
                using (MemoryStream ms = new MemoryStream())
                {
                    Resources.noPhoto.Save(ms, Resources.noPhoto.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фото-заглушки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void DelPhoto_Click(object sender, EventArgs e)
        {
            LoadDefaultImage(); // Загружаем фото-заглушку
            imageBytes = null;
        }

        private void AddPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            openFileDialog.Title = "Выберите фотографию";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Загружаем выбранное изображение в PictureBox
                    BoxImage.Image = Image.FromFile(openFileDialog.FileName);
                    BoxImage.SizeMode = PictureBoxSizeMode.Zoom;

                    // Преобразуем выбранное изображение в массив байтов
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        imageBytes = new byte[fs.Length];
                        fs.Read(imageBytes, 0, (int)fs.Length);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Метод для проверки на дубликаты
        private bool IsDuplicateVaccine()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string vaccineName = NameVaccine.Text;
            double volume;

            if (!double.TryParse(Vol.Text, out volume))
            {
                MessageBox.Show("Некорректный формат объема вакцины.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Считаем, что это дубликат, и блокируем добавление
            }

            // Получаем ID выбранных Unit и Packaging
            int unitId;
            int packagingId;

            // Получаем SelectedValue и преобразуем его в int.  Обрабатываем возможные ошибки.
            if (VaccineUnit.SelectedValue != null && int.TryParse(VaccineUnit.SelectedValue.ToString(), out unitId))
            {
                // unitId успешно получен
            }
            else
            {
                MessageBox.Show("Не выбрана единица измерения, или выбран некорректный ID.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Или другое действие, блокирующее добавление
            }

            if (VaccinePackaging.SelectedValue != null && int.TryParse(VaccinePackaging.SelectedValue.ToString(), out packagingId))
            {
                // packagingId успешно получен
            }
            else
            {
                MessageBox.Show("Не выбрана упаковка, или выбран некорректный ID.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Или другое действие, блокирующее добавление
            }


            string query = @"SELECT COUNT(*) FROM Vaccine
        WHERE VaccineName = @VaccineName
          AND Volume = @Volume
          AND Unit = @Unit
          AND Packaging = @Packaging";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VaccineName", vaccineName);
                        command.Parameters.AddWithValue("@Volume", volume);
                        command.Parameters.AddWithValue("@Unit", unitId); // Используем ID напрямую
                        command.Parameters.AddWithValue("@Packaging", packagingId); // Используем ID напрямую

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; // Возвращаем true, если найдена хотя бы одна запись (это дубликат)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке на дубликат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Считаем, что это НЕ дубликат, и позволяем добавление
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(NameVaccine.Text) ||
                string.IsNullOrWhiteSpace(Vol.Text) ||
                VaccineUnit.SelectedItem == null ||
                VaccinePackaging.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если есть пустые поля
            }

            // Проверка на дубликаты
            if (IsDuplicateVaccine())
            {
                MessageBox.Show("Вакцина с таким названием, объемом, единицей измерения и упаковкой уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если запись уже существует
            }

           
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string vaccineName = NameVaccine.Text;
            double volume;

            if (!double.TryParse(Vol.Text, out volume))
            {
                MessageBox.Show("Неверный формат объема вакцины", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string unitName = VaccineUnit.Text;
            string packagingName = VaccinePackaging.Text;

            // SQL-запрос для вставки данных
            string query = @"INSERT INTO Vaccine (VaccineName, Volume, Unit, Packaging, Image) 
                       VALUES (@VaccineName, @Volume, @Unit, @Packaging, @Image)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры с использованием SqlParameter
                        command.Parameters.AddWithValue("@VaccineName", vaccineName);
                        command.Parameters.AddWithValue("@Volume", volume);
                        command.Parameters.AddWithValue("@Unit", VaccineUnit.SelectedValue);
                        command.Parameters.AddWithValue("@Packaging", VaccinePackaging.SelectedValue);

                        // Проверяем, есть ли изображение для сохранения.  Если нет (null), передаем DBNull.Value
                        command.Parameters.Add("@Image", MySqlDbType.Blob);
                        if (imageBytes != null)
                        {
                            command.Parameters["@Image"].Value = imageBytes;
                        }
                        else
                        {
                            command.Parameters["@Image"].Value = DBNull.Value;
                        }

                        // Выполняем запрос
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Вакцина успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

          

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении вакцины: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

