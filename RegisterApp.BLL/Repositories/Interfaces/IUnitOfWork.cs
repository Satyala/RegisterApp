using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RegisterApp.BLL.Repositories
{
    /// <summary>
    /// Unit fo Work pattern from generic repo
    /// </summary>
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }

        Task<int> Save();
    }
}
