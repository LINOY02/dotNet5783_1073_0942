using DAL;
using DalApi;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        private IDal Dal = new DalList();
        public BO.Order DeliveredOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BO.OrderTracking TruckingOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.OrderForList> GetListedOrders()
        {
            throw new NotImplementedException();
        }

        public BO.Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Order ShipOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Order UpdateOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
