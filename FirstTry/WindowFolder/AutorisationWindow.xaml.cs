using FirstTry.CLassFolder;
using FirstTry.DataFolder;
using System.Linq;
using System.Windows;

namespace FirstTry.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AutorisationWindow.xaml
    /// </summary>
    public partial class AutorisationWindow : Window
    {
        public AutorisationWindow()
        {
            InitializeComponent();
        }

        private void AutoBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateUser(out User user))
                return;


            switch (user.IdRole)
            {
                case 1:
                    MessageBox.Show($"Добро пожаловать {user.Login}");
                    new ListUser().ShowDialog();
                    break;
                case 2:
                    MessageBox.Show($"Добро пожаловать {user.Login}");
                    new ListUser().ShowDialog();
                    break;
                case 3:
                    MessageBox.Show($"Добро пожаловать {user.Login}");
                    new ListUser().ShowDialog();
                    break;
            }
            Close();
        }

        bool ValidateUser(out User user)
        {
            user = DBEntities.GetContext().User.SingleOrDefault(u => u.Login == LoginTb.Text);

            if (user == null)
            {
                MBClass.ErrorMB("Пользователь не найден");
                return false;
            }

            if (user.Password != PasswordTb.Password)
            {
                MBClass.ErrorMB("Не правильный пароль");
                return false;
            }

            return true;
        }

        private void RegBTN_Click(object sender, RoutedEventArgs e)
        {
            new RegWindow().Show();
            Close();
        }
    }
}
