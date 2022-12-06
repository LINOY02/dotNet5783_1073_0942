using System.ComponentModel.DataAnnotations;
using BlApi;
using DAL;
using DalApi;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        private IDal Dal = new DalList();

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
                    throw new Exception("There is not enough in the stock");
                    var bOrderItem = cart.Items.FirstOrDefault(x => x.ProductID == productId);
                if (bOrderItem != null) //Checking if the product is in the cart
                {
                    bOrderItem.Amount++;
                    bOrderItem.TotalPrice += product.Price;
                    cart.TotalPrice += product.Price;

                }
                else// the product is not in the cart
                {
                    //Adding a product to the shopping cart
                    var newItem = new BO.OrderItem
                    {
                        OrderID = Dal.Order.GetAll().Last().ID + 1,
                        ProductID = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Amount = 1,
                        TotalPrice = product.Price,
                    };
                    cart.Items.Add(newItem);
                    cart.TotalPrice += product.Price;
                }
               return cart;
            }
            
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        public void OrderCart(BO.Cart cart)
        {
            if (cart.CustomerName == null)
                throw new Exception("Missing customer name");
            if (cart.CustomerAddress == null)
                throw new Exception("Missing customer address");
            if (! new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                throw new Exception("Missing customer Email");
            if (cart.Items.Count == 0)
                throw new Exception("There are no products in the cart");
            DO.Order newOrder = new DO.Order
            {
                CustomerName = cart.CustomerAddress,
                CustomerAdress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue,
            };

            int orderID = Dal.Order.Add(newOrder);

            foreach (var item in cart.Items)
            {
                Dal.OrderItem.Add(new DO.OrderItem
                {
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    Amount = item.Amount,
                    Price = item.Price,
                });

                DO.Product updateProduct = Dal.Product.GetById(item.ProductID);
                updateProduct.InStock -= item.Amount;
                Dal.Product.Update(updateProduct);
            }

        }

        public BO.Cart UpdateCart(BO.Cart cart, int productId, int amount)
        {
            try
            {
                DO.Product product = Dal.Product.GetById(productId);
                var cartP = cart.Items.FirstOrDefault(x => x.ProductID == productId);
                if (cartP == null)
                    throw new Exception("product not in the cart");
                if(amount > cartP.Amount)
                {
                    if (amount - cartP.Amount < 0)
                        throw new Exception("there is not enough in the stock");
                    cartP.TotalPrice += (amount-cartP.Amount)*cartP.Price;
                    cart.TotalPrice += (amount - cartP.Amount) * cartP.Price;
                    cartP.Amount = amount;
                }
                if(amount < cartP.Amount)
                {
                    cartP.TotalPrice -= (cartP.Amount - amount) * cartP.Price;
                    cart.TotalPrice -= (cartP.Amount - amount) * cartP.Price;
                    cartP.Amount = amount;
                }
                if (amount == 0)
                    cart.Items.Remove(cartP);
                return cart;
            }
            catch (DO.DalDoesNotExistException exc)
            { 

                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }
    }
}
