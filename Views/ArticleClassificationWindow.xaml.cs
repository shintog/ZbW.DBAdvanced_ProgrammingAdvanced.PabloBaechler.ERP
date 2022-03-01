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
using System.Windows.Shapes;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views
{
    /// <summary>
    /// Interaction logic for ArticleClassificationWindow.xaml
    /// </summary>
    public partial class ArticleClassificationWindow : Window
    {
        public ArticleClassificationWindow()
        {
            InitializeComponent();
            HostWindowInFrame(new ApplicationWindow());
        }
        public void HostWindowInFrame(Window win)
        {
            object tmp = win.Content;
            win.Content = null;
            frmSearch.Content = new ContentControl() { Content = tmp };
        }
    }
}
