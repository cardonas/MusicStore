using System.Windows;
using System.Windows.Controls;
using DataObjects;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgCustomerDetails.xaml
    /// </summary>
    public partial class pgCustomerDetails : Page
    {
        private readonly Customer _customer;
        private bool _addMode;
        private bool _editMode;

        public pgCustomerDetails()
        {
            InitializeComponent();
        }

        public pgCustomerDetails(Customer customer, bool editMode = false)
        {
            InitializeComponent();
            _customer = customer;
            _editMode = editMode;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_customer != null)
            {
                AddCustomerInfo();
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
    }
}
