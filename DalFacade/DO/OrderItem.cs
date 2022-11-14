
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    /// <summary>
    /// The id of the order item
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The id of the product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// The id of the order 
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// The price of each product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The amount of the product in the order
    /// </summary>
    public int Amount { get; set; }
    public override string ToString() => $@"
    ID          = {ID},
    ProductId   = {ProductID},
    OrderId     = {OrderID},
    Price       = {Price},
    Amount      = {Amount},
    ";
}
