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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for frmFirstTimeUpdatePassword.xaml
    /// </summary>
    public partial class frmFirstTimeUpdatePassword : Window
    {
        private Employee _user = null;
        private IUserManager _userManager = null;
        public frmFirstTimeUpdatePassword(Employee user, IUserManager userManager)
        {
            InitializeComponent();
            _user = user;
            _userManager = userManager;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (pwdCurrentPassword.Password.Length < 7 || pwdCurrentPassword.Password != "newuser")
            {
                MessageBox.Show("Current Password is incorrect, try again");
                pwdCurrentPassword.Password = "";
                pwdCurrentPassword.Focus();
                return;
            }
            if(pwdNewPassword.Password.Length < 7 || pwdNewPassword.Password == pwdCurrentPassword.Password)
            {
                MessageBox.Show("New password is incorrect, try again");
                pwdNewPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }
            if(pwdRetypePassword.Password != pwdNewPassword.Password)
            {
                MessageBox.Show("New password and Retype must match, try agin");
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            try
            {
                bool updated = _userManager.UpdatePassword(_user.EmployeeID, pwdCurrentPassword.Password.ToString(), pwdNewPassword.Password.ToString());
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
}
