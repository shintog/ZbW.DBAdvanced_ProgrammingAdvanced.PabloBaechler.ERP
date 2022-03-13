namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System.Collections.Generic;

    public class AddressData
    {
        public string AddressKey { get; set; }

        public int AddressNr { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public decimal ZIP { get; set; }

        public string City { get; set; }

        public virtual List<CustomerData> Customers { get; set; }
    }
}
