using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerOrder.Controllers;
using CustomerOrder.Models;
using System.Collections.Generic;
using Moq;
using System.Linq;
using System.Data.Entity;

namespace UnitTestCustomer
{
    [TestClass]
    public class UnitTest1
    {
        MockRepository repository;
        [TestMethod]
        public void TestCustomerOrderForeing()
        {
            // Arrange 
            //initialize some variables
            var Orders = GetTestOrders();
            var Customers = GetTestCustomers();
            var counter = 0;
           
            //Act
            foreach (Orders ListValue in Orders)
            {
                if (ListValue.Customer == Customers[2])
                    counter++;
            }

            //Assert
            Assert.AreEqual(2, counter);
        }



        private List<Customer> GetTestCustomers()
        {
            var TestCustomers = new List<Customer>();
            TestCustomers.Add(new Customer { CustomerID =1,  CompanyName = "Company 1", Country="El Salvador",Address="Street 1", ContactName="Name 1", TelephoneNumber ="2257-7777"});
            TestCustomers.Add(new Customer { CustomerID = 2, CompanyName = "Company 2", Country="El Salvador" , Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" });
            TestCustomers.Add(new Customer { CustomerID = 3, CompanyName = "Company 3", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" });
            TestCustomers.Add(new Customer { CustomerID = 4, CompanyName = "Company 4", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" });

            return TestCustomers;
        }

        private List<Orders> GetTestOrders()
        {
            var testCustomers = GetTestCustomers();
            var TestOrder = new List<Orders>();
            TestOrder.Add(new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[0], Date = DateTime.Now, Description = "TEST1", Quantity = 10 });
            TestOrder.Add(new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[1], Date = DateTime.Now, Description = "TEST2", Quantity = 10 });
            TestOrder.Add(new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[2], Date = DateTime.Now, Description = "TEST3", Quantity = 65 });
            TestOrder.Add(new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[2], Date = DateTime.Now, Description = "TEST4", Quantity = 15 });

            return TestOrder;
        }
    }
}
