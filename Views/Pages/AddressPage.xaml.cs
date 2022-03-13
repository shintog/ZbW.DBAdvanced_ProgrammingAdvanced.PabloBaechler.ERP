using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddressPage.xaml
    /// </summary>
    public partial class AddressPage : Page
    {
        public AddressPage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).AddressViewModel;
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
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("CurrentError", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, txtStreet, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtNumber, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtZIP, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtCity, TextBox.IsEnabledProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
