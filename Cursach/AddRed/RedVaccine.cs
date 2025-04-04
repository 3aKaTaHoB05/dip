using Cursach.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach.DB_Management
{
    public partial class RedVaccine : Form
    {
        private string _vaccineName;
        private double _volume;
        private string _unitName;
        private string _packagingName;
        private int vaccineId;
        private System.Drawing.Image _image;
        private byte[] imageBytes;
        private byte[] originalImageBytes= null;
        public RedVaccine(string vaccineName, double volume, string unitName, string packagingName,int series, Image image)
        {
            InitializeComponent();
            LoadDefaultImage();
            imageBytes = null;
            VaccinePackaging.GotFocus += Focus_GotFocus;
            VaccinePackaging.DataSource = GetPackaging();
            VaccinePackaging.DisplayMember = "PackagingName";
            VaccinePackaging.ValueMember = "PackagingID";

            VaccineUnit.GotFocus += Focus_GotFocus;
            VaccineUnit.DataSource = GetUnit();
            VaccineUnit.DisplayMember = "UnitName";
            VaccineUnit.ValueMember = "UnitID";


            vaccineId = series;
            _vaccineName = vaccineName;
            _volume = volume;
            _unitName = unitName;
            _packagingName = packagingName;
            _image = image;

           

            // Устанавливаем значения в элементы управления
            VaccineName.Text = _vaccineName;
            Volume.Text = _volume.ToString();

            // Автоматически выбираем элемент в ComboBox для единицы измерения
            int unitIndex = VaccineUnit.FindStringExact(_unitName.Trim());
            if (unitIndex != -1)
            {
                VaccineUnit.SelectedIndex = unitIndex;
            }
            else
            {
                MessageBox.Show("Единица измерения не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Автоматически выбираем элемент в ComboBox для упаковки
            int packagingIndex = VaccinePackaging.FindStringExact(_packagingName.Trim());
            if (packagingIndex != -1)
            {
                VaccinePackaging.SelectedIndex = packagingIndex;
            }
            else
            {
                MessageBox.Show("Упаковка не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Устанавливаем изображение, если оно не null
            if (_image != null)
            {
                Image.Image = _image;
            }
            else
            {
                _image = Properties.Resources.noPhoto;
            }

        }

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
            string text = VaccineName.Text;

            // Ограничение длины текста
            if (text.Length > 20)
            {
                VaccineName.Text = text.Substring(0, 20);
                VaccineName.SelectionStart = VaccineName.Text.Length;
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
                VaccineName.Text = filteredText;
                VaccineName.SelectionStart = VaccineName.Text.Length;
            }
        }

        private void Vol_TextChanged(object sender, EventArgs e)
        {
            string input = Regex.Replace(Volume.Text, @"[^0-9,]", ""); // Удаляем все, кроме цифр и запятой

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
                Volume.Text = "0,";
                Volume.SelectionStart = Volume.Text.Length;
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
            Volume.Text = input;
            Volume.SelectionStart = Volume.Text.Length;
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
            public static object SelectedItem { get; internal set; }
            public int UnitID { get; set; }
            public string UnitName { get; set; }
        }

















        #endregion

        #region ФотоЗаглушка
        private void LoadDefaultImage()
        {
            try
            {
                Image.Image = Resources.noPhoto;
                Image.SizeMode = PictureBoxSizeMode.Zoom;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фото-заглушки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        private bool isImageDeleted = false;
        private void button3_Click(object sender, EventArgs e)
        {
            LoadDefaultImage(); 
            imageBytes = null;
            isImageDeleted = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*";
            openFileDialog.Title = "Выберите фотографию";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Загружаем выбранное изображение в PictureBox
                    System.Drawing.Image loadedImage = System.Drawing.Image.FromFile(openFileDialog.FileName); // Загружаем изображение
                    Image.Image = loadedImage; // Присваиваем PictureBox.Image загруженное изображение
                    Image.SizeMode = PictureBoxSizeMode.Zoom;

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
        private bool IsDuplicateVaccine()
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string vaccineName = VaccineName.Text;
            double volume;

            if (!double.TryParse(Volume.Text, out volume))
            {
                return false;
            }

            int unitId = GetUnitId(VaccineUnit.Text);
            int packagingId = GetPackagingId(VaccinePackaging.Text);

            if (unitId == -1 || packagingId == -1)
            {
                return false;
            }

            byte[] existingImageBytes = null;
            if (vaccineId > 0)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string getImageQuery = "SELECT Image FROM Vaccine WHERE VaccineSeries = @VaccineSeries";
                        using (MySqlCommand getImageCommand = new MySqlCommand(getImageQuery, connection))
                        {
                            getImageCommand.Parameters.AddWithValue("@VaccineSeries", vaccineId);
                            using (MySqlDataReader reader = getImageCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (!reader.IsDBNull(0))
                                    {
                                        existingImageBytes = (byte[])reader.GetValue(0);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при получении изображения (например, записать в лог)
                    Console.WriteLine($"Ошибка при получении изображения: {ex.Message}");
                    // Не прекращаем проверку на дубликат, просто не будем учитывать изображение
                }
            }

            // Сравниваем изображения
            bool imagesAreEqual = (existingImageBytes == null && imageBytes == null) ||
                                   (existingImageBytes != null && imageBytes != null && existingImageBytes.SequenceEqual(imageBytes));

            string query = @"SELECT COUNT(*) FROM Vaccine 
                     WHERE VaccineName = @VaccineName 
                       AND Volume = @Volume 
                       AND Unit = @Unit 
                       AND Packaging = @Packaging
                       AND VaccineSeries <> @VaccineSeries";  // Исключаем текущую запись

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VaccineName", vaccineName);
                        command.Parameters.AddWithValue("@Volume", volume);
                        command.Parameters.AddWithValue("@Unit", unitId);
                        command.Parameters.AddWithValue("@Packaging", packagingId);
                        command.Parameters.AddWithValue("@VaccineSeries", vaccineId); //Учитываем текущий VaccineSeries

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0 && !imagesAreEqual)
                        {
                            return false; // Не дубликат, так как изменили только изображение
                        }

                        return count > 0; // Дубликат (с учетом изображения)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке на дубликат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    
            
          
         
        

        private void button1_Click(object sender, EventArgs e)
        {

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(VaccineName.Text) ||
                string.IsNullOrWhiteSpace(Volume.Text) ||
                string.IsNullOrWhiteSpace(VaccineUnit.Text) ||
                string.IsNullOrWhiteSpace(VaccinePackaging.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение, если есть пустые поля
            }

            if (IsDuplicateVaccine())
            {
                if (imageBytes != null && originalImageBytes != null && !imageBytes.SequenceEqual(originalImageBytes))
                {
                    //Изображение поменялось - не дубликат
                }
                else
                {
                    MessageBox.Show("Вакцина с таким названием, объемом, единицей измерения и упаковкой уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Прекращаем выполнение, если запись уже существует
                }

            }

            // Преобразуем Vol.Text в double
            if (!double.TryParse(Volume.Text, out double volume))
            {
                MessageBox.Show("Неверный формат объема вакцины.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (vaccineId <= 0) 
            {
                MessageBox.Show("Некорректная серия вакцины.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string vaccineName = VaccineName.Text;

            string query;

            if (isImageDeleted)
            {
                // Пользователь нажал "Удалить", нужно удалить изображение
                query = @"UPDATE Vaccine 
                  SET VaccineName = @VaccineName, 
                      Volume = @Volume, 
                      Unit = @Unit, 
                      Packaging = @Packaging,
                      Image = NULL  -- Явно удаляем изображение
                  WHERE VaccineSeries = @VaccineSeries";
            }
            else if (imageBytes != null)
            {
                // Пользователь выбрал новое изображение, обновляем все поля, включая Image
                query = @"UPDATE Vaccine 
                  SET VaccineName = @VaccineName, 
                      Volume = @Volume, 
                      Unit = @Unit, 
                      Packaging = @Packaging,
                      Image = @Image
                  WHERE VaccineSeries = @VaccineSeries";
            }
            else
            {
                // Пользователь редактирует другие поля, но НЕ меняет изображение
                query = @"UPDATE Vaccine 
                  SET VaccineName = @VaccineName, 
                      Volume = @Volume, 
                      Unit = @Unit, 
                      Packaging = @Packaging
                  WHERE VaccineSeries = @VaccineSeries";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры (ОБЩИЕ параметры для обоих запросов)
                        command.Parameters.AddWithValue("@VaccineName", vaccineName);
                        command.Parameters.AddWithValue("@Volume", volume);
                        command.Parameters.AddWithValue("@Unit", VaccineUnit.SelectedValue);  //Используем SelectedValue
                        command.Parameters.AddWithValue("@Packaging", VaccinePackaging.SelectedValue); //Используем SelectedValue
                        command.Parameters.AddWithValue("@VaccineSeries", vaccineId);  // VaccineSeries (ID записи)

                        if (imageBytes != null)
                        {
                            // Добавляем параметр изображения, только если было выбрано новое изображение
                            command.Parameters.Add("@Image", MySqlDbType.Blob);
                            command.Parameters["@Image"].Value = imageBytes;
                        }



                        // Выполняем запрос
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Вакцина успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK; // Возвращаем DialogResult.OK
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось изменить вакцину. Возможно, запись не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении вакцины: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Вспомогательные методы для получения ID
        private int GetUnitId(string unitName)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = "SELECT UnitID FROM Unit WHERE UnitName = @UnitName";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UnitName", unitName);
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

        private int GetPackagingId(string packagingName)
        {
            string connectionString = $"server={ConfigurationManager.AppSettings["DbHost"]};" + $"database={ConfigurationManager.AppSettings["DbName"]};" + $"uid={ConfigurationManager.AppSettings["DbUserName"]};" + $"pwd={ConfigurationManager.AppSettings["DbPassword"]};" + "charset=utf8mb4;";
            string query = "SELECT PackagingID FROM Packaging WHERE PackagingName = @PackagingName";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PackagingName", packagingName);
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

        private byte[] GetExistingImageBytes(int vaccineId, string connectionString)
        {
            byte[] imageBytes = null;

            string query = "SELECT Image FROM Vaccine WHERE VaccineSeries = @VaccineSeries";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VaccineSeries", vaccineId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    imageBytes = (byte[])reader.GetValue(0); // Получаем байты изображения
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок (логирование)
                Console.WriteLine($"Ошибка при получении изображения: {ex.Message}");
            }

            return imageBytes;
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
