namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{

    public class PositionData
    {
        public int PositionNr { get; set; }

        public int Order { get; set; }

        public int Article { get; set; }

        public decimal Amount { get; set; }

        public virtual ArticleData Article1 { get; set; }

        public virtual OrderData Order1 { get; set; }
    }
}
