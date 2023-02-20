using BO;


namespace BlApi
{
    public interface IUser
    {
        /// <summary>
        /// the func is log in the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User LogIn(string userName, string pasword);

        /// <summary>
        /// the func is sign in the 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        void SignIn(User user);

        Cart GetCart(User user);
    }
}
