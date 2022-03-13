using System.ComponentModel.DataAnnotations;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    public class V_YearOverYearReport
    {
        [Key]
        [Required]
        public string Category { get; set; }
        public decimal? YOY { get; set; }
        public decimal? Q1_Y3 { get; set; }
        public decimal? Q1_Y2 { get; set; }
        public decimal? Q1_Y1 { get; set; }
        public decimal? Q1_Y0 { get; set; }
        public decimal? Q2_Y3 { get; set; }
        public decimal? Q2_Y2 { get; set; }
        public decimal? Q2_Y1 { get; set; }
        public decimal? Q2_Y0 { get; set; }
        public decimal? Q3_Y3 { get; set; }
        public decimal? Q3_Y2 { get; set; }
        public decimal? Q3_Y1 { get; set; }
        public decimal? Q3_Y0 { get; set; }
        public decimal? Q4_Y3 { get; set; }
        public decimal? Q4_Y2 { get; set; }
        public decimal? Q4_Y1 { get; set; }
        public decimal? Q4_Y0 { get; set; }
    }
}
