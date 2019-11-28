using DataObjects;
using LogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;

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
            var email = TxtEmail.Text;
            var password = PwdPassword.Password;

            if (email.Length < 7 || password.Length < 7)
            {
                MessageBox.Show("Bad username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtEmail.Text = "";
                PwdPassword.Password = "";
                TxtEmail.Focus();
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
                TxtEmail.Text = "";
                PwdPassword.Password = "";
                TxtEmail.Focus();
                return;
            }
            if (_user != null)
            {
                if (password == "newuser")
                {
                    var updatePassword = new FrmFirstTimeUpdatePassword(_user, _userManager);
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
            if(TxtEmail.Text != ""){
                var updatePassword = new FrmFirstTimeUpdatePassword(TxtEmail.Text, _userManager, true);
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
