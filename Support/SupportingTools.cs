using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support
{
    public class SupportingTools
    {
        public static Binding GenerateBinding(Object bindingContext, string path, BindingMode mode, DependencyObject element, DependencyProperty property)
        {
            //Bindings für die verschiedenen Elemente setzen
            Binding bindingItem = new Binding();
            bindingItem.Source = bindingContext;
            bindingItem.Path = new PropertyPath(path);
            bindingItem.Mode = mode;
            bindingItem.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            return bindingItem;
        }
    }
}
