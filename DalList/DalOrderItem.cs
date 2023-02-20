
using DO;
using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// add new order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.NextOrderItemNumber; //Initialize the ID number of the order
        DataSource._orderItems.Add(orderItem);
        return orderItem.ID;
    }

    /// <summary>
    /// Request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public OrderItem GetById(int id)
    {
      return DataSource._orderItems.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"OrderItem num { id } not exist in the list");
    }


    /// <summary>
    /// Update
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(OrderItem orderItem)
    {
        if (!DataSource._orderItems.Exists(x => x?.ID == orderItem.ID))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException($"OrderItem num { orderItem.ID} not exist in the list");
        else //if the order isn exist in the list.
            DataSource._orderItems.Remove(GetById(orderItem.ID));
        DataSource._orderItems.Add(orderItem);

    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {

        if (!DataSource._orderItems.Exists(x => x?.ID == id))// check if the orderItem isn't exist in the list
            throw new DalDoesNotExistException($"OrderItem num {id} not exist in the list");
        else
            DataSource._orderItems.Remove(GetById(id));

    }

    /// <summary>
    /// A function that returns the array
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        if (func == null)
            return DataSource._orderItems.Select(x => x);
        return DataSource._orderItems.Where(x => func(x)).Select(x => x);
    }

    /// <summary>
    /// get item from the orders
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public OrderItem GetItem(Func<OrderItem?, bool>? filter)
    {
        return DataSource._orderItems.Find(x => filter(x)) ?? throw new DalDoesNotExistException("order item under this condition is not exist");
    }
}