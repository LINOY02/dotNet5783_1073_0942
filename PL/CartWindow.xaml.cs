using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(BO.Cart cart1)
        {
            InitializeComponent();
            Cart = cart1;
            itemsListView.ItemsSource = cart1.Items;
            itemsListView.Items.Refresh();
         }
        BlApi.IBl bl = BlApi.Factory.Get();

        

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        private void amountSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((ComboBox)sender).DataContext;
            //var choise = 
            try
            {
               
            }
            catch (BO.BlProductIsNotOrderedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            itemsListView.ItemsSource = Cart.Items;
            itemsListView.Items.Refresh();
            totalPriceTextBox.Text = Cart.TotalPrice.ToString();
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            if (customerAddressTextBox.Text == " ") //Checking if there is an input
            {
                //Error message
                customerAddressTextBox.BorderBrush = Brushes.Red;
                flag = true;
            }
            if (customerEmailTextBox.Text == " ") //Checking if there is an input
            {
                //Error message
                customerEmailTextBox.BorderBrush = Brushes.Red;
                flag = true;
            }
            if (customerNameTextBox.Text == " ") //Checking if there is an input
            {
                //Error message
                customerNameTextBox.BorderBrush = Brushes.Red;
                flag = true;
            }
            if (totalPriceTextBox.Text == "0") //Checking if there is an input
            {
                //Error message
                totalPriceTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("There is no item to order");
                flag = true;
            }
            if (flag) //As long as there is an error, do not continue
            {
                return;
            }
            if (confirmBtn.Content.Equals("Confirm order"))
            {
                try
                {
                    bl.Cart.OrderCart(Cart);
                    MessageBox.Show("Are you sure you want to complate the order?");
                    Close();
                    MessageBox.Show("Your order has been accepted:)");
                }
                catch (BO.BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BO.BlProductIsNotOrderedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void customerAddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerAddressTextBox.BorderBrush = Brushes.DimGray;
        }

        private void customerEmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerEmailTextBox.BorderBrush = Brushes.DimGray;
        }

        private void customerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerNameTextBox.BorderBrush = Brushes.DimGray;

        }

        private void totalPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            totalPriceTextBox.BorderBrush = Brushes.DimGray;

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem OrderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                Cart = bl.Cart.DeleteProductFromCart(Cart, OrderItem.ProductID);
                itemsListView.ItemsSource = Cart.Items;
                itemsListView.Items.Refresh();
                totalPriceTextBox.Text = Cart.TotalPrice.ToString();
            }
            catch (BO.BlAlreadyExistException exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
    }
}