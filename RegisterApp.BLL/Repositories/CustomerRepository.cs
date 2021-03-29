using RegisterApp.DAL;
using RegisterApp.Models.Entities;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Customer Repository inherited from generic repository
    /// </summary>
    public class CustomerRepository : GenericRepository<CustomerTable>, ICustomerRepository
    {
        public CustomerRepository(RegisterAppContext ctx) : base(ctx)
        {
        }
    }
}
