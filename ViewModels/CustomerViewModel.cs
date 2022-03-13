using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.Utilities.Internal;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Views.Pages;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public CustomerViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.CustomerData = new CustomerData();
            this.CurrentSearchMask = new SearchPage();
            this.ErrorList = new Dictionary<string, string>();
        }

        public MainViewModel Parent;

        public AuftragsverwaltungModel DataModel;
        
        private CustomerData _customerData;
        private CustomerData CustomerData
        {
            get { return _customerData; }
            set
            {
                _customerData = new CustomerData();
                NotifyPropertyChanged(nameof(Address));
                _customerData = value;
                NotifyPropertyChanged(nameof(CustomerNr));
                NotifyPropertyChanged(nameof(Name));
                NotifyPropertyChanged(nameof(AddressSelectedItem));
                NotifyPropertyChanged(nameof(EMail));
                NotifyPropertyChanged(nameof(Website));
                NotifyPropertyChanged(nameof(HistoryList));
                NotifyPropertyChanged(nameof(AddressValue));
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

                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Name) );
                if (value == "")
                {
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Name) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.Name)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        public Dictionary<String, String> Address
        {
            get
            {
                Dictionary<String, String> ListDictionary = new Dictionary<String, String>();
                ListDictionary.Add("", null);
                ListDictionary.AddRange(DataModel.Addresses.ToDictionary(a => a.AddressKey + a.AddressNr, a => a.Street + " " + a.Number + ", " + a.City + " " + a.ZIP));
                return ListDictionary;
            }
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
                CustomerData.Address1 = value.Key == null || value.Key == "" ? new AddressData() : DataModel.Addresses.First(a => a.AddressKey + a.AddressNr == value.Key);
                CustomerData.AddressNr = value.Key == null || value.Key == "" ? 0 : int.Parse(value.Key.Substring(2));
                CustomerData.Address = "CU";
                AddressValue = value.Value;
                
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem) );
                if (value.Key == null || value.Key == "")
                {
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(AddressSelectedItem));
                NotifyPropertyChanged(nameof(AddressValue));
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
                
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.EMail) );
                if (value == "")
                {
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.EMail) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.EMail)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(EMail));
            }
        }
        
        public String Website
        {
            get { return CustomerData.Website; }
            set
            {
                CustomerData.Website = value;
                
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Website) );
                if (value == "")
                {
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Website) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.Website)]);
                }
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(Website));
            }
        }
        public List<Customer_HistoryData> HistoryList
        {
            get { return CustomerData != null && CustomerData.CustomerNr != 0 ? DataModel.Customer_History.Where(h => h.CustomerNr == CustomerData.CustomerNr).ToList() : new List<Customer_HistoryData>(); }
        }

        public Dictionary<String, String> ItemList
        {
            get
            {
                Dictionary<String, String> dataDictionary = new Dictionary<String, String>();
                if (SearchTerm != null)
                    dataDictionary = DataModel.Customers.Where(c => (c.CustomerKey + c.CustomerNr).Contains(SearchTerm) ||
                                                                     c.Name.Contains(SearchTerm) ||
                                                                     c.EMail.Contains(SearchTerm) ||
                                                                     c.Website.Contains(SearchTerm))
                        .ToDictionary(c => c.CustomerKey + c.CustomerNr, c => c.Name);
                else
                    dataDictionary = DataModel.Customers.ToDictionary(c => c.CustomerKey + c.CustomerNr, c => c.Name);
                
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
                CustomerData = DataModel.Customers.First(c => c.CustomerKey + c.CustomerNr == value.Key);
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
            get { return _errorList.Count > 0 ? _errorList.FirstOrDefault(e => e.Key.StartsWith(CustomerNr.ToString())).Value : ""; }
        }

        public Visibility Error
        {
            get { return _errorList.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }
        
        public void SaveData()
        {
            if (_errorList == null || _errorList.Where(a => a.Key.StartsWith(CustomerNr.ToString())).ToList().Count == 0)
            {
                if (CustomerNr == "CU0")
                {
                    int newKey = DataModel.Customers.Select(c => c.CustomerNr).DefaultIfEmpty(0).Max();
                    CustomerData.CustomerNr = newKey == 0 ? 1000000 : newKey + 1;
                }
                
                DataModel.WriteData(CustomerData);
                Parent.DataModel = DataModel;
                NotifyPropertyChanged(nameof(ItemList));
                SetEdit = false;
            }
        }
        
        public void DeleteData()
        {
            if (DataModel.Customers.Select(c => c.CustomerNr).Contains(CustomerData.CustomerNr))
            {
                ErrorList.Remove(CustomerData.CustomerNr.ToString());
                KeyValuePair<String, String> DeleteError = DataModel.DeleteData(CustomerData);
                if (DeleteError.Key != "")
                    ErrorList.Add(DeleteError.Key, DeleteError.Value);
                else
                    SetNew = true;

                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Name) );
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem) );
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.EMail) );
                ErrorList.Remove(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Website) );
                NotifyPropertyChanged(nameof(CurrentError));
                NotifyPropertyChanged(nameof(Error));

                Parent.DataModel = DataModel;
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
                    CustomerData newCustomer = new CustomerData();
                    newCustomer.CustomerKey = "CU";
                    CustomerData = newCustomer;
                    AddressSelectedItem = new KeyValuePair<string, string>(null, null);

                    ErrorList = new Dictionary<string, string>();
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Name) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.Name)]);
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.AddressSelectedItem)]);
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.EMail) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.EMail)]);
                    ErrorList.Add(Parent.CustomerViewModel.CustomerNr + "." + nameof(Parent.CustomerViewModel.Website) , Parent.ListOfErrors[nameof(Parent.CustomerViewModel) + "." + nameof(Parent.CustomerViewModel.Website)]);
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

        public void SavePassword(String Password)
        {
            int CustomerId = int.Parse(CustomerNr.Substring(2));
            CustomerData.Password = GetMD5Hash(Password);
        }

        public byte[] GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.Unicode.GetBytes(input);

            bs = x.ComputeHash(bs);
            return bs;
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
