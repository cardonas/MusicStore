using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgCustomerDetails.xaml
    /// </summary>
    public partial class pgCustomerDetails : Page
    {
        private readonly ICustomerManager _customerManager;
        private Customer _customer;
        private bool _addMode;
        private bool _editMode;
        private bool _fromCart;
        private Employee _user;

        public pgCustomerDetails(bool addMode = false, bool fromCart = false, Employee user = null)
        {
            InitializeComponent();
            _customerManager = new CustomerManager();
            _addMode = addMode;
            _fromCart = fromCart;
            _user = user;
        }


        public pgCustomerDetails(ICustomerManager customerManager, Customer customer, bool editMode = false)
        {
            InitializeComponent();
            _customerManager = customerManager;
            _customer = customer;
            _editMode = editMode;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_customer != null &&  _editMode)
            {
                AddCustomerInfo();
                SetEditMode();
            }
            if (_customer != null)
            {
                AddCustomerInfo();
            }

            if (_customer == null && _addMode)
            {
                SetEditMode();
                DgUserTransactions.IsEnabled = false;
            }
        }

        private void AddCustomerInfo()
        {
            LbCustomerId.Content = _customer.CustomerId;
            TxtFirstName.Text = _customer.FirstName;
            TxtLastName.Text = _customer.LastName;
            TxtEmail.Text = _customer.Email;
            TxtPhoneNumber.Text = _customer.PhoneNumber;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new pgCustomerList());
        }

        private void SetEditMode()
        {
            TxtFirstName.IsEnabled = true;
            TxtLastName.IsEnabled = true;
            TxtEmail.IsEnabled = true;
            TxtPhoneNumber.IsEnabled = true;
            BtnEdit.Visibility = Visibility.Collapsed;
            BtnSave.Visibility = Visibility.Visible;
            if (_addMode)
            {
                LblCustomer.Content = "Add New Customer";
                LbCustomerId.Visibility = Visibility.Hidden;
                LblCustomer.SetValue(Grid.ColumnSpanProperty, 3);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _editMode = true;
            SetEditMode();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_editMode)
            {
                Customer oldCustomer = _customer;

                Customer newCustomer = new Customer()
                {
                    Email = TxtEmail.Text,
                    FirstName = TxtFirstName.Text,
                    LastName = TxtLastName.Text,
                    PhoneNumber = TxtPhoneNumber.Value.ToString()
                };

                try
                {
                    if (_customerManager.EditCustomerDetails(oldCustomer, newCustomer))
                    {
                        MessageBox.Show("Update Successful", "Success", MessageBoxButton.OK);
                        this.NavigationService?.Navigate(new pgCustomerList());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                }
            }

            if (_addMode)
            {
                try
                {
                    if (_customerManager.AddCustomer(new Customer()
                    {
                        FirstName = TxtFirstName.Text,
                        LastName = TxtLastName.Text,
                        Email =  TxtEmail.Text,
                        PhoneNumber = TxtPhoneNumber.Value.ToString()
                    }))
                    {
                        MessageBox.Show("Customer Added", "Success", MessageBoxButton.OK);
                        if (_fromCart)
                        {
                            try
                            {
                                _customer = _customerManager.GetCustomerByEmail(TxtEmail.Text);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                            }
                        }
                    }

                    if (MessageBox.Show("Would you like add another Customer?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        TxtFirstName.Text = "";
                        TxtLastName.Text = "";
                        TxtEmail.Text = "";
                        TxtPhoneNumber.Text = "";
                        TxtFirstName.Focus();
                    }
                    else
                    {
                        if (_fromCart)
                        {
                            this.NavigationService?.Navigate(new PgCart(_user, _customer, true));
                        }
                        else
                        {
                            this.NavigationService?.Navigate(new pgCustomerList());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                }

            }
        }
    }
}
