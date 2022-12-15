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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Button.Content = "Add"; 
        }
        public ProductWindow(int ProductId)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Button.Content = "Update";
            BO.Product p = bl.Product.GetProduct(ProductId);
            IDTextBox.Text = p.ID.ToString();
            IDTextBox.IsReadOnly = true;
            NameTextBox.Text = p.Name;
            PriceTextBox.Text = p.Price.ToString();
            InStockTextBox.Text = p.InStock.ToString();
            CategorySelector.Text = p.Category.ToString();
        }
        private IBl bl = new Bl();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Button.Content.Equals("Add"))
            {
                bl.Product.AddProduct(new BO.Product
                {
                    ID = int.Parse(IDTextBox.Text),
                    Name = NameTextBox.Text,
                    Category = (BO.Category)CategorySelector.SelectedItem,
                    Price = Double.Parse(PriceTextBox.Text),
                    InStock = int.Parse(InStockTextBox.Text),
                });
            }
            else
            {
                bl.Product.UpdateProduct(new BO.Product
                {
                    ID = int.Parse(IDTextBox.Text),
                    Name = NameTextBox.Text,
                    Category = (BO.Category)CategorySelector.SelectedItem,
                    Price = Double.Parse(PriceTextBox.Text),
                    InStock = int.Parse(InStockTextBox.Text),
                });
            }
            Close();
           
        }

        
    }
}
