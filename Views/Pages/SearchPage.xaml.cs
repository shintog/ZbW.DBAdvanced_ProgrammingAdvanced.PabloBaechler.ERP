using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            if (((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext) != null && ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).CurrentWindow != null)
            {
                SetBindingSource();
                // Holen der Datenliste
                SetBindings();
            }

        }

        public void SetBindingSource()
        {
            Type WindowType = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).CurrentWindow.GetType();
            if (WindowType == typeof(AddressPage))
                DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).AddressViewModel;
            if (WindowType == typeof(ArticleClassificationPage))
                DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).ArticleClassificationViewModel;
            if (WindowType == typeof(ArticlePage))
                DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).ArticleViewModel;
            if (WindowType == typeof(CustomerPage))
                DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).CustomerViewModel;
            if (WindowType == typeof(OrderPage))
                DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).OrderViewModel;
        }

        public void SetBindings()
        {
            //Suche Rückgabewert setzen
            BindToElement("SearchTerm", BindingMode.OneWayToSource, txtSearch, TextBox.TextProperty);
            //ListView Suchwerte setzen
            BindToElement("ItemList", BindingMode.OneWay, lsvObjectList, ListView.ItemsSourceProperty);
            //ListView => Selektion zurückgeben
            BindToElement("SelectedItem", BindingMode.TwoWay, lsvObjectList, ListView.SelectedItemProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
