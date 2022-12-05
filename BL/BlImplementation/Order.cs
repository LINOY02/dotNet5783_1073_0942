using System.Buffers.Text;
using BO;
using DAL;
using DalApi;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// Presenting all invitations to the manager
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> GetListedOrders()
        {
            return Dal.Order.GetAll().Select(order => new OrderForList
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                Status = CheckStatus(order),
                AmountOfItems = Dal.OrderItem.GetAllOrder(order.ID).Count(),
                TotalPrice = Dal.OrderItem.GetAllOrder(order.ID).Sum(x => x.Price * x.Amount)
            }); 
        }

        /// <summary>
        /// Displaying order details including customer receipts and details of all his products
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order GetOrder(int id)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException("id");//לשנות לחריגות שלנו
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                // Creating a collection of items of order item type
                var items = Dal.OrderItem.GetAllOrder(dOrder.ID).Select(X =>
                     new BO.OrderItem
                     {
                         ID = X.ID,
                         ProductID = X.ProductID,
                         Name = Dal.Product.GetById(X.ProductID).Name,
                         Price = X.Price,
                         Amount = X.Amount,
                         TotalPrice = X.Price * X.Amount
                     });

                //Creating and returning the order (in a more detailed form)
                return new BO.Order
                {
                    ID = dOrder.ID,
                    CustomerName = dOrder.CustomerName,
                    CustomerAdress = dOrder.CustomerAdress,
                    CustomerEmail = dOrder.CustomerEmail,
                    OrderDate = dOrder.OrderDate,
                    ShipDate = dOrder.ShipDate,
                    DeliveryDate = dOrder.DeliveryDate,
                    Status = CheckStatus(dOrder),
                    Items = items,
                    TotalPrice = items.Sum(x => x.TotalPrice)
                };
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        /// <summary>
        /// Update of an order sent to the customer by the manager
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order ShipOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id");//לשנות לחריגות שלנו
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                if(dOrder.ShipDate == DateTime.MinValue)
                {
                    Dal.Order.Update(new DO.Order
                    {
                        ID = id,
                        CustomerName = dOrder.CustomerName,
                        CustomerAdress = dOrder.CustomerAdress,
                        CustomerEmail = dOrder.CustomerEmail,
                        OrderDate = dOrder.OrderDate,
                        ShipDate = DateTime.Now,
                        DeliveryDate = dOrder.DeliveryDate,
                    });
                }
                return this.GetOrder(id);
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        /// <summary>
        /// Update by the manager that the order has been delivered to the customer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order DeliveredOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id");//לשנות לחריגות שלנו
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                if (dOrder.ShipDate != DateTime.MinValue && dOrder.DeliveryDate == DateTime.MinValue)
                {
                    Dal.Order.Update(new DO.Order
                    {
                        ID = id,
                        CustomerName = dOrder.CustomerName,
                        CustomerAdress = dOrder.CustomerAdress,
                        CustomerEmail = dOrder.CustomerEmail,
                        OrderDate = dOrder.OrderDate,
                        ShipDate = dOrder.ShipDate,
                        DeliveryDate = DateTime.Now,
                    });
                }
                return this.GetOrder(id);
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        public BO.OrderTracking TruckingOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id");//לשנות לחריגות שלנו
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id); 
                
                //Creating an order tracking list based on order status
                List<Tuple<DateTime, string>> tracking = new List<Tuple<DateTime, string>>();
                if(dOrder.OrderDate != DateTime.MinValue)//Order date check
                {
                    tracking.Add(new Tuple<DateTime, string>(dOrder.OrderDate, "The order has been created"));
                }
                if (dOrder.ShipDate != DateTime.MinValue)//Checking the date of sending
                {
                    tracking.Add(new Tuple<DateTime, string>(dOrder.ShipDate, "The order has been sent"));
                }
                if(dOrder.DeliveryDate != DateTime.MinValue)//Check delivery date
                {
                    tracking.Add(new Tuple<DateTime, string>(dOrder.DeliveryDate, "The order has been delivered"));
                }

                //Order tracking return with details
                return new BO.OrderTracking
                {
                    ID = id,
                    status = CheckStatus(dOrder),
                    Tracking = tracking
                };
                
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }
        public BO.Order UpdateOrder(int orderId, int productId, int amount , Enum update)
        {
            if (orderId <= 0)
                throw new ArgumentOutOfRangeException("id");//לשנות לחריגות שלנו
            try
            {
                DO.Order dOrder = Dal.Order.GetById(orderId);
                if (CheckStatus(dOrder) != OrderStatus.Ordered)
                    throw new Exception("cant update");
                DO.OrderItem orderItem = Dal.OrderItem.GetByOidAndPid(orderId, productId);
                if(amount < 0)
                    throw new Exception("amount is illigal");
                if(update.Equals(UpdateAction.increase))
                {
                    orderItem.Amount += amount;
                }
                if (update.Equals(UpdateAction.reduction))
                {
                    orderItem.Amount -= amount;
                }
                if (update.Equals(UpdateAction.changing))
                {
                    orderItem.Amount = amount;
                }

                Dal.OrderItem.Update(orderItem);    
                return this.GetOrder(orderId);    
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.DalDoesNotExistException(exc.Message);
            }
        }

        

        //A private helper function that accepts an order and returns its order status
        private OrderStatus CheckStatus(DO.Order item)
        {
            if (item.OrderDate == DateTime.MinValue)
                    return OrderStatus.Initiated;
                else
                    if (item.ShipDate == DateTime.MinValue)
                    return OrderStatus.Ordered;
                else
                    if (item.DeliveryDate == DateTime.MinValue)
                    return OrderStatus.Shipped;
                else
                    return OrderStatus.Delivered;
        }
    }
}
