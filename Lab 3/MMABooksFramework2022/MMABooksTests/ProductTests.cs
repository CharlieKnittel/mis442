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
            Customer c = new Customer("2");
            Assert.AreEqual(2, c.CustomerID);
            Assert.IsTrue(c.Name.Length > 0);
            Assert.IsTrue(c.Address.Length > 0);
            Assert.IsTrue(c.City.Length > 0);
            Assert.IsTrue(c.State.Length > 0);
            Assert.IsTrue(c.ZipCode.Length > 0);
            Assert.IsFalse(c.IsNew);
            Assert.IsTrue(c.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer();
            c.Name = "Who am I";
            c.Address = "Some Address";
            c.City = "Somewhere";
            c.State = "OR";
            c.ZipCode = "11111";
            c.Save();
            Customer c2 = new Customer(c.CustomerID.ToString());
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
            Assert.AreEqual(c2.Address, c.Address);
            Assert.AreEqual(c2.City, c.City);
            Assert.AreEqual(c2.State, c.State);
            Assert.AreEqual(c2.ZipCode, c.ZipCode);
        }

        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer("3");
            c.Name = "Edited Name";
            c.Address = "Edited Address";
            c.City = "Edited City";
            c.State = "SC";
            c.ZipCode = "12345";
            c.Save();

            Customer c2 = new Customer("3");
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
            Assert.AreEqual(c2.Address, c.Address);
            Assert.AreEqual(c2.City, c.City);
            Assert.AreEqual(c2.State, c.State);
            Assert.AreEqual(c2.ZipCode, c.ZipCode);
        }

        [Test]
        public void TestDelete()
        {
            Customer c = new Customer("4");
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer("4"));
        }

        [Test]
        public void TestGetList()
        {
            Customer c = new Customer();
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerID);
            Assert.AreEqual("Molunguri, A", customers[0].Name);
            Assert.AreEqual("1108 Johanna Bay Drive", customers[0].Address);
            Assert.AreEqual("Birmingham", customers[0].City);
            Assert.AreEqual("AL", customers[0].State);
            Assert.AreEqual("35216-6909", customers[0].ZipCode);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
            c.Name = "??";
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Customer c = new Customer();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.State = "CAAAAAAAA");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer("4");
            Customer c2 = new Customer("4");

            c1.Name = "Updated first";
            c1.Save();

            c2.Name = "Updated second";
            Assert.Throws<Exception>(() => c2.Save());
        }
    }
}