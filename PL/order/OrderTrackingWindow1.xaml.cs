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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ShowOrderWindow(OrderT.ID).ShowDialog();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}