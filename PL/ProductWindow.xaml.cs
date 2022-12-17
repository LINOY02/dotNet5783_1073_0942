using System;
using System.Collections.Generic;
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
using System.Windows.Xps.Serialization;
using BlApi;
using BO;

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

            int id, amount;
            double p;
            bool flag = false;
            if ((!int.TryParse(IDTextBox.Text, out id)) || id < 100000)
            {
                IDTextBox.BorderBrush = Brushes.Red;
                IDLebel.Content = "failed";
                IDLebel.Visibility = Visibility.Visible;
                flag = true;
            }
            if (NameTextBox.Text == "")
            {
                NameTextBox.BorderBrush = Brushes.Red;
                NameLebel.Content = "failed";
                NameLebel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!double.TryParse(PriceTextBox.Text, out p)) || p <= 0)
            {
                PriceTextBox.BorderBrush = Brushes.Red;
                PriceLebel.Content = "failed";
                PriceLebel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!int.TryParse(InStockTextBox.Text, out amount)) || amount < 0)
            {
                InStockTextBox.BorderBrush = Brushes.Red;
                InStockLebel.Content = "failed";
                InStockLebel.Visibility = Visibility.Visible;   
                flag = true;
            }
            if (flag)
            {
                return;
            }
            if (Button.Content.Equals("Add"))
            {
                try
                {
                    bl.Product.AddProduct(new Product
                    {
                        ID = id,
                        Name = NameTextBox.Text,
                        Category = (Category)CategorySelector.SelectedItem,
                        Price = p,
                        InStock = amount
                    });
                    Close();
                }
                catch (BlAlreadyExistException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BlMissingInputException ex)
                { 
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    bl.Product.UpdateProduct(new Product
                    {
                        ID = int.Parse(IDTextBox.Text),
                        Name = NameTextBox.Text,
                        Category = (Category)CategorySelector.SelectedItem,
                        Price = Double.Parse(PriceTextBox.Text),
                        InStock = int.Parse(InStockTextBox.Text),
                    });
                    Close();
                }
                catch (BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BlMissingInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceLebel.Visibility = Visibility.Hidden;
            PriceTextBox.BorderBrush = Brushes.DimGray;
        }

        private void InStockTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InStockLebel.Visibility = Visibility.Hidden;    
            InStockTextBox.BorderBrush = Brushes.DimGray;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameLebel.Visibility= Visibility.Hidden;
            NameTextBox.BorderBrush = Brushes.DimGray;
        }

        private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IDLebel.Visibility=Visibility.Hidden;
            IDTextBox.BorderBrush= Brushes.DimGray;
        }
    }
}