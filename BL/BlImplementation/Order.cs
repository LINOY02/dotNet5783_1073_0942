
using DalApi;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
       private static readonly IDal Dal = DalApi.Factory.Get()!;
        /// <summary>
        /// Presenting all invitations to the manager
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> GetListedOrders()
        {
            return Dal.Order.GetAll().Select(order => new BO.OrderForList
            {
                ID = (int)order?.ID!,
                CustomerName = order?.CustomerName!,
                Status = CheckStatus(order),
                AmountOfItems = Dal.OrderItem.GetAll((DO.OrderItem? x) => { return x?.OrderID == order?.ID; }).Count(),
                TotalPrice = Dal.OrderItem.GetAll((DO.OrderItem? x) => { return x?.OrderID == order?.ID; }).Sum(y => (double)y?.Price!* (int)y?.Amount!)
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
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                // Creating a collection of items of order item type
                var items = Dal.OrderItem.GetAll((DO.OrderItem? x) => { return x?.OrderID == dOrder.ID; }).Select(x =>
                     new BO.OrderItem
                     {
                         ID = (int)x?.ID!,
                         ProductID = (int)x?.ProductID!,
                         Name = Dal.Product.GetById((int)x?.ProductID!).Name,
                         Price =(double) x?.Price!,
                         Amount = (int)x?.Amount!,
                         TotalPrice =(double) x?.Price! * (int)x?.Amount!
                     }).ToList();

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
                    Items = items!,
                    TotalPrice = items.Sum(x => x.TotalPrice)
                };
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }

        /// <summary>
        /// Update of an order sent to the customer by the manager
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order ShipOrder(int id , DateTime? date)
        {
            if (id <= 0)
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                if (dOrder.ShipDate == null)
                {
                    Dal?.Order.Update(new DO.Order
                    {
                        ID = id,
                        CustomerName = dOrder.CustomerName,
                        CustomerAdress = dOrder.CustomerAdress,
                        CustomerEmail = dOrder.CustomerEmail,
                        OrderDate = dOrder.OrderDate,
                        ShipDate = date,
                        DeliveryDate = dOrder.DeliveryDate,
                    });
                    return this.GetOrder(id);
                }
                else
                    throw new BO.BlStatusAlreadyUpdateException("The order is already shipped");
                
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }

        /// <summary>
        /// Update by the manager that the order has been delivered to the customer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.Order DeliveredOrder(int id, DateTime? date)
        {
            if (id <= 0)
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id);
                if (dOrder.ShipDate != null && dOrder.DeliveryDate == null)
                {
                    Dal?.Order.Update(new DO.Order
                    {
                        ID = id,
                        CustomerName = dOrder.CustomerName,
                        CustomerAdress = dOrder.CustomerAdress,
                        CustomerEmail = dOrder.CustomerEmail,
                        OrderDate = dOrder.OrderDate,
                        ShipDate = dOrder.ShipDate,
                        DeliveryDate = date,
                    });
                    return this.GetOrder(id);
                }
                else
                    if (dOrder.ShipDate == null)
                        throw new BO.BlStatusNotUpdateException("The order is not shipped yet");
                    else
                        throw new BO.BlStatusAlreadyUpdateException("The order is already deliverd");
            }
            catch (DO.DalDoesNotExistException exc)
            {
                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }


        /// <summary>
        /// Possibility for the manager to follow the order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public BO.OrderTracking TruckingOrder(int id)
        {
            if (id <= 0)
                throw new BO.BlInvalidInputException("The id is invalid");
            try
            {
                DO.Order dOrder = Dal.Order.GetById(id); 
                
                //Creating an order tracking list based on order status
                List<Tuple<DateTime?, string?>> tracking = new List<Tuple<DateTime?, string?>>();
                if(dOrder.OrderDate != null)//Order date check
                {
                    tracking.Add(new Tuple<DateTime?, string?>(dOrder.OrderDate, "The order has been created"));
                }
                if (dOrder.ShipDate != null)//Checking the date of sending
                {
                    tracking.Add(new Tuple<DateTime?, string?>(dOrder.ShipDate, "The order has been sent"));
                }
                if(dOrder.DeliveryDate != null)//Check delivery date
                {
                    tracking.Add(new Tuple<DateTime?, string?>(dOrder.DeliveryDate, "The order has been delivered"));
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
                throw new BO.BlDoesNotExistException(exc.Message);
            }
        }


        /// <summary>
        /// A private helper function that accepts an order and returns its order status
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private BO.OrderStatus CheckStatus(DO.Order? item)
        {
            if (item?.OrderDate == null)
                    return BO.OrderStatus.Initiated;
                else
                    if (item?.ShipDate == null)
                    return BO.OrderStatus.Ordered;
                else
                    if (item?.DeliveryDate == null)
                    return BO.OrderStatus.Shipped;
                else
                    return BO.OrderStatus.Delivered;
        }

        /// <summary>
        /// fun that return the ID of the oldest order
        /// </summary>
        /// <returns></returns>
        public int OrderOldest()
        {
            try
            {
                //all the orders that are only orderd 
                var ordered = GetListedOrders().Where(x => x.Status == BO.OrderStatus.Ordered).Select(x => Dal.Order.GetById(x.ID));
                var firstOrdered = ordered.OrderByDescending(x => x.OrderDate).Last(); //the oldest orderd
                                                                                       //all the orders that are only shipped
                var shipped = GetListedOrders().Where(x => x.Status == BO.OrderStatus.Shipped).Select(x => Dal.Order.GetById(x.ID));
                var firstshipped = ordered.OrderByDescending(x => x.ShipDate).Last(); //the oldest shipped

                if (firstOrdered.OrderDate < firstshipped.ShipDate)
                    return firstOrdered.ID;
                else
                    return firstshipped.ID;
            }
            catch(DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }
    }
}
