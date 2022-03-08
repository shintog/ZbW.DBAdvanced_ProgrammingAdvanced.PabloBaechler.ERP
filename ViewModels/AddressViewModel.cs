using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
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
            this.SetEdit = false;
            this.ErrorList = new Dictionary<string, string>();

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

                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) + ": ");
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Street)]);
                }

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(Street));
            }
        }
        
        public String Number
        {
            get { return AddressData.Number; }
            set
            {
                AddressData.Number = value;
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) + ": ");
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Number)]);
                }

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(Number));
            }
        }
        
        public Decimal ZIP
        {
            get { return AddressData.ZIP; }
            set
            {
                AddressData.ZIP = value;
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) + ": ");
                if (value == 0)
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.ZIP)]);
                }

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(ZIP));
            }
        }
        
        public String City
        {
            get { return AddressData.City; }
            set
            {
                AddressData.City = value;
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) + ": ");
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.City)]);
                }

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

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

                    ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) + ": ");
                    ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) + ": ");
                    ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) + ": ");
                    ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) + ": ");

                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));

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
                    ErrorList = new Dictionary<string, string>();
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Street)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Number)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.ZIP)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) + ": ", Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.City)]);

                    NotifyPropertyChanged(nameof(CurrentError));
                    NotifyPropertyChanged(nameof(Error));
                }
            }
        }

        private Boolean _setEdit;
        public Boolean SetEdit
        {
            get { return _setEdit; }
            set
            {
                _setEdit = value;
                NotifyPropertyChanged(nameof(SetEdit));
            }
        }

        public List<Address_History> HistoryList
        {
            get { return AddressData != null && AddressData.AddressNr != 0 ? DataAccess.Address_History.Where(h => h.AddressNr == AddressData.AddressNr).ToList() : new List<Address_History>(); }
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
            get { return _errorList.Count > 0 ? _errorList.FirstOrDefault(e => e.Key.StartsWith(AddressNr.ToString())).Value : ""; }
        }

        public Visibility Error
        {
            get { return _errorList.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
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
