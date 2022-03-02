using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
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

        private Int32 _custNr;
        public Int32 CustNr
        {
            get { return _custNr; }
            set
            {
                _custNr = value;
                NotifyPropertyChanged("CustNr");
            }
        }

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private Dictionary<Int32, String> _address;
        public Dictionary<Int32, String> Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }

        private KeyValuePair<Int32, String> _addressSelectedItem;
        public KeyValuePair<Int32, String> AddressSelectedItem
        {
            get { return _addressSelectedItem; }
            set
            {
                _addressSelectedItem = value;
                AddressValue = value.Value;
                NotifyPropertyChanged("AddressSelectedItem");
            }
        }

        private String _addressValue;
        public String AddressValue
        {
            get { return _addressValue; }
            set
            {
                _addressValue = value;
                NotifyPropertyChanged("AddressValue");
            }
        }

        private String _eMail;
        public String EMail
        {
            get { return _eMail; }
            set
            {
                _eMail = value;
                NotifyPropertyChanged("EMail");
            }
        }

        private String _website;
        public String Website
        {
            get { return _website; }
            set
            {
                _website = value;
                NotifyPropertyChanged("Website");
            }
        }


        private List<Object> _itemList;
        public List<Object> ItemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                SearchViewModel.ItemList = _itemList;
                NotifyPropertyChanged("ItemList");
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
