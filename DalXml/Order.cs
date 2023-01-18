using DalApi;
using DO;

namespace Dal
{
    internal class Order : IOrder
    {
        public int Add(DO.Order item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
        {
            throw new NotImplementedException();
        }

        public DO.Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DO.Order GetItem(Func<DO.Order?, bool>? filter)
        {
            throw new NotImplementedException();
        }

        public void Update(DO.Order item)
        {
            throw new NotImplementedException();
        }
    }
}
