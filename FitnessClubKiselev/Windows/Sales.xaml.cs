using FitnessClubKiselev.ClassHelper;
using FitnessClubKiselev.DB;
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

namespace FitnessClubKiselev.Windows
{
    /// <summary>
    /// Логика взаимодействия для Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
            GetClientList();

        }

        private void GetClientList()
        {
            List<Order> salesList = new List<Order>();

            salesList = EFClass.context.Order.ToList();


        }

        private void lvSales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
