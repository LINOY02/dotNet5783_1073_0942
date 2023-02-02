using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalUser : IUser
    {

        /// <summary>
        /// add a user to the datasource
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="DalAlreadyExistException"></exception>
        public void Add(User user)
        {
            if (DataSource._users.Exists(x => x?.userName == user.userName))// the user is exist in the list
                throw new DalAlreadyExistException($"The user name {user.userName} already exist in the list");
            DataSource._users.Add(new User
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                userName = user.userName,
                password = user.password,
                status = user.status
            });
        }

        /// <summary>
        /// get user by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="DalDoesNotExistException"></exception>
        public User GetByUserName(string username)
        {
            return DataSource._users.Find(x => x?.userName == username) ?? throw new DalDoesNotExistException($"User name: {username} not exist in the list"); // return the requested user
        }
    }
}
