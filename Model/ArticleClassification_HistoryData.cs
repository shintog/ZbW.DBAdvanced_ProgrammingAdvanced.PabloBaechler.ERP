namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;

    public class ArticleClassification_HistoryData
    {
        public int ClassificationNr { get; set; }

        public int? Parent { get; set; }

        public string Name { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
