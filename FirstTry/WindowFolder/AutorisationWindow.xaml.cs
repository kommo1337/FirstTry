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
            var user = DBEntities.GetContext().User.SingleOrDefault(u => u.Login == LoginTb.Text);
            if (user != null)
            {
                if (user.Password == PasswordTb.Password)
                {
                    MessageBox.Show("Успех");
                    new AddImage().ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неудача password");
                }

            }
            else
            {
                MessageBox.Show("Login incorect");
            }
        }

        private void RegBTN_Click(object sender, RoutedEventArgs e)
        {
            new RegWindow().Show();
            Close();
        }
    }
}
