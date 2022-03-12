using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ApplicationViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            _selectedItem = new KeyValuePair<string, string>("", null);
            this.CurrentApplications = new Dictionary<string, string>();
            this.ApplicationList = new Dictionary<string, Page>();
        }

        public MainViewModel Parent;
        public AuftragsverwaltungModel DataModel;

        private Dictionary<string, string> _currentApplications;
        public Dictionary<string, string> CurrentApplications
        {
            get { return _currentApplications; }
            set
            {
                _currentApplications = value;
                NotifyPropertyChanged(nameof(CurrentApplications));
            }
        }

        public List<V_YearOverYearReportData> YearOverYear
        {
            get { return DataModel.V_YearOverYearReport; }
        }

        private Dictionary<string, Page> _applicationList;
        public Dictionary<string, Page> ApplicationList
        {
            get { return _applicationList; }
            set
            {
                _applicationList = value;
                NotifyPropertyChanged(nameof(ApplicationList));
            }
        }

        private KeyValuePair<string, string> _selectedItem;

        public KeyValuePair<string, string> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                Parent.CurrentWindow = ApplicationList[value.Key];
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
