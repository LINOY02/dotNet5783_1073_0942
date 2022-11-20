
namespace DalApi
{
    public interface IDal
    {
        public IProduct IProduct { get; }
        public IOrder IOrder { get; }
        public IOrderItem IOrderItem { get; }
    }
}
