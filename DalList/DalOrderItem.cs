
using DO;

namespace DAL;

public class DalOrderItem
{
    // Create
    public void Add(OrderItem orderItem)
    {
        int i = 0;
        while (i != 100 && DataSource.OrderItems[i].ID != orderItem.ID)
            i++;
        if (DataSource.OrderItems[i].ID == orderItem.ID)
            throw new Exception("The ID is already exist");
        else
            DataSource.OrderItems[i] = orderItem;
    }

    // Request

    public OrderItem GetById(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.OrderItems[i].ID != id)
            i++;
        if (DataSource.OrderItems[i].ID == id)
            return DataSource.OrderItems[i];
        else
            throw new Exception("The ID is not exist");
    }

    // Update

    public void Update(OrderItem orderItemr)
    {
        int i = 0;
        while (i != 100 && DataSource.OrderItems[i].ID != orderItemr.ID)
            i++;
        if (DataSource.OrderItems[i].ID == orderItemr.ID)
        {
            Delete(orderItemr.ID);
            Add(orderItemr);
        }
        else
            throw new Exception("The ID is not exist");
    }

    // Delete

    public void Delete(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.OrderItems[i].ID != id)
            i++;
        if (DataSource.OrderItems[i].ID == id)
            while (i != 99)
                DataSource.OrderItems[i] = DataSource.OrderItems[i+1];
        else
            throw new Exception("The ID is not exist");
    }
}
