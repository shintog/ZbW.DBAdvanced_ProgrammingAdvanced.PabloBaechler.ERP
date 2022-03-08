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
    /// Interaction logic for ArticleClassificationPage.xaml
    /// </summary>
    public partial class ArticleClassificationPage : Page
    {
        public ArticleClassificationPage()
        {
            InitializeComponent();
            DataContext = ((MainViewModel)((MainWindow)App.Current.MainWindow).DataContext).ArticleClassificationViewModel;
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame -> Suchmaske setzen
            BindToElement("CurrentSearchMask", BindingMode.OneWay, frmSearch, Frame.ContentProperty);
            //Klassifikations-Nr setzen
            BindToElement("ClassificationNr", BindingMode.OneWay, txtClassificationNr, TextBox.TextProperty);
            //Parent-Objects setzen
            BindToElement("ParentClassification", BindingMode.OneWay, cmbParent, ComboBox.ItemsSourceProperty);
            //Parent ->  Selektierter Wert zurückgeben
            BindToElement("ParentSelectedItem", BindingMode.TwoWay, cmbParent, ComboBox.SelectedItemProperty);;
            //Parent-Value setzen
            BindToElement("ParentValue", BindingMode.TwoWay, txtParent, TextBox.TextProperty);
            //Parent-Value setzen
            BindToElement("Name", BindingMode.TwoWay, txtName, TextBox.TextProperty);
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("CurrentError", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, cmbParent, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtName, TextBox.IsEnabledProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
