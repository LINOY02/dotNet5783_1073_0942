using BlApi;
using BO;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for ShowOrderWindow.xaml
    /// </summary>
    public partial class ShowOrderWindow : Window
    {


        public Order ShowOrder
        {
            get { return (Order)GetValue(ShowOrderProperty); }
            set { SetValue(ShowOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowOrderProperty =
            DependencyProperty.Register("ShowOrder", typeof(Order), typeof(Window), new PropertyMetadata(null));

        private static readonly IBl bl = Factory.Get();

        public ShowOrderWindow(int id)
        {
            InitializeComponent();
            ShowOrder = bl.Order.GetOrder(id);
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
