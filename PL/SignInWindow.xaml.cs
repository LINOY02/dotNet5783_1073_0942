using BlApi;
using BO;
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
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow(BO.User user)
        {
            InitializeComponent();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(userStatus));
            statusComboBox.SelectedIndex = 2;
        }



        public BO.User User
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(BO.User), typeof(SignInWindow), new PropertyMetadata(null));


        private static readonly IBl bl = BlApi.Factory.Get();
        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (statusComboBox.SelectedIndex == 2) //Checking if there is an input
            {
                //Error message
                MessageBox.Show("Choose a status");
                return;
            }
            try
            {
                User = new BO.User
                {
                    Name = nameTextBox.Text,
                    Address = addressTextBox.Text,
                    Email = emailTextBox.Text,
                    userName = userNameTextBox.Text,
                    password = passwordTextBox.Text,
                    status = (BO.userStatus)statusComboBox.SelectedItem
                };
                bl.User.SignIn(User);

            }
            catch(BlAlreadyExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
