using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    public class CustomerDBTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestGetCustomer()
        {
            Customer c = CustomerDB.GetCustomer(1);
            Assert.AreEqual(1, c.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            Customer c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            Assert.AreEqual("Mickey Mouse", c.Name);
        }

        [Test]
        public void TestDeleteCustomer()
        {
            // Create and add a customer
            Customer c = new Customer();
            c.Name = "Donald Duck";
            c.Address = "202 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "20202";

            int customerID = CustomerDB.AddCustomer(c);

            // Retrieve it again so we have a complete Customer object
            c = CustomerDB.GetCustomer(customerID);

            // Delete the customer
            CustomerDB.DeleteCustomer(c);

            // Verify the customer no longer exists
            Customer deletedCustomer = CustomerDB.GetCustomer(customerID);
            Assert.IsNull(deletedCustomer, "Customer should be deleted but still exists.");
        }
    }
}
