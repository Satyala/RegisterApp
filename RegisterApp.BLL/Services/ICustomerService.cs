using RegisterApp.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterApp.BLL.Services
{
    public interface ICustomerService
    {
        CustomerResponse RegisterCustomer(CustomerRequest customer);
    }
}
