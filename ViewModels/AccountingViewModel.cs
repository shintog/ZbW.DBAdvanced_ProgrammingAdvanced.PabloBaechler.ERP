using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

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
