using FitnessClubKiselev.DB;
using System;
using System.Collections.Generic;
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
        public RegWindow()
        {
            InitializeComponent();
            CMBGender.ItemsSource = ClassHelper.EFClass.context.Gender.ToList();
            CMBGender.DisplayMemberPath = "NameGender";
            CMBGender.SelectedIndex = 0;
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

            ClassHelper.EFClass.context.Client.Add(client);

            ClassHelper.EFClass.context.SaveChanges();

            // Закрытие окна
            this.Close();
        }
    }
}
