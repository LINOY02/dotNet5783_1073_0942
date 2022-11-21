using DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public IEnumerable<OrderItem> GetAllOrder(int orderId);
        public OrderItem GetByOidAndPid(int orderId, int productId);
    }
}
