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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        public OrderListWindow()
        {
            InitializeComponent();
            OrderListView.ItemsSource = bl.Order.GetListedOrders();
        }
        private static readonly IBl bl = BlApi.Factory.Get();

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList orderId = (OrderForList)(OrderListView.SelectedItem);//The product selected for update
            try
            {
                //Opening a new window to update a order (constractor with an item ID parameter)
               OrderWindow orderWindow = new OrderWindow(orderId?.ID ?? throw new NullReferenceException("Choose order to update"), true);
                orderWindow.ShowDialog();
                OrderListView.ItemsSource = bl.Order.GetListedOrders();//Reopening the catalog after updating the product
            }
            catch (NullReferenceException ex)//In case no parameter was received
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
