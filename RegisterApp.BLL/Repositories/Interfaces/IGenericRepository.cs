using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);

        Task<T> Insert(T obj);

        void Update(T obj);

        void Delete(object id);
    }
}
