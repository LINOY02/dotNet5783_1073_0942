
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Order
{
    /// <summary>
    /// The id of the order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The customer's name
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// The customer's email
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// The customer's adress
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// The order's arrival date 
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// The date of shipment 
    /// </summary>
    public DateTime ShipDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
    ID               = {ID},
    CustomerName     = {CustomerName},
    CustomerEmail    = {CustomerEmail},
    CustomerAdress   = {CustomerAdress},
    OrderDate        = {OrderDate},
    ShipDate         = {ShipDate},
    DeliveryDate     = {DeliveryDate}
    ";
}
