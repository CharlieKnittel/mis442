using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {

        [SetUp]
        public void TestResetDatabase()
        {
            ProductDB db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestNewProductConstructor()
        {
            // not in Data Store - no code
            Product p = new Product();
            Assert.AreEqual(0, p.ProductID);
            Assert.AreEqual(string.Empty, p.ProductCode);
            Assert.AreEqual(string.Empty, p.Description);
            Assert.AreEqual(0m, p.UnitPrice);
            Assert.AreEqual(0, p.OnHandQuantity);
            Assert.IsTrue(p.IsNew);
            Assert.IsFalse(p.IsValid);
        }


        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Product p = new Product("2");
            Assert.AreEqual(2, p.ProductID);
            Assert.IsTrue(p.ProductCode.Length > 0);
            Assert.IsTrue(p.Description.Length > 0);
            Assert.IsTrue(p.UnitPrice > 0);
            Assert.IsTrue(p.OnHandQuantity > 0);
            Assert.IsFalse(p.IsNew);
            Assert.IsTrue(p.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product();
            p.ProductCode = "SMTH";
            p.Description = "Idk what it is";
            p.UnitPrice = 2.00m;
            p.OnHandQuantity = 4000;
            p.Save();
            Product p2 = new Product(p.ProductID.ToString());
            Assert.AreEqual(p2.ProductID, p.ProductID);
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
            Assert.AreEqual(p2.UnitPrice, p.UnitPrice);
            Assert.AreEqual(p2.OnHandQuantity, p.OnHandQuantity);
        }

        [Test]
        public void TestUpdate()
        {
            Product p = new Product("3");
            p.ProductCode = "EditCode";
            p.Description = "Edited Description";
            p.UnitPrice = 10m;
            p.OnHandQuantity = 3000;
            p.Save();

            Product p2 = new Product("3");
            Assert.AreEqual(p2.ProductID, p.ProductID);
            Assert.AreEqual(p2.ProductCode,p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
            Assert.AreEqual(p2.UnitPrice, p.UnitPrice);
            Assert.AreEqual(p2.OnHandQuantity, p.OnHandQuantity);
        }

        [Test]
        public void TestDelete()
        {
            Product p = new Product("4");
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product("4"));
        }

        [Test]
        public void TestGetList()
        {
            Product p = new Product();
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual(1, products[0].ProductID);
            Assert.AreEqual("A4CS", products[0].ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
            Assert.AreEqual(56.5m, products[0].UnitPrice);
            Assert.AreEqual(4637, products[0].OnHandQuantity);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
            p.ProductCode = "??";
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Product p = new Product();
            Assert.Throws<ArgumentOutOfRangeException>(() => p.ProductCode = "CAAAAAAAAAAAAAAAAAAA");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product("4");
            Product p2 = new Product("4");

            p1.Description = "Updated first";
            p1.Save();

            p2.Description = "Updated second";
            Assert.Throws<Exception>(() => p2.Save());
        }
    }
}