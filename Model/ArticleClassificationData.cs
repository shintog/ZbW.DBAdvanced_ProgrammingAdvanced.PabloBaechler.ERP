namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System.Collections.Generic;

    public class ArticleClassificationData
    {
        public int ClassificationNr { get; set; }

        public int? Parent { get; set; }

        public string Name { get; set; }

        public virtual List<ArticleData> Articles { get; set; }

        public virtual List<ArticleClassificationData> ArticleClassification1 { get; set; }

        public virtual ArticleClassificationData ArticleClassification2 { get; set; }
    }
}
