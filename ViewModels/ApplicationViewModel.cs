using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.CurrentApplications = new Dictionary<string, string>()
            {
                {"CUST", "Kundenbenstamm"},
                {"ADDR", "Adressstamm"},
                {"ARTI", "Artikelstamm"},
                {"ARTC", "Artikel-Klassifizierung"},
                {"ORDE", "Bestellwesen"},
            };
            this.ApplicationList = new Dictionary<string, Page>()
            {
                {"CUST", new CustomerPage()},
                {"ADDR", new AddressPage()},
                {"ARTI", new ArticlePage()},
                {"ARTC", new ArticleClassificationPage()},
                {"ORDE", new OrderPage()}
            };
        }

        public Dictionary<string, string> CurrentApplications { get; set; }
        private Dictionary<string, Page> ApplicationList { get; set; }
        public KeyValuePair<String, String> SelectedItem
        {
            get { return new KeyValuePair<String, String>(); }
            set
            {
                if (value.Key != null)
                    value = value; // = ApplicationList[value.Key];
            }
        }
    }
}
