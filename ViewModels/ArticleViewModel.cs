using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
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

        private Int32 _articleNr;
        public Int32 ArticleNr
        {
            get { return _articleNr; }
            set
            {
                _articleNr = value;
                NotifyPropertyChanged("ArticleNr");
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

        private String _designation;
        public String Designation
        {
            get { return _designation; }
            set
            {
                _designation = value;
                NotifyPropertyChanged("Designation");
            }
        }

        private Dictionary<Int32, String> _classification;
        public Dictionary<Int32, String> Classification
        {
            get { return _classification; }
            set
            {
                _classification = value;
                NotifyPropertyChanged("Classification");
            }
        }

        private KeyValuePair<Int32, String> _classificationSelectedItem;
        public KeyValuePair<Int32, String> ClassificationSelectedItem
        {
            get { return _classificationSelectedItem; }
            set
            {
                _classificationSelectedItem = value;
                ClassificationValue = value.Value;
                NotifyPropertyChanged("ClassificationSelectedItem");
            }
        }

        private String _classificationtValue;
        public String ClassificationValue
        {
            get { return _classificationtValue; }
            set
            {
                _classificationtValue = value;
                NotifyPropertyChanged("ClassificationValue");
            }
        }

        private Int32 _purchasingPrice;
        public Int32 PurchasingPrice
        {
            get { return _purchasingPrice; }
            set
            {
                _purchasingPrice = value;
                NotifyPropertyChanged("PurchasingPrice");
            }
        }

        private Dictionary<Int32, String> _currency;
        public Dictionary<Int32, String> Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                NotifyPropertyChanged("Currency");
            }
        }

        private KeyValuePair<Int32, String> _pPCurrencySelectedItem;
        public KeyValuePair<Int32, String> PPCurrencySelectedItem
        {
            get { return _pPCurrencySelectedItem; }
            set
            {
                _pPCurrencySelectedItem = value;
                NotifyPropertyChanged("PPCurrencySelectedItem");
            }
        }

        private Int32 _salesPrice;
        public Int32 SalesPrice
        {
            get { return _salesPrice; }
            set
            {
                _salesPrice = value;
                NotifyPropertyChanged("SalesPrice");
            }
        }

        private KeyValuePair<Int32, String> _sPCurrencySelectedItem;
        public KeyValuePair<Int32, String> SPCurrencySelectedItem
        {
            get { return _sPCurrencySelectedItem; }
            set
            {
                _sPCurrencySelectedItem = value;
                NotifyPropertyChanged("SPCurrencySelectedItem");
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
