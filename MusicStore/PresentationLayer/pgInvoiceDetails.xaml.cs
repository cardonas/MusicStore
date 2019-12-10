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
    /// Interaction logic for pgInvoiceDetails.xaml
    /// </summary>
    public partial class pgInvoiceDetails : Page
    {
        public pgInvoiceDetails()
        {
            InitializeComponent();
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new pgCustomerDetails(true, true));
        }
    }
}
