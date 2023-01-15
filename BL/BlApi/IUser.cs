using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

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
        BO.User LogIn(string userName, string pasword);

        /// <summary>
        /// the func is sign in the 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        void SignIn(BO.User user);

        BO.Cart GetCart(BO.User user);
    }
}
