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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public object BindingContext { get; set; }
        public CustomerPage()
        {
            InitializeComponent();
            BindingContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).BindingContext).CustomerViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("SearchViewModel", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //CustNr setzen
            BindToElement("CustNr", BindingMode.OneWay, txtCustNr, TextBox.TextProperty);
            //TreeView setzen
            BindToElement("Name", BindingMode.TwoWay, txtName, TextBox.TextProperty);
            //Adresse setzen
            BindToElement("Address", BindingMode.OneWay, cmbAddress, ComboBox.ItemsSourceProperty);
            //Adresse ->  Selektierter Wert zurückgeben
            BindToElement("AddressSelectedItem", BindingMode.OneWayToSource, cmbAddress, ComboBox.SelectedItemProperty);
            //Adresse setzen
            BindToElement("AddressValue", BindingMode.OneWay, txtAddress, TextBox.TextProperty);
            //EMail setzen
            BindToElement("EMail", BindingMode.TwoWay, txtEMail, TextBox.TextProperty);
            //Webseite setzen
            BindToElement("Website", BindingMode.TwoWay, txtWebsite, TextBox.TextProperty);
        }
        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(BindingContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
