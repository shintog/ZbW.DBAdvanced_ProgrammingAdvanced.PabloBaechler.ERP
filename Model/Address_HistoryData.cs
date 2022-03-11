namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Address_HistoryData
    {  
        public string AddressKey { get; set; }
        public int AddressNr { get; set; }
        
        public string Street { get; set; }
        public string Number { get; set; }
        public decimal ZIP { get; set; }
        
        public string City { get; set; }
        
        public DateTime SysStartTime { get; set; }
        
        public DateTime SysEndTime { get; set; }
    }
}
