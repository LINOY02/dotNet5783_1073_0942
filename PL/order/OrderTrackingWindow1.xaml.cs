using BlApi;
using BO;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTrackingWindow1 : Window
    {
        public OrderTrackingWindow1(int id)
        {
            InitializeComponent();
            OrderT = bl.Order.TruckingOrder(id);
        }

        private static readonly IBl bl = Factory.Get();

        public OrderTracking OrderT
        {
            get { return (OrderTracking)GetValue(OrderTTrackingProperty); }
            set { SetValue(OrderTTrackingProperty, value);}
        }

        public static readonly DependencyProperty OrderTTrackingProperty =
            DependencyProperty.Register("OrderT", typeof(OrderTracking), typeof(Window), new PropertyMetadata(null));
        /// <summary>
        /// show the Order's Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderDetails_Click(object sender, RoutedEventArgs e)
        {
            new ShowOrderWindow(OrderT.ID).ShowDialog();
        }
        
        /// <summary>
        /// bottun that take you back to the previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}