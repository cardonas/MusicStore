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
    /// Interaction logic for pgInventory.xaml
    /// </summary>
    public partial class pgInventory : Page
    {
        //private bool _addMode = false;
        //private bool _updateMode = false;
        private IInstrumentManager _instrumentManager;
        private List<Instrument> _cart = new List<Instrument>();

        public pgInventory()
        {
            InitializeComponent();
            _instrumentManager = new InstrumentManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           cmbStatus.ItemsSource = getGetAllInsrumentStatuses();
        }

        private InstrumentVM getSelectedInstrument()
        {
            return (InstrumentVM)dgInventoryList.SelectedItem;
        }


        private void dgInventoryList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInventoryList.ItemsSource == null)
                {
                    dgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument();
                    dgInventoryList.Columns.RemoveAt(9);
                    dgInventoryList.Columns.RemoveAt(1);
                    dgInventoryList.Columns[0].Header = "Rental Term";
                    dgInventoryList.Columns[1].Header = "Rental Fee";
                    dgInventoryList.Columns[2].Header = "Prep LIst";
                    dgInventoryList.Columns[3].Header = "InstrumentID";
                    dgInventoryList.Columns[4].Header = "Type";
                    dgInventoryList.Columns[5].Header = "Section";
                    dgInventoryList.Columns[6].Header = "Status";
                    dgInventoryList.Columns[7].Header = "Brand";
                    dgInventoryList.Columns[8].Header = "Price";
                    dgInventoryList.Columns[2].Width = 175;
                    dgInventoryList.Columns[9].Width = 100;
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

        private void dgInventoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pgInstrumentDetail details = new pgInstrumentDetail(getSelectedInstrument(), _instrumentManager);
            this.NavigationService.Navigate(details);
        }

        private void refreshListByStatus(string status)
        {
            dgInventoryList.ItemsSource = _instrumentManager.GetInstrumentsByStatus(status);
            dgInventoryList.Columns.RemoveAt(9);
            dgInventoryList.Columns.RemoveAt(1);
            dgInventoryList.Columns[0].Header = "Rental Term";
            dgInventoryList.Columns[1].Header = "Rental Fee";
            dgInventoryList.Columns[2].Header = "Prep LIst";
            dgInventoryList.Columns[3].Header = "InstrumentID";
            dgInventoryList.Columns[4].Header = "Type";
            dgInventoryList.Columns[5].Header = "Section";
            dgInventoryList.Columns[6].Header = "Status";
            dgInventoryList.Columns[7].Header = "Brand";
            dgInventoryList.Columns[8].Header = "Price";
            dgInventoryList.Columns[1].Width = 100;
            dgInventoryList.Columns[8].Width = 100;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (getSelectedInstrument() != null)
            {
                pgInstrumentDetail details = new pgInstrumentDetail(getSelectedInstrument(), _instrumentManager, editMode: true);
                this.NavigationService.Navigate(details);
            }
            else
            {
                MessageBox.Show("Please select an Instrument to edit", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            pgInstrumentDetail details = new pgInstrumentDetail(_instrumentManager, addMode: true);
            this.NavigationService.Navigate(details);
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            Instrument instrument = (Instrument)dgInventoryList.SelectedItem;

            //switch (instrument.InstrumentStatusID)
            //{
            //    case "For Sale":
            //        instrument.InstrumentStatusID = "Sold";
            //        break;
            //    case "For Rent":
            //        instrument.InstrumentStatusID = "Rented";
            //        break;
            //    case "For Rent to Own":
            //        instrument.InstrumentStatusID = "RentToOwn";
            //        break;
            //    case "Available":
            //        instrument.InstrumentStatusID = "Sold";
            //        break;
            //}

            _cart.Add(instrument);

            //try
            //{
            //    _instrumentManager.AddInstrumentToCart(instrument);
            //    refreshInstrumentList();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            //}

        }

        private List<string> getGetAllInsrumentStatuses()
        {
            List<string> statuses = new List<string>();
            try
            {
                statuses = _instrumentManager.GetAllInstrumentStatusIDs();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            return statuses;
        }

        private void refreshList()
        {
            dgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument();
            dgInventoryList.Columns.RemoveAt(9);
            dgInventoryList.Columns.RemoveAt(1);
            dgInventoryList.Columns[0].Header = "Rental Term";
            dgInventoryList.Columns[1].Header = "Rental Fee";
            dgInventoryList.Columns[2].Header = "Prep LIst";
            dgInventoryList.Columns[3].Header = "InstrumentID";
            dgInventoryList.Columns[4].Header = "Type";
            dgInventoryList.Columns[5].Header = "Section";
            dgInventoryList.Columns[6].Header = "Status";
            dgInventoryList.Columns[7].Header = "Brand";
            dgInventoryList.Columns[8].Header = "Price";
            dgInventoryList.Columns[1].Width = 100;
            dgInventoryList.Columns[8].Width = 100;
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedItem.ToString() != "All") {
                refreshListByStatus(cmbStatus.SelectedItem.ToString());
            }
            if(cmbStatus.SelectedItem.ToString() == "All")
            {
                refreshList();
            }
        }
    }
}
