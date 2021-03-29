using RegisterApp.BLL.Repositories;
using RegisterApp.Models.APIModels;
using RegisterApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RegisterApp.Models;

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
        public CustomerResponse RegisterCustomer(CustomerRequest customer)
        {
            CustomerResponse response = new CustomerResponse();

            //Validate the request using cusotmer validator
            CustomerRequestValidator validator = new CustomerRequestValidator();
            var vresult = validator.Validate(customer);
            if (vresult.IsValid)
            {
                var customerId = GetCustomerById(customer.CustomerID);

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

                    var result = _repository.CustomerRepository.Insert(customerTable);

                    _repository.Save();

                    response.CustomerID = result.CustomerID;
                    response.Message.RequestStatus = CustomerModelContants.SucessMessageStatus;

                    return response;
                }

                response.Message.ErrorMessage.Add(CustomerModelContants.DuplicateCustomerID);
            }
            else
            {
                response.Message.ErrorMessage.AddRange(vresult.Errors.Select(r => r.ErrorMessage).ToList());
            }

            response.Message.RequestStatus = CustomerModelContants.FailedMessageStatus;

            return response;
        }

        public CustomerResponse GetCustomerById(int customerId)
        {
            var customer = _repository.CustomerRepository.GetAll().ToList().Where(a => a.CustomerID == customerId).FirstOrDefault();

            CustomerResponse customers = new CustomerResponse();

            //TODO:Use automapper to copy 
            customers.CustomerID = customer?.CustomerID ?? 0;

            return customers;
        }
    }
}
