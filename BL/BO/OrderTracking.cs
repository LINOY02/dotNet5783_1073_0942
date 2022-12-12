

namespace BO
{

    public class OrderTracking
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The Status of the order
        /// </summary>
        public OrderStatus? status { get; set; }
        /// <summary>
        /// List of tuple
        /// </summary>
        public List<Tuple<DateTime?, string?>>? Tracking { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}