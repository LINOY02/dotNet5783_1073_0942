
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
        public Cart AddProductToCart(Cart cart, int producId);

        /// <summary>
        /// Delet a product from the shopping cart by the customer
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public Cart DeleteProductFromCart(Cart cart, int producId);

        /// <summary>
        /// Updating the quantity of a product from the shopping cart
        /// </summary>
        /// <param name=Cart></param>
        /// <returns></the cart>
        public Cart UpdateCart(Cart cart, int id, int amount);

        /// <summary>
        /// make an order
        /// </summary>
        /// <param name=Cart></param>
        public int OrderCart(Cart cart);
       
    }
}
