
using DO;
namespace DAL;

public class DalOrderItem
{
    // Create
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.NextOrderItemNumber; //Initialize the ID number of the order
        DataSource.OrderItems[DataSource.numOfOI] = orderItem;// adding the product to the list
        DataSource.numOfOI++; //Increasing the number of items in the array by 1
        return orderItem.ID;
    }

    // Request
    public OrderItem GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfOI && DataSource.OrderItems[i].ID != id)
            i++;
        if (DataSource.Orders[i].ID == id)// check if the orderItem is already exist in the list
            return DataSource.OrderItems[i]; // return the requested orderItem
        else // the orderItem is not exist in the list
            throw new Exception("The ID is not exist");
    }

    //request by order and product ID number
    public OrderItem GetByOidAndPid(int orderId, int productId)
    {
 
        for (int i = 0; i < DataSource.numOfOI; i++)
        {
            if (DataSource.OrderItems[i].ID == orderId)// Checking if the item is from the given order
                if (DataSource.OrderItems[i].ProductID == productId)//Checking if this is the given item from the order
                    return DataSource.OrderItems[i]; // return the requested orderItem
        }
         // the orderItem is not exist in the list
            throw new Exception("The ID is not exist");
    }
    
    // Update
    public void Update(OrderItem orderItem)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && orderItem.ID != DataSource.OrderItems[i].ID)
            i++;
        if (orderItem.ID == DataSource.Orders[i].ID)// check if the orderItem is already exist in the list
            DataSource.OrderItems[i] = orderItem; //Overrunning the old object with the new      
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Delete
    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfOI && id != DataSource.OrderItems[i].ID)
            i++;
        if (id == DataSource.OrderItems[i].ID)// check if the orderItem is already exist in the list
        {
            DataSource.numOfOI--;//Reducing the length of the array by 1
            DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.numOfOI];//Deleting the member by overriding with the last member
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    //A function that returns the array
    public OrderItem[] getAll()
    {
        //Creating a new array the size of the number of elements in the array
        OrderItem[] newArr = new OrderItem[DataSource.numOfOI];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfOI; i++)
        {
            newArr[i] = DataSource.OrderItems[i];
        }
        return newArr;//Returning the new array
    }

    //A function that returns all items of the requested order
    public List<Product> getAllOrder(int orderId)
    {
        List<Product> allOrder = new List<Product>();
       for (int i = 0; i < DataSource.numOfOI; i++)
        {
            //If the ordered item belongs to the requested order
            if (DataSource.OrderItems[i].OrderID == orderId)
            {
                //Finding the item from the array of items
                int j = 0;
                while(j < DataSource.numOfP)
                {
                    if (DataSource.OrderItems[i].ProductID == DataSource.Products[j].ID)
                        allOrder.Add(DataSource.Products[j]);//Adding the item to the list if it is the requested item
                    j++;
                }
            }
        }
        return allOrder;//Returning the list
    }
}
