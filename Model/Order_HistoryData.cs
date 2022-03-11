namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Order_HistoryData
    {
        public int OrderNr { get; set; }

        public DateTime Date { get; set; }

        public string Customer { get; set; }

        public int CustomerNr { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
