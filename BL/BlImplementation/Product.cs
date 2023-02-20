using DalApi;
using DO;
using BO;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private static readonly IDal Dal = DalApi.Factory.Get()!;
        /// <summary>
        /// the function add a bProduct
        /// </summary>
        /// <param name="bProduct"></param>
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlAlreadyExistException"></exception>
        public void AddProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000) //check that the ID is valid
                throw new BlInvalidInputException("The id is invalid");
            if (bProduct.Name == "") //check if the name is valid
                throw new BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0) //check that the Price is valid
                throw new BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0) //check that the amount is valid
                throw new BlInvalidInputException("The amount in stock is invalid");
            if ((int)bProduct.Category! < 0 || (int)bProduct.Category > 5) //check that the Category is valid
                throw new BlInvalidInputException("The Category is invalid");
            try
            {   //add the product to the list in the DO
                Dal.Product.Add(new DO.Product
                {
                    ID = bProduct?.ID ?? throw new BlMissingInputException("The id is missing"),
                    Name = bProduct.Name ?? throw new BlMissingInputException("The name is missing"),
                    Price = bProduct?.Price ?? throw new BlMissingInputException("The price is missing"),
                    InStock = bProduct?.InStock ?? throw new BlMissingInputException("The amount is missing"),
                    Category = (DO.Category)bProduct.Category,
                    picture = bProduct.picture ?? @"\Pics\IMG.FAILS.jpg",
                });
            }
            catch (DalAlreadyExistException ex)
            {
                throw new BlAlreadyExistException(ex.Message);
            }
        }

        /// <summary>
        /// the function delete a bProduct
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BlDoesNotExistException"></exception>
        /// <exception cref="BlProductIsOrderedException"></exception>
        public void DeleteProduct(int id)
        {

            if (!Dal.OrderItem.GetAll().Where(X => X?.ProductID == id).Any()) //check if there are any orders that contains this product
            {
                try
                {   //delete the product from the DO
                    Dal.Product.Delete(id);
                }
                catch (DalDoesNotExistException ex)
                {
                    throw new BlDoesNotExistException(ex.Message);
                }
            }
            else
                throw new BlProductIsOrderedException("the product is ordered");
        }

        /// <summary>
        /// The function shows the manager the list of products,
        /// for each bProduct: number, name, price and category
        /// </summary>
        /// <returns></List of ProductsForList>
        public IEnumerable<ProductForList?> GetListedProducts(Func<ProductForList?, bool>? filter)
        {
            var list = from DO.Product product1 in Dal.Product.GetAll()
                       select new ProductForList
                       {
                           ID = product1.ID,
                           Name = product1.Name,
                           Price = product1.Price,
                           Category = (BO.Category)product1.Category!
                       };

            return filter is null ? list : list.Where(filter);
        }

        /// <summary>
        /// The function receives a bProduct ID number 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> details (for the manager)
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlDoesNotExistException"></exception>
        public BO.Product GetProduct(int id)
        {
            DO.Product dProduct;
            if (id < 100000) //check that the ID is valid
                throw new BlInvalidInputException("The id is invalid");
            try
            {
                //get the product from the Dal
                dProduct = Dal.Product.GetById(id);
            }
            catch (DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException(ex.Message);
            }
            return new BO.Product
            {
                ID = dProduct.ID,
                Name = dProduct.Name,
                Price = dProduct.Price,
                Category = (BO.Category)dProduct.Category!,
                InStock = dProduct.InStock,
                picture = dProduct.picture,
            };
        }

        /// <summary>
        /// The buyer enters a bProduct code and receives the 
        /// bProduct details: number, name, price, category and how much in stock
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlDoesNotExistException"></exception>
        /// <exception cref="BlProductIsNotOrderedException"></exception>
        public ProductItem GetDetailsItem(int id, BO.Cart cart)
        {
            DO.Product product1;
            if (id < 100000) //check that the ID is valid
                throw new BlInvalidInputException("The id is invalid");
            try
            {
                //get the product from the Dal
                product1 = Dal.Product.GetById(id);
            }
            catch (DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException(ex.Message);
            }
            BO.OrderItem? OrderItem = cart.Items?.FirstOrDefault(x => x?.ProductID == id);

            if (OrderItem == null) //check if there are any items in the cart
                throw new BlProductIsNotOrderedException("the product is not in the cart");

            return new ProductItem
            {
                ID = id,
                Name = product1.Name,
                Amount = OrderItem.Amount,
                Category = (BO.Category)product1.Category!,
                Price = product1.Price,
                InStock = checkInStock(product1),
                picture = product1.picture,
            };
        }


        //A private function that checks if the product is in stock
        private static bool checkInStock(DO.Product product1)
        {
            if (product1.InStock == 0)
                return false;
            return true;
        }

        /// <summary>
        /// The function receives bProduct details from the user and updates the bProduct in the data layer (for the manager)
        /// </summary>
        /// <param name="bProduct"></param>
        /// <exception cref="BlInvalidInputException"></exception>
        /// <exception cref="BlDoesNotExistException"></exception>
        public void UpdateProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000) //check that the ID is valid
                throw new BlInvalidInputException("The id is invalid");
            if (bProduct.Name == "") //check if the name is valid
                throw new BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0) //check that the Price is valid
                throw new BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0) //check that the Amount is valid
                throw new BlInvalidInputException("The amount in stock is invalid");
            if ((int)bProduct.Category! < 0 && (int)bProduct.Category > 5) //check that the Category is valid
                throw new BlInvalidInputException("The Category is invalid");
            try
            {
                //update the product in the list
                Dal.Product.Update(new DO.Product
                {
                    ID = bProduct?.ID ?? throw new BO.BlMissingInputException("The id is missing"),
                    Name = bProduct.Name ?? throw new BO.BlMissingInputException("The name is missing"),
                    Price = bProduct?.Price ?? throw new BO.BlMissingInputException("The price is missing"),
                    InStock = bProduct?.InStock ?? throw new BO.BlMissingInputException("The amount is missing"),
                    Category = (DO.Category)bProduct.Category,
                    picture = bProduct?.picture ?? @"\Pics\IMG.FAILS.jpg",
                }) ;
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }

        /// <summary>
        /// All products are shown according to the selected category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ProductForList?> BlApi.IProduct.GetListedProductsByCategory(BO.Category category)
        {
            return GetListedProducts(p => p?.Category == category);
        }

        /// <summary>
        /// All productItems are shown according to the selected category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ProductItem?> BlApi.IProduct.GetProductItemsByCategory(BO.Cart cart, BO.Category category)
        {
            return GetProductItems(cart, p => p?.Category == category);
        }

        /// <summary>
        /// The function shows the customerr the list of products,
        /// for each product: number, name, price and category
        /// </summary>
        /// <returns></List of ProductsForList>
        public IEnumerable<ProductItem?> GetProductItems( BO.Cart cart, Func<ProductItem?, bool>? filter )
        {
            var list = from DO.Product product1 in Dal.Product.GetAll()
                       select new ProductItem
                       {
                           ID = product1.ID,
                           Name = product1.Name,
                           Price = product1.Price,
                           Category = (BO.Category)product1.Category!,
                           InStock = checkInStock(product1),
                           picture  = product1.picture ?? @"\Pics\IMG.FAILS.jpg",
                           Amount = AmountInCart(cart, product1.ID),
                       };

            return filter is null ? list : list.Where(filter);
        }

        /// <summary>
        /// func the return the amount of the product in the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private int AmountInCart(BO.Cart cart, int id)
        {
            if (cart == null)
                return 0;
            var productItem = cart.Items?.FirstOrDefault(x => x?.ProductID == id);
            return productItem != null ? productItem.Amount : 0;
        }

        /// <summary>
        /// the func return the top 10 popular products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BlDoesNotExistException"></exception>
        public IEnumerable<ProductItem?> MostPopular(BO.Cart cart)
        {
            //Grouping all ordered products by product ID
            var productList = from item in Dal.OrderItem.GetAll()
                              group item by item?.ProductID into groupPopular
                              select new {id = groupPopular.Key, Items = groupPopular};

            //Sort the products in descending order according to the quantity ordered
            //Take the first 10
            productList = productList.OrderByDescending(x => x.Items.Count()).Take(10);

            return from item in productList
                   let p= Dal.Product.GetById(item?.id ?? throw new BlDoesNotExistException("product doe not exist"))
                   select new ProductItem
                   {
                       ID = p.ID,
                       Name = p.Name,
                       Price =p.Price,
                       Category= (BO.Category)p.Category!,
                       picture = p.picture,
                       Amount = AmountInCart(cart, p.ID),
                       InStock = checkInStock(p)
                   };
        }

        /// <summary>
        /// the func return the top 10 expensive products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BlDoesNotExistException"></exception>
        public IEnumerable<ProductItem?> MostExpensive(BO.Cart cart)
        {
            //Sort the products in descending order by price
            //Take the first 10
            var productList = Dal.Product.GetAll().OrderByDescending(x => x?.Price).Take(10);

            return from DO.Product item in productList
                   select new ProductItem
                   {
                       ID = item.ID,
                       Name = item.Name,
                       Price = item.Price,
                       Category = (BO.Category)item.Category!,
                       picture = item.picture,
                       Amount = AmountInCart(cart, item.ID),
                       InStock= checkInStock(item)
                   };
        }

        /// <summary>
        /// the func return the top 10 cheap products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BlDoesNotExistException"></exception>
        public IEnumerable<ProductItem?> MostCheap(BO.Cart cart)
        {

            //Sort the products in descending order by price
            //Take the last 10
            var productList = Dal.Product.GetAll().OrderByDescending(x => x?.Price).TakeLast(10);

            return from DO.Product item in productList
                   select new ProductItem
                   {
                       ID = item.ID,
                       Name = item.Name,
                       Price = item.Price,
                       Category = (BO.Category)item.Category!,
                       picture = item.picture,
                       Amount = AmountInCart(cart, item.ID),
                       InStock = checkInStock(item) 
                   };
        }
    }
}