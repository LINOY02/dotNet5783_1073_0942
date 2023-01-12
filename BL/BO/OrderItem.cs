
namespace BO
{

    public class OrderItem
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
        /// The name of the profuct
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of each product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The amount of the product in the order
        /// </summary>
        public int Amount { get; set; }

        ///public string Name { get; set; }
        /// <summary>
        /// The total price of the order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// pictures
        /// </summary>
        public string? picture  { get; set; }   
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}