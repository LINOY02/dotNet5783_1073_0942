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
        /// <summary>
        /// constructor for the "add" option
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Button.Content = "Add"; 
        }
        /// <summary>
        /// constructor for the "update" option
        /// </summary>
        /// <param name="ProductId"></param>
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
        /// <summary>
        /// A function that checks the correctness of the input and adds or updates a product accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int id, amount;
            double p;
            bool flag = false;
            if ((!int.TryParse(IDTextBox.Text, out id)) || id < 100000) //Checking if there is an input and if the ID is right
            {
                //Error message
                IDTextBox.BorderBrush = Brushes.Red;
                IDLabel.Content = "failed";
                IDLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if (NameTextBox.Text == "") //Checking if there is an input
            {
                //Error message
                NameTextBox.BorderBrush = Brushes.Red;
                NameLabel.Content = "failed";
                NameLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if (CategorySelector.Text == "" || CategorySelector.Text == "NONE") //Checking if there is an input
            {
                //Error message
                CategorySelector.BorderBrush = Brushes.Red;
                CategoryLabel.Content = "failed";
                CategoryLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!double.TryParse(PriceTextBox.Text, out p)) || p <= 0) //Checking if there is an input and if the price is right
            {
                //Error message
                PriceTextBox.BorderBrush = Brushes.Red;
                PriceLabel.Content = "failed";
                PriceLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!int.TryParse(InStockTextBox.Text, out amount)) || amount < 0) //Checking if there is an input and if the num of the amount is right
            {
                //Error message
                InStockTextBox.BorderBrush = Brushes.Red;
                InStockLabel.Content = "failed";
                InStockLabel.Visibility = Visibility.Visible;   
                flag = true;
            }
            if (flag) //As long as there is an error, do not continue
            {
                return;
            }
            if (Button.Content.Equals("Add")) //when we choose to add a new product
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
            else //when we chose to update a product
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
        /// <summary>
        /// Hiding an error message after correcting the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IDLabel.Visibility = Visibility.Hidden;
            IDTextBox.BorderBrush = Brushes.DimGray;
        }
        /// <summary>
        /// Hiding an error message after correcting the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryLabel.Visibility = Visibility.Hidden;
            CategorySelector.BorderBrush = Brushes.DimGray;
        }
        /// <summary>
        /// Hiding an error message after correcting the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameLabel.Visibility = Visibility.Hidden;
            NameTextBox.BorderBrush = Brushes.DimGray;
        }
        /// <summary>
        /// Hiding an error message after correcting the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceLabel.Visibility = Visibility.Hidden;
            PriceTextBox.BorderBrush = Brushes.DimGray;
        }
        /// <summary>
        /// Hiding an error message after correcting the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InStockTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InStockLabel.Visibility = Visibility.Hidden;    
            InStockTextBox.BorderBrush = Brushes.DimGray;
        }
     
    }
}