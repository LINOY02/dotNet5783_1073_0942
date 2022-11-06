using DO;
namespace DAL;

public class DalOrder
{
    // Create
    public void Add(Order order)
    {
        int i = 0;
        while(i != 100 && DataSource.Orders[i].ID != order.ID)
            i++;
        if (DataSource.Orders[i].ID == order.ID)
            throw new Exception("The ID is already exist");
        else
            DataSource.Orders[i] = order;
    }

    // Request

    public Order GetById(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.Orders[i].ID != id)
            i++;
        if (DataSource.Orders[i].ID == id)
            return DataSource.Orders[i];
        else
            throw new Exception("The ID is not exist");
    }

    // Update

    public void Update(Order order)
    {
        int i = 0;
        while (i != 100 && DataSource.Orders[i].ID != order.ID)
            i++;
        if (DataSource.Orders[i].ID == order.ID)
        {
            Delete(order.ID);
            Add(order);
        }
        else
            throw new Exception("The ID is not exist");
    }

    // Delete

    public void Delete(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.Orders[i].ID != id)
            i++;
        if (DataSource.Orders[i].ID == id)
            while (i != 99)
                DataSource.Orders[i] = DataSource.Orders[i + 1];
        else
            throw new Exception("The ID is not exist");
    }
}
