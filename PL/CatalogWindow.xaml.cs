using System.Windows;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private static readonly IBl bl = Factory.Get();


        public Cart cart
        {
            get { return (Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(Cart), typeof(Window), new PropertyMetadata(null));

        public CatalogWindow(Cart cart1)
        {
            InitializeComponent();
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
            cart = cart1;
        }


        private void MyCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).ShowDialog();
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
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

        private void popularGroup_Click(object sender, RoutedEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.MostPopular(cart);
        }

        private void expensiveGroup_Click(object sender, RoutedEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.MostExpensive(cart);
        }

        private void cheapGroup_Click(object sender, RoutedEventArgs e)
        {
            CatalogListView.ItemsSource=bl.Product.MostCheap(cart); 
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CatalogListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductItem prod = (ProductItem)CatalogListView.SelectedItem;
            new ShowProductWindow(prod , cart).ShowDialog();
        }
    }
    
}
