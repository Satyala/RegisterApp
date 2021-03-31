using Moq;
using NUnit.Framework;
using RegisterApp.BLL.Repositories;
using RegisterApp.BLL.Services;
using RegisterApp.Models;
using RegisterApp.Models.APIModels;
using RegisterApp.Models.Entities;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using System;

namespace RegisterApp.Tests
{
    [TestFixture]
    public class Tests
    {


        [Test]
        [TestCase("aqd")]
        [TestCase("aqdqwer")]
        public void Validate_FirstName_Valid(string firstName)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(a => a.FirstName, new CustomerRequest { FirstName = firstName });
        }

        [Test]
        [TestCase("aq")]
        [TestCase("1234567890123456789012345678901234567890123456789012345")]
        public void Validate_FirstName_NotValid(string firstName)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(a => a.FirstName, new CustomerRequest { FirstName = firstName });
        }


        [Test]
        [TestCase("aqd")]
        [TestCase("aqdqwer")]
        public void Validate_SurName_Valid(string surName)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(a => a.SurName, new CustomerRequest { SurName = surName });
        }

        [Test]
        [TestCase("aq")]
        [TestCase("1234567890123456789012345678901234567890123456789012345")]
        public void Validate_SurName_NotValid(string surName)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(a => a.SurName, new CustomerRequest { SurName = surName });
        }


        [Test]
        [TestCase("XX-123456")]
        public void Validate_PolicyNumber_Valid(string policynumber)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(a => a.PolicyReferenceNumber, new CustomerRequest { PolicyReferenceNumber = policynumber });
        }

        [Test]
        [TestCase("X-123456")]
        [TestCase("XX-AB12345")]
        public void Validate_PolicyNumber_NotValid(string policynumber)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(a => a.PolicyReferenceNumber, new CustomerRequest { PolicyReferenceNumber = policynumber });
        }


        [Test]
        [TestCase("abcd@ab.co.uk")]
        [TestCase("abcd@ab.com")]
        [TestCase("abcdabcd@ababc.co.uk")]
        [TestCase("abcdabcd@ababc.com")]
        public void Validate_Email_Valid(string email)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(a => a.EmailAddress, new CustomerRequest { EmailAddress = email });
        }

        [Test]
        [TestCase("abcdabcd@ababc.info")]
        [TestCase("abc@ababc.co.uk")]
        [TestCase("abcdabcd@a.com")]
        public void Validate_Email_NotValid(string email)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(a => a.EmailAddress, new CustomerRequest { EmailAddress = email });
        }


        private static List<DateTime> validDOBList = new List<DateTime>
            {
                DateTime.Now.Date.AddYears(-18).Date,
                DateTime.Now.Date.AddYears(-19).Date,
                DateTime.Now.Date.AddYears(-20).Date
            };
        private static List<DateTime> notValidDOBList = new List<DateTime>
            {
                DateTime.Now.Date.AddYears(-17).Date,
                DateTime.Now.Date.AddYears(-16).Date,
                DateTime.Now.Date.AddYears(2).Date
            };


        [Test]
        public void Validate_DOB_Valid([ValueSource(nameof(validDOBList))] DateTime validDOBDate)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(a => a.DOB, new CustomerRequest { DOB = validDOBDate });
        }

        [Test]
        public void Validate_DOB_NotValid([ValueSource(nameof(notValidDOBList))] DateTime notValidDOBDate)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(a => a.DOB, new CustomerRequest { DOB = notValidDOBDate });
        }



        private static List<CustomerRequest> customerRequestEmailDOBNotValid = new List<CustomerRequest>
        {
            new CustomerRequest {EmailAddress="ac@ababc.co.uk",DOB=DateTime.Now.Date.AddYears(-17).Date },
            new CustomerRequest {EmailAddress="ac@ababc.info",DOB=DateTime.Now.Date.AddYears(-16).Date },
            new CustomerRequest {EmailAddress="ac@ababc.info",DOB=DateTime.Now.Date.AddYears(-1000).Date },
            new CustomerRequest {EmailAddress=null,DOB=null },
            new CustomerRequest {EmailAddress="",DOB=null }
        };


        private static List<CustomerRequest> customerRequestEmailDOBValid = new List<CustomerRequest>
        {
            new CustomerRequest {EmailAddress="abcd@abc.com",DOB=DateTime.Now.Date.AddYears(-18).Date },
            new CustomerRequest {EmailAddress="abcd@abc.com",DOB=DateTime.Now.Date.AddYears(-19).Date },
            new CustomerRequest {EmailAddress="abcd@abc.com",DOB=null },
            new CustomerRequest {EmailAddress="",DOB=DateTime.Now.Date.AddYears(-20).Date },
            new CustomerRequest {EmailAddress=null,DOB=DateTime.Now.Date.AddYears(-80).Date }
        };


        [Test]
        public void Validate_Email_and_DOB_Valid([ValueSource(nameof(customerRequestEmailDOBValid))] CustomerRequest customerRequestValid)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, new CustomerRequest { EmailAddress = customerRequestValid.EmailAddress, DOB = customerRequestValid.DOB });
            customerValidator.ShouldNotHaveValidationErrorFor(x => x.DOB, new CustomerRequest { EmailAddress = customerRequestValid.EmailAddress, DOB = customerRequestValid.DOB });
        }


        [Test]
        public void Validate_Email_and_DOB_NotValid([ValueSource(nameof(customerRequestEmailDOBNotValid))] CustomerRequest customerRequestNotValid)
        {
            var customerValidator = new CustomerRequestValidator();
            customerValidator.ShouldHaveValidationErrorFor(x => x.EmailAddress, new CustomerRequest { EmailAddress = customerRequestNotValid.EmailAddress, DOB = customerRequestNotValid.DOB });
            customerValidator.ShouldHaveValidationErrorFor(x => x.DOB, new CustomerRequest { EmailAddress = customerRequestNotValid.EmailAddress, DOB = customerRequestNotValid.DOB });
        }


        //[Test]
        //public void ValidateCusotmerRequest_Valid()
        //{

        //    var customerRepoMock = new Mock<ICustomerRepository>();
        //    customerRepoMock.Setup(m => m.GetAll()).ReturnsAsync(customers).Verifiable();
        //    customerRepoMock.Setup(m => m.Insert(It.IsAny<CustomerTable>())).ReturnsAsync(new CustomerTable { CustomerID = 12 });

        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(m => m.CustomerRepository).Returns(customerRepoMock.Object);

        //    ICustomerService sut = new CustomerService(unitOfWorkMock.Object);

        //    CustomerRequest req = new CustomerRequest();
        //    req.EmailAddress = "email@email.com";
        //    req.SurName = "1234567";
        //    req.FirstName = "1234567";
        //    req.PolicyReferenceNumber = "xx-123456";
        //    req.CustomerID = 12;

        //    var actual = sut.RegisterCustomer(req).Result;

        //    //Assert

        //    Assert.IsNotNull(actual);
        //}

        //[Test]
        //public void Validate_CustomerRequest_NotValid()
        //{
        //    var customerRepoMock = new Mock<ICustomerRepository>();
        //    customerRepoMock.Setup(m => m.GetAll()).ReturnsAsync(customers).Verifiable();

        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(m => m.CustomerRepository).Returns(customerRepoMock.Object);

        //    ICustomerService sut = new CustomerService(unitOfWorkMock.Object);

        //    CustomerRequest req = new CustomerRequest();
        //    req.EmailAddress = "email@2.info";
        //    req.SurName = "5";
        //    req.FirstName = "123456";
        //    req.PolicyReferenceNumber = "xx-1223456";
        //    req.CustomerID = 12;

        //    var actual = sut.RegisterCustomer(req);

        //    //Assert

        //    Assert.IsNotNull(actual);//assert that a result was returned
        //    Assert.AreEqual(CustomerModelContants.FailedMessageStatus, actual.Message.RequestStatus);//assert that actual result was as expected

        //}



    }
}