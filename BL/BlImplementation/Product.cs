using DalApi;
using DAL;



namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// the function add a product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.DalAlreadyExistException"></exception>
        /// <exception cref="BO.WrongValue"></exception>
        public void AddProduct(BO.Product product)
        {
            DO.Product product1 = new DO.Product();
            if (product.ID > 0 && product.Name != null && product.Name != "" && product.Price > 0 && product.InStock > 0)
            {
                product1.ID = product.ID;
                product1.Name = product.Name;
                product1.Price = product.Price;
                product1.InStock = product.InStock;
                try
                {
                    Dal.Product.Add(product1);
                }
                catch (DO.DalAlreadyExistException ex)
                {
                    throw new BO.DalAlreadyExistException(ex.Message);
                }
            }
            else
                throw new BO.WrongValue();
        }
        /// <summary>
        /// the function delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BO.DalDoesNotExistException"></exception>
        public void DeleteProduct(int id)
        {
            if (Dal.OrderItem.GetAll().Where(X => X.ProductID == id).Any())
            {
                try
                {
                    Dal.Product.Delete(id);
                }
                catch (DO.DalDoesNotExistException ex)
                {
                    throw new BO.DalDoesNotExistException(ex.Message);
                }
            }
            else
                throw new BO.DalAlreadyExistException();
        }

        /// <summary>
        /// The function shows the manager the list of products,
        /// for each product: number, name, price and category
        /// </summary>
        /// <returns></List of ProductsForList>
        public IEnumerable<BO.ProductForList> GetListedProducts()
        {
            return from DO.Product product1 in Dal.Product.GetAll()
                   select new BO.ProductForList
                   {
                       ID = product1.ID,
                       Name = product1.Name,
                       Price = product1.Price,
                       Category = (BO.Category)product1.Category,
                   };
        }
        /// <summary>
        /// The function receives a product ID number 
        /// and returns its details (for the manager)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Product GetProduct(int id)
        {
            BO.Product product = new BO.Product();
            DO.Product product1;
            try
            {
                product1 = Dal.Product.GetById(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.DalDoesNotExistException(ex.Message);
            }
            if (id > 0)
            {
                product.ID = product1.ID;
                product.Name = product1.Name;
                product.Price = product1.Price;
                product.Category = (BO.Category)product1.Category;
            }
            else
                throw new BO.WrongValue();
            return product;
        }
        /// <summary>
        /// The buyer enters a product code and receives the 
        /// product details: number, name, price, category and how much in stock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></ProductItem>
        public BO.ProductItem GetDetailsItem(int id, BO.Cart cart)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            DO.Product product1;
            try
            {
                product1 = Dal.Product.GetById(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.DalDoesNotExistException(ex.Message);
            }
            if (id > 0)
            {
                productItem.ID = product1.ID;
                productItem.Name = product1.Name;
                productItem.Price = product1.Price;
                productItem.Category = (BO.Category)product1.Category;
            }
            BO.OrderItem orderItem = cart.Items.FirstOrDefault(x => x.OrderID == id)!;
            if (orderItem is not null)
            {
                productItem.Amount = orderItem.Amount;
            }
            if (product1.InStock > 0)
            {
                productItem.InStock = true;
            }
            else
                throw new BO.WrongValue();
            return productItem;
        }
        /// <summary>
        /// show the buyer a list of all the products, 
        /// for each product: number, name, price, category, whether in stock and how many in stock
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductItem> GetProducts()
        {
            IEnumerable<DO.Product> products = Dal.Product.GetAll();
            return (IEnumerable<BO.ProductItem>)products.Select(p => new BO.ProductForList { ID = p.ID, Name = p.Name, Price = p.Price, });
        }
        /// <summary>
        /// The function receives product details from the user and updates the product in the data layer (for the manager)
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(BO.Product product)
        {
            DO.Product product1;
            try
            {
                product1 = Dal.Product.GetById(product.ID);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.DalDoesNotExistException(ex.Message);
            }
            if (product.ID > 0 && product.Name != null && product.Price > 0 && product.InStock > 0)
            {
                product.ID = product1.ID;
                product.Name = product1.Name;
                product.Price = product1.Price;
                product.Category = (BO.Category)product1.Category;
                try
                {
                    Dal.Product.Update(product1);
                }
                catch (DO.DalDoesNotExistException ex)
                {
                    throw new BO.DalDoesNotExistException(ex.Message);
                }
            }
            else
                throw new BO.WrongValue();
        }
    }
}
