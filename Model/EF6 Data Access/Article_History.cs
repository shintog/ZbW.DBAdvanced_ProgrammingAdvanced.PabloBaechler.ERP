namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Article_History
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ArticleNr { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Designation { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Classification { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal PurchasingPrice { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string PPCurrency { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal SalesPrice { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string SPCurrency { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "datetime2")]
        public DateTime SysStartTime { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "datetime2")]
        public DateTime SysEndTime { get; set; }
    }
}
