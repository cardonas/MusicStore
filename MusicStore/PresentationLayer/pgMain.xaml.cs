using DataObjects;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private Employee _user;

        public MainPage(Employee user)
        {
            InitializeComponent();
            _user = user;
            LblMessages.Content = "Hello, " + _user.FirstName;
        }

        private void btnAdministration_Click(object sender, RoutedEventArgs e)
        {
            TabFrame.Content = new PgAdministration();
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            TabFrame.Content = new PgInventory();
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            TabFrame.Content = new PgCart(_user);
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            TabFrame.Content = new pgCustomerList();
        }
    }
}

