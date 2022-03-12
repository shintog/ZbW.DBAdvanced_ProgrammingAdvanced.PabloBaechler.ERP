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

        public OrderPage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).OrderViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //Bestellnummer setzen
            BindToElement("OrderNr", BindingMode.OneWay, txtOrderNr, TextBox.TextProperty);
            //Datum setzen
            BindToElement("Date", BindingMode.TwoWay, dtpDate, DatePicker.SelectedDateProperty);
 //           BindToElement("DateValue", BindingMode.TwoWay, dtpDate, DatePicker.SelectedDateProperty);
            //Kunde setzen
            BindToElement("Customer", BindingMode.OneWay, cmbCustomer, ComboBox.ItemsSourceProperty);
            //Kunde ->  Selektierter Wert zurückgebenn
            BindToElement("CustomerSelectedItem", BindingMode.TwoWay, cmbCustomer, ComboBox.SelectedItemProperty);
            //Kunde Wert setzen
            BindToElement("CustomerValue", BindingMode.OneWay, txtCustomer, TextBox.TextProperty);
            //Positionen Liste setzen
            BindToElement("Positions", BindingMode.OneWay, lsvPositions, ListView.ItemsSourceProperty);
            //Positionen Liste ->  Selektierter Wert zurückgebenn
            BindToElement("PositionsSelectedItem", BindingMode.TwoWay, lsvPositions, ListView.SelectedItemProperty);
            //Positionsnummer setzen
            BindToElement("Position", BindingMode.OneWay, txtPosition, TextBox.TextProperty);
            //Artikel setzen
            BindToElement("Article", BindingMode.OneWay, cmbArticle, ComboBox.ItemsSourceProperty);
            //Artikel ->  Selektierter Wert zurückgebenn
            BindToElement("ArticleSelectedItem", BindingMode.TwoWay, cmbArticle, ComboBox.SelectedItemProperty);
            //Artikel Beschreibung setzen
            BindToElement("ArticleValue", BindingMode.OneWay, txtArticle, TextBox.TextProperty);
            //Menge setzen
            BindToElement("Amount", BindingMode.TwoWay, txtAmount, TextBox.TextProperty);
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("CurrentError", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, dtpDate, DatePicker.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, cmbCustomer, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, lsvPositions, ListView.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, grdPositionData, Grid.IsEnabledProperty);
            BindToElement("SetPositionEdit", BindingMode.OneWay, cmbArticle, ComboBox.IsEnabledProperty);
            BindToElement("SetPositionEdit", BindingMode.OneWay, txtAmount, TextBox.IsEnabledProperty);
        }


        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
