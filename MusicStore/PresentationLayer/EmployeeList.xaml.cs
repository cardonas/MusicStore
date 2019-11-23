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
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        private IUserManager _userManager;
        private bool _inactiveUser;
        private pgAdministration _pgAdmin;
        bool _updateMode = false;

        public EmployeeList()
        {
            InitializeComponent();
            _userManager = new UserManager();
        }

        public EmployeeList(IUserManager userManager, pgAdministration administration, bool inactive = false)
        {
            InitializeComponent();
            _userManager = userManager;
            _pgAdmin = administration; 
            _inactiveUser = inactive;
        }

        public Employee SelectUserFromList()
        {
            Employee employee = (Employee)dgEmployeeList.SelectedItem;
            return employee;
        }

        private void dgEmployeeList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_inactiveUser == false)
                {
                    dgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive();
                    dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[5]);
                    dgEmployeeList.Columns[0].Header = "Employee ID";
                    dgEmployeeList.Columns[1].Header = "First Name";
                    dgEmployeeList.Columns[2].Header = "Last Name";
                    dgEmployeeList.Columns[3].Header = "Phone Number";
                    dgEmployeeList.Columns[4].Header = "Email Address";
                }
                if(_inactiveUser == true)
                {
                    dgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive(false);
                    dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[5]);
                    dgEmployeeList.Columns[0].Header = "Employee ID";
                    dgEmployeeList.Columns[1].Header = "First Name";
                    dgEmployeeList.Columns[2].Header = "Last Name";
                    dgEmployeeList.Columns[3].Header = "Phone Number";
                    dgEmployeeList.Columns[4].Header = "Email Address";
                }
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
    }
}
