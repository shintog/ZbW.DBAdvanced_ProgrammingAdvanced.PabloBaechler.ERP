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
    /// Interaction logic for ArticlePage.xaml
    /// </summary>
    public partial class ArticlePage : Page
    {
        public ArticlePage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).ArticleViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //Artikelnummer setzen
            BindToElement("ArticleNr", BindingMode.OneWay, txtArticleNr, TextBox.TextProperty);
            //Name setzen
            BindToElement("Name", BindingMode.TwoWay, txtName, TextBox.TextProperty);
            //Bezeichnung setzen
            BindToElement("Designation", BindingMode.TwoWay, txtDesignation, TextBox.TextProperty);
            //Klassifikation setzen
            BindToElement("Classification", BindingMode.OneWay, cmbClassification, ComboBox.ItemsSourceProperty);
            //Klassifikation ->  Selektierter Wert zurückgeben
            BindToElement("ClassificationSelectedItem", BindingMode.TwoWay, cmbClassification, ComboBox.SelectedItemProperty);
            //Gewählte Klassifik,kation anzeigen setzen
            BindToElement("ClassificationValue", BindingMode.OneWay, txtClassification, TextBox.TextProperty);
            //Einkaufspreis setzen
            BindToElement("PurchasingPrice", BindingMode.TwoWay, txtPurchasingPrice, TextBox.TextProperty);
            //Währung setzen
            BindToElement("Currency", BindingMode.OneWay, cmbPPCurrency, ComboBox.ItemsSourceProperty);
            //Einkaufspreis ->  Selektierter Wert zurückgeben
            BindToElement("PPCurrencySelectedItem", BindingMode.TwoWay, cmbPPCurrency, ComboBox.SelectedItemProperty);
            //Verkaufspreis setzen
            BindToElement("SalesPrice", BindingMode.TwoWay, txtSalesPrice, TextBox.TextProperty);
            //Währung setzen
            BindToElement("Currency", BindingMode.OneWay, cmbSPCurrency, ComboBox.ItemsSourceProperty);
            //Verkaufspreis ->  Selektierter Wert zurückgeben
            BindToElement("SPCurrencySelectedItem", BindingMode.TwoWay, cmbSPCurrency, ComboBox.SelectedItemProperty);
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("ErrorList", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, txtName, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtDesignation, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, cmbClassification, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtPurchasingPrice, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, cmbPPCurrency, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtSalesPrice, TextBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, cmbSPCurrency, ComboBox.IsEnabledProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}