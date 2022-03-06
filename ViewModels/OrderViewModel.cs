using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public OrderViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.OrderData = new Order();
            this.CurrentSearchMask = new SearchPage();
            this.Error = Visibility.Hidden;
        }

        public MainViewModel Parent;

        public AuftragsverwaltungDataAccess DataAccess;
        
        private Order _orderData;
        private Order OrderData
        {
            get { return _orderData; }
            set
            {
                _orderData = value;
                NotifyPropertyChanged(nameof(OrderNr));
                NotifyPropertyChanged(nameof(Date));
                NotifyPropertyChanged(nameof(CustomerSelectedItem));
                NotifyPropertyChanged(nameof(Positions));
            }
        }

        private SearchPage _currentSearchMask;
        public SearchPage CurrentSearchMask
        {
            get { return _currentSearchMask; }
            set
            {
                _currentSearchMask = value;
                NotifyPropertyChanged(nameof(CurrentSearchMask));
            }
        }

        public Boolean ResetSearchMask
        {
            get { return false; }
            set { if (value == true) CurrentSearchMask = new SearchPage(); }
        }

        public Decimal OrderNr
        {
            get { return OrderData.OrderNr; }
            set { }
        }
        
        public DateTime Date
        {
            get { return OrderData.Date; }
            set
            {
                OrderData.Date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }

        public Dictionary<String, String> Customer
        {
            get { return DataAccess.Customers.ToDictionary(a => a.CustomerKey + a.CustomerNr, a => a.Name); }
            set { NotifyPropertyChanged(nameof(Customer)); }
        }

        public KeyValuePair<String, String> CustomerSelectedItem
        {
            get
            {
                KeyValuePair<String, String> ItemKeyValue = new KeyValuePair<String, String>();

                if (OrderData != null && OrderData.Customer1 != null)
                    ItemKeyValue =
                        new KeyValuePair<String, String>(OrderData.Customer1.CustomerKey + OrderData.Customer1.CustomerNr, OrderData.Customer1.Name);

                return ItemKeyValue;
            }
            set
            {
                OrderData.Customer1 = DataAccess.Customers.First(c => c.CustomerKey + c.CustomerNr == value.Key);
                OrderData.CustomerNr = int.Parse(value.Key.Substring(2));
                OrderData.Customer = "CU";
                CustomerValue = value.Value;
                NotifyPropertyChanged(nameof(CustomerSelectedItem));
            }
        }

        public Dictionary<int, String> Article
        {
            get { return DataAccess.Articles.ToDictionary(a => a.ArticleNr, a => a.Name); }
            set { NotifyPropertyChanged(nameof(Article)); }
        }

        public KeyValuePair<int, String> ArticleSelectedItem
        {
            get
            {
                KeyValuePair<int, String> ItemKeyValue = new KeyValuePair<int, String>();

                if (OrderData.Positions != null && OrderData.Positions.Count > 0 && OrderData.Positions.First(p => p.PositionNr == Position) != null && OrderData.Positions.First(p => p.PositionNr == Position).Article1 != null)
                    ItemKeyValue =
                        new KeyValuePair<int, String>(OrderData.Positions.First(p =>  p.PositionNr == Position).Article, OrderData.Positions.First(p => p.PositionNr == Position).Article1.Name);

                return ItemKeyValue;
            }
            set
            {
                OrderData.Positions.First(p => p.PositionNr == Position).Article = value.Key;
                OrderData.Positions.First(p => p.PositionNr == Position).Article1 = DataAccess.Articles.First(a => a.ArticleNr == value.Key);
                NotifyPropertyChanged(nameof(Positions));
                NotifyPropertyChanged(nameof(PositionsSelectedItem));
                NotifyPropertyChanged(nameof(ArticleValue));
                NotifyPropertyChanged(nameof(ArticleSelectedItem));
            }
        }
        
        private String ArticleValue
        {
            get { return DataAccess.Articles.First(a => a.ArticleNr == ArticleSelectedItem.Key).Designation; }
        }

        public Decimal Amount
        {
            get
            {
                Decimal amount = 0;
                if (OrderData.Positions != null && OrderData.Positions.Count > 0 && OrderData.Positions.First(p => p.PositionNr == Position) != null)
                    amount = OrderData.Positions.First(p => p.PositionNr == Position).Amount;

                return amount;
            }
            set
            {
                OrderData.Positions.First(p => p.PositionNr == Position).Amount = value;
                int savePosition = Position;
                NotifyPropertyChanged(nameof(Amount));
                NotifyPropertyChanged(nameof(Positions));
                PositionsSelectedItem = OrderData.Positions.First(p => p.PositionNr == savePosition);
            }
        }

        private String _customerValue;
        public String CustomerValue
        {
            get { return _customerValue; }
            set
            {
                _customerValue = value;
                NotifyPropertyChanged(nameof(CustomerValue));
            }
        }
        
        public List<Position> Positions
        {
            get { return OrderData.Positions.ToList(); }
        }

        private Position _positionsSelectedItem;

        public Position PositionsSelectedItem
        {
            get { return _positionsSelectedItem; }
            set
            {
                _positionsSelectedItem = value;
                Position = value.PositionNr;
                SetPositionEdit = true;
                NotifyPropertyChanged(nameof(PositionsSelectedItem));
                NotifyPropertyChanged(nameof(Article));
                NotifyPropertyChanged(nameof(ArticleSelectedItem));
                NotifyPropertyChanged(nameof(Amount));
            }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyPropertyChanged(nameof(Position));
            }
        }
        
        public Dictionary<int, String> ItemList
        {
            get
            {
                Dictionary<int, String> dataDictionary;
                if (SearchTerm != null)
                    dataDictionary = DataAccess.Orders.Where(o => o.OrderNr.ToString().Contains(SearchTerm) ||
                                                                  o.Date.ToString().Contains(SearchTerm) ||
                                                                  o.Customer1.Name.Contains(SearchTerm))
                        .ToDictionary(o => o.OrderNr, o => o.Customer1.Name);
                else
                    dataDictionary = DataAccess.Orders.ToDictionary(o => o.OrderNr, o => o.Customer1.Name);
                
                return dataDictionary;
            }
            set
            {
                NotifyPropertyChanged(nameof(ItemList));
            }
        }

        private String _searchTerm;
        public String SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                NotifyPropertyChanged(nameof(ItemList));
                NotifyPropertyChanged(nameof(SearchTerm));
            }
        }

        private KeyValuePair<int, String> _selectedItem;
        public KeyValuePair<int, String> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OrderData = DataAccess.Orders.First(o => o.OrderNr == value.Key);
                SetEdit = false;
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        private Dictionary<Int32, String> _errorList;

        public Dictionary<Int32, String> ErrorList
        {
            get { return _errorList; }
            set
            {
                _errorList = value;
                if (_errorList.Count > 0)
                    Error = Visibility.Visible;
                else
                    Error = Visibility.Hidden;
                NotifyPropertyChanged(nameof(ErrorList));
            }
        }

        private Visibility _error;
        public Visibility Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyPropertyChanged(nameof(Error));
            }
        }

        private bool _saveData;
        public bool SaveData
        {
            get { return _saveData; }
            set
            {
                if ((_errorList == null || _errorList.Count == 0) && value)
                {
                    int newKey = DataAccess.Orders.Select(o => o.OrderNr).DefaultIfEmpty(0).Max() + 1;
                    List<Position> positionList = OrderData.Positions.ToList();
                    OrderData.Positions = new List<Position>();
                    OrderData.OrderNr = OrderData.OrderNr == 0 ? newKey : OrderData.OrderNr;
                    DataAccess.SaveChanges();
                    foreach (var element in positionList)
                    {
                        DataAccess.Positions.AddOrUpdate(element);
                    }
                    DataAccess.SaveChanges();

                    Parent.DataAccess = DataAccess;
                    NotifyPropertyChanged(nameof(ItemList));
                }
            }
        }

        private bool _deleteData;
        public bool DeleteData
        {
            get { return _saveData; }
            set
            {
                if (value && DataAccess.Orders.Select(o => o.OrderNr).Contains(OrderData.OrderNr))
                {
                    DataAccess.Orders.Remove(OrderData);
                    DataAccess.SaveChanges();
                    Parent.DataAccess = DataAccess;
                    NotifyPropertyChanged(nameof(ItemList));
                    SetNew = true;
                }
            }
        }

        private bool _setNew;
        public bool SetNew
        {
            get { return _setNew; }
            set
            {
                if (value)
                {
                    OrderData.Date = DateTime.Today;
                    NotifyPropertyChanged(nameof(OrderNr));
                    NotifyPropertyChanged(nameof(Date));
                }
            }
        }

        private bool _setEdit;
        public bool SetEdit
        {
            get { return _setEdit; }
            set
            {
                _setEdit = value;
                NotifyPropertyChanged(nameof(SetEdit));
            }
        }

        private bool _setPositionEdit;
        public bool SetPositionEdit
        {
            get { return _setPositionEdit; }
            set
            {
                _setPositionEdit = value;
                NotifyPropertyChanged(nameof(SetPositionEdit));
            }
        }
        
        public void AddPosition()
        {
            Position NewPosition = new Position();
            NewPosition.Order = OrderData.OrderNr;

            NewPosition.PositionNr = OrderData.Positions != null && OrderData.Positions.Count() != 0
                ? OrderData.Positions.Select(p => p.PositionNr).Max() + 1
                : 1;
            OrderData.Positions.Add(NewPosition);
            PositionsSelectedItem = NewPosition;
            SetPositionEdit = true;
        }

        public void SubstractPosition()
        {
            if (OrderData.Positions.Select(p => p.PositionNr).Contains(Position))
            {
                OrderData.Positions.Remove(OrderData.Positions.First(p => p.PositionNr == Position));
                NotifyPropertyChanged(nameof(Positions));

                PositionsSelectedItem = OrderData.Positions.First();

                if (OrderData.Positions.Count == 0)
                    SetPositionEdit = false;
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

        private ICommand _clickAddCommand;
        public ICommand ClickAddCommand
        {
            get
            {
                if (_clickAddCommand == null)
                    _clickAddCommand = new Add();
                return _clickAddCommand;
            }
            set
            {
                _clickAddCommand = value;
            }
        }

        private ICommand _clickSubstractCommand;
        public ICommand ClickSubstractCommand
        {
            get
            {
                if (_clickSubstractCommand == null)
                    _clickSubstractCommand = new Substract();
                return _clickSubstractCommand;
            }
            set
            {
                _clickSubstractCommand = value;
            }
        }
    }
    class Add : ICommand
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
            ((OrderViewModel)parameter).AddPosition();
        }
        #endregion
    }
    class Substract : ICommand
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
            ((OrderViewModel)parameter).SubstractPosition();
        }
        #endregion
    }
}
