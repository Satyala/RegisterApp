using RegisterApp.DAL;
using System.Threading.Tasks;

namespace RegisterApp.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private RegisterAppContext context;

        public UnitOfWork(RegisterAppContext context)
        {
            this.context = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return new CustomerRepository(context);
            }
        }

        public async Task<int> Save()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
