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
            number.Text = "0";
        }
        private static readonly IBl bl = Factory.Get();

        public Order Order
        {
            get { return (Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(Order), typeof(Window), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(number.Text);
                //bl.Order.TruckingOrder(id);
                OrderTrackingWindow1 orderTrackingWindow = new OrderTrackingWindow1(bl.Order.TruckingOrder(id).ID);
                orderTrackingWindow.ShowDialog();
                //new OrderTrackingWindow(id).ShowDialog();
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
