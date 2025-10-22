using MMABooksBusinessClasses;
using MMABooksDBClasses;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Collections;

namespace MMABooksTests
{
    public class ProductDBTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestGetProduct()
        {
            Product p = ProductDB.GetProduct("A4CS");
            Assert.AreEqual("A4CS", p.ProductCode);
        }
        
        [Test]
        public void TestGetProducts()
        {
            List<Product> products = ProductDB.GetProducts();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
        }
        
        [Test]
        public void TestGetProductDBUnavailable()
        {
            Assert.Throws<MySqlException>(() => ProductDB.GetProduct("A4CS"));
        }
        
        [Test]
        public void TestCreateProduct()
        {
            // Ensure clean slate
            Product existing = ProductDB.GetProduct("A1B");
            if (existing != null)
            {
                ProductDB.DeleteProduct(existing);
            }

            Product p = new Product();
            p.ProductCode = "A1B";
            p.Description = "Chemistry 101";
            p.OnHandQuantity = 3;
            p.UnitPrice = 10.99m;

            string productCode = ProductDB.AddProduct(p);
            p = ProductDB.GetProduct("A1B");
            Assert.AreEqual("A1B", p.ProductCode, "Failed to create new product.");

            // Retrieve it again so we can delete the extra instance
            p = ProductDB.GetProduct(productCode);

            // Clear out the extra product
            bool deleted = ProductDB.DeleteProduct(p);

            // Verify the product no longer exists
            Product deletedProduct = ProductDB.GetProduct(productCode);
            Assert.IsTrue(deleted, "Failed to delete the test product.");
            Assert.IsNull(deletedProduct);
        }
        /*
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
            bool deleted = CustomerDB.DeleteCustomer(c);

            // Verify the customer no longer exists
            Customer deletedCustomer = CustomerDB.GetCustomer(customerID);
            Assert.IsTrue(deleted);
            Assert.IsNull(deletedCustomer);
        }

        [Test]
        public void TestUpdateCustomer()
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

            // Set up data for update
            Customer d = new Customer();
            d.Name = "Daisy Duck";
            d.Address = "203 Main Street";
            d.City = "Boston";
            d.State = "MA";
            d.ZipCode = "30303";

            // Update the customer
            bool updated = CustomerDB.UpdateCustomer(c, d);

            // Verify the customer has the new data
            Customer updatedCustomer = CustomerDB.GetCustomer(customerID);
            Assert.IsTrue(updated);
            Assert.AreEqual("Daisy Duck", updatedCustomer.Name);
            Assert.AreEqual("203 Main Street", updatedCustomer.Address);
            Assert.AreEqual("Boston", updatedCustomer.City);
            Assert.AreEqual("MA", updatedCustomer.State);
            Assert.AreEqual("30303", updatedCustomer.ZipCode);
        }
        */
    }
}
