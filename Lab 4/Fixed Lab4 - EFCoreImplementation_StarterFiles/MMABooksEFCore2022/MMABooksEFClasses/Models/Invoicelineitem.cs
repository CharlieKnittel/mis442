using MMABooksEFClasses.MarisModels;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace MMABooksEFClasses.Models
{
    public partial class Invoicelineitem
    {
        public int InvoiceId { get; set; }
        public string ProductCode { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }

        public override string ToString()
        {
            return InvoiceId + ": " + ProductCode + ", " + UnitPrice + " * " + Quantity + " units = " + ItemTotal;
        }

        public virtual Invoice Invoice { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
