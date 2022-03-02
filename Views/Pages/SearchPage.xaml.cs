using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public object BindingContext { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
            // Holen der Datenliste
            SetBindings();
        }

        public void SetBindings()
        {
            //Suche Rückgabewert setzen
            BindToElement("SearchTerm", BindingMode.OneWayToSource, txtSearch, TextBox.TextProperty);
            //ListView Suchresultat setzen
            BindToElement("ItemList", BindingMode.OneWayToSource, lsvObjectList, ListView.SelectedItemProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(BindingContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
