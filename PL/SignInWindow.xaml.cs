using BlApi;
using BO;
using System;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow(User user)
        {
            InitializeComponent();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(userStatus));
            statusComboBox.SelectedIndex = 2;
        }



        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(User), typeof(SignInWindow), new PropertyMetadata(null));


        private static readonly IBl bl = Factory.Get();
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
                User = new User
                {
                    Name = nameTextBox.Text,
                    Address = addressTextBox.Text,
                    Email = emailTextBox.Text,
                    userName = userNameTextBox.Text,
                    password = passwordTextBox.Text,
                    status = (userStatus)statusComboBox.SelectedItem
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
