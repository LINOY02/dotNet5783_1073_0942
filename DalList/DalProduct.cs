
using System;
using System.Reflection.Metadata.Ecma335;
using DalApi;
using DO;
namespace Dal;

public class DalProduct : IProduct
{
    // Create
    public int Add(Product product)
    {
        if (DataSource._products.Exists(x => x?.ID == product.ID))// the product is not exist in the list
            throw new DalAlreadyExistException($"Product num {product.ID} already exist in the list");
        else// check if the product is already exist in the list
            DataSource._products.Add(product);
        return product.ID;
    }

    // Request
    public Product GetById(int id)
    {
      return DataSource._products.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"Product num {id} not exist in the list"); // return the requested prodect

    }

    // Update

    public void Update(Product product)
    {
        if (!DataSource._products.Exists(x => x?.ID == product.ID))// check if the product isn't exist in the list
            throw new DalDoesNotExistException($"Product num { product.ID} not exist in the list");
        else// the product is exist in the list
        {
            DataSource._products.Remove(GetById(product.ID));
            DataSource._products.Add(product); //Overrunning the old object with the new 
        }
    }

    // Delete

    public void Delete(int id)
    {
        if (!DataSource._products.Exists(x => x?.ID == id))// check if the product isn't exist in the list
            throw new DalDoesNotExistException($"Product num  { id }  not exist in the list");
        else //the product is exist in the list
            DataSource._products.Remove(GetById(id));

    }

    //A function that returns the array
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)

    {
        if (func == null)
            return DataSource._products.Select(x => x);
        return DataSource._products.Where(x => func(x)).Select(x => x);
    }

    public Product GetItem(Func<Product?, bool> filter )
    {
        
        return DataSource._products.FirstOrDefault(x => filter(x)) ?? throw new DalDoesNotExistException("product under this condition is not exit"); 
    }
}
