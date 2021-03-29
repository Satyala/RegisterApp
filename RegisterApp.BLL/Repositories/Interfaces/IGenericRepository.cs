using System.Collections.Generic;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);

        T Insert(T obj);

        void Update(T obj);

        void Delete(object id);
    }
}
