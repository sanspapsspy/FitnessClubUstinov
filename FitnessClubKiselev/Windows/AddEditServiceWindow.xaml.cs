using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.IO;
using FitnessClubKiselev.ClassHelper;
using FitnessClubKiselev.DB;
using FitnessClubKiselev.Windows;

namespace FitnessClubKiselev.Windows
{
    public partial class AddEditServiceWindow : Window
    {
        private string pathImage = null;
        private bool isEdit = false;
        private Service editService;

        public AddEditServiceWindow()
        {
            // Конструктор для добавления услуги
            InitializeComponent();

            isEdit = false;
        }

        public AddEditServiceWindow(Service service)
        {
            // Конструктор для редактирования услуги
            InitializeComponent();

            // Смена содержимого окна
            TbTitleService.Text = "Редактирование услуги";
            BtnAddService.Content = "Сохранить изменения";

            // Вывод данных из базы данных
            TbNameService.Text = service.NameService.ToString();
            TbPrice.Text = service.Price.ToString();
            TbDuration.Text = service.Duration.ToString();
            TbDescription.Text = service.Description.ToString();


            // Вывод фото
            if (service.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(service.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    ImgService.Source = bitmapImage;
                }
            }


            isEdit = true;
            editService = service;

        }
        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {

            // Выбор фото
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgService.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathImage = openFileDialog.FileName;
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {

            // Валидация
            if (isEdit == true)
            {
                // Изменение услуги
                editService.NameService = TbNameService.Text;
                editService.Price = Convert.ToDecimal(TbPrice.Text);
                editService.Duration = Convert.ToInt32(TbDuration.Text);
                editService.Description = TbDescription.Text;
                if (pathImage != null)
                {
                    editService.Photo = File.ReadAllBytes(pathImage);
                }
                EFClass.context.SaveChanges();

            }
            else
            {
                // Добавление услуги
                Service service = new Service();
                service.NameService = TbNameService.Text;
                service.Price = Convert.ToDecimal(TbPrice.Text);
                service.Duration = Convert.ToInt32(TbDuration.Text);
                service.Description = TbDescription.Text;
                if (pathImage != null)
                {
                    editService.Photo = File.ReadAllBytes(pathImage);
                }

                EFClass.context.Service.Add(service);
                EFClass.context.SaveChanges();
                MessageBox.Show("Услуга успешно добавлена");
            }

            this.Close();
        }
    }
}
