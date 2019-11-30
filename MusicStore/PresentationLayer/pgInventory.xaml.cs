using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgInventory.xaml
    /// </summary>
    public partial class PgInventory
    {
        //private bool _addMode = false;
        //private bool _updateMode = false;
        private readonly IInstrumentManager _instrumentManager;
        private readonly ICartManager _cartManager;

        public PgInventory()
        {
            InitializeComponent();
            _instrumentManager = new InstrumentManager();
            _cartManager = new CartManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CmbStatus.ItemsSource = GetGetAllInstrumentStatuses();
            CmbStatus.SelectedIndex = 0;
        }

        private InstrumentVm GetSelectedInstrument()
        {
            return (InstrumentVm)DgInventoryList.SelectedItem;
        }


        private void dgInventoryList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgInventoryList.ItemsSource == null)
                {
                    DgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument();
                }
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                }
            }
        }

        private void dgInventoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PgInstrumentDetail details = new PgInstrumentDetail(GetSelectedInstrument(), _instrumentManager);
            NavigationService?.Navigate(details);
        }

        private void RefreshListByStatus(string status)
        {
            DgInventoryList.ItemsSource = _instrumentManager.GetInstrumentsByStatus(status);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedInstrument() != null)
            {
                PgInstrumentDetail details = new PgInstrumentDetail(GetSelectedInstrument(), _instrumentManager, editMode: true);
                NavigationService?.Navigate(details);
            }
            else
            {
                MessageBox.Show("Please select an Instrument to edit", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PgInstrumentDetail details = new PgInstrumentDetail(_instrumentManager, addMode: true);
            NavigationService?.Navigate(details);
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            Instrument instrument = (Instrument)DgInventoryList.SelectedItem;
            if (instrument != null)
            {
                Instrument instrument2 = new Instrument()
                {
                    InstrumentId = instrument.InstrumentId,
                    InstrumentStatusId = instrument.InstrumentStatusId,
                    InstrumentTypeId = instrument.InstrumentTypeId,
                    InstrumentBrandId = instrument.InstrumentBrandId,
                    Price = instrument.Price
                };

                if (instrument.InstrumentStatusId == "Available")
                {
                    var updateStatus = new ChangeStatus(instrument, _instrumentManager);
                    if (updateStatus.ShowDialog() == true)
                    {
                        string status = null;
                        if (updateStatus.RbRent.IsChecked != null && (bool) updateStatus.RbRent.IsChecked)
                        {
                            status = "For Rent";
                        }

                        if (updateStatus.RbRentToOwn.IsChecked != null && (bool) updateStatus.RbRentToOwn.IsChecked)
                        {
                            status = "For Rent To Own";
                        }

                        if (updateStatus.RbSale.IsChecked != null && (bool) updateStatus.RbSale.IsChecked)
                        {
                            status = "For Sale";
                        }

                        instrument2.InstrumentStatusId = status;

                        try
                        {
                            _instrumentManager.UpdateInstrumentStatus(instrument, instrument2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                        }

                        AddToCart(instrument2);
                    }
                }
                else
                {
                    AddToCart(instrument);
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void AddToCart(Instrument instrument)
        {
            try
            {
                if (_cartManager.AddCartItem(instrument))
                {
                    MessageBox.Show("Item has been added to Cart");
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        private List<string> GetGetAllInstrumentStatuses()
        {
            List<string> statuses = new List<string>();
            try
            {
                statuses = _instrumentManager.GetAllInstrumentStatusIDs();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
            return statuses;
        }

        private void RefreshList()
        {
            DgInventoryList.ItemsSource = _instrumentManager.GetAllInstrument();
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbStatus.SelectedItem.ToString() != "All")
            {
                RefreshListByStatus(CmbStatus.SelectedItem.ToString());
            }
            if (CmbStatus.SelectedItem.ToString() == "All")
            {
                RefreshList();
            }
        }

        //public Invoice GetInvoice()
        //{
        //    return _cart;
        //}

        private void DgInventoryList_OnAutoGeneratedColumns(object sender, EventArgs e)
        {
            DgInventoryList.Columns.RemoveAt(9);
            DgInventoryList.Columns.RemoveAt(1);
            DgInventoryList.Columns[0].Header = "Rental Term";
            DgInventoryList.Columns[1].Header = "Rental Fee";
            DgInventoryList.Columns[2].Header = "Prep LIst";
            DgInventoryList.Columns[3].Header = "InstrumentID";
            DgInventoryList.Columns[4].Header = "Type";
            DgInventoryList.Columns[5].Header = "Section";
            DgInventoryList.Columns[6].Header = "Status";
            DgInventoryList.Columns[7].Header = "Brand";
            DgInventoryList.Columns[8].Header = "Price";
            DgInventoryList.Columns[1].Width = 100;
            DgInventoryList.Columns[8].Width = 100;
        }
    }
}
