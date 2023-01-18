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
        public OrderWindow(int OrderID) 
        {
            InitializeComponent();
            Order = bl.Order.GetOrder(OrderID);
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
                DateTime? date = shipDateDatePicker.SelectedDate;
               bl.Order.ShipOrder(Order.ID, date);
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
                DateTime? date = deliveryDateDatePicker.SelectedDate;
                bl.Order.DeliveredOrder(Order.ID, date);
            }
            catch (BO.BlStatusAlreadyUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlStatusNotUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
