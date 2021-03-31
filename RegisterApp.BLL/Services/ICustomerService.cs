using RegisterApp.Models.APIModels;
using System.Threading.Tasks;

namespace RegisterApp.BLL.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse> RegisterCustomer(CustomerRequest customer);
    }
}
