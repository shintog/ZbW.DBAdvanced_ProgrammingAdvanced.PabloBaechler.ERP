using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ImExport : Window
    {
        public bool Execute;

        public ImExport()
        {
            InitializeComponent();
            cmbType.ItemsSource = new List<string> { "json", "xml" };
            Execute = false;
        }

        public void Show(bool exportMode)
        {

            lblMoment.Visibility = Visibility.Hidden;
            dtpDate.Visibility = Visibility.Hidden;
            if (exportMode)
            {
                lblMoment.Visibility = Visibility.Visible;
                dtpDate.Visibility = Visibility.Visible;            
            }

            base.ShowDialog();
        }

        private void cmbAbort_Click(object sender, RoutedEventArgs e)
        {
            Execute = false;
            Close();
        }

        private void cmdExecute_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            lblError.Visibility = Visibility.Hidden;
            if (dtpDate.Visibility == Visibility.Visible && dtpDate.Text == "")
            {
                lblError.Content = "Setzen sie den Zeitpunkt der Daten";
                lblError.Visibility = Visibility.Visible;
            }
            else
            {
                Execute = true;
                Hide();
            }
        }

        private void cmbFileexplorer_Click(object sender, RoutedEventArgs e)
        {

            lblError.Content = "";
            lblError.Visibility = Visibility.Hidden;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = "." + cmbType.Text;
            dlg.Filter = "Formated File (." + cmbType.Text + ")|*." + cmbType.Text;

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                txtPath.Text = dlg.FileName;
                cmdExecute.IsEnabled = true;
            }
            else { 
                cmdExecute.IsEnabled = false;

                lblError.Content = "Wählen sie ein File zum gewählten Typ";
                lblError.Visibility = Visibility.Visible;
            }
        }
    }
}
