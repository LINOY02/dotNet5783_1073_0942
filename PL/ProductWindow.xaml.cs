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
            CategorySelector.SelectedIndex = 5;
            Button.Content = "Add"; 
        }
        /// <summary>
        /// constructor for the "update" option
        /// </summary>
        /// <param name="ProductId"></param>
        public ProductWindow(int ProductId )
        {
            InitializeComponent();
            Button.Content = "Update";
            Product = bl.Product.GetProduct(ProductId);
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategorySelector.SelectedIndex = 5;
        }
        private static readonly IBl bl = BlApi.Factory.Get();



        public BO.Product Product
        {
            get { return (BO.Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// A function that checks the correctness of the input and adds or updates a product accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int id , inStock ;
            double p;
            bool flag = false;
            if ((!int.TryParse(IDTextBox.Text, out id)) || id < 100000) //Checking if there is an input and if the ID is right
            {
                //Error message
                IDTextBox.BorderBrush = Brushes.Red;
                IDLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if (NameTextBox.Text == "") //Checking if there is an input
            {
                //Error message
                NameTextBox.BorderBrush = Brushes.Red;
                NameLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if (CategorySelector.SelectedIndex == 5) //Checking if there is an input
            {
                //Error message
                CategorySelector.BorderBrush = new SolidColorBrush(Colors.Red);
                CategoryLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!double.TryParse(PriceTextBox.Text, out p))) //Checking if there is an input and if the price is right
            {
                //Error message
                PriceTextBox.BorderBrush = Brushes.Red;
                PriceLabel.Visibility = Visibility.Visible;
                flag = true;
            }
            if ((!int.TryParse(InStockTextBox.Text, out inStock))) //Checking if there is an input and if the num of the amount is right
            {
                //Error message
                InStockTextBox.BorderBrush = Brushes.Red;
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
                    if(CategorySelector.SelectedIndex == 5)

                    bl.Product.AddProduct(new Product
                    {
                        ID = id,
                        Name = NameTextBox.Text,
                        Category = (Category)CategorySelector.SelectedItem,
                        Price = p,
                        InStock = inStock,
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
                    bl.Product.UpdateProduct(Product);
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

        private void IDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;

        }

        private void InStockTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;

        }

        

        private void PriceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;

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