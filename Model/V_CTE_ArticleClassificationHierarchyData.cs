using System;
using System.Collections.Generic;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    public class V_CTE_ArticleClassificationHierarchyData
    {
        public int ClassificationNr { get; set; }
        public String Name { get; set; }
        public int? ParentProductID { get; set; }
        public int ClassificationLevel { get; set; }
        public virtual V_CTE_ArticleClassificationHierarchyData ReportsTo { get; set; }
        public virtual List<V_CTE_ArticleClassificationHierarchyData> Manages { get; set; }
    }
}
