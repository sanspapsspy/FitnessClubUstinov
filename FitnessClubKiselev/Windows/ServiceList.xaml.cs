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
    /// Логика взаимодействия для ServiceList.xaml
    /// </summary>
    public partial class ServiceList : Window
    {
        List<string> SortList = new List<string>()
        {
                "По умолчанию",
                "По названию (А-Я)",
                "По названию (Я-А)",
                "По Цене(Возрастание)",
                "По Цене(Убывание)",
                "По Описанию (А-Я)",
                "По Описанию (Я-А)"

        };
        public ServiceList()
        {
            InitializeComponent();
            GetServiceList();
            CMBTypeSearch.ItemsSource = SortList;
            CMBTypeSearch.SelectedIndex = 0;
        }

        private void GetServiceList()
        {
            List<Service> serviceList = new List<Service>();

            serviceList = EFClass.context.Service.ToList();

            serviceList = serviceList.Where(z => z.NameService.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            
            switch (CMBTypeSearch.SelectedIndex)
            {
                case 0:
                    serviceList = serviceList.OrderBy(z => z.Id).ToList();
                    break;
                case 1:
                    serviceList = serviceList.OrderBy(z => z.NameService).ToList();
                    break;
                case 2:
                    serviceList = serviceList.OrderByDescending(z => z.NameService).ToList();
                    break;
                case 3:
                    serviceList = serviceList.OrderBy(z => z.Price).ToList();
                    break;
                case 4:
                    serviceList = serviceList.OrderByDescending(z => z.Price).ToList();
                    break;
                case 5:
                    serviceList = serviceList.OrderBy(z => z.Description).ToList();
                    break;
                case 6:
                    serviceList = serviceList.OrderByDescending(z => z.Description).ToList();
                    break;
                default:
                    serviceList = serviceList.OrderBy(i => i.Id).ToList();
                    break;
            }

                lvService.ItemsSource = serviceList;

        }

        private void BtnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            // Изменение услуги

            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var service = button.DataContext as Service;

            // Открытие окна изменения услуги 
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow(service);
            addEditServiceWindow.ShowDialog();

            // Обновить список
            GetServiceList();
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {

            // Открытие окна добавление услуги
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow();
            addEditServiceWindow.ShowDialog();

            // Обновить список
            GetServiceList();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetServiceList();
        }

        private void CMBTypeSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServiceList();
        }
    }
}
