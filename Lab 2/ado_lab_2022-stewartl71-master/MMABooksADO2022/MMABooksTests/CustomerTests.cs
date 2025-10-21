using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer def;
        private Customer c;

        [SetUp]
        public void Setup()
        {
            def = new Customer();
            c = new Customer(1, "Doe, John", "1234 Up St", "Uptown", "AL", "12345");
        }

        [Test]
        public void TestCustomerConstructor()
        {
            Assert.IsNotNull(def);
            Assert.AreEqual(null, def.Name);
            Assert.AreEqual(null, def.Address);
            Assert.AreEqual(null, def.City);
            Assert.AreEqual(null, def.State);
            Assert.AreEqual(null, def.ZipCode);

            Assert.IsNotNull(c);
            Assert.AreEqual("Doe, John", c.Name);
            Assert.AreEqual("1234 Up St", c.Address);
            Assert.AreEqual("Uptown", c.City);
            Assert.AreEqual("AL", c.State);
            Assert.AreEqual("12345", c.ZipCode);
        }
    }
}
