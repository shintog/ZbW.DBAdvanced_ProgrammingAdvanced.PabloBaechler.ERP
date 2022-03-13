namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System.Collections.Generic;

    public class CurrencyData
    {
        public string CurrencyCode { get; set; }
        public string Name { get; set; }

        public virtual List<ArticleData> Articles { get; set; }

        public virtual List<ArticleData> Articles1 { get; set; }
    }
}
