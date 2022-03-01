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
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initial Application Window open
            HostWindowInFrame(new ApplicationWindow());
        }

        public void HostWindowInFrame(Window win)
        {
            object tmp = win.Content;
            win.Content = null;
            frmApplication.Content = new ContentControl() { Content = tmp };
        }
    }
}
