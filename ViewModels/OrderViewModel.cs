using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
        }
        private SearchViewModel _searchViewModel;
        public SearchViewModel SearchViewModel
        {
            get { return _searchViewModel; }
            set
            {
                _searchViewModel = value;
                NotifyPropertyChanged("SearchViewModel");
            }
        }

        private Int32 _orderNr;
        public Int32 OrderNr
        {
            get { return _orderNr; }
            set { }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        private Dictionary<Int32, String> _customer;
        public Dictionary<Int32, String> Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyPropertyChanged("Customer");
            }
        }

        private KeyValuePair<Int32, String> _customerSelectedItem;
        public KeyValuePair<Int32, String> CustomerSelectedItem
        {
            get { return _customerSelectedItem; }
            set
            {
                _customerSelectedItem = value;
                CustomerValue = value.Value;
                NotifyPropertyChanged("CustomerSelectedItem");
            }
        }

        private String _customerValue;
        public String CustomerValue
        {
            get { return _customerValue; }
            set
            {
                _customerValue = value;
                NotifyPropertyChanged("CustomerValue");
            }
        }

        private List<Object> _positions;
        public List<Object> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                SearchViewModel.ItemList = _positions;
                NotifyPropertyChanged("Positions");
            }
        }

        private Int32 _selectedItem;
        public Int32 SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
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
