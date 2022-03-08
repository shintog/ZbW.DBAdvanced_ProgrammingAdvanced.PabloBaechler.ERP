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
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
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
            //ListView Applikationsliste setzen
            BindToElement("HistoryList", BindingMode.OneWay, DataGridHistory, DataGrid.ItemsSourceProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
