using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    public class V_YearOverYearReportData
    {

        static DateTime dt = DateTime.Now;
        [DisplayName("Kategorie")]
        public string Category { get; set; }
        [DisplayName("Vorjahresdifferenz")]
        public decimal? YOY { get; set; }
        [DisplayName("Q1 vor 3 Jahren")]
        public decimal? Q1_Y3 { get; set; }
        [DisplayName("Q1 vor 2 Jahren")]
        public decimal? Q1_Y2 { get; set; }
        [DisplayName("Q1 vor einem Jahr")]
        public decimal? Q1_Y1 { get; set; }
        [DisplayName("Q1 aktuelles Jahr")]
        public decimal? Q1_Y0 { get; set; }
        [DisplayName("Q2 vor 3 Jahren")]
        public decimal? Q2_Y3 { get; set; }
        [DisplayName("Q2 vor 2 Jahren")]
        public decimal? Q2_Y2 { get; set; }
        [DisplayName("Q2 vor einem Jahr")]
        public decimal? Q2_Y1 { get; set; }
        [DisplayName("Q2 aktuelles Jahr")]
        public decimal? Q2_Y0 { get; set; }
        [DisplayName("Q3 vor 3 Jahren")]
        public decimal? Q3_Y3 { get; set; }
        [DisplayName("Q3 vor 2 Jahren")]
        public decimal? Q3_Y2 { get; set; }
        [DisplayName("Q3 vor  einem Jahr")]
        public decimal? Q3_Y1 { get; set; }
        [DisplayName("Q3 aktuelles Jahr")]
        public decimal? Q3_Y0 { get; set; }
        [DisplayName("Q4 vor 3 Jahren")]
        public decimal? Q4_Y3 { get; set; }
        [DisplayName("Q4 vor 2 Jahren")]
        public decimal? Q4_Y2 { get; set; }
        [DisplayName("Q4 vor einem Jahr")]
        public decimal? Q4_Y1 { get; set; }
        [DisplayName("Q4 aktuelles Jahr")]
        public decimal? Q4_Y0 { get; set; }
    }
}
