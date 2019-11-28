using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgUserProfile.xaml
    /// </summary>
    public partial class PgInstrumentDetail : Page
    {

        private readonly IInstrumentManager _instrumentManager;
        private readonly InstrumentVm _instrument;
        private bool _editMode;
        private readonly bool _addMode;

        public PgInstrumentDetail(IInstrumentManager instrumentManager, bool addMode = false)
        {
            InitializeComponent();
            _instrumentManager = instrumentManager;
            _addMode = addMode;
            _editMode = false;
        }

        public PgInstrumentDetail(InstrumentVm instrument, IInstrumentManager instrumentManager, bool editMode = false)
        {
            InitializeComponent();
            _instrumentManager = instrumentManager;
            _instrument = instrument;
            _editMode = false;
            _editMode = editMode;
        }

        private void LoadInstrument()
        {
            LblInstrument.Content = _instrument.InstrumentId;
            LblPriceAmount.Content = _instrument.PriceString;
            CmbType.Text = _instrument.InstrumentTypeId;
            CmbBrand.Text = _instrument.InstrumentBrandId;
            TxtFamily.Text = _instrument.InstrumentFamily;
            TxtRentalTerm.Text = _instrument.RentalTermId;
            TxtRentalFee.Text = _instrument.RentalFeeString;
            CmbStatus.Text = _instrument.InstrumentStatusId;
        }

        private void GetAllInstrumentTypes()
        {
            try
            {
                CmbType.ItemsSource = _instrumentManager.GetAllInstrumentTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void GetAllInstrumentBrands()
        {
            try
            {
                CmbBrand.ItemsSource = _instrumentManager.GetAllInstrumentBrands();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void GetAllInstrumentStatuses()
        {
            try
            {
                List<string> status = _instrumentManager.GetAllInstrumentStatusIDs();
                status.RemoveAt(0);
                CmbStatus.ItemsSource = status;
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
                Instrument instrument = new Instrument
                {
                    InstrumentId = TxtInstrumentId.Text,
                    InstrumentTypeId = CmbType.SelectedItem.ToString(),
                    InstrumentBrandId = CmbBrand.SelectedItem.ToString(),
                    Price = decimal.Parse(TxtPriceAmount.Text, NumberStyles.Currency)
                };

                if (_instrumentManager.AddInstrument(instrument))
                {
                    MessageBox.Show("Employee Added", "Success", MessageBoxButton.OK);
                }

                if (MessageBox.Show("Would you like add another instrument?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    instrument = new Instrument();
                    TxtFamily.Text = "";
                    TxtRentalTerm.Text = "";
                    TxtRentalFee.Text = "";
                    TxtPriceAmount.Text = "";
                    TxtInstrumentId.Text = "";
                    CmbType.SelectedItem = "";
                    CmbBrand.Text = "";
                    CmbStatus.Text = "Available";
                    TxtInstrumentId.Focus();
                }
                else
                {
                    this.NavigationService.Navigate(new PgInventory());
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

            Instrument updatedInstrument = new Instrument
            {
                InstrumentId = LblInstrumentId.Content.ToString(),
                InstrumentBrandId = CmbBrand.SelectedItem.ToString(),
                InstrumentStatusId = CmbStatus.SelectedItem.ToString(),
                InstrumentTypeId = CmbType.SelectedItem.ToString(),
                Price = Decimal.Parse(TxtPriceAmount.Text, NumberStyles.Currency)
            };


            try
            {
                if (_instrumentManager.UpdateInstrumentStatus(oldinstrument, updatedInstrument))
                {
                    MessageBox.Show("Instrument Status Updated", "Success", MessageBoxButton.OK);
                    if ((int)MessageBoxResult.OK == 1)
                    {
                        this.NavigationService.Navigate(new PgInventory());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                this.NavigationService.Navigate(new PgInventory());

            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PgInventory());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllInstrumentTypes();
            GetAllInstrumentBrands();
            GetAllInstrumentStatuses();
            if (_editMode)
            {
                SetEditMode();
                LoadInstrument();
                TxtPriceAmount.Text = _instrument.Price.ToString("c");
            }
            if (_instrument != null)
            {
                LoadInstrument();
            }
            if (_addMode)
            {
                AddMode();
            }
        }

        private void SetEditMode()
        {
            CmbStatus.IsEnabled = true;
            LblPriceAmount.Visibility = Visibility.Hidden;
            TxtPriceAmount.Visibility = Visibility.Visible;
            TxtPriceAmount.IsEnabled = true;
            BtnSave.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _editMode = true;
            SetEditMode();
            if (_instrument != null)
            {
                TxtPriceAmount.Text = _instrument.Price.ToString("c");
            }
        }

        private void AddMode()
        {
            CmbStatus.Text = "Available";
            LblInstrumentId.Visibility = Visibility.Hidden;
            TxtInstrumentId.Visibility = Visibility.Visible;
            TxtInstrumentId.IsEnabled = true;
            CmbType.IsEnabled = true;
            CmbBrand.IsEnabled = true;
            BtnSave.Visibility = Visibility.Visible;
            SetEditMode();
            CmbStatus.IsEnabled = false;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string instrumentType = CmbType.SelectedItem.ToString();
            InstrumentTypeVm instrumentTypeVm = null;
            try
            {
                instrumentTypeVm = _instrumentManager.GetInstrumentTypeByInstrumentTypeId(instrumentType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            TxtFamily.Text = instrumentTypeVm.InstrumentFamilyId;
            TxtRentalTerm.Text = instrumentTypeVm.RentalTermId;
            TxtRentalFee.Text = instrumentTypeVm.RentalFee.ToString("c");
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
