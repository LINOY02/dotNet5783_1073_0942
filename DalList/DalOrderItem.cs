
using DO;
using DalApi;
using System;

namespace Dal;

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
      return DataSource._orderItems.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"OrderItem num { id } not exist in the list");
    }


    // Update
    public void Update(OrderItem orderItem)
    {
        if (!DataSource._orderItems.Exists(x => x?.ID == orderItem.ID))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException($"OrderItem num { orderItem.ID} not exist in the list");
        else //if the order isn exist in the list.
            DataSource._orderItems.Remove(GetById(orderItem.ID));
        DataSource._orderItems.Add(orderItem);

    }

    // Delete
    public void Delete(int id)
    {

        if (!DataSource._orderItems.Exists(x => x?.ID == id))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException($"OrderItem num {id} not exist in the list");
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


    public OrderItem GetItem(Func<OrderItem?, bool>? filter)
    {
        return DataSource._orderItems.Find(x => filter(x)) ?? throw new DalDoesNotExistException("order item under this condition is not exist");
    }
}