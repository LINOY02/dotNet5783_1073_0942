
using DO;
using DalApi;
using System;

namespace DAL;

internal class DalOrderItem : IOrderItem
{
    // Create
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.NextOrderItemNumber; //Initialize the ID number of the order
        DataSource._orderItems.Add(orderItem);
        return orderItem.ID;
    }

    // Request
    public OrderItem GetById(int id)
    {
      return DataSource._orderItems.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException("OrderItem num " + id + " not exist in the list");
    }

    //request by order and product ID number
    public OrderItem GetByOidAndPid(int orderId, int productId)
    {

        return DataSource._orderItems.Find(x=> x?.OrderID == orderId && x?.ProductID == productId) ??
        throw new DalDoesNotExistException("OrderItem with product ID: " + productId + "and ordrt ID: " + orderId + "not exist in the list");
    }

    // Update
    public void Update(OrderItem orderItem)
    {
        if (!DataSource._orderItems.Exists(x => x?.ID == orderItem.ID))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException("OrderItem num " + orderItem.ID + " not exist in the list");
        else //if the order isn exist in the list.
            DataSource._orderItems.Remove(GetById(orderItem.ID));
        DataSource._orderItems.Add(orderItem);

    }

    // Delete
    public void Delete(int id)
    {

        if (!DataSource._orderItems.Exists(x => x?.ID == id))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException("OrderItem num " + id + " not exist in the list");
        else
            DataSource._orderItems.Remove(GetById(id));

    }

    //A function that returns the array
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        if (func == null)
            return DataSource._orderItems.Select(x => x);
        return DataSource._orderItems.Where(x => func(x)).Select(x => x);
    }

    //A function that returns all items of the requested order
    public IEnumerable<OrderItem?> GetAllOrder(int orderId)
    {
        List<OrderItem?> allOrder = new List<OrderItem?>();
        for (int i = 0; i < DataSource._orderItems.Count; i++)
        {
            //If the ordered item belongs to the requested order
            if (DataSource._orderItems[i]?.OrderID == orderId)
            {
                allOrder.Add(DataSource._orderItems[i]);//Adding the item to the list if it is the requested item
            }
        }
        return allOrder;//Returning the list
    }

}