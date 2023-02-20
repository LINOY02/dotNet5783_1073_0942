using BlApi;
using BO;
using System.Windows;


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

        private static readonly IBl bl = Factory.Get();

        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(User), typeof(Window), new PropertyMetadata(null));

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User = bl.User.LogIn(userNameTextBox.Text, passwordTextBox.Text);
                Cart myCart = bl.User.GetCart(User);
                Close();
                if (User.status == userStatus.MANAGER)
                   new ManagerWindow().ShowDialog();
                else
                    new CatalogWindow(myCart).ShowDialog();

            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BlInvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            new SignInWindow(User).ShowDialog();
        }
    }
}
