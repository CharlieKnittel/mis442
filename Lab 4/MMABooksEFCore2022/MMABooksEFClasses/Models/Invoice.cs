using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Shipping { get; set; }
        public decimal InvoiceTotal { get; set; }

        public override string ToString()
        {
            return InvoiceId + ": " + CustomerId + " - " + InvoiceDate + ", " + ProductTotal.ToString("c") + " Item Total + " + 
                SalesTax.ToString("c") + " Sales Tax + " + Shipping.ToString("c") + " Shipping = " + InvoiceTotal.ToString("c") + " Total";
        }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }
    }
}
