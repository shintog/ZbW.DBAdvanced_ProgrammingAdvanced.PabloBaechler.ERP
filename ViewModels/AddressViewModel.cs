using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class AddressViewModel : INotifyPropertyChanged
    {
        public AddressViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.AddressData = new Address();
            this.CurrentSearchMask = new SearchPage();
            this.Error = Visibility.Hidden;
            this.SetEdit = false;

        }

        public MainViewModel Parent;

        public AuftragsverwaltungDataAccess DataAccess;
        
        private Address _addressData;
        private Address AddressData
        {
            get { return _addressData; }
            set
            {
                _addressData = value;
                NotifyPropertyChanged(nameof(AddressNr));
                NotifyPropertyChanged(nameof(Street));
                NotifyPropertyChanged(nameof(Number));
                NotifyPropertyChanged(nameof(ZIP));
                NotifyPropertyChanged(nameof(City));
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
            set { if(value == true) CurrentSearchMask = new SearchPage(); }
        }
        
        public string AddressNr
        {
            get { return AddressData.AddressKey + AddressData.AddressNr; }
            set
            {
                AddressData.AddressKey = value.Substring(0,2);
                NotifyPropertyChanged(nameof(AddressNr));
            }
        }
        
        public String Street
        {
            get { return AddressData.Street; }
            set
            {
                AddressData.Street = value;
                NotifyPropertyChanged(nameof(Street));
            }
        }
        
        public String Number
        {
            get { return AddressData.Number; }
            set
            {
                AddressData.Number = value;
                NotifyPropertyChanged(nameof(Number));
            }
        }
        
        public Decimal ZIP
        {
            get { return AddressData.ZIP; }
            set
            {
                AddressData.ZIP = value;
                NotifyPropertyChanged(nameof(ZIP));
            }
        }
        
        public String City
        {
            get { return AddressData.City; }
            set
            {
                AddressData.City = value;
                NotifyPropertyChanged(nameof(City));
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
                    if (AddressNr == "CU0")
                    {
                        int newKey = DataAccess.Addresses.Select(a => a.AddressNr).DefaultIfEmpty(0).Max();
                        AddressData.AddressNr = newKey == 0 ? 1000000 : newKey + 1;
                    }
                    
                    DataAccess.Addresses.AddOrUpdate(AddressData);
                    DataAccess.SaveChanges();
                    Parent.DataAccess = DataAccess;
                    NotifyPropertyChanged(nameof(ItemList));
                    SelectedItem = new KeyValuePair<string, string>(AddressData.AddressKey + AddressData.AddressNr,
                        AddressData.Street + " " + AddressData.Number + ", " + AddressData.City + " " + AddressData.ZIP);
                }
            }
        }

        private bool _deleteData;
        public bool DeleteData
        {
            get { return _saveData; }
            set
            {
                if (value && DataAccess.Addresses.Select(a => a.AddressNr).Contains(AddressData.AddressNr))
                {
                    DataAccess.Addresses.Remove(AddressData);
                    DataAccess.SaveChanges();
                    Parent.DataAccess = DataAccess;
                    NotifyPropertyChanged(nameof(ItemList));
                    SetNew = true;
                }
            }
        }

        private Boolean _setNew;
        public Boolean SetNew
        {
            get { return _setNew; }
            set
            {
                if (value)
                {
                    AddressData = new Address();
                    AddressData.AddressKey = "CU";
                }
            }
        }

        private Boolean _setEdit;
        public Boolean SetEdit
        {
            get { return _setEdit; }
            set
            {
                _setEdit = value && AddressNr == "CU0";
                NotifyPropertyChanged(nameof(SetEdit));
            }
        }

        public Dictionary<String, String> ItemList
        {
            get
            {
                Dictionary<String, String> dataDictionary = new Dictionary<string, string>();
                if (SearchTerm != null)
                    dataDictionary = DataAccess.Addresses.Where(a => (a.AddressKey + a.AddressNr).Contains(SearchTerm) ||
                                                                     a.City.Contains(SearchTerm) ||
                                                                     a.ZIP.ToString().Contains(SearchTerm) ||
                                                                     a.Street.Contains(SearchTerm) ||
                                                                     a.Number.Contains(SearchTerm) ||
                                                                     (a.Customers.FirstOrDefault().CustomerKey +
                                                                      a.Customers.FirstOrDefault().CustomerNr)
                                                                     .Contains(SearchTerm) ||
                                                                     a.Customers.FirstOrDefault().Name
                                                                         .Contains(SearchTerm))
                        .ToDictionary(a => a.AddressKey + a.AddressNr, a => a.Street + " " + a.Number + ", " + a.City + " " + a.ZIP);
                else
                    dataDictionary = DataAccess.Addresses.ToDictionary(a => a.AddressKey + a.AddressNr,
                        a => a.Street + " " + a.Number + ", " + a.City + " " + a.ZIP);
                
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

        private KeyValuePair<String, String> _selectedItem;
        public KeyValuePair<String, String> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                AddressData = DataAccess.Addresses.First(a => a.AddressKey + a.AddressNr == value.Key);
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
