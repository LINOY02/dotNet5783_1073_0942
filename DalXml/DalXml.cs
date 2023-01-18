using DalApi;

namespace DalXml
{
    sealed internal class DalXml : IDal
    {
        public IProduct Product => new Dal.Product();
        public IOrder Order => new Dal.Order();
        public IOrderItem OrderItem => new Dal.OrderItem();
        public IUser User => new Dal.User();
        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }
    }
}