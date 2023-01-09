using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow(int OrderID , bool isManager) 
        {
            InitializeComponent();
            Order = bl.Order.GetOrder(OrderID);
            if(!isManager)
            {
                ShipUpdBtn.Visibility = Visibility.Hidden;
                DeliverUpdBtn.Visibility = Visibility.Hidden;
                shipDateDatePicker.Visibility = Visibility.Hidden;
                deliveryDateDatePicker.Visibility= Visibility.Hidden;
            }
        }

        private static readonly IBl bl = BlApi.Factory.Get();

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        private void ShipUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               bl.Order.ShipOrder(Order.ID);
            }
            catch(BO.BlStatusAlreadyUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void DeliverUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.DeliveredOrder(Order.ID);
            }
            catch (BO.BlStatusAlreadyUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
