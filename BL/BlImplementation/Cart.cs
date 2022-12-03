using BlApi;
using DAL;
using DalApi;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        private IDal Dal = new DalList();
        public BO.Cart AddProductToCart(BO.Cart cart, int id)
        {
            throw new NotImplementedException();
        }

        public void OrderCart(BO.Cart cart, string castumerName, string castumerEmail, string castumerAdress)
        {
            throw new NotImplementedException();
        }

        public BO.Cart UpdateCart(BO.Cart cart, int id, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
