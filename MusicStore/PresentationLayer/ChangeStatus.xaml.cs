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
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ChangeStatus.xaml
    /// </summary>
    public partial class ChangeStatus : Window
    {
        private Instrument _instrument;
        private IInstrumentManager _instrumentManager;
        public ChangeStatus(Instrument instrument, IInstrumentManager instrumentManager)
        {
            InitializeComponent();
            _instrument = instrument;
            _instrumentManager = instrumentManager;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //string status = null;
            //if ((bool) RbRent.IsChecked)
            //{
            //    status = "For Rent";
            //}

            //if ((bool) RbRentToOwn.IsChecked)
            //{
            //    status = "For Rent to Own";
            //}
            //if ((bool)RbSale.IsChecked)
            //{
            //    status = "For Sale";
            //}

            //Instrument instrument = new Instrument
            //{
            //    InstrumentId = _instrument.InstrumentId,
            //    InstrumentStatusId = status,
            //    Price = _instrument.Price
            //};

            //try
            //{
            //    _instrumentManager.UpdateInstrumentStatus(_instrument, instrument);
                this.DialogResult = true;
                this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            //}


        }
    }
}
