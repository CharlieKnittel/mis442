using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        public Product() { }

        public Product(string productcode, string description, int onhandquantity, decimal unitprice) 
        {
            ProductCode = productcode;
            Description = description;
            OnHandQuantity = onhandquantity;
            UnitPrice = unitprice;
        }

        //instance variables
        private string productCode;
        private string description;
        private int onHandQuantity;
        private decimal unitPrice;

        public string ProductCode
        {
            get
            {
                return productCode; ;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 10)
                    productCode = value;
                else
                    throw new ArgumentOutOfRangeException("Must be at least one character and no more than 10 characters");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    description = value;
                else
                    throw new ArgumentOutOfRangeException("Must be at least one character and no more than 50 characters");
            }
        }

        public int OnHandQuantity
        {
            get
            {
                return onHandQuantity;
            }
            set
            {
                if (value > 0)
                    onHandQuantity = value;
                else
                    throw new ArgumentOutOfRangeException("On Hand Quantity must be a positive integer");
            }
        }

        public decimal UnitPrice
        { 
            get 
            { 
                return unitPrice; 
            }
            set
            {
                if (value > 0 && value.ToString().Trim().Length <= 11)
                {
                    unitPrice = value;
                }
                else
                    throw new ArgumentOutOfRangeException
                        ("On Hand Quantity must be a positive decimal with no more than 10 total digits and no more than 4 digits after the decimal.");
            }
        }

        public override string ToString()
        {
            return ProductCode + ": " + Description + " | " + OnHandQuantity.ToString() + " on hand, $" + UnitPrice.ToString("C") + " per unit";
        }
    }
}
