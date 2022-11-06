
using DO;

namespace DAL;

public class DalProduct
{
    // Create
    public void Add(Product product)
    {
        int i = 0;
        while (i != 100 && DataSource.Products[i].ID != product.ID)
            i++;
        if (DataSource.Products[i].ID == product.ID)
            throw new Exception("The ID is already exist");
        else
            DataSource.Products[i] = product;
    }

    // Request

    public Product GetById(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.Products[i].ID != id)
            i++;
        if (DataSource.Products[i].ID == id)
            return DataSource.Products[i];
        else
            throw new Exception("The ID is not exist");
    }

    // Update

    public void Update(Product product)
    {
        int i = 0;
        while (i != 100 && DataSource.Products[i].ID != product.ID)
            i++;
        if (DataSource.Products[i].ID == product.ID)
        {
            Delete(product.ID);
            Add(product);
        }
        else
            throw new Exception("The ID is not exist");
    }

    // Delete

    public void Delete(int id)
    {
        int i = 0;
        while (i != 100 && DataSource.Products[i].ID != id)
            i++;
        if (DataSource.Products[i].ID == id)
            while (i != 99)
                DataSource.Products[i] = DataSource.Products[i+1];
        else
            throw new Exception("The ID is not exist");
    }
}
