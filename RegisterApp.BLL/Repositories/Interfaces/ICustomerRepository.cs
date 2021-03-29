using RegisterApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Cusotmer Repo interface from generic repo interface
    /// </summary>
    public interface ICustomerRepository : IGenericRepository<CustomerTable>
    {
    }
}
