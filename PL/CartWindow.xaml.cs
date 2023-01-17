using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
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
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(BO.Cart cart1)
        {
            InitializeComponent();
            Cart = cart1;
            itemsListView.ItemsSource = cart1.Items;
            itemsListView.Items.Refresh();
        }
        BlApi.IBl bl = BlApi.Factory.Get();



        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (confirmBtn.Content.Equals("Confirm order"))
            {
                try
                {
                    int id = bl.Cart.OrderCart(Cart);
                    MessageBox.Show("Are you sure you want to complate the order?");
                    Close();
                    MessageBox.Show("Your order ID is" + id);
                }
                catch (BO.BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BO.BlProductIsNotOrderedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem OrderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                Cart = bl.Cart.DeleteProductFromCart(Cart, OrderItem.ProductID);
                itemsListView.ItemsSource = Cart.Items;
                itemsListView.Items.Refresh();
                totalPriceTextBox.Text = Cart.TotalPrice.ToString();
            }
            catch (BO.BlAlreadyExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
    }
}