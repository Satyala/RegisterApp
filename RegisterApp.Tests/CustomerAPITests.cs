using Moq;
using NUnit.Framework;
using RegisterApp.BLL.Repositories;
using RegisterApp.BLL.Services;
using RegisterApp.Models;
using RegisterApp.Models.APIModels;
using RegisterApp.Models.Entities;
using System.Collections.Generic;

namespace RegisterApp.Tests
{
    [TestFixture]
    public class Tests
    {
        List<CustomerTable> customers = new List<CustomerTable>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidateCusotmerRequest_Valid()
        {

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(m => m.GetAll()).Returns(customers).Verifiable();
            customerRepoMock.Setup(m => m.Insert(It.IsAny<CustomerTable>())).Returns(new CustomerTable { CustomerID = 12 });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.CustomerRepository).Returns(customerRepoMock.Object);

            ICustomerService sut = new CustomerService(unitOfWorkMock.Object);

            CustomerRequest req = new CustomerRequest();
            req.EmailAddress = "email@email.com";
            req.SurName = "1234567";
            req.FirstName = "1234567";
            req.PolicyReferenceNumber = "xx-123456";
            req.CustomerID = 12;

            var actual = sut.RegisterCustomer(req);

            //Assert

            Assert.IsNotNull(actual);//assert that a result was returned
            Assert.AreEqual(CustomerModelContants.SucessMessageStatus, actual.Message.RequestStatus);//assert that actual result was as expected

        }

        [Test]
        public void ValidateEmail_NotValid()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(m => m.GetAll()).Returns(customers).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.CustomerRepository).Returns(customerRepoMock.Object);

            ICustomerService sut = new CustomerService(unitOfWorkMock.Object);

            CustomerRequest req = new CustomerRequest();
            req.EmailAddress = "email@2.info";
            req.SurName = "5";
            req.FirstName = "123456";
            req.PolicyReferenceNumber = "xx-1223456";
            req.CustomerID = 12;

            var actual = sut.RegisterCustomer(req);

            //Assert

            Assert.IsNotNull(actual);//assert that a result was returned
            Assert.AreEqual(CustomerModelContants.FailedMessageStatus, actual.Message.RequestStatus);//assert that actual result was as expected

        }

        //Further tests ion fluent API itself

        //Valid / not valid Email and DOB Either tests

        //Valid / not valid first name and surname

        //Valid / not valid policy number tests

        //Duplicate customer id tests




    }
}