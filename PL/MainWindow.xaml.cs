using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// show the list of the products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OrderTraking_Click(object sender, RoutedEventArgs e)
        {
            new OrderIDWindow1().ShowDialog();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            new LogInWindow().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }
    }
}
