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
    /// Interaction logic for pgUserProfile.xaml
    /// </summary>
    public partial class pgInstrumentDetail : Page
    {

        private IInstrumentManager _instrumentManager;
        private Instrument _instrument;

        public pgInstrumentDetail()
        {
            InitializeComponent();
        }

        public pgInstrumentDetail(Instrument instrument, InstrumentManager instrumentManager)
        {
            InitializeComponent();
            _instrumentManager = instrumentManager;
            _instrument = instrument;
            lblInstrument.Content = _instrument.InstrumentID;
            lblPriceAmount.Content = _instrument.PriceString;
            txtType.Text = _instrument.InstrumentTypeID;
            txtBrand.Text = _instrument.Brand;
            lblStatus.Content = _instrument.InstrumentStatusID;
        }

        //public void SaveNewEmployee()
        //{
        //    try
        //    {
        //        Employee employee = new Employee();

        //        employee.FirstName = txtFirstName.Text;
        //        employee.LastName = txtLastName.Text;
        //        employee.PhoneNumber = txtPhoneNumber.Text;

        //        if (_userManager.AddNewEmployee(employee))
        //        {
        //            MessageBox.Show("Employee Added", "Success", MessageBoxButton.OK);
        //        }

        //        if (MessageBox.Show("Would you like add another employee?", "Add Another?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //        {
        //            txtFirstName.Text = "";
        //            txtLastName.Text = "";
        //            txtAddress.Text = "";
        //            txtState.Text = "";
        //            txtZipcode.Text = "";
        //            txtPhoneNumber.Text = "";
        //            txtFirstName.Focus();
        //        }
        //        else
        //        {
        //            this.NavigationService.Navigate(new EmployeeList());
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
        //    }

        //}

        //public void SaveUpdatedEmployee()
        //{
        //    Employee oldEmployee = _employee;
        //    //txtFirstName.Text = _employee.FirstName;
        //    //txtLastName.Text = _employee.LastName;
        //    //txtAddress.Text = "";
        //    //txtState.Text = "";
        //    //txtZipcode.Text = "";
        //    //txtPhoneNumber.Text = _employee.PhoneNumber;


        //    Employee updatedEmployee = new Employee();
        //    updatedEmployee.FirstName = txtFirstName.Text;
        //    updatedEmployee.LastName = txtLastName.Text;
        //    updatedEmployee.PhoneNumber = txtPhoneNumber.Text;
        //    updatedEmployee.Email = oldEmployee.Email;


        //    try
        //    {
        //        if (_userManager.UpdateEmployeeInfo(oldEmployee, updatedEmployee))
        //        {
        //            MessageBox.Show("Employee Profile Updated", "Success", MessageBoxButton.OK);
        //            if ((int)MessageBoxResult.OK == 1)
        //            {
        //                this.NavigationService.Navigate(new EmployeeList());

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
        //            this.NavigationService.Navigate(new EmployeeList());

        //    }


        //}
    }
}
