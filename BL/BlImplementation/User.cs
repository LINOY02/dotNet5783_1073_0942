using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class User : BlApi.IUser
    {
        private static readonly IDal Dal = DalApi.Factory.Get()!;

        public BO.Cart GetCart(BO.User user)
        {
            return new BO.Cart
            {
                CustomerName = user.Name,
                CustomerAddress = user.Address,
                CustomerEmail = user.Email,
                Items = new List<BO.OrderItem?>(),
                TotalPrice = 0,
            };
        }

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
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                userName = userName,
                password = password,
                status =(BO.userStatus) user.status
            };
        }

        public void SignIn(BO.User bUser)
        {
            if (!new EmailAddressAttribute().IsValid(bUser.Email))
                throw new BO.BlInvalidInputException("Missing customer Email");
            try 
            {
                Dal.User.Add(new DO.User
                {
                    Name = bUser.Name ?? throw new BO.BlMissingInputException("The name is missing"),
                    Address = bUser.Address ?? throw new BO.BlMissingInputException("The address is missing"),
                    Email = bUser.Email,
                    userName = bUser.userName ?? throw new BO.BlMissingInputException("The user name is missing"),
                    password = bUser.password ?? throw new BO.BlMissingInputException("The password is missing"),
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
