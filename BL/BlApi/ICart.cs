
using BO;
namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// Adding a product to the shopping cart by the customer
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public BO.Cart AddProductToCart(BO.Cart cart, int id);

        /// <summary>
        /// Updating the quantity of a product from the shopping cart
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public BO.Cart UpdateCart(BO.Cart cart, int id, int amount);

        /// <summary>
        /// make an order
        /// </summary>
        /// <param name=Cart></param>
        public void OrderCart(BO.Cart cart, string castumerName, string castumerEmail, string castumerAdress);
       
    }
}
