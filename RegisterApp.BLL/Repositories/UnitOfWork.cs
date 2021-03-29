using RegisterApp.DAL;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
