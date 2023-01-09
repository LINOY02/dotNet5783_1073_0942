/// <summary>
/// A main interface that centers all the interfaces of the logical layer
/// </summary>
namespace BlApi
{
    public interface IBl
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

        /// <summary>
        /// A variable to represent a user's interface
        /// </summary>
        public IUser User { get; }
    }
}