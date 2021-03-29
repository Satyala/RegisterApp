using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegisterApp.Models.APIModels
{
    /// <summary>
    /// API Response for Customer Request. Message will have the status on the api processing status and error if failed.
    /// </summary>
    public class CustomerResponse
    {
        public CustomerResponse()
        {
            Message = new APIReqeustStatus();
        }
        public int CustomerID { get; set; }

        public APIReqeustStatus Message { get; set; }

    }
}
