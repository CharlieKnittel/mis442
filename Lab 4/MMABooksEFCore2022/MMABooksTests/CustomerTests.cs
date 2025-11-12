using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.MarisModels;
//Removed MMABooksEFClasses.Models because references to Customer were ambiguous and StateTests used MarisModels
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        MMABooksContext dbContext;
        Customer? c;
        List<Customer>? customers;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.CustomerId).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual("Molunguri, A", customers[0].Name);
            PrintAll(customers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(2);
            Assert.IsNotNull(c);
            Assert.AreEqual("Muhinyi, Mauda", c.Name);
            Console.WriteLine(c);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the customers who live in OR
        }

        [Test]
        public void GetWithInvoicesTest()
        {
           // get the customer whose id is 20 and all of the invoices for that customer
        }

        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.StateCode,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.StateCode, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        public void DeleteTest()
        {

        }

        [Test]
        public void CreateTest()
        {

        }

        [Test]
        public void UpdateTest()
        {

        }

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }
    }
}