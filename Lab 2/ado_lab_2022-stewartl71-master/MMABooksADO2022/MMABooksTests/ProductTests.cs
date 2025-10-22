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
    }
}
