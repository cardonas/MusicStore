﻿using System;
using System.Windows;
using System.Windows.Controls;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for pgCustomerList.xaml
    /// </summary>
    public partial class pgCustomerList : Page
    {
        private readonly ICustomerManager _customerManager;


        public pgCustomerList()
        {
            InitializeComponent();
            _customerManager = new CustomerManager();
        }

        private void DgCustomerList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DgCustomerList.ItemsSource = _customerManager.GetAllCustomers(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        private void DgCustomerList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            DgCustomerList.Columns.RemoveAt(0);
            DgCustomerList.Columns.RemoveAt(2);
            DgCustomerList.Columns[0].Header = "First Name";
            DgCustomerList.Columns[1].Header = "Last Name";
            DgCustomerList.Columns[2].Header = "Phone Number";
            DgCustomerList.Columns[3].Header = "Email";
        }

        private void DgCustomerList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.NavigationService?.Navigate(new pgCustomerDetails((Customer) DgCustomerList.SelectedItem));
        }
    }
}
