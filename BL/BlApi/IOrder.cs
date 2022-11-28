
using BO;
namespace BlApi;

public interface IOrder
{
    //IEnumerable<OrderForList> ();
    /// <summary>
    /// Presenting all invitations to the manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetOrder(int id);
    /// <summary>
    /// Displaying order details including customer receipts and details of all his products
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.Order SendOrder(int id);
    /// <summary>
    /// Update of an order sent to the customer by the manager
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.Order arrivedOrder(int id);
    /// <summary>
    /// Update by the manager that the order has been delivered to the customer
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.OrderTracking FollowingOrder(int id);
    /// <summary>
    /// Possibility for the manager to follow the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.Order UpdateOrder(int id);
}
