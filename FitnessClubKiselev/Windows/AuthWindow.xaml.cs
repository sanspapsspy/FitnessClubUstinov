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
                // Сохраняем пользователя
                ClassHelper.UserClass.AuthUser = authUser;

                // Переход в окно MainWindow (IdRole == 1 - клиент)
                if (authUser.IdRole == 1)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                // Переход в окно Тренера (IdRole == 2 - Тренер)
                else if (authUser.IdRole == 2) 
                {
                    CoachMainWindow coachMainWindow = new CoachMainWindow();
                    coachMainWindow.Show();
                    Close();
                }
                // Переход в окно Админа (IdRole == 3 - Администратор)
                else if (authUser.IdRole == 3)
                {
                    AdminMainWindow adminMainWindow = new AdminMainWindow();
                    adminMainWindow.Show();
                    Close();
                }
                // Переход в окно Директора (IdRole == 4 - Директор)
                else if (authUser.IdRole == 4)
                {
                    DirectorMainWindow directorMainWindow = new DirectorMainWindow();
                    directorMainWindow.Show();
                    Close();
                }
                
            }
            // Если пароль или логин введены неправильно, то выдаст соответствующее сообщение
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
