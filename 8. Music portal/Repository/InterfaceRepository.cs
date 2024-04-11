using _8._Music_portal.Models;

namespace _8._Music_portal.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        bool ModelExists(int id);
        Task Save();
    }
}
