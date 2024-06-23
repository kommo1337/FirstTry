using FirstTry.DataFolder;
using System;
using System.Linq;
using System.Windows;

namespace FirstTry.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void AutoBTN_Click(object sender, RoutedEventArgs e)
        {


            var exUser = DBEntities.GetContext().User.SingleOrDefault(u => u.Login == LoginTb.Text);

            if (PasswordTb.Password != RepeatPasswordTb.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (!IsStrongPassword(PasswordTb.Password))
            {
                MessageBox.Show("Пароль слишком слабый");
                return;
            }

            if (exUser != null)
            {
                MessageBox.Show("Пользователь существует");
                return;
            }

            try
            {
                DBEntities.GetContext().User.Add(new User
                {
                    Login = LoginTb.Text,
                    Password = PasswordTb.Password,
                    IdRole = 1
                });
                DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static bool IsStrongPassword(string password)
        {
            return password.Length >= 8
                && password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && password.IndexOfAny("!@#$%^&*()_+-=[]{}|;:'\",.<>?/`~".ToCharArray()) != -1;
        }


        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            new AutorisationWindow().Show();
            Close();
        }
    }
}
