namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System.Collections.Generic;

    public class ArticleData
    {
        public int ArticleNr { get; set; }

        public string Name { get; set; }

        public string Designation { get; set; }

        public int Classification { get; set; }

        public double PurchasingPrice { get; set; }

        public string PPCurrency { get; set; }

        public double SalesPrice { get; set; }

        public string SPCurrency { get; set; }

        public virtual ArticleClassificationData ArticleClassification { get; set; }

        public virtual CurrencyData CurrencyPP { get; set; }

        public virtual CurrencyData CurrencySP { get; set; }

        public virtual List<PositionData> Positions { get; set; }
    }
}
