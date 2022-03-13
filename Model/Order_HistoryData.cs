namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

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
