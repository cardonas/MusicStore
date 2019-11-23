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

        private void dgInventoryList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_inactiveInstrument == false)
                {
                    dgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument();
                    dgInventoryList.Columns.RemoveAt(5);
                    dgInventoryList.Columns[0].Header = "InstrumentID";
                    dgInventoryList.Columns[1].Header = "Instrument Type";
                    dgInventoryList.Columns[2].Header = "Instrument Group";
                    dgInventoryList.Columns[3].Header = "Instrument Status";
                    dgInventoryList.Columns[4].Header = "Brand";
                    dgInventoryList.Columns[5].Header = "Price";
                    dgInventoryList.Columns[6].Width = 150;
                }
                if (_inactiveInstrument == true)
                {
                    dgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument(false);
                    dgInventoryList.Columns.RemoveAt(4);
                    dgInventoryList.Columns[0].Header = "InstrumentID";
                    dgInventoryList.Columns[1].Header = "Instrument Type";
                    dgInventoryList.Columns[2].Header = "Instrument Status";
                    dgInventoryList.Columns[3].Header = "Brand";
                    dgInventoryList.Columns[4].Header = "Price";
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
    }
}
