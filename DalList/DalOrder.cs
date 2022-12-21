using DO;
namespace Dal;
using DalApi;

internal class DalOrder : IOrder
{
    // Create
    public int Add(Order order)
    {
        order.ID = DataSource.NextOrderNumber; //Initialize the ID number of the order
        DataSource._orders.Add(order);
        return order.ID;
    }

    // Request 
    public Order GetById(int id)
    {
      
            return DataSource._orders.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException("Order num " + id + " not exist in the list");
    }

    // Update
    public void Update(Order order)
    {

        if (!DataSource._orders.Exists(x => x?.ID == order.ID))// check if the order isn't exist in the list
            throw new DalDoesNotExistException("Order num " + order.ID + " not exist in the list");
        else// the order is exist in the list
        {
            DataSource._orders.Remove(GetById(order.ID));
            DataSource._orders.Add(order);
        }
    }

    // Delete
    public void Delete(int id)
    {
        if (!DataSource._orders.Exists(x => x?.ID == id))// check if the order is already exist in the list
            throw new DalDoesNotExistException("Order num " + id + " not exist in the list");
        else
            DataSource._orders.Remove(GetById(id));

    }

    //A function that returns the array
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        if (func == null)
            return DataSource._orders.Select(x=> x);
        return DataSource._orders.Where(x => func(x)).Select(x => x);
    }

    public Order GetItem(Func<Order?, bool>? filter)
    {
        return DataSource._orders.Find(x => filter(x)) ?? throw new DalDoesNotExistException("order under this condition is not exist");
    }
}