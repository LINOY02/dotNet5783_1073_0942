using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using DO;


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
        private static readonly IBl bl = BlApi.Factory.Get();

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

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
            catch (BO.BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
