using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class User : BlApi.IUser
    {
        private static readonly IDal Dal = DalApi.Factory.Get()!;

        public BO.User LogIn(string userName, string password)
        {
            DO.User user;
            try
            {
                user = Dal.User.GetByUserName(userName);
                if (user.password != password)
                    throw new BO.BlInvalidInputException("Worng Password");
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
            return new BO.User
            {
                userName = userName,
                password = password,
                status =(BO.userStatus) user.status
            };
        }

        public void SignIn(BO.User bUser)
        {
            try 
            {
                Dal.User.Add(new DO.User
                {
                    userName = bUser.userName,
                    password = bUser.password,
                    status = (DO.userStatus)bUser.status
                });   
            }
            catch (DO.DalAlreadyExistException ex)
            {
                throw new BO.BlAlreadyExistException(ex.Message);
            }
        }
    }
}
