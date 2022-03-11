namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public class CurrencyData
    {
        public string CurrencyCode { get; set; }
        public string Name { get; set; }

        public virtual List<ArticleData> Articles { get; set; }

        public virtual List<ArticleData> Articles1 { get; set; }
    }
}
