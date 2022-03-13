using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            BindToElement("ParentSelectedItem", BindingMode.TwoWay, cmbParent, ComboBox.SelectedItemProperty); ;
            //Parent-Value setzen
            BindToElement("ParentValue", BindingMode.TwoWay, txtParent, TextBox.TextProperty);
            //Parent-Value setzen
            BindToElement("Name", BindingMode.TwoWay, txtName, TextBox.TextProperty);
            // Hierarchy setzen
            BindToElement("HierarchyTree", BindingMode.OneWay, TreeHierarchy, TreeView.ItemsSourceProperty);
            //Error Sichtbarkeit setzen
            BindToElement("Error", BindingMode.OneWay, lblError, Label.VisibilityProperty);
            //Error Fehlerliste setzen
            BindToElement("CurrentError", BindingMode.OneWay, lblError, Label.ContentProperty);
            //BearbeitungsModus setzen
            BindToElement("SetEdit", BindingMode.OneWay, cmbParent, ComboBox.IsEnabledProperty);
            BindToElement("SetEdit", BindingMode.OneWay, txtName, TextBox.IsEnabledProperty);
        }

        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
