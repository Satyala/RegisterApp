using RegisterApp.BLL.Repositories;
using RegisterApp.Models.APIModels;
using RegisterApp.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterApp.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _repository { get; set; }

        public CustomerService(IUnitOfWork repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Register Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> RegisterCustomer(CustomerRequest customer)
        {
            CustomerResponse response = new CustomerResponse();

            //Validate the request using cusotmer validator

            var customerId = await GetCustomerById(customer.CustomerID);

            if (customerId?.CustomerID == 0)
            {
                //TODO:User auto mapper
                CustomerTable customerTable = new CustomerTable();
                customerTable.CustomerID = customer.CustomerID;
                customerTable.FirstName = customer.FirstName;
                customerTable.SurName = customer.SurName;
                customerTable.DOB = customer.DOB;
                customerTable.Email = customer.EmailAddress;
                customerTable.PolicyReferenceNumber = customer.PolicyReferenceNumber;


                customerTable.DateCreated = DateTime.Now;
                customerTable.DateUpdated = DateTime.Now;

                //loggedin user id
                customerTable.UpdatedByID = customer.APIConsumerId;
                customerTable.CreatedByID = customer.APIConsumerId;

                var result = await _repository.CustomerRepository.Insert(customerTable);

                await _repository.Save();

                response.CustomerID = result.CustomerID;

                return response;
            }

            return response;
        }

        public async Task<CustomerResponse> GetCustomerById(int customerId)
        {
            var customer = await _repository.CustomerRepository.GetAll();

            CustomerResponse customers = new CustomerResponse();

            //TODO:Use automapper to copy 
            customers.CustomerID = customer?.Where(a => a.CustomerID == customerId).FirstOrDefault().CustomerID ?? 0;

            return customers;
        }
    }
}
