using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ArticleClassificationViewModel
    {
        public ArticleClassificationViewModel()
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

        private Int32 _classificationNr;
        public Int32 ClassificationNr
        {
            get { return _classificationNr; }
            set
            {
                _classificationNr = value;
                NotifyPropertyChanged("ClassificationNr");
            }
        }

        private Dictionary<Int32, String> _parent;
        public Dictionary<Int32, String> Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyPropertyChanged("Parent");
            }
        }

        private KeyValuePair<Int32, String> _parentSelectedItem;
        public KeyValuePair<Int32, String> ParentSelectedItem
        {
            get { return _parentSelectedItem; }
            set
            {
                _parentSelectedItem = value;
                ParentValue = value.Value;
                NotifyPropertyChanged("ParentSelectedItem");
            }
        }

        private String _parentValue;
        public String ParentValue
        {
            get { return _parentValue; }
            set
            {
                _parentValue = value;
                NotifyPropertyChanged("ParentValue");
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
