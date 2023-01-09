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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private static readonly IBl bl = BlApi.Factory.Get();

        public BO.User User
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(BO.User), typeof(Window), new PropertyMetadata(null));

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User = bl.User.LogIn(userNameTextBox.Text, passwordTextBox.Text);
                if(User.status == userStatus.MANAGER)
                   new ManagerWindow().ShowDialog();
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BlInvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();

        }

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            new SignInWindow().ShowDialog();
        }
    }
}
