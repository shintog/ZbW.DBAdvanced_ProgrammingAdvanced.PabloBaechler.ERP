namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting")]
    public partial class Accounting
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerNr { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ZIP { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvoiceDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceNr { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceAmountNet { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceAmountGross { get; set; }
    }
}
