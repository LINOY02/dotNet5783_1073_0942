
using BO;
namespace BlApi;

internal interface ICart
{
    public BO.Cart AddCart(BO.Cart cart, int id);
    /// <summary>
    /// Adding a product to the shopping cart by the customer
    /// </summary>
    /// <param name=Cart></param>
    /// <returns></the cart>
    public BO.Cart UpdateCart(BO.Cart cart, int id, int amount);
    /// <summary>
    /// Updating the quantity of a product from the shopping cart
    /// </summary>
    /// <param name=Cart></param>
    /// <returns></the cart>
    public void GetCart(BO.Cart cart, string castumerName, string castumerEmail, string castumerAdress);
    /// <summary>
    /// make an order
    /// </summary>
    /// <param name=Cart></param>
}
