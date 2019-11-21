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
        private InventoryList _inventoryList;
        private pgUserProfile _profile;
        private IUserManager _userManager;

        public pgInventory()
        {
            InitializeComponent();
            _inventoryList = new InventoryList(false);
            _profile = new pgUserProfile();
            btnSave.Visibility = Visibility.Hidden;
            _userManager = new UserManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frmAdmin.NavigationService.Navigate(_inventoryList);
        }
    }
}
