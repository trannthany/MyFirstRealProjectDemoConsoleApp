using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite_Automation_XML
{
    class Quote_Job
    {
        public int QuoteID { get; set; }
        public string QuoteDesciptn { get; set; }

        public string RevisionDate { get; set; }
        public string City { get; set; }

        public string SalesPerson { get; set; }
        public string ContactPerson { get; set; }

        public double CostPrice { get; set; }
        public double GrossPrice { get; set; }
        public string JobDiscription { get; set; }
        public double NetQuotation { get; set; }

        public double Hours { get; set; }


        public override string ToString() 
        { 
            return string.Format("Quote ID: {0}\n" +
                "Quote Description: {1}\n" +
                "Revision Date: {2}\n" +
                "City: {3}\n" +
                "Sales Person: {4}\n" +
                "Contact Person: {5}\n" +
                "Cost/Price: ${6}\n" +
                "Gross Price: ${7}\n" +
                "Job Description: {8}\n" +
                "Net Quotation: ${9}\n" +
                "Hours: {10}", QuoteID, QuoteDesciptn, RevisionDate, City, SalesPerson,
                ContactPerson, CostPrice, GrossPrice, JobDiscription, NetQuotation, Hours);
        }



    }
}
