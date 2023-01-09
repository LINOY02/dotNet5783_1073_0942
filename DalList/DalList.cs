
using DAL;
using DalApi;
namespace Dal
{
    sealed internal class DalList : IDal
    {
        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();
        public IUser User => new DalUser();
        public static IDal Instance { get; } = new DalList();
        private DalList() { }
    }
}