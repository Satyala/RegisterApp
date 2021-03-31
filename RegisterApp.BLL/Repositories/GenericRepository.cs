using Microsoft.EntityFrameworkCore;
using RegisterApp.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="obj"></param>
        public async Task<T> Insert(T obj)
        {
            var result = await Task.Run(() =>
            {
                var entity = table.AddAsync(obj);
                return entity.Result.Entity;
            });

            return result;            
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