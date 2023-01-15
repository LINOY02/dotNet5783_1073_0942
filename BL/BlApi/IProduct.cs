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
        IEnumerable<ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);

        /// <summary>
        /// All products are shown according to the selected category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ProductForList?> GetListedProductsByCategory(BO.Category category);

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
        /// The buyer enters a product code and receives the 
        /// product details: number, name, price, category and how much in stock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></ProductItem>
        ProductItem GetDetailsItem(int id, BO.Cart cart);
        #endregion

        /// <summary>
        /// The function shows the customerr the list of products,
        /// for each product: number, name, price and category
        /// </summary>
        /// <returns></List of ProductsForList>
        IEnumerable<ProductItem?> GetProductItems(BO.Cart cart, Func<BO.ProductItem?, bool>? filter = null);

        /// <summary>
        /// All productItems are shown according to the selected category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ProductItem?> GetProductItemsByCategory(BO.Cart cart, BO.Category category);

    }
}
