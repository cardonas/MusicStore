
using DataObjects;
using LogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgAdministration.xaml
    /// </summary>
    public partial class PgAdministration : Page
    {
        public Employee Employee;
        private PgUserProfile _profile;
        private IUserManager _userManager;

        public PgAdministration()
        {
            InitializeComponent();
            _userManager = new UserManager();
        }

        private void dgEmployeeList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgEmployeeList.ItemsSource == null)
                {
                    DgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive();
                    DgEmployeeList.Columns.Remove(DgEmployeeList.Columns[5]);
                    DgEmployeeList.Columns[0].Header = "Employee ID";
                    DgEmployeeList.Columns[1].Header = "First Name";
                    DgEmployeeList.Columns[2].Header = "Last Name";
                    DgEmployeeList.Columns[3].Header = "Phone Number";
                    DgEmployeeList.Columns[4].Header = "Email Address";
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
            _profile = new PgUserProfile(_userManager, addMode: true);
            this.NavigationService.Navigate(_profile);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Employee = SelectUserFromList();
            if (null != Employee)
            {
                _profile = new PgUserProfile(_userManager, Employee, editMode: true);
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
                if (ChkInActive.IsChecked == false)
                {
                    if (employee.LastName != "System")
                    {
                        try
                        {
                            bool isDeactivated = _userManager.DeactivateEmployee(employee);
                            if (isDeactivated)
                            {
                                MessageBox.Show("You Successfully De-activated " + employee.FirstName, "Success", MessageBoxButton.OK);
                                RefreshEmployeeList(true);
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
                if(ChkInActive.IsChecked == true)
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
                                RefreshEmployeeList(false);
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
                            RefreshEmployeeList(false);
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
            RefreshEmployeeList(false);
            BtnReActivate.Visibility = Visibility.Visible;
            BtnDelete.Content = "Delete";
        }

        private void chkInActive_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeList(true);
            BtnReActivate.Visibility = Visibility.Hidden;
            BtnDelete.Content = "De-activate";
        }
        private Employee SelectUserFromList() => (Employee)DgEmployeeList.SelectedItem;

        private void dgEmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _profile = new PgUserProfile(_userManager, SelectUserFromList());
            this.NavigationService.Navigate(_profile);
        }

        private void RefreshEmployeeList(bool active = true)
        {
            DgEmployeeList.ItemsSource = _userManager.GetEmployeesByActive(active);
            DgEmployeeList.Columns.RemoveAt(5);
        }

    }
}
