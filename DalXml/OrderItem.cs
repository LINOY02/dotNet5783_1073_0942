using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        string s_orderItem = "orderItems";
        public int Add(DO.OrderItem item)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            item.ID = Config.GetOrderItemId(); //Initialize the ID number of the order
            listOrder.Add(item);
            Config.SaveNextOrderItemId(item.ID + 1);
            XMLTools.SaveListToXMLSerializer(listOrder, s_orderItem);
            return item.ID;
        }


        public void Delete(int id)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            if (!listOrder.Exists(x => x?.ID == id))// check if the orderItem isn't exist in the list
                throw new DalDoesNotExistException($"OrderItem num {id} not exist in the list");
            else
                listOrder.Remove(GetById(id));
            XMLTools.SaveListToXMLSerializer(listOrder, s_orderItem);
        }

        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            if (func == null)
                return listOrder.Select(x => x);
            return listOrder.Where(x => func(x)).Select(x => x);
        }


        public DO.OrderItem GetById(int id)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            return listOrder.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"OrderItem num {id} not exist in the list");
        }

        public DO.OrderItem GetItem(Func<DO.OrderItem?, bool>? filter)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            return listOrder.Find(x => filter(x)) ?? throw new DalDoesNotExistException("order item under this condition is not exist");
        }

        public void Update(DO.OrderItem item)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            if (!listOrder.Exists(x => x?.ID == item.ID))// check if the orderItem isn't exist in the list
                throw new DalDoesNotExistException($"OrderItem num {item.ID} not exist in the list");
            else //if the order isn exist in the list.
                listOrder.Remove(GetById(item.ID));
            listOrder.Add(item);
            XMLTools.SaveListToXMLSerializer(listOrder, s_orderItem);
        }
    }
}
