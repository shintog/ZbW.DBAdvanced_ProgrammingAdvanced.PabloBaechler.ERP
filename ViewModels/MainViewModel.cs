using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.Interface;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views;
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
            this.CustomerViewModel = new CustomerViewModel();
            this.AddressViewModel = new AddressViewModel();
            this.ArticleViewModel = new ArticleViewModel();
            this.ArticleClassificationViewModel = new ArticleClassificationViewModel();
            this.OrderViewModel = new OrderViewModel();
            this.AccountingViewModel = new AccountingViewModel();

            ApplicationViewModel.Parent = this;
            CustomerViewModel.Parent = this;
            AddressViewModel.Parent = this;
            ArticleViewModel.Parent = this;
            ArticleClassificationViewModel.Parent = this;
            OrderViewModel.Parent = this;
            AccountingViewModel.Parent = this;

            this.DataModel = new AuftragsverwaltungModel();

            this.SelectedItem = new KeyValuePair<string, string>();

            this.CurrentApplications = new Dictionary<string, string>();
            this.ApplicationList = new Dictionary<string, Page>();

        }

        private IRepository _dataModel;
        public IRepository DataModel
        {
            get { return _dataModel; }
            set
            {
                _dataModel = new AuftragsverwaltungModel();
                CustomerViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                AddressViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                ArticleViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                ArticleClassificationViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                OrderViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                AccountingViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;
                ApplicationViewModel.DataModel = (AuftragsverwaltungModel)_dataModel;

            }
        }

        private AddressViewModel _addressViewModel;

        public AddressViewModel AddressViewModel
        {
            get { return _addressViewModel; }
            set
            {
                _addressViewModel = value;
                NotifyPropertyChanged(nameof(AddressViewModel));
            }
        }

        private ApplicationViewModel _applicationViewModel;

        public ApplicationViewModel ApplicationViewModel
        {
            get { return _applicationViewModel; }
            set
            {
                _applicationViewModel = value;
                NotifyPropertyChanged(nameof(ApplicationViewModel));
            }
        }

        private ArticleClassificationViewModel _articleClassificationViewModel;

        public ArticleClassificationViewModel ArticleClassificationViewModel
        {
            get { return _articleClassificationViewModel; }
            set
            {
                _articleClassificationViewModel = value;
                NotifyPropertyChanged(nameof(ArticleClassificationViewModel));
            }
        }

        private ArticleViewModel _articleViewModel;

        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel; }
            set
            {
                _articleViewModel = value;
                NotifyPropertyChanged(nameof(ArticleViewModel));
            }
        }

        private CustomerViewModel _customerViewModel;

        public CustomerViewModel CustomerViewModel
        {
            get { return _customerViewModel; }
            set
            {
                _customerViewModel = value;
                NotifyPropertyChanged(nameof(CustomerViewModel));
            }
        }

        private OrderViewModel _orderViewModel;

        public OrderViewModel OrderViewModel
        {
            get { return _orderViewModel; }
            set
            {
                _orderViewModel = value;
                NotifyPropertyChanged(nameof(OrderViewModel));
            }
        }

        private AccountingViewModel _accountingViewModel;

        public AccountingViewModel AccountingViewModel
        {
            get { return _accountingViewModel; }
            set
            {
                _accountingViewModel = value;
                NotifyPropertyChanged(nameof(AccountingViewModel));
            }
        }

        private bool _resetCurrentWindow;

        public bool ResetCurrentWindow
        {
            get { return _resetCurrentWindow; }
            set
            {
                if (value)
                    CurrentWindow = new ApplicationPage();

                _resetCurrentWindow = false;
                NotifyPropertyChanged(nameof(ResetCurrentWindow));
            }
        }

        private Page _currentWindow;

        public Page CurrentWindow
        {
            get { return _currentWindow; }
            set
            {
                _currentWindow = value;
                Type WindowType = CurrentWindow
                    .GetType();
                if (WindowType == typeof(AddressPage))
                    AddressViewModel.ResetSearchMask = true;
                if (WindowType == typeof(ArticleClassificationPage))
                    ArticleClassificationViewModel.ResetSearchMask = true;
                if (WindowType == typeof(ArticlePage))
                    ArticleViewModel.ResetSearchMask = true;
                if (WindowType == typeof(CustomerPage))
                    CustomerViewModel.ResetSearchMask = true;
                if (WindowType == typeof(OrderPage))
                    OrderViewModel.ResetSearchMask = true;
                NotifyPropertyChanged(nameof(CurrentWindow));
            }
        }

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

        private KeyValuePair<String, String> _selectedItem;
        public KeyValuePair<String, String> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value.Key != null)
                {
                    _selectedItem = value;
                    CurrentWindow = ApplicationList[value.Key];
                    NotifyPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private bool _canExecute = false;
        public bool CanExecute
        {
            get { return CurrentWindow.GetType() != typeof(ApplicationPage) ? true : false; }
            set { _canExecute = CurrentWindow.GetType() != typeof(ApplicationPage) ? true : false; }

        }

        private Visibility _historyVisibility;
        public Visibility HistoryVisibility
        {
            get { return _historyVisibility; }
            set { _historyVisibility = value; }
        }

        public void SetNew()
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(AddressPage))
            {
                AddressViewModel.SetNew();
                AddressViewModel.SetEdit = true;
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.SetNew();
                ArticleClassificationViewModel.SetEdit = true;
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.SetNew();
                ArticleViewModel.SetEdit = true;
            }

            if (WindowType == typeof(CustomerPage))
            {
                CustomerViewModel.SetNew();
                CustomerViewModel.SetEdit = true;
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.SetNew();
                OrderViewModel.SetEdit = true;
            }
        }

        public void SetEdit()
        {
            Type WindowType = CurrentWindow
                .GetType();
            if (WindowType == typeof(AddressPage))
                AddressViewModel.SetEdit = true;
            if (WindowType == typeof(ArticleClassificationPage))
                ArticleClassificationViewModel.SetEdit = true;
            if (WindowType == typeof(ArticlePage))
                ArticleViewModel.SetEdit = true;
            if (WindowType == typeof(CustomerPage))
                CustomerViewModel.SetEdit = true;
            if (WindowType == typeof(OrderPage))
                OrderViewModel.SetEdit = true;
        }

        public void DoSave()
        {
            Type WindowType = CurrentWindow
                .GetType();
            if (WindowType == typeof(AddressPage))
            {
                AddressViewModel.SaveData();
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.SaveData();
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.SaveData();
            }

            if (WindowType == typeof(CustomerPage))
            {
                if (((CustomerPage)CurrentWindow).pwbPassword.Password != "" && ((CustomerPage)CurrentWindow).pwbPassword.Password == ((CustomerPage)CurrentWindow).pwbPasswordSec.Password)
                    CustomerViewModel.SavePassword(((CustomerPage)CurrentWindow).pwbPassword.Password);

                CustomerViewModel.SaveData();
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.SaveData();
            }
        }
        public void DoDelete()
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(AddressPage))
            {
                AddressViewModel.DeleteData();
                AddressViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.DeleteData();
                ArticleClassificationViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.DeleteData();
                ArticleViewModel.SetEdit = false;
            }

            if (WindowType == typeof(CustomerPage))
            {
                CustomerViewModel.DeleteData();
                CustomerViewModel.SetEdit = false;
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.DeleteData();
                OrderViewModel.SetEdit = false;
            }
        }

        public void DoImport()
        {
            Type WindowType = CurrentWindow.GetType();
            // Für erweiterung auf mehrere Quellen
            if (WindowType == typeof(CustomerPage))
            {
                var Window = new ImExport();
                Window.Show(false);

                while (!Window.Execute && Window.Visibility == Visibility.Visible)
                {
                    Thread.Sleep(10);
                }

                if (Window.Execute)
                {
                    string Type = Window.cmbType.SelectedValue.ToString();
                    string Path = Window.txtPath.Text;
                    DateTime Moment = DateTime.Parse(Window.dtpDate.Text);
                    Window.Close();


                    if (WindowType == typeof(CustomerPage))
                        CustomerViewModel.ImportData(Type == "json", Path);
                }
            }            
        }

        public void DoExport()
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(CustomerPage))
            {
                var Window = new ImExport();
                Window.Show(true);

                while (!Window.Execute && Window.Visibility == Visibility.Visible)
                {
                    Thread.Sleep(10);
                }

                if (Window.Execute)
                {
                    string Type = Window.cmbType.SelectedValue.ToString();
                    string Path = Window.txtPath.Text;
                    DateTime Moment = DateTime.Parse(Window.dtpDate.Text);
                    Window.Close();

                    if (WindowType == typeof(CustomerPage))
                        CustomerViewModel.ExportData(Type == "json", Path, Moment);

                }
            }
        }

        public void DoAbort()
        {
            CurrentWindow = new ApplicationPage();
        }

        private Page _behindHistory;
        public void ShowHistory()
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(HistoryPage))
            {
                CurrentWindow = _behindHistory;
            }
            else if (WindowType != typeof(ApplicationPage) && WindowType != typeof(AccountingPage))
            {
                _behindHistory = CurrentWindow;
                CurrentWindow = new HistoryPage();
            }
        }

        public Dictionary<String, String> ListOfErrors => new Dictionary<String, String>()
        {
            {nameof(CustomerViewModel) + "." + nameof(CustomerViewModel.Name), "Der Name muss befüllt sein"},
            {
                nameof(CustomerViewModel) + "." + nameof(CustomerViewModel.AddressSelectedItem),
                "Wählen sie eine Adresse aus"
            },
            {nameof(CustomerViewModel) + "." + nameof(CustomerViewModel.EMail), "Die E-Mail Adresse muss befüllt sein"},
            {nameof(CustomerViewModel) + "." + nameof(CustomerViewModel.Website), "Die Webseite muss befüllt sein"},
            {nameof(AddressViewModel) + "." + nameof(AddressViewModel.Street), "Die Strasse muss befüllt sein"},
            {nameof(AddressViewModel) + "." + nameof(AddressViewModel.Number), "Die Strassennummer muss befüllt sein"},
            {nameof(AddressViewModel) + "." + nameof(AddressViewModel.ZIP), "Die Postleitzahl muss befüllt sein"},
            {nameof(AddressViewModel) + "." + nameof(AddressViewModel.City), "Der Ort muss befüllt sein"},
            {nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.Name), "Die Name muss befüllt sein"},
            {
                nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.Designation),
                "Die Bezeichnnung muss befüllt sein"
            },
            {nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.ClassificationSelectedItem), "Die Klassifikation muss befüllt sein"},
            {
                nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.PurchasingPrice),
                "Der Einkaufspreis muss befüllt sein"
            },
            {
                nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.PPCurrencySelectedItem),
                "Wählen sie eine Einkaufspreis Währung aus"
            },
            {
                nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.SalesPrice),
                "Der Verkaufspreis muss befüllt sein"
            },
            {
                nameof(ArticleViewModel) + "." + nameof(ArticleViewModel.SPCurrencySelectedItem),
                "Wählen sie eine Verkaufspreis Währung aus"
            },
            {
                nameof(ArticleClassificationViewModel) + "." + nameof(ArticleClassificationViewModel.Name),
                "Der Name muss befüllt sein"
            },
            {nameof(OrderViewModel) + "." + nameof(OrderViewModel.CustomerSelectedItem), "Wählen sie einen Kunden aus"},
            {nameof(OrderViewModel) + "." + nameof(OrderViewModel.Date), "Setzen sie ein Auftragsdatum"},
            {nameof(OrderViewModel) + "." + nameof(OrderViewModel.Positions), "Ergänzen sie alle Postionsdaten zu Position:"}
        };

        private ICommand _clickNewCommand;
        public ICommand ClickNewCommand
        {
            get
            {
                if (_clickNewCommand == null)
                    _clickNewCommand = new New();
                return _clickNewCommand;
            }
            set
            {
                _clickNewCommand = value;
            }
        }

        private ICommand _clickEditCommand;
        public ICommand ClickEditCommand
        {
            get
            {
                if (_clickEditCommand == null)
                    _clickEditCommand = new Edit();
                return _clickEditCommand;
            }
            set
            {
                _clickEditCommand = value;
            }
        }

        private ICommand _clickSaveCommand;
        public ICommand ClickSaveCommand
        {
            get
            {
                if (_clickSaveCommand == null)
                    _clickSaveCommand = new Save();
                return _clickSaveCommand;
            }
            set
            {
                _clickSaveCommand = value;
            }
        }

        private ICommand _clickDeleteCommand;
        public ICommand ClickDeleteCommand
        {
            get
            {
                if (_clickDeleteCommand == null)
                    _clickDeleteCommand = new Delete();
                return _clickDeleteCommand;
            }
            set
            {
                _clickDeleteCommand = value;
            }
        }

        private ICommand _clickImportCommand;
        public ICommand ClickImportCommand
        {
            get
            {
                if (_clickImportCommand == null)
                    ClickImportCommand = new Import();
                return _clickImportCommand;
            }
            set
            {
                _clickImportCommand = value;
            }
        }

        private ICommand _clickExportCommand;
        public ICommand ClickExportCommand
        {
            get
            {
                if (_clickExportCommand == null)
                    ClickExportCommand = new Export();
                return _clickExportCommand;
            }
            set
            {
                _clickExportCommand = value;
            }
        }

        private ICommand _clickAbortCommand;
        public ICommand ClickAbortCommand
        {
            get
            {
                if (_clickAbortCommand == null)
                    _clickAbortCommand = new Abort();
                return _clickAbortCommand;
            }
            set
            {
                _clickAbortCommand = value;
            }
        }

        private ICommand _clickHistoryCommand;
        public ICommand ClickHistoryCommand
        {
            get
            {
                if (_clickHistoryCommand == null)
                    _clickHistoryCommand = new History();
                return _clickHistoryCommand;
            }
            set
            {
                _clickHistoryCommand = value;
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

    class New : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).SetNew();

        }
        #endregion
    }
    class Edit : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).SetEdit();

        }
        #endregion
    }
    class Save : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).DoSave();

        }
        #endregion
    }
    class Delete : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).DoDelete();

        }
        #endregion
    }
    class Import : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).DoImport();

        }
        #endregion
    }
    class Export : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).DoExport();

        }
        #endregion
    }
    class Abort : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).DoAbort();

        }
        #endregion
    }
    class History : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ((MainViewModel)parameter).ShowHistory();

        }
        #endregion
    }
}
