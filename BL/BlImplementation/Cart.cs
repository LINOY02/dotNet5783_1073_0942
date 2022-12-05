using BlApi;
using DAL;
using DalApi;
using DO;

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
                if (cart.Items == null)
                {
                    //Adding a product to the shopping cart
                    cart.Items = Dal.Product.GetAll().Where(y=>y.ID == productId).Select(x => new BO.OrderItem
                    {
                        OrderID = Dal.Order.GetAll().Last().ID + 1,
                        ProductID = productId,
                        Name = x.Name,
                        Price = x.Price,
                        Amount = 1,
                        TotalPrice = x.Price,
                    }).ToList();
                    cart.TotalPrice += product.Price;
                }
                else
                {
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
                        cart.Items = cart.Items.Append(new BO.OrderItem
                        {
                            OrderID = Dal.Order.GetAll().Last().ID + 1,
                            ProductID= productId,
                            Name = product.Name,
                            Price = product.Price,
                            Amount = 1,
                            TotalPrice = product.Price,
                        });
                        cart.TotalPrice += product.Price;
                    }
                }
               return cart;
             }
            
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        public void OrderCart(BO.Cart cart, string castumerName, string castumerEmail, string castumerAdress)
        {
            throw new NotImplementedException();
        }

        public BO.Cart UpdateCart(BO.Cart cart, int id, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
