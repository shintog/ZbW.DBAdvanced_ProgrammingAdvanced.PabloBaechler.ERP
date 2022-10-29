using System;
using System.Text.RegularExpressions;
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
            if (pwbPassword.Password != "" && pwbPassword.Password == pwbPasswordSec.Password &&
                Regex.Match(pwbPassword.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").Success)
                imgPasswordCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Checked.png", UriKind.Relative));
            else
                imgPasswordCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Unchecked.png", UriKind.Relative));
        }

        private void txtCustNrFree_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.Match(txtCustNrFree.Text, "^CU[1-9]{5}$").Success)
            {
                imgCustomerNrCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Checked.png", UriKind.Relative));
                lblCustomerNrWrong.Visibility = Visibility.Hidden;
            }
            else
            {
                imgCustomerNrCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Unchecked.png", UriKind.Relative));
                lblCustomerNrWrong.Visibility = Visibility.Visible;
            }
        }

        private void txtEMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.Match(txtEMail.Text, "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])").Success)
            {
                imgEmailCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Checked.png", UriKind.Relative));
                lblEmailWrong.Visibility = Visibility.Hidden;
            }
            else
            {
                imgEmailCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Unchecked.png", UriKind.Relative));
                lblEmailWrong.Visibility = Visibility.Visible;
            }
        }

        private void txtWebsite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.Match(txtWebsite.Text, "^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$").Success ||
                Regex.Match(txtWebsite.Text, "^[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$").Success)
            {
                imgWebsiteCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Checked.png", UriKind.Relative));
                lblWebsiteWrong.Visibility = Visibility.Hidden;
            }
            else
            {
                imgWebsiteCheck.Source = new BitmapImage(new Uri(@"/Resources/Icons/Unchecked.png", UriKind.Relative));
                lblWebsiteWrong.Visibility = Visibility.Visible;
            }
        }
    }
}
