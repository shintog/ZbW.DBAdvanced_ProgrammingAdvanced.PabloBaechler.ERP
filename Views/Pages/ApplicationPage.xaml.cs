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
    /// Interaction logic for ApplicationPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        public ApplicationPage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).ApplicationViewModel; 
            SetBindings();
        }
        
        public void SetBindings()
        {
            //ListView Applikationsliste setzen
            BindToElement("CurrentApplications", BindingMode.OneWay, lsvApplications, ListView.ItemsSourceProperty);

            //ListView Rückgabewert setzen
            BindToElement("SelectedItem", BindingMode.TwoWay, lsvApplications, ListView.SelectedItemProperty);

            //Jahresvergleich setzen
            BindToElement("YearOverYear", BindingMode.OneWay, DataGridYearOverYear, DataGrid.ItemsSourceProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
