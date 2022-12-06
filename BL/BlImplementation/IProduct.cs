using BO;
namespace BlApi
{
    /// <summary>
    /// Interface for product behavior in the logical layer
    /// </summary>
    public interface IProduct
    {
        #region METHOSD FOR MANAGER
        /// <summary>
        /// The function shows the manager the list of products,
        /// for each product: number, name, price and category
        /// </summary>
        /// <returns></List of ProductsForList>
        IEnumerable<ProductForList> GetListedProducts();


        /// <summary>
        /// The function receives a product ID number 
        /// and returns its details (for the manager)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></Product>
        BO.Product GetProduct(int id);

        /// <summary>
        /// The function receives product details from the user and adds to the data layer (for the manager)
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(BO.Product product);

        /// <summary>
        /// The function receives a product number and deletes it from the data layer (for the manager)
        /// </summary>
        /// </summary>
        /// <param name="id"></param>
        void DeleteProduct(int id);

        /// <summary>
        /// The function receives product details from the user and updates the product in the data layer (for the manager)
        /// </summary>
        /// <param name="product"></param>
        void UpdateProduct(BO.Product product);
        #endregion

        #region METHODS FOR CUSTOMER
        /// <summary>
        /// show the buyer a list of all the products, 
        /// for each product: number, name, price, category, whether in stock and how many in stock
        /// </summary>
        /// <returns></List of ProductItem>
        IEnumerable<ProductItem> GetProducts();

        /// <summary>
        /// The buyer enters a product code and receives the 
        /// product details: number, name, price, category and how much in stock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></ProductItem>
        ProductItem GetDetailsItem(int id, BO.Cart cart);
        #endregion


    }
}
