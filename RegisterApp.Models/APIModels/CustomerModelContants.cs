using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterApp.Models
{
    public static class CustomerModelContants
    {
        public const string SucessMessageStatus = "Success";

        public const string FailedMessageStatus = "Failed";

        public const string DuplicateCustomerID = "Cusotmer Id already exists";
        public const string CustomerId = "Check customer Id. Grater than 0 and unique";
        public const string FirstName = "Check firstName. Should be between 3 and 50";

        public const string SurName = "Check SurName. Should be between 3 and 50";

        public const string PolicyReferenceNUmber = "Check policy number format XX-999999";
        public const string DOB = "Check DOB. Should be atleast 18 years old";
        
        public const string EmailAddress = "Check at leat 4 characters follwed by @ and 2 characters. End in either .com or .co.uk";

        
    }
}
