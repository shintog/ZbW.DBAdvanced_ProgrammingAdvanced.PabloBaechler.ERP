namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

    public class Article_HistoryData
    {

        public int ArticleNr { get; set; }

        public string Name { get; set; }

        public string Designation { get; set; }

        public int Classification { get; set; }

        public decimal PurchasingPrice { get; set; }

        public string PPCurrency { get; set; }

        public decimal SalesPrice { get; set; }

        public string SPCurrency { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
