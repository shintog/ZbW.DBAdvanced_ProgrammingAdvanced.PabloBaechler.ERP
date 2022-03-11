using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class AddressViewModel : INotifyPropertyChanged
    {
        public AddressViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.AddressData = new AddressData();
            this.CurrentSearchMask = new SearchPage();
            this.SetEdit = false;
            this.ErrorList = new Dictionary<string, string>();

        }

        public MainViewModel Parent;

        public AuftragsverwaltungModel DataModel;
        
        private AddressData _addressData;
        private AddressData AddressData
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

                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) );
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Street)]);
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
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) );
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Number)]);
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
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) );
                if (value == 0)
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.ZIP)]);
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
                
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) );
                if (value == "")
                {
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.City)]);
                }

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                NotifyPropertyChanged(nameof(City));
            }
        }
        
        public void SaveData()
        {
            if ((_errorList == null || _errorList.Where(a => a.Key.StartsWith(AddressData.AddressNr.ToString().Substring(2))).ToList().Count == 0))
            {
                if (AddressNr == "CU0")
                {
                    int newKey = DataModel.Addresses.Select(a => a.AddressNr).DefaultIfEmpty(0).Max();
                    AddressData.AddressNr = newKey == 0 ? 1000000 : newKey + 1;
                }
                
                DataModel.WriteData(AddressData);
                Parent.DataModel = DataModel;
                SetEdit = false;
                NotifyPropertyChanged(nameof(ItemList));
                SelectedItem = new KeyValuePair<string, string>(AddressData.AddressKey + AddressData.AddressNr,
                    AddressData.Street + " " + AddressData.Number + ", " + AddressData.City + " " + AddressData.ZIP);
            }
        }
        
        public void DeleteData()
        {
            if (DataModel.Addresses.Select(a => a.AddressNr).Contains(AddressData.AddressNr))
            {

                ErrorList.Remove(AddressData.AddressNr.ToString());
                KeyValuePair<String, String> DeleteError = DataModel.DeleteData(AddressData);
                if (DeleteError.Key != "")
                    ErrorList.Add(DeleteError.Key, DeleteError.Value);
                else
                    SetNew = true;

                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) );
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) );
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) );
                ErrorList.Remove(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) );

                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                Parent.DataModel = DataModel;
                NotifyPropertyChanged(nameof(ItemList));
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
                    AddressData = new AddressData();
                    AddressData.AddressKey = "CU";
                    ErrorList = new Dictionary<string, string>();
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Street) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Street)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.Number) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.Number)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.ZIP) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.ZIP)]);
                    ErrorList.Add(Parent.AddressViewModel.AddressNr + "." + nameof(Parent.AddressViewModel.City) , Parent.ListOfErrors[nameof(Parent.AddressViewModel) + "." + nameof(Parent.AddressViewModel.City)]);

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

        public List<Address_HistoryData> HistoryList
        {
            get { return AddressData != null && AddressData.AddressNr != 0 ? DataModel.Address_History.Where(h => h.AddressNr == AddressData.AddressNr).ToList() : new List<Address_HistoryData>(); }
        }

        public Dictionary<String, String> ItemList
        {
            get
            {
                Dictionary<String, String> dataDictionary = new Dictionary<string, string>();
                if (SearchTerm != null)
                    dataDictionary = DataModel.Addresses.Where(a => (a.AddressKey + a.AddressNr).Contains(SearchTerm) ||
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
                    dataDictionary = DataModel.Addresses.ToDictionary(a => a.AddressKey + a.AddressNr,
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
                AddressData = DataModel.Addresses.First(a => a.AddressKey + a.AddressNr == value.Key);
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
            get { return _errorList.Count > 0 ? _errorList.FirstOrDefault(e => e.Key.StartsWith(AddressNr.ToString().Substring(2))).Value : ""; }
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
