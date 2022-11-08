
using DO;

namespace DAL;

public class DalProduct
{
    // Create
    public void Add(Product product)
    {
        if (DataSource.Products.Exists(it => it.ID == product.ID)) // check if the product is already exist in the list
            throw new Exception("The ID is already exist");
        else // the product is not exist in the list
            DataSource.Products.Add(product);// adding the product to the list
    }

    // Request

    public Product GetById(int id)
    {
        if (DataSource.Products.Exists(it => it.ID == id)) // check if the product is already exist in the list
            return DataSource.Products.Find(it => it.Equals(id)); // return the requested prodect
        else // the product is not exist in the list
            throw new Exception("The ID is not exist");
    }

    // Update

    public void Update(Product product)
    {
        if (DataSource.Products.Exists(it => it.ID == product.ID)) // check if the product is already exist in the list
        {
            Product temp = DataSource.Products.Find(it => it.ID == product.ID);// find the product in the list
            DataSource.Products.Remove(temp);// delete the old product from the list
            DataSource.Products.Add(product);// add the new product to the list
        }
        else// the product is not exist in the list
            throw new Exception("The ID is not exist");
    }

    // Delete

    public void Delete(int id)
    {
        if (DataSource.Products.Exists(it => it.ID == id)) // check if the product is already exist in the list
        {
            Product temp = DataSource.Products.Find(it => it.ID == id);// find the product in the list
            DataSource.Products.Remove(temp);// delete the old product from the list
        }
        else// the product is not exist in the list
            throw new Exception("The ID is not exist");
    }
}
