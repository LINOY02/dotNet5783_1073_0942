using DO;
namespace DAL;
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
        if (DataSource._orders.Exists(x => x.ID == id))// check if the order is already exist in the list
            return DataSource._orders.Find(x => x.ID == id); // return the requested order
        else // the order is exist in the list
            throw new NotExistException();
    }

    // Update
    public void Update(Order order)
    {

        if (!DataSource._products.Exists(x => x.ID == order.ID))// check if the order isn't exist in the list
            throw new NotExistException();
        else// the order is exist in the list
        {
            DataSource._orders.Remove(GetById(order.ID));
            DataSource._orders.Add(order);
        }
    }

    // Delete
    public void Delete(int id)
    {
        if (!DataSource._orders.Exists(x => x.ID == id))// check if the order is already exist in the list
            throw new NotExistException();
        else
            DataSource._orders.Remove(GetById(id));

    }

    //A function that returns the array
    public IEnumerable<Order> GetAll()
    {
        List<Order> newOrder = new List<Order>();
        for (int i = 0; i < DataSource._orders.Count; i++)
        {
            newOrder.Add(DataSource._orders[i]);
        }
        return newOrder;
    }
}