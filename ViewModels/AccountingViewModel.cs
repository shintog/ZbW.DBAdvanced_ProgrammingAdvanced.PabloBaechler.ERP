using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class AccountingViewModel
    {
        public AccountingViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.AccountingData = new AccountingData();
        }

        public MainViewModel Parent;

        public AuftragsverwaltungModel DataModel;

        private AccountingData _accountingData;
        private AccountingData AccountingData
        {
            get { return _accountingData; }
            set
            {
                _accountingData = value;
            }
        }

        public List<AccountingData> Accountings
        {
            get { return DataModel.Accountings.ToList(); }
        }

    }
}
