using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace RegisterApp.Models.APIModels
{
    /// <summary>
    /// Cusotmer Request Validator
    /// </summary>
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public const string PolicyNumberRegEx = @"^[A-Z]{2}-\d{6}$";


        public CustomerRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .Length(3, 50).WithMessage(CustomerModelContants.FirstName);
            RuleFor(x => x.SurName)
                .Length(3, 50).WithMessage(CustomerModelContants.SurName);
            RuleFor(x => x.PolicyReferenceNumber)
                .Matches(PolicyNumberRegEx).WithMessage(CustomerModelContants.PolicyReferenceNUmber);
            RuleFor(x => x.EmailAddress)
                .Must((x, a) => ValidateEmail(x, a)).WithMessage(CustomerModelContants.EmailAddress);
            RuleFor(x => x.DOB)
                .Must((x, a) => ValidateDOB(x, a)).WithMessage(CustomerModelContants.DOB);
        }

        /// <summary>
        /// Validate Email and Returns true when DOB is avilable
        /// </summary>
        /// <param name="cusotmerRequest"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private bool ValidateEmail(CustomerRequest cusotmerRequest, string emailAddress)
        {
            if (!string.IsNullOrWhiteSpace(emailAddress?.Trim()))
            {
                Regex emailRegEx = new Regex(@"^[a-zA-Z0-9]{4,}@[a-zA-Z0-9]{2,}(?:.com|.co.uk)$");
                if (emailRegEx.IsMatch(emailAddress.Trim()))
                { return true; }
            }
            else if (cusotmerRequest.DOB.HasValue)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check DOB
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        private bool ValidateDOB(CustomerRequest cusotmerRequest, DateTime? dob)
        {
            if (dob?.Date < DateTime.Today.Date)
            {
                var ts = DateTime.Today.Date - dob.Value.Date;
                var year = DateTime.MinValue.Add(ts).Year - 1;
                return year >= 18 && year < 150;
            }
            else if (!string.IsNullOrWhiteSpace(cusotmerRequest.EmailAddress?.Trim()))
            {
                return true;
            }

            return false;
        }
    }
}
