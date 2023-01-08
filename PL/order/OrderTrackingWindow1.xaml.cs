using BlApi;
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

        private static readonly IBl bl = BlApi.Factory.Get();

        public BO.OrderTracking? OrderT
        {
            get { return (BO.OrderTracking?)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderT", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));

       

        private void showOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = OrderT.ID;
            new OrderWindow(id, false).ShowDialog();
        }
    }
}