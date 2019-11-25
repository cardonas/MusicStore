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
        private bool _inactiveInstrument = false;

        public pgInventory()
        {
            InitializeComponent();
            _instrumentManager = new InstrumentManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Hidden;
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

        private void refreshInstrumentList(bool active = true)
        {
            dgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument(active);
            dgInventoryList.Columns.RemoveAt(9);
            dgInventoryList.Columns.RemoveAt(1);
            dgInventoryList.Columns[2].Width = 175;
            dgInventoryList.Columns[9].Width = 100;
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

    }
}
