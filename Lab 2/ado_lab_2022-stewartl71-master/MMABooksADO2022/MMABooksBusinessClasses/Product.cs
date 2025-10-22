using System;
using System.Collections.Generic;
using System.Text;

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
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    productCode = value;
                else
                    throw new ArgumentOutOfRangeException("Must be at least one character and no more than 50 characters");
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
                    productCode = value;
                else
                    throw new ArgumentOutOfRangeException("Must be at least one character and no more than 50 characters");
            }
        public int OnHandQuantity
            { get { return onHandQuantity; }

        public decimal UnitPrice
            { get { return unitPrice; } }
    }
}
