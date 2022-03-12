using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
using Binding = System.Windows.Data.Binding;
using ComboBox = System.Windows.Controls.ComboBox;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = (MainViewModel)(new MainViewModel());
            InitializeComponent();
            RegisterPages();
            SetBindings();
        }

        public void SetBindings()
        {
            //Frame Pages -> Aktuelle Page setzen
            BindToElement("CurrentWindow", BindingMode.TwoWay, frmApplication, Frame.ContentProperty);
            //ComboBox Appliaktionen -> Applikationsliste setzen
            BindToElement("CurrentApplications", BindingMode.OneWay, cmbApplications, ComboBox.ItemsSourceProperty);
            //ComboBox Applikationen -> Rückgabewert setzen
            BindToElement("SelectedItem", BindingMode.TwoWay, cmbApplications, ComboBox.SelectedItemProperty);
        }

        public void RegisterPages()
        {
            ((MainViewModel)DataContext).CurrentApplications = SupportingTools.SetApplicationListDictionary();
            ((MainViewModel)DataContext).ApplicationList = SupportingTools.SetApplicationPageListDictionary();
            ((ApplicationViewModel)((MainViewModel)DataContext).ApplicationViewModel).CurrentApplications = SupportingTools.SetApplicationListDictionary();
            ((ApplicationViewModel)((MainViewModel)DataContext).ApplicationViewModel).ApplicationList = SupportingTools.SetApplicationPageListDictionary();

            ((MainViewModel)DataContext).ResetCurrentWindow = true;
        }
        public void BindToElement(string path, BindingMode mode, DependencyObject element, DependencyProperty property, String StringFormat = "")
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = SupportingTools.GenerateBinding(DataContext, path, mode, element, property, StringFormat);
            BindingOperations.SetBinding(element, property, bindingItem);
        }
    }
}
