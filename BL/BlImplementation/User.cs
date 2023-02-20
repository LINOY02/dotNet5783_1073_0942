using BO;
using DalApi;
using DO;
using System.ComponentModel.DataAnnotations;


namespace BlImplementation
{
    internal class User : BlApi.IUser
    {
        private static readonly IDal Dal = Factory.Get()!;

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
        /// <summary>
        /// log in as an exsit user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlDoesNotExistException"></exception>
        public BO.User LogIn(string userName, string password)
        {
            DO.User user;
            try
            {
                user = Dal.User.GetByUserName(userName);
                if (user.password != password)
                    throw new BlInvalidInputException("Worng Password");
            }
            catch (DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException(ex.Message);
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
        /// <summary>
        /// sign in as a new user
        /// </summary>
        /// <param name="bUser"></param>
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlMissingInputException"></exception>
        /// <exception cref="BlAlreadyExistException"></exception>
        public void SignIn(BO.User bUser)
        {
            if (!new EmailAddressAttribute().IsValid(bUser.Email))
                throw new BlInvalidInputException("Missing customer Email");
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
            catch (DalAlreadyExistException ex)
            {
                throw new BlAlreadyExistException(ex.Message);
            }
           
        }
    }
}
