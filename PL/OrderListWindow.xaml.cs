using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {


        private ObservableCollection<OrderForList> orderForLists
        {
            get { return (ObservableCollection<OrderForList>)GetValue(orderForListsProperty); }
            set { SetValue(orderForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty orderForListsProperty =
            DependencyProperty.Register("orderForLists", typeof(ObservableCollection<OrderForList>), typeof(OrderListWindow));


        public OrderListWindow()
        {
            InitializeComponent();
            orderForLists = new ObservableCollection<OrderForList>( bl.Order.GetListedOrders()!);
        }
        private static readonly IBl bl = Factory.Get();

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList orderId = (OrderForList)(OrderListView.SelectedItem);//The product selected for update
            try
            {
                //Opening a new window to update a order (constractor with an item ID parameter)
               OrderWindow orderWindow = new OrderWindow(orderId?.ID ?? throw new NullReferenceException("Choose order to update"));
                orderWindow.ShowDialog();
                orderForLists = new ObservableCollection<OrderForList>(bl.Order.GetListedOrders()!); ;//Reopening the catalog after updating the product
            }
            catch (NullReferenceException ex)//In case no parameter was received
            {
                MessageBox.Show(ex.Message);
            };
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
