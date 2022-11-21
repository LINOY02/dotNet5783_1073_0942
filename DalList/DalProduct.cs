
using DalApi;
using DO;
namespace DAL;

public class DalProduct : IProduct
{
    // Create
    public int Add(Product product)
    {
        if (DataSource._products.Exists(x => x.ID == product.ID))// the product is not exist in the list
            throw new ExistException();
        else// check if the product is already exist in the list
            DataSource._products.Add(product);
            return product.ID;
    }

    // Request
    public Product GetById(int id)
    {
        if (!DataSource._products.Exists(x => x.ID == id))// check if the product is already exist in the list
            throw new NotExistException();
        else // the product is not exist in the list
            return DataSource._products.Find(x => x.ID == id); // return the requested prodect
        
    }

    // Update

    public void Update(Product product)
    {
        if (!DataSource._products.Exists(x => x.ID == product.ID))// check if the product isn't exist in the list
            throw new NotExistException();
        else// the product is exist in the list
        {
            DataSource._products.Remove(GetById(product.ID));
            DataSource._products.Add(product); //Overrunning the old object with the new 
        }
    }

    // Delete

    public void Delete(int id)
    {
        if (!DataSource._products.Exists(x => x.ID == id))// check if the product isn't exist in the list
            throw new NotExistException();
        else //the product is exist in the list
            DataSource._products.Remove(GetById(id)); 
        
    }

    //A function that returns the array
    public IEnumerable<Product> GetAll()

    {
        //Creating a new list 
        List<Product> NewProducts = new List<Product>();
        //Going over the whole array and copying it to the new array
        for (int i = 0; i < DataSource._products.Count; i++)
            NewProducts.Add(DataSource._products[i]);

        return NewProducts;
    }
}
