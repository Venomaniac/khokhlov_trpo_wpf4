using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;

namespace khokhlov_trpo_wpf4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Course CurrentCourse => (Course)Resources["CurrentCourse"];

        private string GetFilePath(int id) => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{id}.json");

        private int GenerateUniqueId()
        {
            var rnd = new Random();
            int id;
            int attempts = 0;
            do
            {
                id = rnd.Next(1, int.MaxValue);
                attempts++;
                if (attempts > 1000)
                    throw new Exception("Не удалось сгенерировать уникальный Id");
            } while (File.Exists(GetFilePath(id)));
            return id;
        }

        private void NewCourse(object sender, RoutedEventArgs e)
        {
            CurrentCourse.Id = GenerateUniqueId();
            CurrentCourse.Title = "Название курса";
            CurrentCourse.Teacher = "Преподаватель";
            CurrentCourse.PricePerHour = 1000;
            CurrentCourse.Hours = 10;
        }

        private void SaveCourse(object sender, RoutedEventArgs e)
        {
            if (CurrentCourse.Id == 0)
            {
                MessageBox.Show("Сначала создайте новую запись.");
                return;
            }
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(CurrentCourse, options);
                File.WriteAllText(GetFilePath(CurrentCourse.Id), json);
                MessageBox.Show($"Курс сохранён в файл {CurrentCourse.Id}.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
        private void FindCourse(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SearchBox.Text, out int id))
            {
                MessageBox.Show("Введите корректный числовой ID.");
                return;
            }
            string path = GetFilePath(id);
            if (!File.Exists(path))
            {
                MessageBox.Show("Курс с таким ID не найден.");
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                var course = JsonSerializer.Deserialize<Course>(json);
                if (course == null)
                {
                    MessageBox.Show("Не удалось прочитать данные.");
                    return;
                }
                CurrentCourse.Id = course.Id;
                CurrentCourse.Title = course.Title;
                CurrentCourse.Teacher = course.Teacher;
                CurrentCourse.PricePerHour = course.PricePerHour;
                CurrentCourse.Hours = course.Hours;
                MessageBox.Show("Курс загружен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }
    }
}
