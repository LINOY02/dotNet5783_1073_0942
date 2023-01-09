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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ManagerBtn.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// show the list of the products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().ShowDialog();
        }

        private void OrderTraking_Click(object sender, RoutedEventArgs e)
        {
            new OrderIDWindow1().ShowDialog();
        }

        List<BO.OrderItem?> items = new List<BO.OrderItem?>();

        BO.Cart cart = new BO.Cart
        {
            TotalPrice = 0,
            CustomerAddress = null, 
            CustomerEmail = null,
            CustomerName = null,
            Items = null,
        };

        private void catalogBtn_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow(cart).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new LogInWindow().ShowDialog();
        }
    }
}
