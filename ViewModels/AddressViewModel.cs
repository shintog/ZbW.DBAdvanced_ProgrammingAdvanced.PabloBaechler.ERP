using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
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

        private Int32 _addressNr;
        public Int32 AddressNr
        {
            get { return _addressNr; }
            set { }
        }

        private String _street;
        public String Street
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyPropertyChanged("Street");
            }
        }

        private Int32 _number;
        public Int32 Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }

        private Int32 _zip;
        public Int32 ZIP
        {
            get { return _zip; }
            set
            {
                _zip = value;
                NotifyPropertyChanged("ZIP");
            }
        }

        private String _city;
        public String City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
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
