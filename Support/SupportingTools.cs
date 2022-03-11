using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support
{
    public class SupportingTools
    {
        public static Binding GenerateBinding(Object bindingContext, string path, BindingMode mode,
            DependencyObject element, DependencyProperty property, String StringFormat)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = new Binding();
            bindingItem.Source = bindingContext;
            bindingItem.Path = new PropertyPath(path);
            bindingItem.Mode = mode;
            if (StringFormat != "")
                bindingItem.StringFormat = StringFormat;
            bindingItem.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            return bindingItem;
        }

        public static Dictionary<string, string> SetApplicationListDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"CUST", "Kundenbenstamm"},
                {"ADDR", "Adressstamm"},
                {"ARTI", "Artikelstamm"},
                {"ARTC", "Artikel-Klassifizierung"},
                {"ORDE", "Bestellwesen"},
                {"ACCO", "Rechnungswesen"}
            };
        }


        public static Dictionary<string, Page> SetApplicationPageListDictionary()
        {
            return new Dictionary<string, Page>()
            {
                {"CUST", new CustomerPage()},
                {"ADDR", new AddressPage()},
                {"ARTI", new ArticlePage()},
                {"ARTC", new ArticleClassificationPage()},
                {"ORDE", new OrderPage()},
                {"ACCO", new AccountingPage()},
                {"HIST", new HistoryPage()}
            };
        }
    }
}
