namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class ArticleClassification_HistoryData
    {
        public int ClassificationNr { get; set; }

        public int? Parent { get; set; }

        public string Name { get; set; }

        public DateTime SysStartTime { get; set; }

        public DateTime SysEndTime { get; set; }
    }
}
