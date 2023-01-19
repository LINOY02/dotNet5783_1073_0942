using DalApi;
using DO;

namespace Dal
{
    internal class User : IUser
    {
        string s_user = "users";
        public void Add(DO.User user)
        {
            List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_user);
            if (listUsers.Exists(x => x?.userName == user.userName))// the user is exist in the list
                throw new DalAlreadyExistException($"The user name {user.userName} already exist in the list");
            listUsers.Add(new DO.User
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                userName = user.userName,
                password = user.password,
                status = user.status
            });
            XMLTools.SaveListToXMLSerializer(listUsers, s_user);
        }

        public DO.User GetByUserName(string username)
        {
            List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_user);
            return listUsers.Find(x => x?.userName == username) ?? throw new DalDoesNotExistException($"User name: {username} not exist in the list");
        }
    }
}
