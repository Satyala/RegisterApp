using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegisterApp.BLL.Services;
using RegisterApp.Models;
using RegisterApp.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterApp.WebAPI.Controllers
{
    [ApiController]
    [Route("/v1/Customer")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService { get; set; }

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }


        /// <summary>
        /// POST Method of API to create Cuatomer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerResponse Register(CustomerRequest request)
        {
            try
            {
                return _customerService.RegisterCustomer(request);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Un expected error");

                var message = new CustomerResponse();
                message.Message.RequestStatus = CustomerModelContants.FailedMessageStatus;
                message.Message.ErrorMessage.Add("Unexpected Error");

                return message;
            }

        }
    }
}
