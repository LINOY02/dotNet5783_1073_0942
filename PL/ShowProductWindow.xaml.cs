using BlApi;
using BO;
using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for ShowProductWindow.xaml
    /// </summary>
    public partial class ShowProductWindow : Window
    {
        private static readonly IBl bl = Factory.Get();

        public ProductItem productItem
        {
            get { return (ProductItem)GetValue(productItemProperty); }
            set { SetValue(productItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemProperty =
            DependencyProperty.Register("productItem", typeof(ProductItem), typeof(ShowProductWindow), new PropertyMetadata(null));

        public Cart cart
        {
            get { return (Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(Cart), typeof(ShowProductWindow), new PropertyMetadata(null));


        public ShowProductWindow(ProductItem prod, Cart myCart)
        {
            InitializeComponent();
            productItem = prod;
            cart = myCart;
        }



        private void addToCartBtn_Click_1(object sender, RoutedEventArgs e)
        {
            productItem = (ProductItem)((Button)sender).DataContext;
            try
            {
                bl.Cart.AddProductToCart(cart, productItem.ID);
                productItem = bl.Product.GetDetailsItem(productItem.ID, cart);
            }
            catch (BlDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (BlOutOfStockException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void delFromCartBtn_Click(object sender, RoutedEventArgs e)
        {
            productItem = (ProductItem)((Button)sender).DataContext;
            try
            {
                bl.Cart.UpdateCart(cart, productItem.ID, productItem.Amount - 1);
                productItem = bl.Product.GetDetailsItem(productItem.ID, cart);
            }
            catch (BlDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (BlProductIsNotOrderedException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MyCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).ShowDialog();
            Close();
        }
    }

}

