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

        [Test]
        public void TestCustomerIDSetter()
        {
            c.CustomerID = 2;
            Assert.AreEqual(2, c.CustomerID);
            c.CustomerID = 3;
            Assert.AreEqual(3, c.CustomerID);
        }

        [Test]
        public void TestSettersCustomerIDNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.CustomerID = -1);
        }

        [Test]
        public void TestNameSetter()
        {
            // call the setters
            c.Name = "Doe, Jane";
            // assert that the property now returns the new values
            Assert.AreEqual("Doe, Jane", c.Name);
            // the previous part of the test isn't sufficient because the setters might ALWAYS set the properties to these values
            // make sure that's not the case by providing a different set of values
            c.Name = "Doe, Jack";
            Assert.AreEqual("Doe, Jack", c.Name);
        }

        [Test]
        public void TestSettersNameTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789");
        }

        [Test]
        public void TestAddressSetter()
        {
            c.Address = "1234 Down St";
            Assert.AreEqual("1234 Down St", c.Address);
            c.Address = "1234 Main St";
            Assert.AreEqual("1234 Main St", c.Address);
        }

        [Test]
        public void TestSettersAddressTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Address = "012345678901234567890123456789012345678901234567890123456789");
        }

        [Test]
        public void TestCitySetter()
        {
            c.City = "Downtown";
            Assert.AreEqual("Downtown", c.City);
            c.City = "Suburbia";
            Assert.AreEqual("Suburbia", c.City);
        }

        [Test]
        public void TestSettersCityTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.City = "012345678901234567890123456789");
        }

        [Test]
        public void TestStateSetter()
        {
            c.State = "WY";
            Assert.AreEqual("WY", c.State);
            c.State = "MN";
            Assert.AreEqual("MN", c.State);
        }

        [Test]
        public void TestSettersStateTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.State = "012");
        }

        [Test]
        public void TestZipCodeSetter()
        {
            c.ZipCode = "54321";
            Assert.AreEqual("54321", c.ZipCode);
            c.ZipCode = "33333";
            Assert.AreEqual("33333", c.ZipCode);
        }

        [Test]
        public void TestSettersZipCodeTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.ZipCode = "01234567890123456789");
        }

        //The tests above also test the getters.

        [Test]
        public void TestCustomerToString()
        {
            Assert.IsTrue(c.ToString().Contains("1 "));
            Assert.IsTrue(c.ToString().Contains("Doe, John"));
            Assert.IsTrue(c.ToString().Contains("1234 Up St"));
            Assert.IsTrue(c.ToString().Contains("Uptown"));
            Assert.IsTrue(c.ToString().Contains("AL"));
            Assert.IsTrue(c.ToString().Contains("12345"));
        }
    }
}
