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
    /// Interaction logic for pgUserProfile.xaml
    /// </summary>
    public partial class pgUserProfile : Page
    {

        private IUserManager _userManager;
        private Employee _employee;

        public pgUserProfile()
        {
            InitializeComponent();
            _userManager = new UserManager();
            lblUserTypeID.Content = "Add New Employee";
            lblUserTypeID.HorizontalAlignment = HorizontalAlignment.Center;
            lblUserTypeID.SetValue(Grid.ColumnSpanProperty, 3);
            lblUserID.Visibility = Visibility.Hidden;
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            txtZipcode.Text = "";
            txtPhoneNumber.Text = "";
            txtFirstName.Focus();
        }

        public pgUserProfile(Employee employee)
        {
            InitializeComponent();
            _userManager = new UserManager();
            _employee = employee;
            lblUserID.Content = _employee.EmployeeID;
            txtFirstName.Text = _employee.FirstName;
            txtLastName.Text = _employee.LastName;
            txtAddress.Text = "";
            txtState.Text = "";
            txtZipcode.Text = "";
            txtPhoneNumber.Text = _employee.PhoneNumber;

        }

        public void SaveNewEmployee()
        {
            try
            {
                Employee employee = new Employee();

                employee.FirstName = txtFirstName.Text;
                employee.LastName = txtLastName.Text;
                employee.PhoneNumber = txtPhoneNumber.Text;

                if (_userManager.AddNewEmployee(employee))
                {
                    MessageBox.Show("Employee Added", "Success", MessageBoxButton.OK);
                }

                if (MessageBox.Show("Would you like add another employee?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    txtState.Text = "";
                    txtZipcode.Text = "";
                    txtPhoneNumber.Text = "";
                    txtFirstName.Focus();
                }
                else
                {
                    this.NavigationService.Navigate(new EmployeeList());
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }

        public void SaveUpdatedEmployee(Employee employee)
        {
            Employee oldEmployee = employee;
            //txtFirstName.Text = _employee.FirstName;
            //txtLastName.Text = _employee.LastName;
            //txtAddress.Text = "";
            //txtState.Text = "";
            //txtZipcode.Text = "";
            //txtPhoneNumber.Text = _employee.PhoneNumber;


            Employee updatedEmployee = new Employee
            {
                FirstName = txtFirstName.Text.ToString(),
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Email = oldEmployee.Email
            };


            try
            {
                if (_userManager.UpdateEmployeeInfo(oldEmployee, updatedEmployee))
                {
                    MessageBox.Show("Employee Profile Updated", "Success", MessageBoxButton.OK);
                    if ((int)MessageBoxResult.OK == 1)
                    {
                        NavigationService.Navigate(new EmployeeList());
                    }
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    this.NavigationService.Navigate(new EmployeeList());
            }


        }
    }
}
