
using DO;
namespace DAL;

public class DalProduct
{
    // Create
    public int Add(Product product)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfP && product.ID != DataSource.Products[i].ID)
              i++;
        if (product.ID == DataSource.Products[i].ID)// check if the product is already exist in the list
            throw new Exception("The ID is already exist");
        else // the product is not exist in the list
        {
            DataSource.Products[DataSource.numOfP] = product;// adding the product to the list
            DataSource.numOfP++; //Increasing the number of items in the array by 1
        }
        return product.ID;
    }

    // Request

    public Product GetById(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i < DataSource.numOfP &&  DataSource.Products[i].ID != id)
            i++;
        if (DataSource.Products[i].ID == id)// check if the product is already exist in the list
            return DataSource.Products[i]; // return the requested prodect
        else // the product is not exist in the list
            throw new Exception("The ID is not exist");
    }

    // Update

    public void Update(Product product)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i <= DataSource.numOfP && DataSource.Products[i].ID != product.ID)
            i++;
        if (DataSource.Products[i].ID == product.ID)// check if the product is already exist in the list
            DataSource.Products[i] = product; //Overrunning the old object with the new   
        else// the product is not exist in the list
            throw new Exception("The ID is not exist");
    }

    // Delete

    public void Delete(int id)
    {
        int i = 0;
        //Go through the entire list until it ends or we have found the item
        while (i <= DataSource.numOfP && DataSource.Products[i].ID != id)
            i++;
        if (DataSource.Products[i].ID == id)// check if the product is already exist in the list
        {
            DataSource.numOfP--; //Reducing the length of the array by 1
            DataSource.Products[i] = DataSource.Products[DataSource.numOfP];//Deleting the member by overriding with the last member
        }
        else// the product is not exist in the list
            throw new Exception("The ID is not exist");
    }

    //A function that returns the array
    public Product[] getAll()
    {
        //Creating a new array the size of the number of elements in the array
        Product[] newArr = new Product[DataSource.numOfP];
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource.numOfP; i++)
        {
            newArr[i] = DataSource.Products[i];
        }
        return newArr;//Returning the new array
    }
}
