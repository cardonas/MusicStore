
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
    /// Interaction logic for pgAdministration.xaml
    /// </summary>
    public partial class pgAdministration : Page
    {
        private bool _addMode = false;
        public bool _updateMode = false;
        public Employee _employee;
        private EmployeeList _employeeList;
        private pgUserProfile _profile;
        private IUserManager _userManager;

        public pgAdministration()
        {
            InitializeComponent();
            _profile = new pgUserProfile();
            btnSave.Visibility = Visibility.Hidden;
            _userManager = new UserManager();
            _employeeList = new EmployeeList(_userManager, this, false);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frmAdmin.NavigationService.Navigate(_employeeList);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            _addMode = true;

            toggleButtons();

            _profile = new pgUserProfile();
            frmAdmin.NavigationService.Navigate(_profile);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _employee= _employeeList.SelectUserFromList();
            if (_employee != null)
            {
                _updateMode = true;
                _profile = new pgUserProfile(_employee);
                frmAdmin.NavigationService.Navigate(_profile);
                toggleButtons();

            }
            else
            {
                MessageBox.Show("Please Select An Employee To Edit", "No Employee Selected!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frmAdmin.NavigationService.Navigate(_employeeList);
            toggleButtons(true);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_updateMode == true)
            {
                _profile.SaveUpdatedEmployee(_employee);
                toggleButtons(true);
            }

            if (_addMode == true)
            {
                _profile.SaveNewEmployee();
                toggleButtons(true);
            }
        }

        // if toggleOn == true : hides back and save buttons shows add edit and delete buttons
        // if toggleOn == false : hides show, add, edit buttons and show back and save buttons
        // parameter defaults to false
        public void toggleButtons(bool toggleOn = false)
        {
            if (toggleOn == true)
            {
                btnBack.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            } else
            {
                btnSave.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = _employeeList.SelectUserFromList();
            if(employee != null)
            {
                if (chkInActive.IsChecked == false)
                {
                    if (employee.LastName != "System")
                    {
                        try
                        {
                            bool isDeactivated = _userManager.DeactivateEmployee(employee);
                            if (isDeactivated)
                            {
                                MessageBox.Show("You Successfully De-activated " + employee.FirstName, "Success", MessageBoxButton.OK);
                                _employeeList = new EmployeeList(_userManager, this, false);
                                frmAdmin.NavigationService.Navigate(_employeeList);
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The System Admin is not allowed to be deactivated.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if(chkInActive.IsChecked == true)
                {
                   var result = MessageBox.Show("This action will permanently delete the user! \n\n Do you wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = _userManager.DeleteEmployee(employee);
                            if (isDeleted)
                            {
                                MessageBox.Show("You Successfully Deleted " + employee.FirstName, "Success", MessageBoxButton.OK);
                                _employeeList = new EmployeeList(_userManager, this, true);
                                frmAdmin.NavigationService.Navigate(_employeeList);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select An Employee To De-activate", "No Employee Selected!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReActivate_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = _employeeList.SelectUserFromList();
            if (employee != null)
            {
                try
                {
                    
                        bool isDeactivated = _userManager.ReActivateEmployee(employee);
                        if (isDeactivated)
                        {
                            MessageBox.Show("You Successfully Re-activated " + employee.FirstName, "Success", MessageBoxButton.OK);
                            _employeeList = new EmployeeList(_userManager, this, false);
                            frmAdmin.NavigationService.Navigate(_employeeList);
                            chkInActive.IsChecked = false;
                        } 

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Select An Employee To Delete", "No Employee Selected!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void chkInActive_Checked(object sender, RoutedEventArgs e)
        {
            _employeeList = new EmployeeList(_userManager, this, true);
            frmAdmin.NavigationService.Navigate(_employeeList);
            btnReActivate.Visibility = Visibility.Visible;
            btnDelete.Content = "Delete";
        }

        private void chkInActive_Unchecked(object sender, RoutedEventArgs e)
        {
            _employeeList = new EmployeeList();
            frmAdmin.NavigationService.Navigate(_employeeList);
            btnReActivate.Visibility = Visibility.Hidden;
            btnDelete.Content = "De-activate";
        }
    }
}
