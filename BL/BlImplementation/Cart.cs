using System.ComponentModel.DataAnnotations;
using BlApi;
using DalApi;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        private static readonly IDal Dal = DalApi.Factory.Get()!;

        /// <summary>
        /// Adding a product to the shopping cart by the customer
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public BO.Cart AddProductToCart(BO.Cart cart, int productId)
        {
            try
            {
                DO.Product product = Dal.Product.GetById(productId);
                if (product.InStock < 1) //Check if the product is out of stock
                    throw new BO.BlOutOfStockException("There is not enough in the stock");
                    var bOrderItem = cart.Items?.FirstOrDefault(x => x?.ProductID == productId);
                if (bOrderItem != null) //Checking if the product is in the cart
                {
                    if (product.InStock < bOrderItem.Amount + 1) //Check if the product is out of stock
                        throw new BO.BlOutOfStockException("There is not enough in the stock");
                    cart.Items?.Remove(bOrderItem);
                    bOrderItem.Amount++;
                    bOrderItem.TotalPrice += product.Price;
                    cart.Items?.Add(bOrderItem);
                    cart.TotalPrice += product.Price;
                }
                else// the product is not in the cart
                {
                    //Adding a product to the shopping cart
                    var newItem = new BO.OrderItem
                    {
                        ProductID = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Amount = 1,
                        TotalPrice = product.Price,
                        picture = product.picture,
                    };
                    cart.Items?.Add(newItem);
                    cart.TotalPrice += product.Price;
                }
               return cart;
            }
            
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }

        public BO.Cart DeleteProductFromCart(BO.Cart cart, int productId)
        {
            DO.Product product = Dal.Product.GetById(productId);
             var bOrderItem = cart.Items?.FirstOrDefault(x => x?.ProductID == productId);
            if (bOrderItem != null) //Checking if the product is in the cart
            {
                cart.Items?.Remove(bOrderItem);
                cart.TotalPrice -= product.Price*bOrderItem.Amount;
                product.InStock -= bOrderItem.Amount;
            }
            else// the product is not in the cart
            {
                throw new BO.BlAlreadyExistException("product is not exist in the cart");
            }
            return cart;
        }

        /// <summary>
        /// make an order
        /// </summary>
        /// <param name=Cart></param>
        public int OrderCart(BO.Cart cart)
        {
            //Checking the correctness of the values
            if (cart.CustomerName == " ")
                throw new BO.BlInvalidInputException("Missing customer name");
            if (cart.CustomerAddress == " ")
                throw new BO.BlInvalidInputException("Missing customer address");
            if (! new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                throw new BO.BlInvalidInputException("Missing customer Email");
            if (cart.Items?.Count == 0)
                throw new BO.BlProductIsNotOrderedException("There are no products in the cart");
            //Create a new order
            DO.Order newOrder = new DO.Order
            {
                CustomerName = cart.CustomerName,
                CustomerAdress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null,
            };
            try
            {
                //Adding the order to the list and receiving an ID number
                int orderID = Dal.Order.Add(newOrder);

                //Adding the products in the cart to the order item list
                cart.Items?.ForEach(item =>
                Dal?.OrderItem.Add(new DO.OrderItem
                {
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    Amount = item.Amount,
                    Price = item.Price,
                }));

                //Temporary list of products after the new stock update
                var products = from BO.OrderItem item in cart.Items!
                               let updateProduct = Dal.Product.GetById(item.ProductID)
                               select new DO.Product
                               {
                                   ID = updateProduct.ID,
                                   Name = updateProduct.Name,
                                   Category = updateProduct.Category,
                                   Price = updateProduct.Price,
                                   InStock = updateProduct.InStock - item.Amount,
                                   picture = updateProduct.picture,
                               };

                //Updating the products in the product list
                products.ToList().ForEach(item => Dal.Product.Update(item));
                return orderID;
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
           
        }

        /// <summary>
        /// Updating the quantity of a product from the shopping cart
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public BO.Cart UpdateCart(BO.Cart cart, int productId, int amount)
        {
            try
            {
                DO.Product? product = Dal?.Product.GetById(productId);
                // check if the product in the cart
                var cartProduct = cart.Items?.FirstOrDefault(x => x?.ProductID == productId);
                if (cartProduct == null)
                    throw new BO.BlProductIsNotOrderedException("product not in the cart");
                else
                    cart.Items?.Remove(cartProduct);
                // In case the customer wants to increase the quantity 
                if (amount == 0)
                    cart.Items?.Remove(cartProduct);
                if (amount > cartProduct.Amount)
                {
                    if (amount - cartProduct.Amount > product?.InStock)//Check if in stock
                        throw new BO.BlOutOfStockException("There is not enough in the stock");
                    //adding the difference
                    cartProduct.TotalPrice += (amount- cartProduct.Amount)* cartProduct.Price;
                    cart.TotalPrice += (amount - cartProduct.Amount) * cartProduct.Price;
                    //Update the new quantity
                    cartProduct.Amount = amount;
                    cart.Items?.Add(cartProduct);
                }
                // In case the customer wants to reduce the quantity
                if (amount < cartProduct.Amount)
                {
                    //lowering the difference
                    cartProduct.TotalPrice -= (cartProduct.Amount - amount) * cartProduct.Price;
                    cart.TotalPrice -= (cartProduct.Amount - amount) * cartProduct.Price;
                    //Update the new quantity
                    cartProduct.Amount = amount;
                    cart.Items?.Add(cartProduct);
                }
                //In case the customer wants to remove the product from the cart
                return cart;
            }
            catch (DO.DalDoesNotExistException exc)
            { 

                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }
    }
}
