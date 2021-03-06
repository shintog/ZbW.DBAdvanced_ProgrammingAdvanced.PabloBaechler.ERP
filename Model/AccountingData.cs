namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

    public class AccountingData
    {
        public int CustomerNr { get; set; }
        public string Currency { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public decimal ZIP { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int InvoiceNr { get; set; }

        public string InvoiceAmountNet { get; set; }

        public string InvoiceAmountGross { get; set; }
    }
}
