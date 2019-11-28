using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for frmFirstTimeUpdatePassword.xaml
    /// </summary>
    public partial class FrmFirstTimeUpdatePassword : Window
    {
        private Employee _user = null;
        private IUserManager _userManager = null;
        private bool _forgotPassword = false;
        private string _email = null;

        public FrmFirstTimeUpdatePassword(Employee user, IUserManager userManager)
        {
            InitializeComponent();
            _user = user;
            _userManager = userManager;
        }

        public FrmFirstTimeUpdatePassword(string email, IUserManager userManager, bool forgotPassword)
        {
            InitializeComponent();
            _email = email;
            _userManager = userManager;
            _forgotPassword = forgotPassword;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!_forgotPassword)
            {
                if (PwdCurrentPassword.Password.Length < 7 || PwdCurrentPassword.Password != "newuser")
                {
                    MessageBox.Show("Current Password is incorrect, try again");
                    PwdCurrentPassword.Password = "";
                    PwdCurrentPassword.Focus();
                    return;
                }
                if (PwdNewPassword.Password.Length < 7 || PwdNewPassword.Password == PwdCurrentPassword.Password)
                {
                    MessageBox.Show("New password is incorrect, try again");
                    PwdNewPassword.Password = "";
                    PwdNewPassword.Focus();
                    return;
                }
                if (PwdRetypePassword.Password != PwdNewPassword.Password)
                {
                    MessageBox.Show("New password and Retype must match, try agin");
                    PwdNewPassword.Password = "";
                    PwdRetypePassword.Password = "";
                    PwdNewPassword.Focus();
                    return;
                }

                try
                {
                    bool updated = _userManager.UpdatePassword(_user.EmployeeId, PwdCurrentPassword.Password.ToString(), PwdNewPassword.Password.ToString());
                    if (updated)
                    {
                        MessageBox.Show("Password Updated");
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    this.DialogResult = false;
                    throw;
                }
            }
            else
            {
                if (PwdNewPassword.Password.Length < 7 || PwdNewPassword.Password == PwdCurrentPassword.Password)
                {
                    MessageBox.Show("New password is incorrect, try again");
                    PwdNewPassword.Password = "";
                    PwdNewPassword.Focus();
                    return;
                }
                if (PwdRetypePassword.Password != PwdNewPassword.Password)
                {
                    MessageBox.Show("New password and Retype must match, try agin");
                    PwdNewPassword.Password = "";
                    PwdRetypePassword.Password = "";
                    PwdNewPassword.Focus();
                    return;
                }

                try
                {
                    bool updated = _userManager.UpdatePassword(_email,  PwdNewPassword.Password.ToString());
                    if (updated)
                    {
                        MessageBox.Show("Password Updated");
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    this.DialogResult = false;
                    throw;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_forgotPassword)
            {
                CurrentPassword.Visibility = Visibility.Hidden;
                PwdCurrentPassword.Visibility = Visibility.Hidden;
            }
        }
    }
}
