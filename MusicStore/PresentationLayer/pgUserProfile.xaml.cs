using DataObjects;
using LogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgUserProfile.xaml
    /// </summary>
    public partial class PgUserProfile : Page
    {

        private IUserManager _userManager;
        private Employee _employee;
        private bool _editMode = false;
        private bool _addMode = false;

        public PgUserProfile(IUserManager userManager, bool addMode = false)
        {
            InitializeComponent();
            _userManager = userManager;
            _addMode = addMode;
        }

        public PgUserProfile(IUserManager userManager, Employee employee, bool editMode = false)
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
                SetEditMode();
                LoadEmployee();
            }
            if (!_editMode && null != _employee)
            {
                LoadEmployee();
            }
            if (_addMode)
            {
                AddMode();
            }
        }

        private void SaveNewEmployee()
        {
            try
            {
                Employee employee = new Employee();

                employee.FirstName = TxtFirstName.Text;
                employee.LastName = TxtLastName.Text;
                employee.PhoneNumber = TxtPhoneNumber.Text;

                if (_userManager.AddNewEmployee(employee))
                {
                    MessageBox.Show("Employee Added", "Success", MessageBoxButton.OK);
                }

                if (MessageBox.Show("Would you like add another employee?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TxtFirstName.Text = "";
                    TxtLastName.Text = "";
                    TxtAddress.Text = "";
                    TxtState.Text = "";
                    TxtZipcode.Text = "";
                    TxtPhoneNumber.Text = "";
                    TxtFirstName.Focus();
                }
                else
                {
                    this.NavigationService.Navigate(new PgAdministration());
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
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                PhoneNumber = TxtPhoneNumber.Text,
                Email = oldEmployee.Email
            };


            try
            {
                if (_userManager.UpdateEmployeeInfo(oldEmployee, updatedEmployee))
                {
                    MessageBox.Show("Employee Profile Updated", "Success", MessageBoxButton.OK);
                    NavigationService.Navigate(new PgAdministration());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                this.NavigationService.Navigate(new PgAdministration());
            }


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetEditMode();
            _editMode = true;
        }

        private void SetEditMode()
        {
            TxtFirstName.IsEnabled = true;
            TxtLastName.IsEnabled = true;
            TxtAddress.IsEnabled = true;
            TxtState.IsEnabled = true;
            TxtZipcode.IsEnabled = true;
            TxtPhoneNumber.IsEnabled = true;
            LstAssignedRoles.IsEnabled = true;
            LstUnassignedRoles.IsEnabled = true;
            BtnAssign.IsEnabled = true;
            BtnUnassign.IsEnabled = true;
            BtnSave.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Hidden;
        }

        private void AddMode()
        {
            LblUserTypeId.Content = "Add New Employee";
            LblUserTypeId.HorizontalAlignment = HorizontalAlignment.Center;
            LblUserTypeId.SetValue(Grid.ColumnSpanProperty, 3);
            LblUserId.Visibility = Visibility.Hidden;
            SetEditMode();
        }

        private void LoadEmployee()
        {
            TxtFirstName.Text = _employee.FirstName;
            TxtLastName.Text = _employee.LastName;
            TxtAddress.Text = "";
            TxtState.Text = "";
            TxtZipcode.Text = "";
            TxtPhoneNumber.Text = _employee.PhoneNumber;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PgAdministration());
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
