using NUnit.Framework;

using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    public class ProductDBTests
    {
        ProductDB db;

        [SetUp]
        public void ResetData()
        {
            db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            ProductProps p = (ProductProps)db.Retrieve(1);
            Assert.AreEqual(1, p.ProductID);
            Assert.AreEqual("A4CS", p.ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", p.Description);
            Assert.AreEqual(56.50m, p.UnitPrice);
            Assert.AreEqual(4637, p.OnHandQuantity);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
            Assert.AreEqual(16, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            ProductProps p = (ProductProps)db.Retrieve(2);
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(2));
        }

        // Foreign key constraint is currently set to a delete cascade, unit testing would require implementation of invoice classes.

        /*
        [Test]
        public void TestDeleteForeignKeyConstraint()
        {
            ProductProps p = (ProductProps)db.Retrieve(15);
            Assert.Throws<MySqlException>(() => db.Delete(p));
        }
        */

        [Test]
        public void TestUpdate()
        {
            ProductProps p = (ProductProps)db.Retrieve(2);
            p.ProductCode = "HDHD";
            p.Description = "Hot Dawg";
            p.UnitPrice = 10.00m;
            p.OnHandQuantity = 3;
            Assert.True(db.Update(p));
            p = (ProductProps)db.Retrieve(2);
            Assert.AreEqual("HDHD", p.ProductCode);
            Assert.AreEqual("Hot Dawg", p.Description);
            Assert.AreEqual(10.00m, p.UnitPrice);
            Assert.AreEqual(3, p.OnHandQuantity);
        }

        // Products does not reference any other tables
        /*
        [Test]
        public void TestUpdateForeignKeyConstraint()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(2);
            p.Name = "Minnie Mouse";
            p.Address = "101 Main Street";
            p.City = "Orlando";
            p.State = "ZZ";
            p.ZipCode = "10001";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }
        */

        [Test]
        public void TestUpdateFieldTooLong()
        {
            ProductProps p = (ProductProps)db.Retrieve(2);
            p.ProductCode = "Here is a product code that is longer than 10 characters.";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            ProductProps p = new ProductProps();
            p.ProductCode = "PCA";
            p.Description = "It sure is something";
            p.UnitPrice = 1000.0000m;
            p.OnHandQuantity = 2;
            db.Create(p);
            ProductProps p2 = (ProductProps)db.Retrieve(p.ProductID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

        // Primary key is auto-incrementing, no primary key violation test needed
        // However.....InvoiceLineItems references ProductCode as a foreign key/composite primary key, so we should probably add a unique constraint.
        // This is not listed in the scope of this lab, but I would definitely do so, and several other SQL changes, in the field.

        /*
        [Test]
        public void TestCreatePrimaryKeyViolation()
        {
            ProductProps p = new ProductProps();
            p.ProductCode = "A4VB";
            p.Description = "It sure is something";
            p.UnitPrice = 1000.0000m;
            p.OnHandQuantity = 2;
            Assert.Throws<MySqlException>(() => db.Create(p));
        }
        */
    }
}