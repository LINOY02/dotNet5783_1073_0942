
using System;
using System.Reflection.Metadata.Ecma335;
using DalApi;
using DO;
namespace Dal;

public class DalProduct : IProduct
{
    /// <summary>
    /// add product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Add(Product product)
    {
        if (DataSource._products.Exists(x => x?.ID == product.ID))// the product is not exist in the list
            throw new DalAlreadyExistException($"Product num {product.ID} already exist in the list");
        else// check if the product is already exist in the list
            DataSource._products.Add(product);
        return product.ID;
    }

    /// <summary>
    /// get product from the list by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Product GetById(int id)
    {
      return DataSource._products.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"Product num {id} not exist in the list"); // return the requested prodect

    }

    
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
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

    
    /// <summary>
    /// delete product
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        if (!DataSource._products.Exists(x => x?.ID == id))// check if the product isn't exist in the list
            throw new DalDoesNotExistException($"Product num  { id }  not exist in the list");
        else //the product is exist in the list
            DataSource._products.Remove(GetById(id));

    }

    /// <summary>
    /// return all the product
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)

    {
        if (func == null)
            return DataSource._products.Select(x => x);
        return DataSource._products.Where(x => func(x)).Select(x => x);
    }

    /// <summary>
    /// return product by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Product GetItem(Func<Product?, bool>? filter )
    {
        
        return DataSource._products.FirstOrDefault(x => filter!(x)) ?? throw new DalDoesNotExistException("product under this condition is not exit"); 
    }
}
