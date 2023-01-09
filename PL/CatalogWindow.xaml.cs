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




        public ObservableCollection<ProductItem?> productItems
        {
            get { return (ObservableCollection<ProductItem>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemsProperty =
            DependencyProperty.Register("productItems", typeof(ObservableCollection<ProductItem>), typeof(Window), new PropertyMetadata(null));



        public CatalogWindow()
        {
            InitializeComponent();
            //productItems = new ObservableCollection<ProductItem?>( bl.Product.GetProductItems(cart));
            CatalogListView.ItemsSource = bl.Product.GetProductItems(cart);
        }
           
      
        

        

        
    }
}
