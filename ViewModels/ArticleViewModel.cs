using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        public ArticleViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.ArticleData = new Article();
            this.CurrentSearchMask = new SearchPage();
            this.Error = Visibility.Hidden;
        }

        public MainViewModel Parent;

        public AuftragsverwaltungDataAccess DataAccess;
        
        private Article _articleData;
        private Article ArticleData
        {
            get { return _articleData; }
            set
            {
                _articleData = value;
                NotifyPropertyChanged(nameof(ArticleNr));
                NotifyPropertyChanged(nameof(Name));
                NotifyPropertyChanged(nameof(Designation));
                NotifyPropertyChanged(nameof(ClassificationSelectedItem));
                NotifyPropertyChanged(nameof(PurchasingPrice));
                NotifyPropertyChanged(nameof(PPCurrencySelectedItem));
                NotifyPropertyChanged(nameof(SalesPrice));
                NotifyPropertyChanged(nameof(SPCurrencySelectedItem));
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

        public int ArticleNr
        {
            get { return ArticleData.ArticleNr; }
            set
            {
                ArticleData.ArticleNr = value;
                NotifyPropertyChanged(nameof(ArticleNr));
            }
        }
        
        public String Name
        {
            get { return ArticleData.Name; }
            set
            {
                ArticleData.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        public String Designation
        {
            get { return ArticleData.Designation; }
            set
            {
                ArticleData.Designation = value;
                NotifyPropertyChanged(nameof(Designation));
            }
        }
        
        public Dictionary<int, String> Classification
        {
            get { return DataAccess.ArticleClassifications.ToDictionary(a => a.ClassificationNr, a => a.Name); }
            set { NotifyPropertyChanged(nameof(Classification)); }
        }
        
        public KeyValuePair<int, String> ClassificationSelectedItem
        {
            get
            {
                KeyValuePair<int, String> ItemKeyValue = new KeyValuePair<int, String>();

                if (ArticleData != null && ArticleData.ArticleClassification != null)
                    ItemKeyValue = new KeyValuePair<int, String>(ArticleData.ArticleClassification.ClassificationNr, ArticleData.ArticleClassification.Name);

                return ItemKeyValue;
            }
            set
            {
                if (value.Key > 0)
                {
                    ArticleData.ArticleClassification =
                        DataAccess.ArticleClassifications.First(a => a.ClassificationNr == value.Key);
                    ClassificationValue = value.Value;
                    NotifyPropertyChanged(nameof(ClassificationSelectedItem));
                }
            }
        }

        private String _classificationtValue;
        public String ClassificationValue
        {
            get { return _classificationtValue; }
            set
            {
                _classificationtValue = value;
                NotifyPropertyChanged(nameof(ClassificationValue));
            }
        }
        
        public Decimal PurchasingPrice
        {
            get { return ArticleData.PurchasingPrice; }
            set
            {
                ArticleData.PurchasingPrice = value;
                NotifyPropertyChanged(nameof(PurchasingPrice));
            }
        }
        
        public Dictionary<String, String> Currency
        {
            get { return DataAccess.Currencies.ToDictionary(c => c.CurrencyCode, c => c.Name); }
            set
            {
                NotifyPropertyChanged(nameof(Currency));
            }
        }
        
        public KeyValuePair<String, String> PPCurrencySelectedItem
        {
            get
            {
                KeyValuePair<String, String> ItemKeyValue = new KeyValuePair<String, String>();

                if (ArticleData != null && ArticleData.Currency != null)
                    ItemKeyValue =
                        new KeyValuePair<string, string>(ArticleData.Currency.CurrencyCode, ArticleData.Currency.Name);

                return ItemKeyValue;
            }
            set
            {
                ArticleData.PPCurrency = value.Key;
                NotifyPropertyChanged(nameof(PPCurrencySelectedItem));
            }
        }

        public Decimal SalesPrice
        {
            get { return ArticleData.SalesPrice; }
            set
            {
                ArticleData.SalesPrice = value;
                NotifyPropertyChanged(nameof(SalesPrice));
            }
        }
        
        public KeyValuePair<String, String> SPCurrencySelectedItem
        {
            get
            {
                KeyValuePair<String, String> ItemKeyValue = new KeyValuePair<String, String>();

                if (ArticleData != null && ArticleData.Currency1 != null)
                    ItemKeyValue =
                        new KeyValuePair<string, string>(ArticleData.Currency1.CurrencyCode, ArticleData.Currency1.Name);

                return ItemKeyValue;
            }
            set
            {
                ArticleData.SPCurrency = value.Key;
                NotifyPropertyChanged(nameof(SPCurrencySelectedItem));
            }
        }
        public Dictionary<int, String> ItemList
        {
            get
            {
                Dictionary<int, String> dataDictionary;
                if (SearchTerm != null)
                    dataDictionary = DataAccess.Articles.Where(a => a.ArticleNr.ToString().Contains(SearchTerm) ||
                                                                    a.Name.Contains(SearchTerm) ||
                                                                    a.Designation.Contains(SearchTerm))
                        .ToDictionary(a => a.ArticleNr, a => a.Name);
                else
                    dataDictionary = DataAccess.Articles.ToDictionary(a => a.ArticleNr, a => a.Name);
                
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
                ArticleData = DataAccess.Articles.First(a => a.ArticleNr == value.Key);
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
                    if (ArticleNr == 0)
                    {
                        int newKey = DataAccess.Articles.Select(a => a.ArticleNr).DefaultIfEmpty(0).Max();
                        ArticleData.ArticleNr = newKey == 0 ? 1000000 : newKey + 1;
                    }

                    DataAccess.Articles.AddOrUpdate(ArticleData);
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
                if (value && DataAccess.Articles.Select(a => a.ArticleNr).Contains(ArticleData.ArticleNr))
                {
                    DataAccess.Articles.Remove(ArticleData);
                    DataAccess.SaveChanges();
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
                    ArticleData = new Article();
                    ClassificationSelectedItem = new KeyValuePair<int, string>();
                    PPCurrencySelectedItem = new KeyValuePair<string, string>();
                    SPCurrencySelectedItem = new KeyValuePair<string, string>();
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
