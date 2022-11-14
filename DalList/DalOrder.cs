using DO;
namespace DAL;

public class DalOrder
{
    // Create
    public int Add(Order order)
    {
       order.ID = DataSource.Config.NextOrderNumber; //Initialize the ID number of the order
       DataSource.Orders[DataSource.numOfO] = order;// adding the order to the list
        DataSource.numOfO++; //Increasing the number of items in the array by 1
       return order.ID;
    }

    // Request
    public Order GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && DataSource.Orders[i].ID != id)
            i++;
        if (DataSource.Orders[i].ID == id)// check if the order is already exist in the list
            return DataSource.Orders[i]; // return the requested order
        else // the order is not exist in the list
            throw new Exception("The ID is not exist");
    }

    // Update
    public void Update(Order order)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && order.ID != DataSource.Orders[i].ID)
            i++;
        if (order.ID == DataSource.Orders[i].ID)// check if the order is already exist in the list
            DataSource.Orders[i] = order; //Overrunning the old object with the new      
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    // Delete
    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfO && id != DataSource.Orders[i].ID)
            i++;
        if (id == DataSource.Orders[i].ID)// check if the order is already exist in the list
        {
            DataSource.numOfO--;//Reducing the length of the array by 1
            DataSource.Orders[i] = DataSource.Orders[DataSource.numOfO];//Deleting the member by overriding with the last member
        }
        else //if the order isn't exist in the list.
            throw new Exception("The ID is not exist");
    }

    //A function that returns the array
    public Order[] getAll()
    {
        //Creating a new array the size of the number of elements in the array
        Order[] newArr = new Order[DataSource.numOfO];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfO; i++)
        {
            newArr[i] = DataSource.Orders[i];
        }
        return newArr;//Returning the new array
    }

}
