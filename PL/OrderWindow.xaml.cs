using System;
using System.Windows;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow(int OrderID) 
        {
            InitializeComponent();
            Order = bl.Order.GetOrder(OrderID);
        }

        private static readonly IBl bl = Factory.Get();

        public Order Order
        {
            get { return (Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(Order), typeof(Window), new PropertyMetadata(null));

        private void ShipUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? date = shipDateDatePicker.SelectedDate;
               bl.Order.ShipOrder(Order.ID, date);
            }
            catch(BlStatusAlreadyUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void DeliverUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? date = deliveryDateDatePicker.SelectedDate;
                bl.Order.DeliveredOrder(Order.ID, date);
            }
            catch (BlStatusAlreadyUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlStatusNotUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
