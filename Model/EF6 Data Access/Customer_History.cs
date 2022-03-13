namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Customer_History
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CustomerKey { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerNr { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Address { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressNr { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string EMail { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string Website { get; set; }

        [Key]
        [Column(Order = 7)]
        [MaxLength(64)]
        public byte[] Password { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "datetime2")]
        public DateTime SysStartTime { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "datetime2")]
        public DateTime SysEndTime { get; set; }
    }
}
