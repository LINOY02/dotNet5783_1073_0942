
using DalApi;

namespace DAL
{
    sealed public class DalList : IDal
    {
        public IProduct Product => new DalProduct();

    }
}
