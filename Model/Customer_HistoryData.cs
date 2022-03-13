namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

    public class Customer_HistoryData
    {
        public string CustomerKey { get; set; }

        public int CustomerNr { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int AddressNr { get; set; }

        public string EMail { get; set; }

        public string Website { get; set; }

        public byte[] Password { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
