
using DO;
namespace DAL;

public class DalOrderItem
{
    // Create
    public void Add(OrderItem orderItem)
    {
        if (DataSource.OrderItems.Exists(it => it.ID == orderItem.ID)) //Check if the order is already exist in the list.
            throw new Exception("The ID is already exist");
        else //if the order isn't exist in the list.
            DataSource.OrderItems.Add(orderItem); //Adding the order to the list.
    }

    // Request
    public OrderItem GetById(int id)
    {
        if (DataSource.OrderItems.Exists(it => it.ID == id)) //Check if the order is already exist in the list.
            return DataSource.OrderItems.Find(it => it.Equals(id)); //Return the request order
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Update
    public void Update(OrderItem orderItem)
    {
        if (DataSource.OrderItems.Exists(it => it.ID == orderItem.ID)) //Check if the order is already exist in the list.
        {
            OrderItem temp = DataSource.OrderItems.Find(it => it.ID == orderItem.ID); //find the order in the list.
            DataSource.OrderItems.Remove(temp); //remove the order.
            DataSource.OrderItems.Add(orderItem); //update the new order.
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Delete
    public void Delete(int id)
    {
        if (DataSource.OrderItems.Exists(it => it.ID == id)) //Check if the order is already exist in the list.
        {
            OrderItem temp = DataSource.OrderItems.Find(it => it.ID == id); //find the order in the list.
            DataSource.OrderItems.Remove(temp); //remove the order.
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    public List<Product> ListOfProduct(int id)
    {
        List<Product> list = new List<Product>();
        
    }
}
