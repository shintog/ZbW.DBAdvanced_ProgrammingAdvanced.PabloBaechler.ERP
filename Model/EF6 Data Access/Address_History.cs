namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Address_History
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string AddressKey { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressNr { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Street { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ZIP { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string City { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "datetime2")]
        public DateTime SysStartTime { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "datetime2")]
        public DateTime SysEndTime { get; set; }
    }
}
