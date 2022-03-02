using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public interface ISupportParentViewModel
    {
        object ParentViewModel { get; set; }
    }
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;


            this.ApplicationViewModel = new ApplicationViewModel();
            
            this.CurrentWindow = new ApplicationPage();
            this.SelectedItem = new KeyValuePair<string, string>();
            this.CurrentApplications = new Dictionary<string, string>()
            {
                {"CUST", "Kundenbenstamm"},
                {"ADDR", "Adressstamm"},
                {"ARTI", "Artikelstamm"},
                {"ARTC", "Artikel-Klassifizierung"},
                {"ORDE", "Bestellwesen"},
            };
            this.ApplicationList = new Dictionary<string, Page>()
            {
                {"APPL", new ApplicationPage()},
                {"CUST", new CustomerPage()},
                {"ADDR", new AddressPage()},
                {"ARTI", new ArticlePage()},
                {"ARTC", new ArticleClassificationPage()},
                {"ORDE", new OrderPage()}
            };
        }

        private AddressViewModel _addressViewModel;
        public AddressViewModel AddressViewModel
        {
            get { return _addressViewModel; }
            set
            {
                _addressViewModel = value;
                NotifyPropertyChanged("AddressViewModel");
            }
        }
        private ApplicationViewModel _applicationViewModel;
        public ApplicationViewModel ApplicationViewModel
        {
            get { return _applicationViewModel; }
            set
            {
                _applicationViewModel = value;
                NotifyPropertyChanged("ApplicationViewModel");
            }
        }
        private ArticleClassificationViewModel _articleClassificationViewModel;
        public ArticleClassificationViewModel ArticleClassificationViewModel
        {
            get { return _articleClassificationViewModel; }
            set
            {
                _articleClassificationViewModel = value;
                NotifyPropertyChanged("ArticleClassificationViewModel");
            }
        }
        private ArticleViewModel _articleViewModel;
        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel; }
            set
            {
                _articleViewModel = value;
                NotifyPropertyChanged("ArticleViewModel");
            }
        }
        private CustomerViewModel _customerViewModel;
        public CustomerViewModel CustomerViewModel
        {
            get { return _customerViewModel; }
            set
            {
                _customerViewModel = value;
                NotifyPropertyChanged("CustomerViewModel");
            }
        }
        private OrderViewModel _orderViewModel;
        public OrderViewModel OrderViewModel
        {
            get { return _orderViewModel; }
            set
            {
                _orderViewModel = value;
                NotifyPropertyChanged("OrderViewModel");
            }
        }

        private Page _currentWindow;

        public Page CurrentWindow
        {
            get { return _currentWindow; }
            set
            {
                _currentWindow = value;
                NotifyPropertyChanged("CurrentWindow");
            }
        }

        private Dictionary<string, string> _currentApplications;
        public Dictionary<string, string> CurrentApplications
        {
            get { return _currentApplications; }
            set
            {
                _currentApplications = value;
                NotifyPropertyChanged("CurrentApplications");
            }
        }

        private Dictionary<string, Page> _applicationList;
        private Dictionary<string, Page> ApplicationList
        {
            get { return _applicationList; }
            set
            {
                _applicationList = value;
                NotifyPropertyChanged("ApplicationList");
            }
        }

        private KeyValuePair<String, String> _selectedItem;
        public KeyValuePair<String, String> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value.Key != null)
                {
                    CurrentWindow = ApplicationList[value.Key];
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }
        public bool CanExecute
        {
            get
            {
                return CurrentWindow.GetType() != typeof(ApplicationPage) ? true : false;
            }
        }
        private ICommand _clickSaveCommand;
        public ICommand ClickSaveCommand
        {
            get
            {
                return _clickSaveCommand;
            }
        }
        private ICommand _clickAbortCommand;
        public ICommand ClickAbortCommand
        {
            get
            {
                CurrentWindow = ApplicationList["APPL"];
                return _clickAbortCommand;
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
