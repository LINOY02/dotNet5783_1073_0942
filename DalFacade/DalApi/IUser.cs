using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IUser
    {
        /// <summary>
        /// the func add user to the list
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        public void Add(User user);

        /// <summary>
        /// /The func return the user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByUserName(string username);

    }
}
