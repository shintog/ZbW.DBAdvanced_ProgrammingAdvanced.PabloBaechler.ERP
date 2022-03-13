namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

    public class Position_HistoryData
    {
        public int PositionNr { get; set; }

        public int Order { get; set; }

        public int Article { get; set; }

        public decimal Amount { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
