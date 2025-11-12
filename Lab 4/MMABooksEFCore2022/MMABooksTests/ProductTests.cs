using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.MarisModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        MMABooksContext dbContext;
        Products? p;
        List<Products>? products;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            products = dbContext.Products.OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
            PrintAll(products);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            p = dbContext.Products.Find("A4VB");
            Assert.IsNotNull(p);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with VB 2010", p.Description);
            Console.WriteLine(p);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the products that have a unit price of 56.50
            products = dbContext.Products.Where(p => p.UnitPrice == 56.50m).OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(7, products.Count);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
            PrintAll(products);
        }

        [Test]
        public void GetWithCalculatedFieldTest()
        {
            // get a list of objects that include the productcode, unitprice, quantity and inventoryvalue
            var products = dbContext.Products.Select(
            p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
            OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        [Test]
        public void DeleteTest()
        {
            p = dbContext.Products.Find("ADC4");
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Products.Find("ADC4"));
        }

        [Test]
        public void CreateTest()
        {
            p = new Products();
            p.ProductCode = "TTBG";
            p.Description = "Coding Print Tote Bag";
            p.UnitPrice = 15.00m;
            p.OnHandQuantity = 304;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Products.Find("TTBG"));
        }

        [Test]
        public void UpdateTest()
        {
            p = dbContext.Products.Find("ADV4");
            p.Description = "Murach's ADO.NET 4 with VB 2020";
            dbContext.Products.Update(p);
            dbContext.SaveChanges();
            Assert.AreEqual("Murach's ADO.NET 4 with VB 2020", dbContext.Products.Find("ADV4").Description);
        }

        public void PrintAll(List<Products> products)
        {
            foreach (Products p in products)
            {
                Console.WriteLine(p);
            }
        }
    }
}