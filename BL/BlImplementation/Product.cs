using DalApi;
using DAL;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();
        public void AddProduct(BO.Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public BO.ProductItem GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ProductForList> GetListedProducts()
        {
            throw new NotImplementedException();
        }

        public BO.Product GetProduc(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ProductItem> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(BO.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
