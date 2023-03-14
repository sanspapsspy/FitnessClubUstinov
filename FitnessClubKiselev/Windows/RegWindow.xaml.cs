using FitnessClubKiselev.ClassHelper;
using FitnessClubKiselev.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace FitnessClubKiselev.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private string pathImage = null;
        private Client editClient;
        private bool isEdit = false;
        public RegWindow()
        {
            InitializeComponent();
            CMBGender.ItemsSource = ClassHelper.EFClass.context.Gender.ToList();
            CMBGender.DisplayMemberPath = "NameGender";
            CMBGender.SelectedIndex = 0;
            isEdit = false;
        }

        public RegWindow(Client client)
        {
            InitializeComponent();
            CMBGender.ItemsSource = ClassHelper.EFClass.context.Gender.ToList();
            CMBGender.DisplayMemberPath = "NameGender";
            CMBGender.SelectedIndex = 0;

            // Смена содержимого окна
            TbTitleClient.Text = "Редактирование клиента";
            BtnReg.Content = "Сохранить изменения";

            // Вывод данных из базы данных
            TbFName.Text = client.FName.ToString();
            TbLName.Text = client.LName.ToString();
            TbPatronymic.Text = client.Patronymic.ToString();
            TbEmail.Text = client.Email.ToString();
            TbPhone.Text = client.Phone.ToString();
            TbSeriesPass.Text = client.PassportSeries.ToString();
            TbNumberPass.Text = client.PassportNumber.ToString();
            DPbDateOfBirth.Text = client.Birthday.ToString();
            CMBGender.SelectedItem = ClassHelper.EFClass.context.Gender.Where(i => i.Id == client.IdGender).FirstOrDefault();

            // Вывод фото
            if (client.PhotoClient != null)
            {
                using (MemoryStream stream = new MemoryStream(client.PhotoClient))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    ImgClient.Source = bitmapImage;
                }
            }


            isEdit = true;
            editClient = client;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {

            // Валидация

            if (string.IsNullOrWhiteSpace(TbFName.Text))
            {
                MessageBox.Show("Вы не заполнили Имя.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbLName.Text))
            {
                MessageBox.Show("Вы не заполнили Фамилию.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                MessageBox.Show("Вы не заполнили Телефон.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbNumberPass.Text))
            {
                MessageBox.Show("Вы не заполнили Номер паспорта.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbSeriesPass.Text))
            {
                MessageBox.Show("Вы не заполнили Серию паспорта.");
                return;
            }

            if (TbSeriesPass.Text.Length < 4 || TbSeriesPass.Text.Length > 4)
            {
                MessageBox.Show("Серия паспорта должна состоять из 4 цифр.");
                return;
            }

            if (TbNumberPass.Text.Length < 6 || TbNumberPass.Text.Length > 6)
            {
                MessageBox.Show("Номер паспорта должна состоять из 6 цифр.");
                return;
            }
            if (CMBGender.SelectedItem is null)
            {
                MessageBox.Show("Вы не указали пол.");
                return;
            }

            if (isEdit == true)
            {
                // Изменение услуги
                editClient.LName = TbLName.Text;
                editClient.FName = TbFName.Text;
                editClient.Patronymic = TbPatronymic.Text;
                editClient.Email = TbEmail.Text;
                editClient.Phone = TbPhone.Text;
                editClient.Birthday = DPbDateOfBirth.SelectedDate.Value;
                editClient.PassportSeries = TbSeriesPass.Text;
                editClient.PassportNumber = TbNumberPass.Text;
                editClient.IdGender = (CMBGender.SelectedItem as Gender).Id;

                if (pathImage != null)
                {
                    editClient.PhotoClient = File.ReadAllBytes(pathImage);
                }
                EFClass.context.SaveChanges();

            }
            else
            {
                // Добавление клиента
                Client client = new Client();


                client.LName = TbLName.Text;
                client.FName = TbFName.Text;
                client.Patronymic = TbPatronymic.Text;
                client.Email = TbEmail.Text;
                client.Phone = TbPhone.Text;
                client.Birthday = DPbDateOfBirth.SelectedDate.Value;
                client.PassportSeries = TbSeriesPass.Text;
                client.PassportNumber = TbNumberPass.Text;
                client.IdGender = (CMBGender.SelectedItem as Gender).Id;

                if (pathImage != null)
                {
                    client.PhotoClient = File.ReadAllBytes(pathImage);
                }


                ClassHelper.EFClass.context.Client.Add(client);

                ClassHelper.EFClass.context.SaveChanges();
            }

           

            // Закрытие окна
            this.Close();
        }

        private void TbLName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnAddPhotoClient_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgClient.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathImage = openFileDialog.FileName;
            }
        }
    }
}
