using FirstTry.CLassFolder;
using FirstTry.DataFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FirstTry.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        User _user;
        List<Role> _roles;

        public EditUser(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = this;
            LoadRole();
            LoadUser();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                UpdateUser();
                try
                {
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Успешно");
                    new ListUser().Show();
                    Close();
                }
                catch (Exception ex)
                {

                    MBClass.ErrorMB(ex);
                }
            }
        }

        void LoadRole()
        {
            _roles = DBEntities.GetContext().Role.ToList();
            RoleComboBox.ItemsSource = _roles;
        }

        void LoadUser()
        {
            LoginTextBox.Text = _user.Login;
            PasswordTextBox.Text = _user.Password;
            RoleComboBox.SelectedValue = _user.IdRole;
        }

        bool ValidateInput()
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text) ||
                RoleComboBox.SelectedItem == null)
            {
                MBClass.ErrorMB("Выбирите все поля");
                return false;
            }
            return true;
        }

        void UpdateUser()
        {
            _user.Login = LoginTextBox.Text;
            _user.Password = PasswordTextBox.Text;
            _user.IdRole = (int)RoleComboBox.SelectedValue;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            new ListUser().Show();
            Close();
        }
    }
}
