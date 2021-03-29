using Microsoft.EntityFrameworkCore;
using RegisterApp.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly RegisterAppContext _context;
        private DbSet<T> table = null;

        public GenericRepository(RegisterAppContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        /// <summary>
        /// List all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return table.Find(id);
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="obj"></param>
        public T Insert(T obj)
        {
            var entity = table.Add(obj);
            return entity.Entity;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }



    }
}