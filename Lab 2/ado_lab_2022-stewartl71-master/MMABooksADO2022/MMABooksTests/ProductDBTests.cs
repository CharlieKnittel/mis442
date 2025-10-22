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
        
        //Because properly testing the creation of new products requires deleting them after, and deleting products requires creating them first, I am using this test for both.
        [Test]
        public void TestCreateAndDeleteProduct()
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

        [Test]
        public void TestUpdateProduct()
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

            //Set up data for update
            Product q = new Product();
            q.ProductCode = "A1C";
            q.Description = "Chemistry 102";
            q.OnHandQuantity = 2;
            q.UnitPrice = 11.99m;

            // Update the product
            bool updated = ProductDB.UpdateProduct(p, q);

            // Verify the product has the new data
            Product updatedProduct = ProductDB.GetProduct("A1C");
            Assert.IsTrue(updated);
            Assert.AreEqual("A1C", updatedProduct.ProductCode);
            Assert.AreEqual("Chemistry 102", updatedProduct.Description);
            Assert.AreEqual(2, updatedProduct.OnHandQuantity);
            Assert.AreEqual(11.99m, updatedProduct.UnitPrice);

            // Retrieve it again so we can delete the extra instance
            p = ProductDB.GetProduct("A1C");

            // Clear out the extra product
            bool deleted = ProductDB.DeleteProduct(p);

            // Verify the product no longer exists
            Product deletedProduct = ProductDB.GetProduct(productCode);
            Assert.IsTrue(deleted, "Failed to delete the test product.");
            Assert.IsNull(deletedProduct);
        }
    }
}
