using DO;
namespace DAL;

public class DalOrder
{
    // Create
    public void Add(Order order)
    {
        if (DataSource.Orders.Exists(it => it.ID == order.ID)) //Check if the order is already exist in the list.
            throw new Exception("The ID is already exist");
        else //if the order isn't exist in the list.
            DataSource.Orders.Add(order); //Adding the order to the list.
    }

    // Request
    public Order GetById(int id)
    {
        if (DataSource.Orders.Exists(it => it.ID == id)) //Check if the order is already exist in the list.
            return DataSource.Orders.Find(it => it.Equals(id)); //Return the request order
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Update
    public void Update(Order order)
    {
        if (DataSource.Orders.Exists(it => it.ID == order.ID)) //Check if the order is already exist in the list.
        {
           Order temp = DataSource.Orders.Find(it => it.ID == order.ID); //find the order in the list.
            DataSource.Orders.Remove(temp); //remove the order.
            DataSource.Orders.Add(order); //update the new order.
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Delete
    public void Delete(int id)
    {
        if (DataSource.Orders.Exists(it => it.ID == id)) //Check if the order is already exist in the list.
        {
            Order temp = DataSource.Orders.Find(it => it.ID == id); //find the order in the list.
            DataSource.Orders.Remove(temp); //remove the order.
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }
}
