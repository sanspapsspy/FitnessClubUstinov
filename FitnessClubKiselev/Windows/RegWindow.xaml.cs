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
            this.Close();
        }
    }
}
