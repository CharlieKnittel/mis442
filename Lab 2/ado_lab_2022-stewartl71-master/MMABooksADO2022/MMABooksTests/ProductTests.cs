using MMABooksBusinessClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksTests
{
    public class ProductTests
    {
        private Product def;
        private Product c;

        [SetUp]
        public void Setup()
        {
            def = new Product();
            c = new Product("A1B", "Chemistry 101", 3, 10.99m);
        }

        [Test]
        public void TestProductConstructor()
        {
            Assert.IsNotNull(def);
            Assert.AreEqual(null, def.ProductCode);
            Assert.AreEqual(null, def.Description);

            Assert.IsNotNull(c);
            Assert.AreEqual("A1B", c.ProductCode);
            Assert.AreEqual("Chemistry 101", c.Description);
            Assert.AreEqual(3, c.OnHandQuantity);
            Assert.AreEqual(10.99m, c.UnitPrice);
        }

        [Test]
        public void TestProductCodeSetter()
        {
            c.ProductCode = "A1C";
            Assert.AreEqual("A1C", c.ProductCode);
            c.ProductCode = "A1D";
            Assert.AreEqual("A1D", c.ProductCode);
        }

        [Test]
        public void TestSettersProductCodeTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.ProductCode = "01234567890123456789");
        }

        [Test]
        public void TestDescriptionSetter()
        {
            c.Description = "Biology 101";
            Assert.AreEqual("Biology 101", c.Description);
            c.Description = "Chemistry 201";
            Assert.AreEqual("Chemistry 201", c.Description);
        }

        [Test]
        public void TestSettersDescriptionTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Description = "012345678901234567890123456789012345678901234567890123456789");
        }

        [Test]
        public void TestOnHandQuantitySetter()
        {
            c.OnHandQuantity = 2;
            Assert.AreEqual(2, c.OnHandQuantity);
            c.OnHandQuantity = 1;
            Assert.AreEqual(1, c.OnHandQuantity);
        }

        [Test]
        public void TestSettersOnHandQuantityNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.OnHandQuantity = -1);
        }

        [Test]
        public void TestUnitPriceSetter()
        {
            c.UnitPrice = 2.99m;
            Assert.AreEqual(2.99m, c.UnitPrice);
            c.UnitPrice = 9.49m;
            Assert.AreEqual(9.49m, c.UnitPrice);
        }

        [Test]
        public void TestUnitPriceNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.UnitPrice = -1.99m);
        }

        [Test]
        public void TestSettersUnitPriceTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.UnitPrice = 12345678901234567890.39m);
        }

        [Test]
        public void TestProductToString()
        {
            Assert.IsTrue(c.ToString().Contains("A1B"));
            Assert.IsTrue(c.ToString().Contains("Chemistry 101"));
            Assert.IsTrue(c.ToString().Contains("3"));
            Assert.IsTrue(c.ToString().Contains("10.99"));
        }
    }
}
