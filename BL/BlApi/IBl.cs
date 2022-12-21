/// <summary>
/// A main interface that centers all the interfaces of the logical layer
/// </summary>
namespace BlApi
{
    internal interface IBl
    {
        /// <summary>
        /// A variable to represent a product's interface
        /// </summary>
        public IProduct Product { get; }

        /// <summary>
        /// A variable to represent an order's interface
        /// </summary>
        public IOrder Order { get; }

        /// <summary>
        /// A variable to represent a cart's interface
        /// </summary>
        public ICart Cart { get; }
    }
}