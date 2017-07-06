using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerOrder.Models;
using System.Collections.Generic;
using System.Linq;

namespace CustomerOrderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCustomerOrderForeing()
        {
            // Arrange 
            //initialize some variables as a single list
            var Orders = GetTestOrders();
            var Customers = GetTestCustomers();
            var counter = 0;

            //Act
            foreach (Orders ListValue in Orders)
            {
                if (ListValue.Customer.CustomerID == Customers[2].CustomerID)
                    counter++;
            }

            //Assert
            Assert.AreEqual(2, counter);
        }

        [TestMethod]
        public void TestIQueryable()
        {

            //Arrange
            //initialize some variables as a IQueryable
            var testCustomers = GetTestCustomers();
            IQueryable<Orders> orders = new List<Orders> {
                  new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[0], Date = DateTime.Now, Description = "TEST1", Quantity = 10 },
                  new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[1], Date = DateTime.Now, Description = "TEST2", Quantity = 10 },
                  new Orders { OrdersID = 1, Amount = 50, Customer = testCustomers[2], Date = DateTime.Now, Description = "TEST3", Quantity = 65 },
                  new Orders{ OrdersID = 1, Amount = 50, Customer = testCustomers[2], Date = DateTime.Now, Description = "TEST4", Quantity = 15 }
            }.AsQueryable();

            //Act
            //Get a result like a query 
            var result = orders.Where(m => m.Customer.CustomerID == 3).ToList();

            //Assert
            Assert.AreEqual(2, result.Count());

        }

        [TestMethod]
        public void TestObjectAutenticity()
        {
            //Arrange
            var customer = new Customer { CustomerID = 5, CompanyName = "Company 1", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" };
            IQueryable<Customer> customers = new List<Customer> {
                    new Customer { CustomerID =1,  CompanyName = "Company 1", Country="El Salvador",Address="Street 1", ContactName="Name 1", TelephoneNumber ="2257-7777"},
                    new Customer { CustomerID = 2, CompanyName = "Company 2", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" },
                    new Customer { CustomerID = 3, CompanyName = "Company 3", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" },
                    new Customer { CustomerID = 4, CompanyName = "Company 4", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" }
            }.AsQueryable();

            //Act
            var customerTmp = customers.Where(m => m.CustomerID == 4).Single();

            //Assert
            //The id is diferent, so are not the same object
            Assert.AreNotEqual(customer, customerTmp);

        }

        private List<Customer> GetTestCustomers()
        {
            var TestCustomers = new List<Customer>();
            TestCustomers.Add(new Customer { CustomerID = 1, CompanyName = "Company 1", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" });
            TestCustomers.Add(new Customer { CustomerID = 2, CompanyName = "Company 2", Country = "El Salvador", Address = "Street 1", ContactName = "Name 1", TelephoneNumber = "2257-7777" });
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
