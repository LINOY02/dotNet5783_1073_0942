using DalApi;
using DAL;
using DO;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// the function add a bProduct
        /// </summary>
        /// <param name="bProduct"></param>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlAlreadyExistException"></exception>
        public void AddProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000) //check that the ID is valid
                throw new BO.BlInvalidInputException("The id is invalid");
            if (bProduct.Name.Length == 0) //check that the Name is valid
                throw new BO.BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0) //check that the Price is valid
                throw new BO.BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0) //check that the amount is valid
                throw new BO.BlInvalidInputException("The amount in stock is invalid");
           if ((int)bProduct.Category < 0 || (int)bProduct.Category > 5) //check that the Category is valid
                throw new BO.BlInvalidInputException("The Category is invalid");
            try
            {   //add the product to the list in the DO
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
        /// <exception cref="BO.BlProductIsOrderedException"></exception>
        public void DeleteProduct(int id)
        {

            if (!Dal.OrderItem.GetAll().Where(X => X.ProductID == id).Any()) //check if there are any orders that contains this product
            {
                try
                {   //delete the product from the DO
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
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> details (for the manager)
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlDoesNotExistException"></exception>
        public BO.Product GetProduct(int id)
        {
            DO.Product dProduct;
            if (id < 100000) //check that the ID is valid
                throw new BO.BlInvalidInputException("The id is invalid");    
            try
            {
                //get the product from the Dal
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
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlDoesNotExistException"></exception>
        /// <exception cref="BO.BlProductIsNotOrderedException"></exception>
        public BO.ProductItem GetDetailsItem(int id, BO.Cart cart)
        {
            DO.Product product1;
            if (id < 100000) //check that the ID is valid
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                //get the product from the Dal
                product1 = Dal.Product.GetById(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
            BO.OrderItem OrderItem = cart.Items.FirstOrDefault(x => x.ProductID == id);

            if (OrderItem == null) //check if there are any items in the cart
                throw new BO.BlProductIsNotOrderedException("the product is not in the cart");

            return new BO.ProductItem
            {
                ID = id,
                Name = product1.Name,
                Amount = OrderItem.Amount,
                Category = (BO.Category)product1.Category,
                Price = product1.Price,
                InStock = checkInStock(product1)
            };
        }

        
        //A private function that checks if the product is in stock
        private bool checkInStock(DO.Product product1)
        {
            if(product1.InStock == 0)
                return false;
            return true;
        }

        /// <summary>
        /// The function receives bProduct details from the user and updates the bProduct in the data layer (for the manager)
        /// </summary>
        /// <param name="bProduct"></param>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlDoesNotExistException"></exception>
        public void UpdateProduct(BO.Product bProduct)
        {
            if (bProduct.ID < 100000) //check that the ID is valid
                throw new BO.BlInvalidInputException("The id is invalid");
            if (bProduct.Name.Length == 0) //check that the Name is valid
                throw new BO.BlInvalidInputException("The name is invalid");
            if (bProduct.Price <= 0) //check that the Price is valid
                throw new BO.BlInvalidInputException("The price is invalid");
            if (bProduct.InStock < 0) //check that the Amount is valid
                throw new BO.BlInvalidInputException("The amount in stock is invalid");
            if ((int)bProduct.Category < 0 && (int)bProduct.Category > 5) //check that the Category is valid
                throw new BO.BlInvalidInputException("The Category is invalid");
            try
            {
                //update the product in the list
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