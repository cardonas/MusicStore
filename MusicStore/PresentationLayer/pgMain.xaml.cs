using DataObjects;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private Employee _user;

        public MainPage(Employee user)
        {
            InitializeComponent();
            _user = user;
            lblMessages.Content = "Hello, " + _user.FirstName;
        }

        private void btnAdministration_Click(object sender, RoutedEventArgs e)
        {
            tabFrame.Content = new pgAdministration();
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            tabFrame.Content = new pgInventory();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            tabFrame.Content = new pgCart();
        }
    }
}

