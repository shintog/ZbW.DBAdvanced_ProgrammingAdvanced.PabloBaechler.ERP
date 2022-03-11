using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        public ArticleViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.ArticleData = new ArticleData();
            this.CurrentSearchMask = new SearchPage();
            this.ErrorList = new Dictionary<string, string>();
        }

        public MainViewModel Parent;

        public AuftragsverwaltungModel DataModel;
        
        private ArticleData _articleData;
        private ArticleData ArticleData
        {
            get { return _articleData; }
            set
            {
                _articleData = value;
                NotifyPropertyChanged(nameof(ArticleNr));
                NotifyPropertyChanged(nameof(Name));
                NotifyPropertyChanged(nameof(Designation));
                NotifyPropertyChanged(nameof(Classification));
                NotifyPropertyChanged(nameof(Currency));
                NotifyPropertyChanged(nameof(ClassificationSelectedItem));
                NotifyPropertyChanged(nameof(PurchasingPrice));
                NotifyPropertyChanged(nameof(PPCurrencySelectedItem));
                NotifyPropertyChanged(nameof(SalesPrice));
                NotifyPropertyChanged(nameof(SPCurrencySelectedItem));
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
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Name) );
                if (value == "")
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Name) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.Name)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        public String Designation
        {
            get { return ArticleData.Designation; }
            set
            {
                ArticleData.Designation = value;
                
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Designation) );
                if (value == "")
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Designation) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.Designation)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(Designation));
            }
        }
        
        public Dictionary<int, String> Classification
        {
            get
            {
                Dictionary<int, String> ListDictionary = new Dictionary<int, String>();
                ListDictionary.Add(0, null);
                ListDictionary.AddRange(DataModel.ArticleClassifications.ToDictionary(a => a.ClassificationNr, a => a.Name));
                return ListDictionary;
            }
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
                ArticleData.ArticleClassification =
                    value.Key > 0 ? DataModel.ArticleClassifications.First(a => a.ClassificationNr == value.Key) :  new ArticleClassificationData();
                ArticleData.Classification = value.Key;
                ClassificationValue = value.Value;
                
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.ClassificationSelectedItem) );
                if (value.Key == 0)
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.ClassificationSelectedItem) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.ClassificationSelectedItem)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(ClassificationSelectedItem));
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
        
        public Double PurchasingPrice
        {
            get { return ArticleData.PurchasingPrice; }
            set
            {
                ArticleData.PurchasingPrice = value;

                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PurchasingPrice) );
                if (value == 0)
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PurchasingPrice) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.PurchasingPrice)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(PurchasingPrice));
            }
        }
        
        public Dictionary<String, String> Currency
        {
            get
            {
                Dictionary<String, String> ListDictionary = new Dictionary<String, String>();
                ListDictionary.Add("", null);
                ListDictionary.AddRange(DataModel.Currencies.ToDictionary(c => c.CurrencyCode, c => c.Name));
                return ListDictionary;
            }
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

                if (ArticleData != null)
                    ItemKeyValue = ArticleData.CurrencyPP != null ? new KeyValuePair<string, string>(ArticleData.CurrencyPP.CurrencyCode, ArticleData.CurrencyPP.Name) : new KeyValuePair<string, string>("", null);

                return ItemKeyValue;
            }
            set
            {
                ArticleData.PPCurrency = value.Key;
                ArticleData.CurrencyPP = value.Key != ""
                    ? DataModel.Currencies.First(c => c.CurrencyCode == value.Key)
                    : new CurrencyData();
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PPCurrencySelectedItem) );
                if (value.Key == "")
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PPCurrencySelectedItem) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.PPCurrencySelectedItem)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(PPCurrencySelectedItem));
            }
        }

        public Double SalesPrice
        {
            get { return ArticleData.SalesPrice; }
            set
            {
                ArticleData.SalesPrice = value;

                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SalesPrice) );
                if (value == 0)
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SalesPrice) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.SalesPrice)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(SalesPrice));
            }
        }
        
        public KeyValuePair<String, String> SPCurrencySelectedItem
        {
            get
            {
                KeyValuePair<String, String> ItemKeyValue = new KeyValuePair<String, String>();

                if (ArticleData != null)
                    ItemKeyValue = ArticleData.CurrencySP != null ? new KeyValuePair<string, string>(ArticleData.CurrencySP.CurrencyCode, ArticleData.CurrencySP.Name) : new KeyValuePair<string, string>("", null);

                return ItemKeyValue;
            }
            set
            {
                ArticleData.SPCurrency = value.Key;
                ArticleData.CurrencySP = value.Key != ""
                    ? DataModel.Currencies.First(c => c.CurrencyCode == value.Key)
                    : new CurrencyData();

                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SPCurrencySelectedItem) );
                if (value.Key == "")
                {
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SPCurrencySelectedItem) , Parent.ListOfErrors[nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.SPCurrencySelectedItem)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(SPCurrencySelectedItem));
            }
        }
        public List<Article_HistoryData> HistoryList
        {
            get { return ArticleData != null && ArticleData.ArticleNr != 0 ? DataModel.Article_History.Where(h => h.ArticleNr == ArticleData.ArticleNr).ToList() : new List<Article_HistoryData>(); }
        }
        public Dictionary<int, String> ItemList
        {
            get
            {
                Dictionary<int, String> dataDictionary = new Dictionary<int, string>();
                if (SearchTerm != null)
                    dataDictionary = DataModel.Articles.Where(a => a.ArticleNr.ToString().Contains(SearchTerm) ||
                                                                    a.Name.Contains(SearchTerm) ||
                                                                    a.Designation.Contains(SearchTerm))
                        .ToDictionary(a => a.ArticleNr, a => a.Name);
                else
                    dataDictionary = DataModel.Articles.ToDictionary(a => a.ArticleNr, a => a.Name);
                
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
                ArticleData = DataModel.Articles.First(a => a.ArticleNr == value.Key);
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
            get
            {
                return _errorList.Count > 0 ? _errorList.FirstOrDefault(e => e.Key.StartsWith(ArticleNr.ToString())).Value : "";
            }
        }

        public Visibility Error
        {
            get { return _errorList.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }
        
        public void SaveData()
        {
            if ((_errorList == null || _errorList.Where(a => a.Key.StartsWith(ArticleData.ArticleNr.ToString())).ToList().Count == 0))
            {
                if (ArticleNr == 0)
                {
                    int newKey = DataModel.Articles.Select(a => a.ArticleNr).DefaultIfEmpty(0).Max();
                    ArticleData.ArticleNr = newKey == 0 ? 1000000 : newKey + 1;
                }
                
                DataModel.WriteData(ArticleData);
                Parent.DataModel = DataModel;
                SetEdit = false;
                NotifyPropertyChanged(nameof(ItemList));
            }
    }
        
        public void DeleteData()
        {
            if (DataModel.Articles.Select(a => a.ArticleNr).Contains(ArticleData.ArticleNr))
            {
                ErrorList.Remove(ArticleData.ArticleNr.ToString());
                KeyValuePair <String, String> DeleteError = DataModel.DeleteData(ArticleData);
                if (DeleteError.Key != "")
                    ErrorList.Add(DeleteError.Key , DeleteError.Value);
                else
                    SetNew = true;

                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Name) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Designation) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.ClassificationSelectedItem) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PurchasingPrice) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PPCurrencySelectedItem) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SalesPrice) );
                ErrorList.Remove(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SPCurrencySelectedItem) );
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(ItemList));
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
                    ArticleData = new ArticleData();
                    
                    ClassificationSelectedItem = new KeyValuePair<int, string>(0, null);
                    PPCurrencySelectedItem = new KeyValuePair<string, string>("", null);
                    SPCurrencySelectedItem = new KeyValuePair<string, string>("", null);

                    ErrorList = new Dictionary<string, string>();
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Name) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.Name)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.Designation) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.Designation)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.ClassificationSelectedItem) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." +
                            nameof(Parent.ArticleViewModel.ClassificationSelectedItem)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PurchasingPrice) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.PurchasingPrice)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.PPCurrencySelectedItem) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." +
                            nameof(Parent.ArticleViewModel.PPCurrencySelectedItem)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SalesPrice) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." + nameof(Parent.ArticleViewModel.SalesPrice)]);
                    ErrorList.Add(Parent.ArticleViewModel.ArticleNr + "." + nameof(Parent.ArticleViewModel.SPCurrencySelectedItem) ,
                        Parent.ListOfErrors[
                            nameof(Parent.ArticleViewModel) + "." +
                            nameof(Parent.ArticleViewModel.SPCurrencySelectedItem)]);
                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));
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
