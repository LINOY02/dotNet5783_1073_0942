using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class Product : IProduct
    {
        const string s_product = "products";

        static DO.Product? createProductFromXElement(XElement prod)
        {
            return new DO.Product()
            {
                ID = prod.ToIntNullable("ID") ?? throw new FormatException("id"),
                Name = (string?)prod.Element("Name"),
                Price = prod.ToDoubleNullable("Price") ?? throw new FormatException("price"),
                Category = prod.ToEnumNullable<DO.Category>("Category"),
                InStock = prod.ToIntNullable("InStock") ?? throw new FormatException("in stock"),
                picture = (string?)prod.Element("picture"),
            };
        }
        

        /// <summary>
        /// add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="DalAlreadyExistException"></exception>
        public int Add(DO.Product product)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
            XElement? prod = (from p in productsRootElem.Elements()
                              where p.ToIntNullable("ID") == product.ID
                              select p).FirstOrDefault();

            if (prod != null)// check if the product is already exist in the list
                throw new DalAlreadyExistException($"Product num {product.ID} already exist in the list");
            // the product is not exist in the list
            XElement productElem = new XElement("Product",
                                             new XElement("ID", product.ID),
                                             new XElement("Name", product.Name),
                                             new XElement("Price", product.Price),
                                             new XElement("InStock", product.InStock),
                                             new XElement("Category", product.Category)
                                             );
            productsRootElem.Add(productElem);
            XMLTools.SaveListToXMLElement(productsRootElem, s_product);
            return product.ID;
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

            XElement? prod = (from p in productsRootElem.Elements()
                              where p.ToIntNullable("ID") == id
                              select p).FirstOrDefault() ?? throw new DalDoesNotExistException($"Product num  {id}  not exist in the list");// check if the product isn't exist in the list

           //the product is exist in the list
            prod.Remove();
            XMLTools.SaveListToXMLElement(productsRootElem, s_product);
        }

        /// <summary>
        /// return all the product
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
            if (filter == null)
            {
                return from prod in productsRootElem.Elements()
                       select createProductFromXElement(prod);
            }
            return from prod in productsRootElem.Elements()
                   let doProd = createProductFromXElement(prod)
                   where filter(doProd)
                   select doProd;
        }

       

        /// <summary>
        /// get product from the list by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DalDoesNotExistException"></exception>
        public DO.Product GetById(int id)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
            return (from prod in productsRootElem.Elements()
                    let doProd = createProductFromXElement(prod)
                    where prod.ToIntNullable("ID") == id
                    select doProd).FirstOrDefault() ?? throw new DalDoesNotExistException($"Product num {id} not exist in the list"); // return the requested prodect
        }

        /// <summary>
        /// return product by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="DalDoesNotExistException"></exception>
        public DO.Product GetItem(Func<DO.Product?, bool>? filter)
        {
            if (filter != null)
            {
                XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
                return (from prod in productsRootElem.Elements()
                        let doProd = createProductFromXElement(prod)
                        where filter(doProd)
                        select doProd).FirstOrDefault() ?? throw new DalDoesNotExistException("product under this condition is not exit");
            }
            throw new DalDoesNotExistException("no filter was found");
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="DalDoesNotExistException"></exception>
        public void Update(DO.Product product)
        {
            Delete(product.ID);
            Add(product);
        }
    }
}