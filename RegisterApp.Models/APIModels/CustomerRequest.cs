using FluentValidation;
using System;
using System.Collections.Generic;
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

    /// <summary>
    /// Cusotmer Request Validator
    /// </summary>
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.CustomerID).GreaterThan(0).WithMessage(CustomerModelContants.CustomerId);
            RuleFor(x => x.FirstName).Length(3, 50).WithMessage(CustomerModelContants.FirstName);
            RuleFor(x => x.SurName).Length(3, 50).WithMessage(CustomerModelContants.SurName);
            RuleFor(x => x.PolicyReferenceNumber).MinimumLength(9).Must(ValidatePolicyNumber).WithMessage(CustomerModelContants.PolicyReferenceNUmber);
            RuleFor(x => x.EmailAddress)
                .Must(ValidateEmail).When(x => ValidateEmailNoDOB(x)).WithMessage(CustomerModelContants.EmailAddress);
            RuleFor(x => x.DOB)
                .Must(ValidateAge).When(x => ValidateDOBNoEmail(x)).WithMessage(CustomerModelContants.DOB);
        }

        /// <summary>
        /// Verify Policy Number
        /// </summary>
        /// <param name="policyNumber"></param>
        /// <returns></returns>
        private bool ValidatePolicyNumber(string policyNumber)
        {
            //Create regex to macth the format

            if (policyNumber.Length > 9 || string.IsNullOrWhiteSpace(policyNumber))
            {
                return false;
            }

            char[] two = policyNumber.Substring(0, 2).ToCharArray();

            foreach (var c in two)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            string three = policyNumber.Substring(2, 1);

            if (three != "-")
            {
                return false;
            }

            char[] last = policyNumber.Substring(3, 6).ToCharArray();

            foreach (var c in last)
            {

                if (!char.IsDigit(c))
                {
                    return false;
                }
            }


            return true;
        }

        /// <summary>
        /// Validate DOB When no email provided
        /// </summary>
        /// <param name="cusotmerRequest"></param>
        /// <returns></returns>
        private bool ValidateDOBNoEmail(CustomerRequest cusotmerRequest)
        {
            if (!string.IsNullOrWhiteSpace(cusotmerRequest.EmailAddress))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// validare email when no dob
        /// </summary>
        /// <param name="cusotmerRequest"></param>
        /// <returns></returns>
        private bool ValidateEmailNoDOB(CustomerRequest cusotmerRequest)
        {
            DateTime validdob;
            DateTime.TryParse(cusotmerRequest.DOB?.ToString(), out validdob);
            if (validdob > DateTime.MinValue)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Email Validation logic can be replaced by regex
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private bool ValidateEmail(string emailAddress)
        {
            //Create regex to macth the format

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }





            var split = emailAddress.Split('@');

            if (split.Length > 0)
            {
                var firstpart = split[0];

                if (firstpart.Length < 4)
                {
                    return false;
                }

                if (emailAddress.EndsWith(".com"))
                {
                    if (Between(split[1], "@", ".com").Length < 2)
                    {
                        return false;
                    }
                }
                if (emailAddress.EndsWith(".co.uk"))
                {
                    if (Between(split[1], "@", ".co.uk").Length < 2)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }


            if (emailAddress.EndsWith(".com") || emailAddress.EndsWith(".co.uk"))
            {
                return true;
            }
            return true;
        }


        /// <summary>
        /// Check DOB
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        private bool ValidateAge(DateTime? dob)
        {
            if (dob != null)
            {
                var bday = Convert.ToDateTime(dob);
                var ts = DateTime.Today.Date - bday.Date;
                var year = DateTime.MinValue.Add(ts).Year - 1;
                return year >= 18;
            }

            return true;
        }

        /// <summary>
        /// TODO: String Extension
        /// </summary>
        /// <param name="STR"></param>
        /// <param name="FirstString"></param>
        /// <param name="LastString"></param>
        /// <returns></returns>
        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
