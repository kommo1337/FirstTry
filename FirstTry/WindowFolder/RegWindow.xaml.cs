using FirstTry.DataFolder;
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
            var user = DBEntities.GetContext().User.SingleOrDefault(u => u.Login == LoginTb.Text);

            if (RepeatPasswordTb.Password == PasswordTb.Password)
            {
                if (IsStrongPassword(PasswordTb.Password))
                {

                    if (user == null)
                    {
                        try
                        {
                            var newUser = new User();
                            {

                                newUser.Login = LoginTb.Text;
                                newUser.Password = PasswordTb.Password;
                                newUser.IdRole = 1;
                            }
                            DBEntities.GetContext().User.Add(newUser);
                            DBEntities.GetContext().SaveChanges();
                            MessageBox.Show("Успех");

                        }
                        catch (System.Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь существует");

                    }
                }
                else
                {
                    MessageBox.Show("Пароль слишком слабый");
                }

            }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }

        }


        static bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
                return false;

            
            string special = @"!@#$%^&*()_+-=[]{}|;:'"",.<>?/`~";

            bool upper = password.Any(char.IsUpper);
            bool lower = password.Any(char.IsLower);
            bool digit = password.Any(char.IsDigit);
            bool specChar = password.Any(c => special.Contains(c));
            
            return upper && lower && digit && specChar;
            
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
           new AutorisationWindow().Show();
            Close();
        }
    }
}
