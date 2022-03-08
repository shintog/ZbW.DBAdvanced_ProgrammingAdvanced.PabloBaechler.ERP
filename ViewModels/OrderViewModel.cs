using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
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
            this.ErrorList = new Dictionary<string, string>();
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
                NotifyPropertyChanged(nameof(HistoryList));
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

                ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Date) + ": ");
                if (value == new DateTime())
                {
                    ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Date) + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.Date)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(Date));
            }
        }

        public Dictionary<String, String> Customer
        {
            get { return DataAccess.Customers.ToDictionary(a => a.CustomerKey + a.CustomerNr, a => a.Name); }
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
                OrderData.Customer1 = value.Key != "" ? DataAccess.Customers.First(c => c.CustomerKey + c.CustomerNr == value.Key) : new Customer();
                OrderData.CustomerNr = int.Parse(value.Key != "" ? value.Key.Substring(2) : "0");
                OrderData.Customer = "CU";
                CustomerValue = value.Value;


                ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.CustomerSelectedItem) + ": ");
                if (value.Key == "")
                {
                    ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.CustomerSelectedItem) + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.CustomerSelectedItem)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(CustomerSelectedItem));
            }
        }

        public Dictionary<int, String> Article
        {
            get
            {

                Dictionary<int, String> ListDictionary = new Dictionary<int, String>();
                ListDictionary.Add(0, null);
                ListDictionary.AddRange(DataAccess.Articles.ToDictionary(a => a.ArticleNr, a => a.Name));
                return ListDictionary;
            }
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
                if (Position > 0)
                {
                    OrderData.Positions.First(p => p.PositionNr == Position).Article = value.Key;
                    OrderData.Positions.First(p => p.PositionNr == Position).Article1 = value.Key > 0
                        ? DataAccess.Articles.First(a => a.ArticleNr == value.Key)
                        : new Article();


                    ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ");
                    if (Amount == 0 || value.Key == 0)
                    {
                        ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.Positions)] + Position);
                    }
                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));
                }

                NotifyPropertyChanged(nameof(Positions));
                NotifyPropertyChanged(nameof(PositionsSelectedItem));
                NotifyPropertyChanged(nameof(ArticleValue));
                NotifyPropertyChanged(nameof(ArticleSelectedItem));
         
            }
        }

        public String ArticleValue
        {
            get { return ArticleSelectedItem.Key > 0 ? DataAccess.Articles.First(a => a.ArticleNr == ArticleSelectedItem.Key).Designation : ""; }
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
                if (OrderData.Positions.Count > 0)
                {
                    OrderData.Positions.First(p => p.PositionNr == Position).Amount = value;
                    int savePosition = Position;

                    ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ");
                    if (value == 0 || ArticleSelectedItem.Key == 0)
                    {
                        ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.Positions)] + Position);
                    }
                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));

                    NotifyPropertyChanged(nameof(Amount));
                    NotifyPropertyChanged(nameof(Positions));
                    PositionsSelectedItem = OrderData.Positions.First(p => p.PositionNr == savePosition);
                }
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
                if (value != null && ErrorList.Count(e => e.Key == nameof(Parent.OrderViewModel.Positions) + ": ") == 0)
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

        public List<Position_History> HistoryList
        {
            get { return OrderData != null && OrderData.OrderNr != 0 ? DataAccess.Position_History.Where(h => h.Order == OrderData.OrderNr).ToList() : new List<Position_History>(); }
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
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
            }
        }

        private Dictionary<String, String> _errorList;
        public Dictionary<String, String> ErrorList
        {
            get { return _errorList; }
            set
            {
                _errorList = value;
                NotifyPropertyChanged(nameof(ErrorList));
            }
        }

        public String CurrentError
        {
            get { return _errorList.Count > 0 ? _errorList.FirstOrDefault(e => e.Key.StartsWith(OrderNr.ToString())).Value : ""; }
        }

        public Visibility Error
        {
            get { return _errorList.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
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
                        element.Order = OrderData.OrderNr;
                        element.Order1 = OrderData;
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

                    ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Customer) + ": ");
                    ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Date) + ": ");
                    for (int i = 1; i <= Positions.Count; i++)
                    {
                        ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + i + ": ");
                    }

                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));

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
                    Order newOrder = new Order();
                    newOrder.Date = DateTime.Today;
                    newOrder.Positions = new List<Position>();
                    OrderData = newOrder;
                    Position = 0;
                    CustomerSelectedItem = new KeyValuePair<string, string>("", null);
                    ArticleSelectedItem = new KeyValuePair<int, string>(0, null);
                    Amount = 0;

                    ErrorList = new Dictionary<string, string>();
                    ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.CustomerSelectedItem) + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.CustomerSelectedItem)]);
                    ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Date) + ": ", Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.Date)]);
                    
                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));

                    SetPositionEdit = false;
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
            if (ErrorList.Count(e => e.Key == nameof(Parent.OrderViewModel.Positions) + ": ") == 0)
            {
                Position NewPosition = new Position();
                NewPosition.Order = OrderData.OrderNr;

                NewPosition.PositionNr = OrderData.Positions != null && OrderData.Positions.Count() != 0
                    ? OrderData.Positions.Select(p => p.PositionNr).Max() + 1
                    : 1;
                OrderData.Positions.Add(NewPosition);

                PositionsSelectedItem = NewPosition;
                ArticleSelectedItem = new KeyValuePair<int, string>(0, null);
                SetPositionEdit = true;
                
                ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ");

                ErrorList.Add(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ",
                    Parent.ListOfErrors[nameof(Parent.OrderViewModel) + "." + nameof(Parent.OrderViewModel.Positions)] + Position);
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
            }
        }

        public void SubstractPosition()
        {
            if (OrderData.Positions.Select(p => p.PositionNr).Contains(Position))
            {
                OrderData.Positions.Remove(OrderData.Positions.First(p => p.PositionNr == Position));
                NotifyPropertyChanged(nameof(Positions));
                ArticleSelectedItem = new KeyValuePair<int, string>(0, null);
                PositionsSelectedItem = OrderData.Positions.First();

                ErrorList.Remove(Parent.OrderViewModel.OrderNr + "." + nameof(Parent.OrderViewModel.Positions) + Position + ": ");
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
