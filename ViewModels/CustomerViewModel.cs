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
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public CustomerViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.CustomerData = new Customer();
            this.CurrentSearchMask = new SearchPage();
            this.Error = Visibility.Hidden;
        }

        public MainViewModel Parent;

        public AuftragsverwaltungDataAccess DataAccess;
        
        private Customer _customerData;
        private Customer CustomerData
        {
            get { return _customerData; }
            set
            {
                _customerData = value;
                NotifyPropertyChanged(nameof(CustomerNr));
                NotifyPropertyChanged(nameof(Name));
                NotifyPropertyChanged(nameof(AddressSelectedItem));
                NotifyPropertyChanged(nameof(EMail));
                NotifyPropertyChanged(nameof(Website));
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

        public string CustomerNr
        {
            get { return CustomerData.CustomerKey + CustomerData.CustomerNr; }
            set
            {
                CustomerData.CustomerKey = value.Substring(0, 2);
                CustomerData.CustomerNr = int.Parse(value.Substring(2));
                NotifyPropertyChanged(nameof(CustomerNr));
            }
        }
        
        public String Name
        {
            get { return CustomerData.Name; }
            set
            {
                CustomerData.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        public Dictionary<String, String> Address
        {
            get { return DataAccess.Addresses.ToDictionary(a => a.AddressKey + a.AddressNr, a => a.Street + " " + a.Number + ", " + a.ZIP + " " + a.City); }
            set { NotifyPropertyChanged(nameof(Address)); }
        }
        
        public KeyValuePair<String, String> AddressSelectedItem
        {
            get
            {
                KeyValuePair<String, String> ItemKeyValue = new KeyValuePair<String, String>();

                if (CustomerData != null && CustomerData.Address1 != null)
                    ItemKeyValue = new KeyValuePair<string, string>(CustomerData.Address1.AddressKey + CustomerData.Address1.AddressNr, CustomerData.Address1.Street + " " + CustomerData.Address1.Number + ", " + CustomerData.Address1.City + " " + CustomerData.Address1.ZIP);

                return ItemKeyValue;
            }
            set
            {

                CustomerData.Address1 = value.Key == null ? new Address() : DataAccess.Addresses.First(a => a.AddressKey + a.AddressNr == value.Key);
                CustomerData.AddressNr = value.Key == null ? 0 : int.Parse(value.Key.Substring(2));
                CustomerData.Address = "CU";
                AddressValue = value.Value;
                NotifyPropertyChanged(nameof(AddressSelectedItem));
            }
        }

        private String _addressValue;
        public String AddressValue
        {
            get { return _addressValue; }
            set
            {
                _addressValue = value;
                NotifyPropertyChanged(nameof(AddressValue));
            }
        }
        
        public String EMail
        {
            get { return CustomerData.EMail; }
            set
            {
                CustomerData.EMail = value;
                NotifyPropertyChanged(nameof(EMail));
            }
        }
        
        public String Website
        {
            get { return CustomerData.Website; }
            set
            {
                CustomerData.Website = value;
                NotifyPropertyChanged(nameof(Website));
            }
        }

        public Dictionary<String, String> ItemList
        {
            get
            {
                Dictionary<String, String> dataDictionary;
                if (SearchTerm != null)
                    dataDictionary = DataAccess.Customers.Where(c => (c.CustomerKey + c.CustomerNr).Contains(SearchTerm) ||
                                                                     c.Name.Contains(SearchTerm) ||
                                                                     c.EMail.Contains(SearchTerm) ||
                                                                     c.Website.Contains(SearchTerm))
                        .ToDictionary(c => c.CustomerKey + c.CustomerNr, c => c.Name);
                else
                    dataDictionary = DataAccess.Customers.ToDictionary(c => c.CustomerKey + c.CustomerNr, c => c.Name);
                
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
                CustomerData = DataAccess.Customers.First(c => c.CustomerKey + c.CustomerNr == value.Key);
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
                    if (CustomerNr == "CU0")
                    {
                        int newKey = DataAccess.Customers.Select(c => c.CustomerNr).DefaultIfEmpty(0).Max();
                        CustomerData.CustomerNr = newKey == 0 ? 1000000 : newKey + 1;
                    }

                    DataAccess.Customers.AddOrUpdate(CustomerData);
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
                if (value && DataAccess.Customers.Select(c => c.CustomerNr).Contains(CustomerData.CustomerNr))
                {
                    DataAccess.Customers.Remove(CustomerData);
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
                    CustomerData = new Customer();
                    CustomerData.CustomerKey = "CU";
                    AddressSelectedItem = new KeyValuePair<string, string>();
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
