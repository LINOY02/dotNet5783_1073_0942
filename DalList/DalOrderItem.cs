
using DO;
using DAL;
namespace DAL;

public class DalOrderItem
{
    // Create
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.NextOrderItemNumber; //Initialize the ID number of the order
        DataSource._orderItems[DataSource.numOfOI] = orderItem;// adding the product to the list
        DataSource.numOfOI++; //Increasing the number of items in the array by 1
        return orderItem.ID;
    }

    // Request
    public OrderItem GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfOI && DataSource._orderItems[i].ID != id)
            i++;
        if (i == DataSource.numOfOI)// check if the orderItem is already exist in the list
            throw new Exception("The ID is not exist");
        else // the orderItem is not exist in the list
            return DataSource._orderItems[i]; // return the requested orderItem

    }

    //request by order and product ID number
    public OrderItem GetByOidAndPid(int orderId, int productId)
    {
 
        for (int i = 0; i < DataSource.numOfOI; i++)
        {
            if (DataSource._orderItems[i].OrderID == orderId)// Checking if the item is from the given order
                if (DataSource._orderItems[i].ProductID == productId)//Checking if this is the given item from the order
                    return DataSource._orderItems[i]; // return the requested orderItem
        }
         // the orderItem is not exist in the list
            throw new Exception("The ID is not exist");
    }
    
    // Update
    public void Update(OrderItem orderItem)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfOI && orderItem.ID != DataSource._orderItems[i].ID)
            i++;
        if (i == DataSource.numOfOI)// check if the orderItem is already exist in the list
            throw new Exception("The ID is not exist");
        else //if the order isn't exist in the list.
            DataSource._orderItems[i] = orderItem; //Overrunning the old object with the new 
       
    }

    // Delete
    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfOI && id != DataSource._orderItems[i].ID)
            i++;
        if (i == DataSource.numOfOI)// check if the orderItem is already exist in the list
            throw new Exception("The ID is not exist");
        else //if the order isn't exist in the list.
        {
            DataSource.numOfOI--;//Reducing the length of the array by 1
            DataSource._orderItems[i] = DataSource._orderItems[DataSource.numOfOI];//Deleting the member by overriding with the last member
        }
        
    }

    //A function that returns the array
    public OrderItem[] GetAll()
    {
        //Creating a new array the size of the number of elements in the array
        OrderItem[] newArr = new OrderItem[DataSource.numOfOI];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfOI; i++)
        {
            newArr[i] = DataSource._orderItems[i];
        }
        return newArr;//Returning the new array
    }

    //A function that returns all items of the requested order
    public List<OrderItem> GetAllOrder(int orderId)
    {
        List<OrderItem> allOrder = new List<OrderItem>();
       for (int i = 0; i < DataSource.numOfOI; i++)
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
