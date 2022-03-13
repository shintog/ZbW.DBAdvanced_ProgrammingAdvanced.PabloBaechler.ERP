using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).CustomerViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //CustNr setzen
            BindToElement("CustomerNr", BindingMode.OneWay, txtCustNr, TextBox.TextProperty);
            //TreeView setzen
            BindToElement("Name", BindingMode.TwoWay, txtName, TextBox.TextProperty);
            //Adresse setzen
            BindToElement("Address", BindingMode.OneWay, cmbAddress, ComboBox.ItemsSourceProperty);
            //Adresse ->  Selektierter Wert zurückgeben
            BindToElement("AddressSelectedItem", BindingMode.TwoWay, cmbAddress, ComboBox.SelectedItemProperty);
            //Adresse setzen
            BindToElement("AddressValue", BindingMode.OneWay, txtAddress, TextBox.TextProperty);
            //EMail setzen
            BindToElement("EMail", BindingMode.TwoWay, txtEMail, TextBox.TextProperty);
            //Webseite setzen
            BindToElement("Website", BindingMode.TwoWay, txtWebsite, TextBox.TextProperty);
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("CurrentError", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, txtName, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, cmbAddress, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtEMail, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtWebsite, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, pwbPassword, PasswordBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, pwbPasswordSec, PasswordBox.IsEnabledProperty);
        }
        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwbPassword.Password != "" && pwbPassword.Password == pwbPasswordSec.Password)
                imgPasswordCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Checked.png", UriKind.Relative));
            else
                imgPasswordCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Unchecked.png", UriKind.Relative));
        }

    }
}
