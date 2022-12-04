
namespace BO
{

    public class ProductForList
    {
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The Category of the product
        /// </summary>
        public Category Category { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}