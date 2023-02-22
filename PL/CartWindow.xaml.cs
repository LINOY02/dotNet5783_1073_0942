using BlApi;
using BO;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(Cart cart1)
        {
            InitializeComponent();
            Cart = cart1;
            itemsListView.ItemsSource = cart1.Items;
            itemsListView.Items.Refresh();
        }
        IBl bl = Factory.Get();



        public Cart Cart
        {
            get { return (Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(Cart), typeof(Window), new PropertyMetadata(null));



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
                catch (BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BlProductIsNotOrderedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// bottun that take you back to the previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderItem OrderItem = (OrderItem)((Button)sender).DataContext;
            try
            {
                Cart = bl.Cart.DeleteProductFromCart(Cart, OrderItem.ProductID);
                itemsListView.ItemsSource = Cart.Items;
                itemsListView.Items.Refresh();
                totalPriceTextBox.Text = Cart.TotalPrice.ToString();
            }
            catch (BlAlreadyExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
    }
}