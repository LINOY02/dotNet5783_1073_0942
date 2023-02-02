

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        public int Add(T item);
        public T GetById(int id);
        public void Update(T item);
        public void Delete(int id);
        public IEnumerable<T?> GetAll(Func<T?,bool>? filter = null);
        public T GetItem(Func<T?, bool>? filter );


    }
}
