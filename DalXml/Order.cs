using Dal;
using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class Order : IOrder
    {
        string s_order = "order";


        public int Add(DO.Order item)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            item.ID = Config.GetNextOrderID(); //Initialize the ID number of the order
            Orders.Add(item);
            XMLTools.SaveListToXMLSerializer(Orders, s_order);
            return item.ID;
        }

        public DO.Order GetById(int id)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            return Orders.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException("Order num " + id + " not exist in the list");
        }

        public void Update(DO.Order item)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            if (!Orders.Exists(x => x?.ID == item.ID))// check if the order isn't exist in the list
                throw new DalDoesNotExistException("Order num " + item.ID + " not exist in the list");
            else// the order is exist in the list
            {
                Orders.Remove(GetById(item.ID));
                Orders.Add(item);
            }
            XMLTools.SaveListToXMLSerializer(Orders, s_order);
        }

        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            if (func == null)
                return Orders.Select(x => x);
            return Orders.Where(x => func(x)).Select(x => x);
        }

        public DO.Order GetItem(Func<DO.Order?, bool>? filter)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            return Orders.Find(x => filter!(x)) ?? throw new DalDoesNotExistException("order under this condition is not exist");
        }

        public void Delete(int id)
        {
            List<DO.Order?> Orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            if (!Orders.Exists(x => x?.ID == id))// check if the order is already exist in the list
               throw new DalDoesNotExistException("Order num " + id + " not exist in the list");
            else
                Orders.Remove(GetById(id));
            XMLTools.SaveListToXMLSerializer(Orders, s_order);
        }
    }
}
