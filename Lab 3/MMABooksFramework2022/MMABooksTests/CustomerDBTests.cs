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
    public class CustomerDBTests
    {
        CustomerDB db;

        [SetUp]
        public void ResetData()
        {
            db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(1);
            Assert.AreEqual(1, p.CustomerID);
            Assert.AreEqual("Molunguri, A", p.Name);
            Assert.AreEqual("1108 Johanna Bay Drive", p.Address);
            Assert.AreEqual("Birmingham", p.City);
            Assert.AreEqual("AL", p.State);
            Assert.AreEqual("35216-6909", p.ZipCode);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<CustomerProps> list = (List<CustomerProps>)db.RetrieveAll();
            Assert.AreEqual(696, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(2);
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(2));
        }

        // Foreign key constraint is currently set to a delete cascade, unit testing would require implementation of invoice classes.

        /*
        [Test]
        public void TestDeleteForeignKeyConstraint()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(694);
            Assert.Throws<MySqlException>(() => db.Delete(p));
        }
        */

        [Test]
        public void TestUpdate()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(2);
            p.Name = "Minnie Mouse";
            p.Address = "101 Main Street";
            p.City = "Orlando";
            p.State = "FL";
            p.ZipCode = "10001";
            Assert.True(db.Update(p));
            p = (CustomerProps)db.Retrieve(2);
            Assert.AreEqual("Minnie Mouse", p.Name);
            Assert.AreEqual("101 Main Street", p.Address);
            Assert.AreEqual("Orlando", p.City);
            Assert.AreEqual("FL", p.State);
            Assert.AreEqual("10001", p.ZipCode);
        }

        [Test]
        public void TestUpdateFieldTooLong()
        {
            StateProps p = (StateProps)db.Retrieve("OR");
            p.Name = "Oregon is the state where Crater Lake National Park is.";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            StateProps p = new StateProps();
            p.Code = "??";
            p.Name = "Where am I";
            db.Create(p);
            StateProps p2 = (StateProps)db.Retrieve(p.Code);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

        [Test]
        public void TestCreatePrimaryKeyViolation()
        {
            StateProps p = new StateProps();
            p.Code = "OR";
            p.Name = "Oregon";
            Assert.Throws<MySqlException>(() => db.Create(p));
        }
    }
}