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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;
using System.Xml.Linq;
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
        }
        /// <summary>
        /// constructor for the "update" option
        /// </summary>
        /// <param name="ProductId"></param>
        public ProductWindow(int ProductId )
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Product = bl.Product.GetProduct(ProductId);
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
            if (CategorySelector.SelectedIndex == 5) //Checking if there is an input
            {
                //Error message
                MessageBox.Show("Choose a Category");
                return;
            }
            if (Button.Content.Equals("Add")) //when we choose to add a new product
            {
                try
                {
                    bl.Product.AddProduct(new Product
                    {
                        ID = int.Parse(IDTextBox.Text),
                        Name = NameTextBox.Text,
                        Category = (Category)CategorySelector.SelectedItem,
                        Price = double.Parse(PriceTextBox.Text),
                        InStock = int.Parse(InStockTextBox.Text),
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

    }
}