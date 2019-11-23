using DataObjects;
using LogicLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private Employee _user = null;
        private IUserManager _userManager;

        public Login()
        {
            InitializeComponent();
            _userManager = new UserManager();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = pwdPassword.Password;

            if (email.Length < 7 || password.Length < 7)
            {
                MessageBox.Show("Bad username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Text = "";
                pwdPassword.Password = "";
                txtEmail.Focus();
                return;
            }

            try
            {
                _user = _userManager.AuthenticateUser(email, password);

                string roles = "";
                for (int i = 0; i < _user.Roles.Count; i++)
                {
                    roles += _user.Roles[i];
                    if (i < _user.Roles.Count - 1)
                    {
                        roles += ", ";
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bad username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Text = "";
                pwdPassword.Password = "";
                txtEmail.Focus();
                return;
            }
            if (_user != null)
            {
                if (password == "newuser")
                {
                    var updatePassword = new frmFirstTimeUpdatePassword(_user, _userManager);
                    if (updatePassword.ShowDialog() == false)
                    {
                        return;
                    }
                }
                MainPage main = new MainPage(_user);
                this.NavigationService.Navigate(main);
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text != ""){
                var updatePassword = new frmFirstTimeUpdatePassword(txtEmail.Text, _userManager, true);
                if (updatePassword.ShowDialog() == false)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("You must enter you email before pressing \"Forgot Password\"", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
