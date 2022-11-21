
using DO;
using DalApi;

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
        if (!DataSource._orderItems.Exists(x => x.ID == id))
            throw new NotExistException();
        else
            return DataSource._orderItems.Find(x => x.ID == id);

    }

    //request by order and product ID number
    public OrderItem GetByOidAndPid(int orderId, int productId)
    {
 
        for (int i = 0; i < DataSource._orderItems.Count; i++)
        {
            if (DataSource._orderItems[i].OrderID == orderId)// Checking if the item is from the given order
                if (DataSource._orderItems[i].ProductID == productId)//Checking if this is the given item from the order
                    return DataSource._orderItems[i]; // return the requested orderItem
        }
         // the orderItem is not exist in the list
        throw new NotExistException();
    }
    
    // Update
    public void Update(OrderItem orderItem)
    { 
        if (!DataSource._orderItems.Exists(x => x.ID == orderItem.ID))// check if the orderItem isn't exist in the list
            throw new NotExistException();
        else //if the order isn exist in the list.
            DataSource._orderItems.Remove(GetById(orderItem.ID));
            DataSource._orderItems.Add(orderItem);

    }

    // Delete
    public void Delete(int id)
    {

        if (!DataSource._orderItems.Exists(x => x.ID == id))// check if the orderItem isn't exist in the list
            throw new NotExistException();
        else
            DataSource._orderItems.Remove(GetById(id));

    }

    //A function that returns the array
    public IEnumerable<OrderItem> GetAll()
    {
        //Creating a new array the size of the number of elements in the array
        List<OrderItem> newOrder = new List<OrderItem>();
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource._orderItems.Count; i++)
            newOrder.Add(DataSource._orderItems[i]);

        return newOrder;
    }

    //A function that returns all items of the requested order
    public IEnumerable<OrderItem> GetAllOrder(int orderId)
    {
        List<OrderItem> allOrder = new List<OrderItem>();
       for (int i = 0; i < DataSource._orderItems.Count; i++)
        {
            //If the ordered item belongs to the requested order
            if (DataSource._orderItems[i].OrderID == orderId)
            {
                allOrder.Add(DataSource._orderItems[i]);//Adding the item to the list if it is the requested item
            }
        }
        return allOrder;//Returning the list
    }
}
