using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        /// <summary>
        /// The customer's name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The customer's email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// The customer's adress
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// The uSerName of the person who use the program
        /// </summary>
        public string? userName { get; set; }
        /// <summary>
        /// The password of the product
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// The statu of the user
        /// </summary>
        public userStatus status { get; set; }

        public override string ToString() => this.ToStringProperty();
    }
}
