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
using FitnessClubKiselev.DB;
using FitnessClubKiselev.Windows;
using FitnessClubKiselev.ClassHelper;

namespace FitnessClubKiselev.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Window
    {
        List<string> SortList = new List<string>()
        {
                "По умолчанию",
                "По Фамилии (А-Я)",
                "По Фамилии (Я-А)",
                "По Имени (А-Я)",
                "По Имени (А-Я)",
                "По Отчеству (А-Я)",
                "По Отчеству (Я-А)"

        };
        public ClientList()
        {
            InitializeComponent();
            GetClientList();
            CMBTypeSearchClient.ItemsSource = SortList;
            CMBTypeSearchClient.SelectedIndex = 0;
        }

        private void GetClientList()
        {
            List<Client> clientList = new List<Client>();

            clientList = EFClass.context.Client.ToList();

            clientList = clientList.Where(i => i.LName.ToLower().Contains(TbSearchClient.Text.ToLower())).ToList();

            switch (CMBTypeSearchClient.SelectedIndex)
            {
                case 0:
                    clientList = clientList.OrderBy(z => z.Id).ToList();
                    break;
                case 1:
                    clientList = clientList.OrderBy(z => z.LName).ToList();
                    break;
                case 2:
                    clientList = clientList.OrderByDescending(z => z.LName).ToList();
                    break;
                case 3:
                    clientList = clientList.OrderBy(z => z.FName).ToList();
                    break;
                case 4:
                    clientList = clientList.OrderByDescending(z => z.FName).ToList();
                    break;
                case 5:
                    clientList = clientList.OrderBy(z => z.Patronymic).ToList();
                    break;
                case 6:
                    clientList = clientList.OrderByDescending(z => z.Patronymic).ToList();
                    break;
                default:
                    clientList = clientList.OrderBy(i => i.Id).ToList();
                    break;
            }

            lvClient.ItemsSource = clientList;

        }
        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.ShowDialog();
        }

        private void CMBTypeSearchClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetClientList();
        }

        private void TbSearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetClientList();
        }
    }
}
