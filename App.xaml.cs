using System.Globalization;
using System.Threading;
using System.Windows;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-CH");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-CH");
        }
    }
}
