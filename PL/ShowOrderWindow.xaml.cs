using BlApi;
using BO;
using DO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ShowOrderWindow.xaml
    /// </summary>
    public partial class ShowOrderWindow : Window
    {


        public BO.Order ShowOrder
        {
            get { return (BO.Order)GetValue(ShowOrderProperty); }
            set { SetValue(ShowOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowOrderProperty =
            DependencyProperty.Register("ShowOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        private static readonly IBl bl = BlApi.Factory.Get();

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
