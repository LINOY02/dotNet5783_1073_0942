
using BO;
namespace BlApi
{

    public interface IOrder
    {
        /// <summary>
        /// Presenting all invitations to the manager
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderForList?> GetListedOrders();

        /// <summary>
        /// Displaying order details including customer receipts and details of all his products
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order GetOrder(int id);

        /// <summary>
        /// Update of an order sent to the customer by the manager
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order ShipOrder(int id, DateTime? date);

        /// <summary>
        /// Update by the manager that the order has been delivered to the customer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order DeliveredOrder(int id, DateTime? date);

        /// <summary>
        /// Possibility for the manager to follow the order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.OrderTracking TruckingOrder(int id);

        
    }
}