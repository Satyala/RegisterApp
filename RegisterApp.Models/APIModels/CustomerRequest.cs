using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegisterApp.Models.APIModels
{
    /// <summary>
    /// Cusotmer Request from API
    /// </summary>
    public class CustomerRequest
    {
        public int CustomerID { get; set; }


        public string FirstName { get; set; }

       
        public string SurName { get; set; }

        public string EmailAddress { get; set; }

     
        public string PolicyReferenceNumber { get; set; }

        public DateTime? DOB { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int APIConsumerId { get; set; }
    }
}
