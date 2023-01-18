using BlApi;
using BO;
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
    /// Interaction logic for ShowProductWindow.xaml
    /// </summary>
    public partial class ShowProductWindow : Window
    {
        private static readonly IBl bl = BlApi.Factory.Get();

        public BO.ProductItem productItem
        {
            get { return (BO.ProductItem)GetValue(productItemProperty); }
            set { SetValue(productItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemProperty =
            DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(ShowProductWindow), new PropertyMetadata(null));

        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(ShowProductWindow), new PropertyMetadata(null));


        public ShowProductWindow(BO.ProductItem prod, BO.Cart myCart)
        {
            InitializeComponent();
            productItem = prod;
            cart = myCart;
        }



        private void addToCartBtn_Click_1(object sender, RoutedEventArgs e)
        {
            productItem = (BO.ProductItem)((Button)sender).DataContext;
            try
            {
                bl.Cart.AddProductToCart(cart, productItem.ID);
                productItem = bl.Product.GetDetailsItem(productItem.ID, cart);
            }
            catch (BO.BlDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (BO.BlOutOfStockException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void delFromCartBtn_Click(object sender, RoutedEventArgs e)
        {
            productItem = (BO.ProductItem)((Button)sender).DataContext;
            try
            {
                bl.Cart.UpdateCart(cart, productItem.ID, productItem.Amount - 1);
                productItem = bl.Product.GetDetailsItem(productItem.ID, cart);
            }
            catch (BO.BlDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (BO.BlProductIsNotOrderedException exc)
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
            productItem = bl.Product.GetDetailsItem(productItem.ID, cart);
            Close();
        }
    }

}

