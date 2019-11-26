using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for pgUserProfile.xaml
    /// </summary>
    public partial class pgInstrumentDetail : Page
    {

        private IInstrumentManager _instrumentManager;
        private InstrumentVM _instrument;
        private bool _editMode = false;
        private bool _addMode = false;

        public pgInstrumentDetail(IInstrumentManager instrumentManager, bool addMode = false)
        {
            InitializeComponent();
            _instrumentManager = instrumentManager;
            _addMode = addMode;
        }

        public pgInstrumentDetail(InstrumentVM instrument, IInstrumentManager instrumentManager, bool editMode = false)
        {
            InitializeComponent();
            _instrumentManager = instrumentManager;
            _instrument = instrument;
            _editMode = editMode;
        }

        private void loadInstrument()
        {
            lblInstrumentID.Content = _instrument.InstrumentID;
            lblPriceAmount.Content = _instrument.PriceString;
            cmbType.Text = _instrument.InstrumentTypeID;
            cmbBrand.Text = _instrument.InstrumentBrandID;
            txtFamily.Text = _instrument.InstrumentFamily;
            txtRentalTerm.Text = _instrument.RentalTermID;
            txtRentalFee.Text = _instrument.RentalFeeString;
            cmbStatus.Text = _instrument.InstrumentStatusID;
        }

        private void getAllInstrumentTypes()
        {
            try
            {
                cmbType.ItemsSource = _instrumentManager.GetAllInstrumentTypes();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void getAllInstrumentFamilies()
        //{
        //    try
        //    {
        //        cmbBrand.ItemsSource = _instrumentManager.GetAllInstrumentFamilies();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void getAllInstrumentBrands()
        {
            try
            {
                cmbBrand.ItemsSource = _instrumentManager.GetAllInstrumentBrands();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n"  + ex.InnerException.Message);
            }
        }

        private void getAllInstrumentStatuses()
        {
            try
            {
                cmbStatus.ItemsSource = _instrumentManager.GetAllInstrumentStatusIDs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        public void SaveNewInstrument()
        {
            try
            {
                Instrument instrument = new Instrument();

                instrument.InstrumentID = txtInstrumentID.Text;
                instrument.InstrumentTypeID = cmbType.SelectedItem.ToString();
                instrument.InstrumentBrandID = cmbBrand.SelectedItem.ToString();
                instrument.Price = decimal.Parse(txtPriceAmount.Text, NumberStyles.Currency);

                if (_instrumentManager.AddInstrument(instrument))
                {
                    MessageBox.Show("Employee Added", "Success", MessageBoxButton.OK);
                }

                if (MessageBox.Show("Would you like add another instrument?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    instrument = new Instrument();
                    txtFamily.Text = "";
                    txtRentalTerm.Text = "";
                    txtRentalFee.Text = "";
                    txtPriceAmount.Text = "";
                    txtInstrumentID.Text = "";
                    cmbType.SelectedItem = "";
                    cmbBrand.Text = "";
                    cmbStatus.Text = "Available";
                    txtInstrumentID.Focus();
                }
                else
                {
                    this.NavigationService.Navigate(new pgInventory());
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }

        public void SaveUpdatedInstrument()
        {
            Instrument oldinstrument = _instrument;

            Instrument updatedInstrument = new Instrument();
            updatedInstrument.InstrumentID = lblInstrumentID.Content.ToString();
            updatedInstrument.InstrumentBrandID = cmbBrand.SelectedItem.ToString();
            updatedInstrument.InstrumentStatusID = cmbStatus.SelectedItem.ToString();
            updatedInstrument.InstrumentTypeID = cmbType.SelectedItem.ToString();
            updatedInstrument.Price = Decimal.Parse(txtPriceAmount.Text, NumberStyles.Currency);


            try
            {
                if (_instrumentManager.UpdateInstrumentStatus(oldinstrument, updatedInstrument))
                {
                    MessageBox.Show("Instrument Status Updated", "Success", MessageBoxButton.OK);
                    if ((int)MessageBoxResult.OK == 1)
                    {
                        this.NavigationService.Navigate(new pgInventory());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                this.NavigationService.Navigate(new pgInventory());

            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgInventory());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getAllInstrumentTypes();
            getAllInstrumentBrands();
            getAllInstrumentStatuses();
            if (_editMode)
            {
                setEditMode();
                loadInstrument();
                txtPriceAmount.Text = _instrument.Price.ToString("c");
            }
            if (_instrument != null)
            {
                loadInstrument(); 
            }
            if (_addMode)
            {
                addMode();
            }
        }

        private void setEditMode()
        {
            cmbStatus.IsEnabled = true;
            lblPriceAmount.Visibility = Visibility.Hidden;
            txtPriceAmount.Visibility = Visibility.Visible;
            txtPriceAmount.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {           
            _editMode = true;
            setEditMode();
            if (_instrument != null)
            {
                txtPriceAmount.Text = _instrument.Price.ToString("c");
            }
        }

        private void addMode()
        {
            cmbStatus.Text = "Available";
            lblInstrumentID.Visibility = Visibility.Hidden;
            txtInstrumentID.Visibility = Visibility.Visible;
            txtInstrumentID.IsEnabled = true;
            cmbType.IsEnabled = true;
            cmbBrand.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            setEditMode();
            cmbStatus.IsEnabled = false;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string instrumentType = cmbType.SelectedItem.ToString();
            InstrumentTypeVM instrumentTypeVM = null;
            try
            {
                instrumentTypeVM = _instrumentManager.GetInstrumentTypeByInstrumentTypeID(instrumentType);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            txtFamily.Text = instrumentTypeVM.InstrumentFamilyID;
            txtRentalTerm.Text = instrumentTypeVM.RentalTermID;
            txtRentalFee.Text = instrumentTypeVM.RentalFee.ToString("c");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_editMode)
            {
                SaveUpdatedInstrument();
            }
            if (_addMode)
            {
                SaveNewInstrument();
            }
        }
    }
}
