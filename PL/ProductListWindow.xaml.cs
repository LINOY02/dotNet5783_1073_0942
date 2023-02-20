using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        
        private ObservableCollection<ProductForList> productForLists
        {
            get { return (ObservableCollection<ProductForList>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty productForListsProperty =
            DependencyProperty.Register("productForLists", typeof(ObservableCollection<ProductForList>), typeof(ProductListWindow));

        public ProductListWindow()
        {
            InitializeComponent();
            //When the page opens, the product catalog will appear
            productForLists = new ObservableCollection<ProductForList>( bl.Product.GetListedProducts()!);
            //The options of the combobox are the categories of the product
            CategorySelector.ItemsSource = Enum.GetValues(typeof(Category));
            CategorySelector.SelectedIndex = 5;
        }

        private  static readonly IBl bl = BlApi.Factory.Get();

        /// <summary>
        /// Event to add a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();//Opening a new window to add a product (empty constractor)
            productForLists = new ObservableCollection<ProductForList>(bl.Product.GetListedProducts()!);//Reopening the catalog after adding the product

        }

        

        /// <summary>
        /// Product update event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Checking that the event is really a double click on an item and not a similar event
            if (ProductListView.SelectedItem as ProductForList != null)
            {
                ProductForList productId = (ProductForList)(ProductListView.SelectedItem);//The product selected for update
                try
                {
                    //Opening a new window to update a product (constractor with an item ID parameter)
                    ProductWindow productW = new ProductWindow(productId?.ID ?? throw new NullReferenceException("Choose product to update"));
                    productW.ShowDialog();
                    productForLists = new ObservableCollection<ProductForList>(bl.Product.GetListedProducts()!);//Reopening the catalog after updating the product
                }
                catch (NullReferenceException ex)//In case no parameter was received
                {
                    MessageBox.Show(ex.Message);
                }
            }      
            
        }

        /// <summary>
        /// Event to filter catalog by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var choise = CategorySelector.SelectedItem;//the selected category
            if (choise.Equals(Category.NONE))//If no category is selected
                productForLists = new ObservableCollection<ProductForList>( bl.Product?.GetListedProducts()!);//Show the entire catalog
            else//If a category is selected
                productForLists = new ObservableCollection<ProductForList>( bl.Product?.GetListedProductsByCategory((Category)choise)!);//Show all products of the selected category
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}
