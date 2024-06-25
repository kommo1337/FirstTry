using FirstTry.CLassFolder;
using FirstTry.DataFolder;
using System;
using System.Linq;
using System.Windows;

namespace FirstTry.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для ListUser.xaml
    /// </summary>
    public partial class ListUser : Window
    {
        public ListUser()
        {
            InitializeComponent();
            UserDG.ItemsSource = DBEntities.GetContext().User.ToList().OrderBy(u => u.Login);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var users = DBEntities.GetContext().User.ToList();
            var searchText = SearchTb.Text.ToLower();

            var result = users.Where(u => u.Login.ToLower().Contains(searchText));

            UserDG.ItemsSource = result;
        }

        private void DeleteIt_Click(object sender, RoutedEventArgs e)
        {

            var selecteduser = UserDG.SelectedItem as User;

            if (selecteduser == null)
            {
                MBClass.ErrorMB("Выберите пользователя");
                return;
            }

            try
            {
                DBEntities.GetContext().User.Remove(selecteduser);
                DBEntities.GetContext().SaveChanges();
                UserDG.ItemsSource = DBEntities.GetContext().User.ToList().OrderBy(u => u.Login);
                MBClass.InfoMB("удалили пользователя");
            }
            catch (Exception ex)
            {

                MBClass.ErrorMB(ex);
            }
        }

        private void ModifyIt_Click(object sender, RoutedEventArgs e)
        {
            new EditUser(UserDG.SelectedItem as User).Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ImageAndAudio().Show();
        }
    }
}
