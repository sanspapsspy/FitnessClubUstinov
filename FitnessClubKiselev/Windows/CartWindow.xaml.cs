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
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            lvServiceCart.ItemsSource = ClassHelper.CartClass.serviceCart.ToList();
        }
        

        private void BtnDeleteCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;
            ClassHelper.CartClass.serviceCart.Remove(service);
            lvServiceCart.ItemsSource = ClassHelper.CartClass.serviceCart.ToList();

        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
