using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegisterApp.BLL.Services;
using RegisterApp.Models.APIModels;
using System;
using System.Threading.Tasks;

namespace RegisterApp.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Customer")]
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
        [Route("Register")]
        public async Task<IActionResult> Register(CustomerRequest request)
        {
            try
            {
                CustomerRequestValidator validator = new CustomerRequestValidator();
                var vresult = validator.Validate(request);
                if (vresult.IsValid)
                {
                    var result = await _customerService.RegisterCustomer(request);
                    if (result.CustomerID > 0)
                    {
                        return Ok(result.CustomerID);
                    }
                    else
                    {
                        return BadRequest(0);
                    }
                }
                else
                {
                    return BadRequest(-1);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Un expected error");
                return StatusCode(StatusCodes.Status500InternalServerError, -2);
            }
        }
    }
}
