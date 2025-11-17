using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.Models;
//Removed MMABooksEFClasses.Models because references to Customer were ambiguous
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
            customers = dbContext.Customers.Where(c => c.StateCode == "OR").OrderBy(c => c.CustomerId).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual("Swenson, Vi", customers[0].Name);
            PrintAll(customers);
        }

        [Test]
        public void GetWithInvoicesTest()
        {
            // get the customer whose id is 20 and all of the invoices for that customer
            c = dbContext.Customers.Include("Invoices").Where(c => c.CustomerId == 20).SingleOrDefault();
            Assert.IsNotNull(c);
            Assert.AreEqual("Chamberland, Sarah", c.Name);
            Assert.AreEqual(3, c.Invoices.Count);
            Console.WriteLine(c);
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
            c = dbContext.Customers.Find(3);
            dbContext.Customers.Remove(c);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Customers.Find(3));
        }

        [Test]
        public void CreateTest()
        {
            c = new Customer();
            c.Name = "Doe, John";
            c.Address = "123 Main St";
            c.City = "Anytown";
            c.StateCode = "AK";
            c.ZipCode = "12345";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Customers.Find(c.CustomerId));
        }

        [Test]
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(4);
            c.Name = "Smythe, Ahmad";
            dbContext.Customers.Update(c);
            dbContext.SaveChanges();
            Assert.AreEqual("Smythe, Ahmad", dbContext.Customers.Find(4).Name);
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