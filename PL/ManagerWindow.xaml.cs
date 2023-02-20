using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog();
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().ShowDialog();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
