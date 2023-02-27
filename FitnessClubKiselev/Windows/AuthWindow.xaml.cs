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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            // Авторизация
            
            // Получиили список пользователей
            var authUser = ClassHelper.EFClass.context.User.ToList()
                // Выбрали пользователя по условию
                .Where(i => i.Login == TbLogin.Text && i.Password == TbPassword.Text).FirstOrDefault();

            if(authUser != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неравильно введён логин или пароль");
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Открытие окна регистрации
            RegWindow regWindow = new RegWindow();
            regWindow.ShowDialog();
        }
    }
}
