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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(Category));
        }

        private IBl bl = new Bl();

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetListedProducts().OrderBy(x => x.ID);
        }


        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ProductListView.SelectedItem as ProductForList != null)
            {
                ProductForList productId = (ProductForList)(ProductListView.SelectedItem);
                try
                {
                    ProductWindow productWindoe = new ProductWindow(productId?.ID ?? throw new NullReferenceException("Choose product to update"));
                    productWindoe.ShowDialog();
                    ProductListView.ItemsSource = bl.Product.GetListedProducts().OrderBy(x => x.ID);
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            
        }

        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var choise = CategorySelector.SelectedItem;
            if (CategorySelector.SelectedIndex == 5)
                ProductListView.ItemsSource = bl.Product.GetListedProducts();
            else
                ProductListView.ItemsSource = bl.Product.GetListedProducts(p => p?.Category == (Category)choise);
        }
    }
}
