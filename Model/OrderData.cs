namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    using System;
    using System.Collections.Generic;

    public class OrderData
    {
        public int OrderNr { get; set; }

        public DateTime Date { get; set; }

        public string Customer { get; set; }

        public int CustomerNr { get; set; }

        public virtual CustomerData Customer1 { get; set; }

        public virtual List<PositionData> Positions { get; set; }
    }
}
