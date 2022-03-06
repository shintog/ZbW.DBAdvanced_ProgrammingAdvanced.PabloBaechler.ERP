using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class ArticleClassificationViewModel : INotifyPropertyChanged
    {
        public ArticleClassificationViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.ClassificationData = new ArticleClassification();
            this.CurrentSearchMask = new SearchPage();
            this.Error = Visibility.Hidden;

            NotifyPropertyChanged(nameof(ParentClassification));
        }

        public MainViewModel Parent;

        public AuftragsverwaltungDataAccess DataAccess;

        private ArticleClassification _classificationData;
        private ArticleClassification ClassificationData
        {
            get { return _classificationData; } 
            set
            {
                _classificationData = value;
                NotifyPropertyChanged(nameof(ClassificationNr));
                NotifyPropertyChanged(nameof(ParentClassification));
                NotifyPropertyChanged(nameof(ParentSelectedItem));
                NotifyPropertyChanged(nameof(Name));
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


        public int ClassificationNr
        {
            get { return ClassificationData.ClassificationNr; }
            set
            {
                ClassificationData.ClassificationNr = value;
                NotifyPropertyChanged(nameof(ClassificationNr));
            }
        }


        public Dictionary<int, String> ParentClassification
        {
            get
            {
                return DataAccess.ArticleClassifications.Where(a => a.ClassificationNr != ClassificationNr).ToDictionary(a => a.ClassificationNr, a => a.Name) ;
            }
        }
        
        public KeyValuePair<int, String> ParentSelectedItem
        {
            get
            {
                KeyValuePair<int, String> ItemKeyValue = new KeyValuePair<int, String>();

                if (ClassificationData != null && ClassificationData.ArticleClassification2 != null)
                    ItemKeyValue =
                        new KeyValuePair<int, String>(ClassificationData.ArticleClassification2.ClassificationNr, ClassificationData.ArticleClassification2.Name);

                return ItemKeyValue;
            }
            set
            {
                ClassificationData.Parent = value.Key;
                ParentValue = value.Value;
                NotifyPropertyChanged(nameof(ParentSelectedItem));
            }
        }
        
        private String _parentValue;
        public String ParentValue
        {
            get { return _parentValue; }
            set
            {
                _parentValue = value;
                NotifyPropertyChanged(nameof(ParentValue));
            }
        }
        
        public String Name
        {
            get { return ClassificationData.Name; }
            set
            {
                ClassificationData.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public Dictionary<int, String> ItemList
        {
            get
            {
                Dictionary<int, String> dataDictionary;
                if (SearchTerm != null)
                    dataDictionary = DataAccess.ArticleClassifications.Where(a => a.ClassificationNr.ToString().Contains(SearchTerm) ||
                                                                     a.Name.Contains(SearchTerm) ||
                                                                     a.Parent.ToString().Contains(SearchTerm))
                        .ToDictionary(a => a.ClassificationNr, a => a.Name);
                else
                    dataDictionary = DataAccess.ArticleClassifications.ToDictionary(a => a.ClassificationNr, a => a.Name);
                
                return dataDictionary;
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
                ClassificationData = DataAccess.ArticleClassifications.First(a => a.ClassificationNr == value.Key);
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
                    if (ClassificationNr == 0)
                    {
                        int newKey = DataAccess.ArticleClassifications.Select(a => a.ClassificationNr).DefaultIfEmpty(0).Max();
                        ClassificationData.ClassificationNr = newKey == 0 ? 1000000 : newKey + 1;
                    }

                    ClassificationData.Parent = ClassificationData.Parent == 0 ? null : ClassificationData.Parent;

                    DataAccess.ArticleClassifications.AddOrUpdate(ClassificationData);
                    DataAccess.SaveChanges();
                    Parent.DataAccess = DataAccess;
                    NotifyPropertyChanged(nameof(ItemList));
                    SelectedItem = new KeyValuePair<int, string>(ClassificationData.ClassificationNr,
                        ClassificationData.Name);
                }
            }
        }

        private bool _deleteData;
        public bool DeleteData
        {
            get { return _saveData; }
            set
            {
                if (value && DataAccess.ArticleClassifications.Select(a => a.ClassificationNr).Contains(ClassificationData.ClassificationNr))
                {
                    DataAccess.ArticleClassifications.Remove(ClassificationData);
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
                    ClassificationData = new ArticleClassification();
                    ParentSelectedItem = new KeyValuePair<int, string>();
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
