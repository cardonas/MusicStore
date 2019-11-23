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
        private bool _editMode = false;
        private bool _addMode = false;

        public pgUserProfile(IUserManager userManager, bool addMode = false)
        {
            InitializeComponent();
            _userManager = userManager;
            _addMode = addMode;
        }

        public pgUserProfile(IUserManager userManager, Employee employee, bool editMode = false)
        {
            InitializeComponent();
            _userManager = userManager;
            _employee = employee;
            _editMode = editMode;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_editMode)
            {
                setEditMode();
                LoadEmployee();
            }
            if (!_editMode && null != _employee)
            {
                LoadEmployee();
            }
            if (_addMode)
            {
                addMode();
            }
        }

        private void SaveNewEmployee()
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
                    this.NavigationService.Navigate(new pgAdministration());
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }

        public void SaveUpdatedEmployee()
        {
            Employee oldEmployee = _employee;
            //txtFirstName.Text = _employee.FirstName;
            //txtLastName.Text = _employee.LastName;
            //txtAddress.Text = "";
            //txtState.Text = "";
            //txtZipcode.Text = "";
            //txtPhoneNumber.Text = _employee.PhoneNumber;


            Employee updatedEmployee = new Employee
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Email = oldEmployee.Email
            };


            try
            {
                if (_userManager.UpdateEmployeeInfo(oldEmployee, updatedEmployee))
                {
                    MessageBox.Show("Employee Profile Updated", "Success", MessageBoxButton.OK);
                    NavigationService.Navigate(new pgAdministration());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                this.NavigationService.Navigate(new pgAdministration());
            }


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            setEditMode();
            _editMode = true;
        }

        private void setEditMode()
        {
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            txtAddress.IsEnabled = true;
            txtState.IsEnabled = true;
            txtZipcode.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            lstAssignedRoles.IsEnabled = true;
            lstUnassignedRoles.IsEnabled = true;
            btnAssign.IsEnabled = true;
            btnUnassign.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
        }

        private void addMode()
        {
            lblUserTypeID.Content = "Add New Employee";
            lblUserTypeID.HorizontalAlignment = HorizontalAlignment.Center;
            lblUserTypeID.SetValue(Grid.ColumnSpanProperty, 3);
            lblUserID.Visibility = Visibility.Hidden;
            setEditMode();
        }

        private void LoadEmployee()
        {
            txtFirstName.Text = _employee.FirstName;
            txtLastName.Text = _employee.LastName;
            txtAddress.Text = "";
            txtState.Text = "";
            txtZipcode.Text = "";
            txtPhoneNumber.Text = _employee.PhoneNumber;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgAdministration());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_addMode)
            {
                SaveNewEmployee();
            }
            if (_editMode)
            {
                SaveUpdatedEmployee();
            }
        }
    }
}
