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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public object BindingContext { get; set; }

        public OrderPage()
        {
            InitializeComponent();
            BindingContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).BindingContext).OrderViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //Bestellnummer setzen
            BindToElement("OrderNr", BindingMode.OneWay, txtOrderNr, TextBox.TextProperty);
            //Datum setzen
            BindToElement("Date", BindingMode.TwoWay, dtpDate, DatePicker.TextProperty);
            //Kunde setzen
            BindToElement("Customer", BindingMode.OneWay, cmbCustomer, ComboBox.ItemsSourceProperty);
            //Kunde ->  Selektierter Wert zurückgebenn
            BindToElement("CustomerSelectedItem", BindingMode.OneWayToSource, cmbCustomer, ComboBox.SelectedItemProperty);
            //Kunde Wert setzen
            BindToElement("CustomerValue", BindingMode.OneWay, txtCustomer, TextBox.TextProperty);
            //Positionen setzen
            BindToElement("Positions", BindingMode.TwoWay, lsvPositions, ListView.ItemsSourceProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(BindingContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
