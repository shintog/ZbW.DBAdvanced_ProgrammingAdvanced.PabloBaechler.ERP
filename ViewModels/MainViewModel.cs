using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Support;
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

            ApplicationViewModel.Parent = this;
            CustomerViewModel.Parent = this;
            AddressViewModel.Parent = this;
            ArticleViewModel.Parent = this;
            ArticleClassificationViewModel.Parent = this;
            OrderViewModel.Parent = this;

            this.DataAccess = new AuftragsverwaltungDataAccess();

            this.SelectedItem = new KeyValuePair<string, string>();

            this.CurrentApplications = new Dictionary<string, string>();
            this.ApplicationList = new Dictionary<string, Page>();
            
        }

        private AuftragsverwaltungDataAccess _dataAccess;
        public AuftragsverwaltungDataAccess DataAccess
        {
            get { return _dataAccess; }
            set
            {
                _dataAccess = value;
                CustomerViewModel.DataAccess = _dataAccess;
                AddressViewModel.DataAccess = _dataAccess;
                ArticleViewModel.DataAccess = _dataAccess;
                ArticleClassificationViewModel.DataAccess = _dataAccess;
                OrderViewModel.DataAccess = _dataAccess;
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

        public void SetNew() 
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(AddressPage))
            {
                AddressViewModel.SetNew = true;
                AddressViewModel.SetEdit = true;
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.SetNew = true;
                ArticleClassificationViewModel.SetEdit = true;
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.SetNew = true;
                ArticleViewModel.SetEdit = true;
            }

            if (WindowType == typeof(CustomerPage))
            {
                CustomerViewModel.SetNew = true;
                CustomerViewModel.SetEdit = true;
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.SetNew = true;
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
                AddressViewModel.SaveData = true;
                AddressViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.SaveData = true;
                ArticleClassificationViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.SaveData = true;
                ArticleViewModel.SetEdit = false;
            }

            if (WindowType == typeof(CustomerPage))
            {
                CustomerViewModel.SaveData = true;
                CustomerViewModel.SetEdit = false;
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.SaveData = true;
                OrderViewModel.SetEdit = false;
            }
        }
        public void DoDelete()
        {
            Type WindowType = CurrentWindow.GetType();
            if (WindowType == typeof(AddressPage))
            {
                AddressViewModel.DeleteData = true;
                AddressViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticleClassificationPage))
            {
                ArticleClassificationViewModel.DeleteData = true;
                ArticleClassificationViewModel.SetEdit = false;
            }

            if (WindowType == typeof(ArticlePage))
            {
                ArticleViewModel.DeleteData = true;
                ArticleViewModel.SetEdit = false;
            }

            if (WindowType == typeof(CustomerPage))
            {
                CustomerViewModel.DeleteData = true;
                CustomerViewModel.SetEdit = false;
            }

            if (WindowType == typeof(OrderPage))
            {
                OrderViewModel.DeleteData = true;
                OrderViewModel.SetEdit = false;
            }
        }

        public void DoAbort()
        {
            CurrentWindow = new ApplicationPage();
        }
        public void ShowHistory()
        {
            CurrentWindow = new ApplicationPage();
        }

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
