using System.Windows;
using BlApi;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for OrderIDWindow.xaml
    /// </summary>
    public partial class OrderIDWindow1 : Window
    {
        public OrderIDWindow1()
        {
            InitializeComponent();
        }
        private static readonly IBl bl = Factory.Get();

        public Order Order
        {
            get { return (Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(Order), typeof(Window), new PropertyMetadata(null));
        /// <summary>
        /// show order details by the id that the user types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(number.Text);
                OrderTrackingWindow1 orderTrackingWindow = new OrderTrackingWindow1(bl.Order.TruckingOrder(id).ID);
                orderTrackingWindow.ShowDialog();
                Close();
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BlInvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
