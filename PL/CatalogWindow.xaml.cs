using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private static readonly IBl bl = BlApi.Factory.Get();


        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));






        public BO.ProductItem productItem
        {
            get { return (BO.ProductItem)GetValue(productItemProperty); }
            set { SetValue(productItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemProperty =
            DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));

        private ObservableCollection <BO.ProductItem> productItems = new ObservableCollection<BO.ProductItem>();    


        public CatalogWindow(BO.Cart cart1)
        {
            InitializeComponent();
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
            cart = cart1;
        }



        private void addToCartBtn_Click_1(object sender, RoutedEventArgs e)
        {
            productItem = (BO.ProductItem)((Button)sender).DataContext;
            try
            {
                cart = bl.Cart.AddProductToCart(cart, productItem.ID);
               // productItem.Amount++;
                CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
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

        private void MyCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).ShowDialog();
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
        }

        private void delFromCartBtn_Click(object sender, RoutedEventArgs e)
        {
            productItem = (BO.ProductItem)((Button)sender).DataContext;
            try
            {
                cart = bl.Cart.UpdateCart(cart, productItem.ID, productItem.Amount-1);
                CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
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

        private void TabelLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItemsByCategory(cart, Category.TABLE);
        }

        private void ChairLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItemsByCategory(cart, Category.CHAIR);
        }

        private void SofaLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItemsByCategory(cart, Category.SOFA);
        }

        private void ClosetLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItemsByCategory(cart, Category.CLOSET);
        }

        private void BedLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItemsByCategory(cart, Category.BED);
        }
    }
    
}
