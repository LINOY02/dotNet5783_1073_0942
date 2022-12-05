using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{

    public class Cart
    {
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
        public string CustomerAddress { get; set; }
        /// <summary>
        /// The items of the order
        /// </summary>
        public IEnumerable<BO.OrderItem> Items { get; set; }
        /// <summary>
        /// The total price of the cart
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}