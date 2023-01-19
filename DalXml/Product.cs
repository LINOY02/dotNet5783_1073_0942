using DalApi;
using DO;

namespace Dal
{
    internal class Product : IProduct
    {
        string s_product = "products";

        /// <summary>
        /// add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="DalAlreadyExistException"></exception>
        public int Add(DO.Product product)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            
            if (listProds.Exists(x => x?.ID == product.ID))// the product is not exist in the list
                throw new DalAlreadyExistException($"Product num {product.ID} already exist in the list");
            else// check if the product is already exist in the list
                listProds.Add(product);
            
            XMLTools.SaveListToXMLSerializer(listProds, s_product);
            return product.ID;
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);

            if (!listProds.Exists(x => x?.ID == id))// check if the product isn't exist in the list
                throw new DalDoesNotExistException($"Product num  {id}  not exist in the list");
            else //the product is exist in the list
                listProds.Remove(GetById(id));
            XMLTools.SaveListToXMLSerializer(listProds, s_product);
        }

        /// <summary>
        /// return all the product
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            if (func == null)
                return listProds.Select(x => x);
            return listProds.Where(x => func(x)).Select(x => x);
        }

        /// <summary>
        /// get product from the list by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DalDoesNotExistException"></exception>
        public DO.Product GetById(int id)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            return listProds.Find(x => x?.ID == id) ?? throw new DalDoesNotExistException($"Product num {id} not exist in the list"); // return the requested prodect
        }

        /// <summary>
        /// return product by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="DalDoesNotExistException"></exception>
        public DO.Product GetItem(Func<DO.Product?, bool>? filter)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            return listProds.FirstOrDefault(x => filter!(x)) ?? throw new DalDoesNotExistException("product under this condition is not exit");
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="DalDoesNotExistException"></exception>
        public void Update(DO.Product product)
        {
            List<DO.Product?> listProds = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_product);
            if (!listProds.Exists(x => x?.ID == product.ID))// check if the product isn't exist in the list
                throw new DalDoesNotExistException($"Product num {product.ID} not exist in the list");
            else// the product is exist in the list
            {
                listProds.Remove(GetById(product.ID));
                listProds.Add(product); //Overrunning the old object with the new 
            }
            XMLTools.SaveListToXMLSerializer(listProds, s_product);
        }
    }
}