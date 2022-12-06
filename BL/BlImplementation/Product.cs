using DalApi;
using DAL;



namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// the function add a bProduct
        /// </summary>
        /// <param name="bProduct"></param>
        /// <exception cref="BO.DalAlreadyExistException"></exception>
        /// <exception cref="BO.WrongValue"></exception>
        public void AddProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000)
                throw new BO.BlInvalidInputException("The id is invalid");
            if (bProduct.Name.Length == 0)
                throw new BO.BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0)
                throw new BO.BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0)
                throw new BO.BlInvalidInputException("The amount in stock is invalid");
            if ((int)bProduct.Category < 0 || (int)bProduct.Category > 5)
                throw new BO.BlInvalidInputException("The Category is invalid");
            try
            {
                Dal.Product.Add(new DO.Product
                {
                    ID = bProduct.ID,
                    Name = bProduct.Name,
                    Price = bProduct.Price,
                    InStock = bProduct.InStock,
                    Category = (DO.Category)bProduct.Category,
                });
            }
            catch (DO.DalAlreadyExistException ex)
            {
                throw new BO.BlAlreadyExistException(ex.Message);
            }
        }

        /// <summary>
        /// the function delete a bProduct
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BO.BlDoesNotExistException"></exception>
        public void DeleteProduct(int id)
        {

            if (!Dal.OrderItem.GetAll().Where(X => X.ProductID == id).Any())
            {
                try
                {
                    Dal.Product.Delete(id);
                }
                catch (DO.DalDoesNotExistException ex)
                {
                    throw new BO.BlDoesNotExistException(ex.Message);
                }
            }
            else
                throw new BO.BlProductIsOrderedException("the product is ordered");
        }

        /// <summary>
        /// The function shows the manager the list of products,
        /// for each bProduct: number, name, price and category
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
        /// The function receives a bProduct ID number 
        /// and returns its details (for the manager)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Product GetProduct(int id)
        {
            DO.Product dProduct;
            if (id < 100000)
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                dProduct = Dal.Product.GetById(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }

            return new BO.Product
            {
                ID = dProduct.ID,
                Name = dProduct.Name,
                Price = dProduct.Price,
                Category = (BO.Category)dProduct.Category,
                InStock = dProduct.InStock,
            };
        }





        /// <summary>
        /// The buyer enters a bProduct code and receives the 
        /// bProduct details: number, name, price, category and how much in stock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></ProductItem>
        public BO.ProductItem GetDetailsItem(int id, BO.Cart cart)
        {
            DO.Product product1;
            if (id < 100000)
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                product1 = Dal.Product.GetById(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
            BO.OrderItem OrderItem = cart.Items.FirstOrDefault(x => x.ProductID == id);
            if (OrderItem == null)
                throw new BO.BlProductIsNotOrderedException("the product is not in the cart");
            BO.ProductItem productItem = new BO.ProductItem
            {
                ID = id,
                Name = product1.Name,
                Amount = OrderItem.Amount,
                Category = (BO.Category)product1.Category,
                Price = product1.Price,
                InStock = true
            };
            if (product1.InStock == 0)
                productItem.InStock = false;
            return productItem;
        }
    
          
            
            
    
        /// <summary>
        /// show the buyer a list of all the products, 
        /// for each bProduct: number, name, price, category, whether in stock and how many in stock
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductItem> GetProducts()
        {

            var inStockProduct = from DO.Product product1 in Dal.Product.GetAll()
                   where product1.InStock != 0
                   select new BO.ProductItem
                   {
                       ID = product1.ID,
                       Name = product1.Name,
                       Price = product1.Price,
                       Category = (BO.Category)product1.Category,
                       InStock = true,
                   };

            var outStockProduct = from DO.Product product1 in Dal.Product.GetAll()
                    where product1.InStock == 0
                    select new BO.ProductItem
                    {
                        ID = product1.ID,
                        Name = product1.Name,
                        Price = product1.Price,
                        Category = (BO.Category)product1.Category,
                        InStock = false,
                    };

            return inStockProduct.Union(outStockProduct);
        }
        /// <summary>
        /// The function receives bProduct details from the user and updates the bProduct in the data layer (for the manager)
        /// </summary>
        /// <param name="bProduct"></param>
        public void UpdateProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000)
                throw new BO.BlInvalidInputException("The id is invalid");
            if (bProduct.Name.Length == 0)
                throw new BO.BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0)
                throw new BO.BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0)
                throw new BO.BlInvalidInputException("The amount in stock is invalid");
            if ((int)bProduct.Category < 0 && (int)bProduct.Category > 5)
                throw new BO.BlInvalidInputException("The Category is invalid");
            try
                {
                    Dal.Product.Update(new DO.Product
                    {
                        ID = bProduct.ID,
                        Name = bProduct.Name,
                        Price = bProduct.Price,
                        InStock = bProduct.InStock,
                        Category = (DO.Category)bProduct.Category,
                    });
                }
                catch (DO.DalDoesNotExistException ex)
                {
                    throw new BO.BlDoesNotExistException(ex.Message);
                }
         }
    }
}
