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
    /// Interaction logic for AddressPage.xaml
    /// </summary>
    public partial class AddressPage : Page
    {
        public object BindingContext { get; set; }
        public AddressPage()
        {
            InitializeComponent();
            BindingContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).BindingContext).AddressViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //Adressnummer setzen
            BindToElement("AddressNr", BindingMode.OneWay, txtAddressNr, TextBox.TextProperty);
            //Strasse setzen
            BindToElement("Street", BindingMode.TwoWay, txtStreet, TextBox.TextProperty);
            //Hausnummer setzen
            BindToElement("Number", BindingMode.TwoWay, txtNumber, TextBox.TextProperty);
            //PLZ setzen
            BindToElement("ZIP", BindingMode.TwoWay, txtZIP, TextBox.TextProperty);
            //Ort setzen
            BindToElement("City", BindingMode.TwoWay, txtCity, TextBox.TextProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(BindingContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
