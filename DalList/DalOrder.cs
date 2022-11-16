using DO;
namespace DAL;

public class DalOrder
{
    // Create
    public int Add(Order order)
    {
       order.ID = DataSource.NextOrderNumber; //Initialize the ID number of the order
       DataSource._orders[DataSource.numOfO] = order;// adding the order to the list
        DataSource.numOfO++; //Increasing the number of items in the array by 1
       return order.ID;
    }

    // Request 
    public Order GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && DataSource._orders[i].ID != id)
            i++;
        if (i == DataSource.numOfO)// check if the order is already exist in the list
            throw new Exception("The ID is not exist");
        else // the order is not exist in the list
            return DataSource._orders[i]; // return the requested order
    }


    // Update
    public void Update(Order order)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && order.ID != DataSource._orders[i].ID)
            i++;
        if (i == DataSource.numOfO)// check if the order is already exist in the list
            throw new Exception("The ID is not exist");
        else //if the order isn't exist in the list.
             DataSource._orders[i] = order; //Overrunning the old object with the new    
    }

    // Delete
    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && id != DataSource._orders[i].ID)
            i++;
        if (i == DataSource.numOfO)// check if the order is already exist in the list
            throw new Exception("The ID is not exist");
        else //if the order isn't exist in the list.
        {
            DataSource.numOfO--;//Reducing the length of the array by 1
            DataSource._orders[i] = DataSource._orders[DataSource.numOfO];//Deleting the member by overriding with the last member
        }
        
    }

    //A function that returns the array
    public Order[] GetAll()
    {
        //Creating a new array the size of the number of elements in the array
        Order[] newArr = new Order[DataSource.numOfO];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfO; i++)
        {
            newArr[i] = DataSource._orders[i];
        }
        return newArr;//Returning the new array
    }

}
