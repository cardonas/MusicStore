
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
        public bool _updateMode = false;
        public Employee _employee;
        private pgUserProfile _profile;
        private IUserManager _userManager;
        private bool _inactiveUser = false;

        public pgAdministration()
        {
            InitializeComponent();
            _userManager = new UserManager();
        }

        private void dgEmployeeList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployeeList.ItemsSource == null)
                {
                    dgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _profile = new pgUserProfile(_userManager, addMode: true);
            this.NavigationService.Navigate(_profile);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _employee = SelectUserFromList();
            if (null != _employee)
            {
                _profile = new pgUserProfile(_userManager, _employee, editMode: true);
                this.NavigationService.Navigate(_profile);
            }
            else
            {
                MessageBox.Show("Please select an employee to edit", "Select An Employee", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = SelectUserFromList();
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
                                refreshEmployeeList(true);
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
                                refreshEmployeeList(false);
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
            Employee employee = SelectUserFromList();
            if (employee != null)
            {
                try
                {
                    
                        bool isDeactivated = _userManager.ReActivateEmployee(employee);
                        if (isDeactivated)
                        {
                            MessageBox.Show("You Successfully Re-activated " + employee.FirstName, "Success", MessageBoxButton.OK);
                            refreshEmployeeList(false);
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
            refreshEmployeeList(false);
            btnReActivate.Visibility = Visibility.Visible;
            btnDelete.Content = "Delete";
        }

        private void chkInActive_Unchecked(object sender, RoutedEventArgs e)
        {
            refreshEmployeeList(true);
            btnReActivate.Visibility = Visibility.Hidden;
            btnDelete.Content = "De-activate";
        }
        private Employee SelectUserFromList() => (Employee)dgEmployeeList.SelectedItem;

        private void dgEmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _profile = new pgUserProfile(_userManager, SelectUserFromList());
            this.NavigationService.Navigate(_profile);
        }

        private void refreshEmployeeList(bool active = true)
        {
            dgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive(active);
            dgEmployeeList.Columns.RemoveAt(5);
        }

    }
}
