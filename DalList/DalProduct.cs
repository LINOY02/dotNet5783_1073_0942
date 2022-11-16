
using DO;
namespace DAL;

public class DalProduct
{
    // Create
    public int Add(Product product)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfP ) //&& product.ID != DataSource.Products[i].ID)
              i++;
        if (i == DataSource.numOfP)// the product is not exist in the list
        {
            DataSource._products[DataSource.numOfP] = product;// adding the product to the list
            DataSource.numOfP++; //Increasing the number of items in the array by 1
            return product.ID;
        }
        else// check if the product is already exist in the list
            throw new Exception("The ID is already exist");

    }

    // Request

    public Product GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfP &&  DataSource._products[i].ID != id)
            i++;
        if (i == DataSource.numOfP)// check if the product is already exist in the list
            throw new Exception("The ID is not exist");
        else // the product is not exist in the list
            return DataSource._products[i]; // return the requested prodect
        
    }

    // Update

    public void Update(Product product)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i <= DataSource.numOfP && DataSource._products[i].ID != product.ID)
            i++;
        if (i == DataSource.numOfP)// check if the product is already exist in the list
           throw new Exception("The ID is not exist");
        else// the product is not exist in the list
            DataSource._products[i] = product; //Overrunning the old object with the new 
        
    }

    // Delete

    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i <= DataSource.numOfP && DataSource._products[i].ID != id)
            i++;
        if (i == DataSource.numOfP)// check if the product is already exist in the list
            throw new Exception("The ID is not exist");
        else// the product is not exist in the list
        {
            DataSource.numOfP--; //Reducing the length of the array by 1
            DataSource._products[i] = DataSource._products[DataSource.numOfP];//Deleting the member by overriding with the last member
        }
        
    }

    //A function that returns the array
    public Product[] GetAll()

    {
        //Creating a new array the size of the number of elements in the array
        Product[] newArr = new Product[DataSource.numOfP];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfP; i++)
        {
            newArr[i] = DataSource._products[i];
        }
        return newArr;//Returning the new array
    }
}
