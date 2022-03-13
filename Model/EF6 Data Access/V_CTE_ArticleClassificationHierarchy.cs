using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    public class V_CTE_ArticleClassificationHierarchy
    {
        [Key]
        [Required]
        public int ClassificationNr { get; set; }
        public String Name { get; set; }
        public int? ParentProductID { get; set; }
        public int ClassificationLevel { get; set; }
        public virtual V_CTE_ArticleClassificationHierarchy ReportsTo { get; set; }
        public virtual ICollection<V_CTE_ArticleClassificationHierarchy> Manages { get; set; }
    }
}
