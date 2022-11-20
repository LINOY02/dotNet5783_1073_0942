
namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add(T item);
        public T GetById(int id);
        public void Update(T item);
        public void Delete(int id);
        public IEnumerable<T> GetAll();

    }
}
