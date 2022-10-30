using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access;
using ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.Interface;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model
{
    public class AuftragsverwaltungModel : IRepository
    {
        public AuftragsverwaltungModel()
        {
            DataAccess = new AuftragsverwaltungDataAccess();

            LoadData();
        }

        public AuftragsverwaltungDataAccess DataAccess;

        public Dictionary<String, String> ListOfErrors => new Dictionary<String, String>()
        {
            {
                nameof(AddressData) + nameof(DeleteData),
                "Die Adresse kann nicht gelöscht werden da Abhängigkeiten vorhanden sind"
            },
            {
                nameof(ArticleClassificationData) + nameof(DeleteData),
                "Die Klassifikation kann nicht gelöscht werden da Abhängigkeiten vorhanden sind"
            },
            {
                nameof(ArticleData) + nameof(DeleteData),
                "Der Artikel kann nicht gelöscht werden da Abhängigkeiten vorhanden sind"
            },
            {
                nameof(CustomerData) + nameof(DeleteData),
                "Der Kunde kann nicht gelöscht werden da Abhängigkeiten vorhanden sind"
            }
        };

        public void LoadData()
        {
            _accountings = new List<AccountingData>();
            _address_History = new List<Address_HistoryData>();
            _v_Classification_Hierarchy = new List<V_CTE_ArticleClassificationHierarchyData>();
            _positions = new List<PositionData>();
            _position_History = new List<Position_HistoryData>();
            _orders = new List<OrderData>();
            _customers = new List<CustomerData>();
            _articleClassifications = new List<ArticleClassificationData>();
            _articles = new List<ArticleData>();
            _currencies = new List<CurrencyData>();
            _order_History = new List<Order_HistoryData>();
            _article_History = new List<Article_HistoryData>();
            _articleClassification_History = new List<ArticleClassification_HistoryData>();
            _addresses = new List<AddressData>();
            _customer_History = new List<Customer_HistoryData>();
            _v_YearOverYearReport = new List<V_YearOverYearReportData>();

            foreach (var VARIABLE in DataAccess.Accountings)
            {
                AccountingData Record = new AccountingData();
                Record.CustomerNr = VARIABLE.CustomerNr;
                Record.City = VARIABLE.City;
                Record.Country = VARIABLE.Country;
                Record.InvoiceAmountGross = VARIABLE.InvoiceAmountGross;
                Record.InvoiceAmountNet = VARIABLE.InvoiceAmountNet;
                Record.InvoiceDate = VARIABLE.InvoiceDate;
                Record.InvoiceNr = VARIABLE.InvoiceNr;
                Record.Name = VARIABLE.Name;
                Record.Street = VARIABLE.Street;
                Record.ZIP = VARIABLE.ZIP;
                Record.Currency = VARIABLE.Currency;
                _accountings.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Address_History)
            {
                Address_HistoryData Record = new Address_HistoryData();
                Record.AddressNr = VARIABLE.AddressNr;
                Record.City = VARIABLE.City;
                Record.Number = VARIABLE.Number;
                Record.Street = VARIABLE.Street;
                Record.ZIP = VARIABLE.ZIP;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _address_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Addresses)
            {
                AddressData Record = new AddressData();
                Record.AddressKey = VARIABLE.AddressKey;
                Record.AddressNr = VARIABLE.AddressNr;
                Record.City = VARIABLE.City;
                Record.Number = VARIABLE.Number;
                Record.Street = VARIABLE.Street;
                Record.ZIP = VARIABLE.ZIP;
                _addresses.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.ArticleClassification_History)
            {
                ArticleClassification_HistoryData Record = new ArticleClassification_HistoryData();
                Record.ClassificationNr = VARIABLE.ClassificationNr;
                Record.Name = VARIABLE.Name;
                Record.Parent = VARIABLE.Parent;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _articleClassification_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Article_History)
            {
                Article_HistoryData Record = new Article_HistoryData();
                Record.ArticleNr = VARIABLE.ArticleNr;
                Record.Classification = VARIABLE.Classification;
                Record.Designation = VARIABLE.Designation;
                Record.Name = VARIABLE.Name;
                Record.PPCurrency = VARIABLE.PPCurrency;
                Record.PurchasingPrice = VARIABLE.PurchasingPrice;
                Record.SPCurrency = VARIABLE.SPCurrency;
                Record.SalesPrice = VARIABLE.SalesPrice;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _article_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Order_History)
            {
                Order_HistoryData Record = new Order_HistoryData();
                Record.CustomerNr = VARIABLE.CustomerNr;
                Record.Customer = VARIABLE.Customer;
                Record.Date = VARIABLE.Date;
                Record.OrderNr = VARIABLE.OrderNr;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _order_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Customer_History)
            {
                Customer_HistoryData Record = new Customer_HistoryData();
                Record.AddressNr = VARIABLE.AddressNr;
                Record.Address = VARIABLE.Address;
                Record.CustomerKey = VARIABLE.CustomerKey;
                Record.CustomerNr = VARIABLE.CustomerNr;
                Record.EMail = VARIABLE.EMail;
                Record.Name = VARIABLE.Name;
                Record.Website = VARIABLE.Website;
                Record.Password = VARIABLE.Password;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _customer_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Currencies)
            {
                CurrencyData Record = new CurrencyData();
                Record.Name = VARIABLE.Name;
                Record.CurrencyCode = VARIABLE.CurrencyCode;
                _currencies.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.ArticleClassifications)
            {
                ArticleClassificationData Record = new ArticleClassificationData();
                Record.Articles = Articles.Where(a => a.Classification == VARIABLE.ClassificationNr).ToList();
                Record.ClassificationNr = VARIABLE.ClassificationNr;
                Record.Name = VARIABLE.Name;
                Record.Parent = VARIABLE.Parent;
                _articleClassifications.Add(Record);
            }

            foreach (var VARIABLE in _articleClassifications)
            {
                VARIABLE.ArticleClassification2 =
                    VARIABLE.Parent == null ? new ArticleClassificationData() : ArticleClassifications.First(a => a.ClassificationNr == VARIABLE.Parent);
            }

            foreach (var VARIABLE in DataAccess.Articles)
            {
                ArticleData Record = new ArticleData();
                Record.Positions = Positions.Where(p => p.Article == VARIABLE.ArticleNr).ToList();
                Record.ArticleNr = VARIABLE.ArticleNr;
                Record.Classification = VARIABLE.Classification;
                Record.ArticleClassification = ArticleClassifications.First(a => a.ClassificationNr == VARIABLE.Classification);
                Record.CurrencyPP = Currencies.First(c => c.CurrencyCode == VARIABLE.PPCurrency);
                Record.CurrencySP = Currencies.First(c => c.CurrencyCode == VARIABLE.SPCurrency); ;
                Record.Designation = VARIABLE.Designation;
                Record.Name = VARIABLE.Name;
                Record.PPCurrency = VARIABLE.PPCurrency;
                Record.PurchasingPrice = Convert.ToDouble(VARIABLE.PurchasingPrice);
                Record.SPCurrency = VARIABLE.SPCurrency;
                Record.SalesPrice = Convert.ToDouble(VARIABLE.SalesPrice);
                _articles.Add(Record);
            }


            foreach (var VARIABLE in DataAccess.Customers)
            {
                CustomerData Record = new CustomerData();
                Record.AddressNr = VARIABLE.AddressNr;
                Record.Address = VARIABLE.Address;
                Record.Address1 = Addresses.First(a => a.AddressNr == VARIABLE.AddressNr);
                Record.CustomerKey = VARIABLE.CustomerKey;
                Record.CustomerNr = VARIABLE.CustomerNr;
                Record.EMail = VARIABLE.EMail;
                Record.Name = VARIABLE.Name;
                Record.Website = VARIABLE.Website;
                Record.Password = VARIABLE.Password;
                _customers.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Positions)
            {
                PositionData Record = new PositionData();
                Record.Amount = VARIABLE.Amount;
                Record.Article = VARIABLE.Article;
                Record.Article1 = Articles.First(a => a.ArticleNr == VARIABLE.Article);
                Record.Order = VARIABLE.Order;
                Record.PositionNr = VARIABLE.PositionNr;
                _positions.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Orders)
            {
                OrderData Record = new OrderData();
                Record.Positions = Positions.Where(p => p.Order == VARIABLE.OrderNr).ToList();
                Record.CustomerNr = VARIABLE.CustomerNr;
                Record.Customer = VARIABLE.Customer;
                Record.Customer1 = Customers.First(c => c.CustomerNr == VARIABLE.CustomerNr);
                Record.Date = VARIABLE.Date;
                Record.OrderNr = VARIABLE.OrderNr;
                _orders.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.Position_History)
            {
                Position_HistoryData Record = new Position_HistoryData();
                Record.Amount = VARIABLE.Amount;
                Record.Article = VARIABLE.Article;
                Record.Order = VARIABLE.Order;
                Record.PositionNr = VARIABLE.PositionNr;
                Record.SysStartTime = VARIABLE.SysStartTime;
                Record.SysEndTime = VARIABLE.SysEndTime;
                _position_History.Add(Record);
            }

            foreach (var VARIABLE in DataAccess.V_Classification_Hierarchy)
            {
                V_CTE_ArticleClassificationHierarchyData Record = new V_CTE_ArticleClassificationHierarchyData();
                Record.ClassificationLevel = VARIABLE.ClassificationLevel;
                Record.ClassificationNr = VARIABLE.ClassificationNr;
                Record.Name = VARIABLE.Name;
                Record.ParentProductID = VARIABLE.ParentProductID;
                Record.ReportsTo = VARIABLE.ParentProductID == null ? new V_CTE_ArticleClassificationHierarchyData() : V_Classification_Hierarchy.First(a => a.ClassificationNr == VARIABLE.ParentProductID);
                _v_Classification_Hierarchy.Add(Record);
            }

            foreach (var VARIABLE in _v_Classification_Hierarchy)
            {
                VARIABLE.Manages = V_Classification_Hierarchy.Where(a => a.ReportsTo.ClassificationNr == VARIABLE.ClassificationNr).ToList();

            }

            foreach (var VARIABLE in DataAccess.V_YoYReport)
            {
                V_YearOverYearReportData Record = new V_YearOverYearReportData();
                Record.Category = VARIABLE.Category;
                Record.YOY = VARIABLE.YOY;
                Record.Q1_Y3 = VARIABLE.Q1_Y3;
                Record.Q1_Y2 = VARIABLE.Q1_Y2;
                Record.Q1_Y1 = VARIABLE.Q1_Y1;
                Record.Q1_Y0 = VARIABLE.Q1_Y0;
                Record.Q2_Y3 = VARIABLE.Q2_Y3;
                Record.Q2_Y2 = VARIABLE.Q2_Y2;
                Record.Q2_Y1 = VARIABLE.Q2_Y1;
                Record.Q2_Y0 = VARIABLE.Q2_Y0;
                Record.Q3_Y3 = VARIABLE.Q3_Y3;
                Record.Q3_Y2 = VARIABLE.Q3_Y2;
                Record.Q3_Y1 = VARIABLE.Q3_Y1;
                Record.Q3_Y0 = VARIABLE.Q3_Y0;
                Record.Q4_Y3 = VARIABLE.Q4_Y3;
                Record.Q4_Y2 = VARIABLE.Q4_Y2;
                Record.Q4_Y1 = VARIABLE.Q4_Y1;
                Record.Q4_Y0 = VARIABLE.Q4_Y0;

                _v_YearOverYearReport.Add(Record);
            }
        }
        public void WriteData(Object dataObject)
        {
            if (dataObject.GetType() == typeof(AddressData))
            {
                AddressData Data = (AddressData)dataObject;
                Address Record = new Address();
                Record.AddressNr = Data.AddressNr;
                Record.AddressKey = Data.AddressKey;
                Record.City = Data.City;
                Record.Number = Data.Number;
                Record.Street = Data.Street;
                Record.ZIP = Data.ZIP;
                DataAccess.Addresses.AddOrUpdate(Record);
            }

            if (dataObject.GetType() == typeof(ArticleClassificationData))
            {
                ArticleClassificationData Data = (ArticleClassificationData)dataObject;
                ArticleClassification Record = new ArticleClassification();
                Record.Articles = DataAccess.Articles.Where(a => a.Classification == Data.ClassificationNr).ToList();
                Record.ClassificationNr = Data.ClassificationNr;
                Record.Name = Data.Name;
                Record.Parent = Data.Parent;
                DataAccess.ArticleClassifications.AddOrUpdate(Record);
            }

            if (dataObject.GetType() == typeof(ArticleData))
            {
                ArticleData Data = (ArticleData)dataObject;
                Article Record = new Article();
                Record.Positions = DataAccess.Positions.Where(p => p.Article == Data.ArticleNr).ToList();
                Record.ArticleNr = Data.ArticleNr;
                Record.Classification = Data.Classification;
                Record.Currency = DataAccess.Currencies.First(c => c.CurrencyCode == Data.PPCurrency);
                Record.Currency1 = DataAccess.Currencies.First(c => c.CurrencyCode == Data.SPCurrency); ;
                Record.Designation = Data.Designation;
                Record.Name = Data.Name;
                Record.PPCurrency = Data.PPCurrency;
                Record.PurchasingPrice = Convert.ToDecimal(Data.PurchasingPrice);
                Record.SPCurrency = Data.SPCurrency;
                Record.SalesPrice = Convert.ToDecimal(Data.SalesPrice);
                DataAccess.Articles.AddOrUpdate(Record);
            }

            if (dataObject.GetType() == typeof(CustomerData))
            {
                CustomerData Data = (CustomerData)dataObject;
                Customer Record = new Customer();
                Record.AddressNr = Data.AddressNr;
                Record.Address = Data.Address;
                Record.Address1 = DataAccess.Addresses.First(a => a.AddressNr == Data.AddressNr);
                Record.CustomerKey = Data.CustomerKey;
                Record.CustomerNr = Data.CustomerNr;
                Record.EMail = Data.EMail;
                Record.Name = Data.Name;
                Record.Website = Data.Website;
                Record.Password = Data.Password;
                DataAccess.Customers.AddOrUpdate(Record);
            }

            if (dataObject.GetType() == typeof(OrderData))
            {
                OrderData Data = (OrderData)dataObject;
                Order Record = new Order();
                Record.Positions = DataAccess.Positions.Where(p => p.Order == Data.OrderNr).ToList();
                Record.CustomerNr = Data.CustomerNr;
                Record.Customer = Data.Customer;
                Record.Customer1 = DataAccess.Customers.First(c => c.CustomerNr == Data.CustomerNr);
                Record.Date = Data.Date;
                Record.OrderNr = Data.OrderNr;
                DataAccess.Orders.AddOrUpdate(Record);

                foreach (var VARIABLE in Record.Positions)
                {
                    if (!Data.Positions.Select(p => p.PositionNr).Contains(VARIABLE.PositionNr))
                        DataAccess.Positions.Remove(VARIABLE);
                }

                foreach (var VARIABLE in Data.Positions)
                {
                    Position PositionRecord = new Position();
                    PositionRecord.Amount = VARIABLE.Amount;
                    PositionRecord.Article = VARIABLE.Article;
                    PositionRecord.Order = Record.OrderNr;
                    PositionRecord.PositionNr = VARIABLE.PositionNr;
                    DataAccess.Positions.AddOrUpdate(PositionRecord);
                }
            }

            DataAccess.SaveChanges();
            LoadData();
        }

        public KeyValuePair<String, String> DeleteData(Object dataObject)
        {
            KeyValuePair<String, String> errorMsg = new KeyValuePair<string, string>("", "");

            if (dataObject.GetType() == typeof(AddressData))
            {
                AddressData Data = (AddressData)dataObject;
                if (!HasAddressDependencies(Data.AddressNr))
                    DataAccess.Addresses.Remove(DataAccess.Addresses.First(o => o.AddressNr == Data.AddressNr));
                else
                    errorMsg = new KeyValuePair<String, String>(Data.AddressNr.ToString(), ListOfErrors[nameof(AddressData) + nameof(DeleteData)]);
            }

            if (dataObject.GetType() == typeof(ArticleClassificationData))
            {
                ArticleClassificationData Data = (ArticleClassificationData)dataObject;
                if (!HasClassificationDependencies(Data.ClassificationNr))
                    DataAccess.ArticleClassifications.Remove(DataAccess.ArticleClassifications.First(o => o.ClassificationNr == Data.ClassificationNr));
                else
                    errorMsg = new KeyValuePair<String, String>(Data.ClassificationNr.ToString(), ListOfErrors[nameof(ArticleClassificationData) + nameof(DeleteData)]);
            }

            if (dataObject.GetType() == typeof(ArticleData))
            {
                ArticleData Data = (ArticleData)dataObject;
                if (!HasArticleDependencies(Data.ArticleNr))
                    DataAccess.Articles.Remove(DataAccess.Articles.First(o => o.ArticleNr == Data.ArticleNr));
                else
                    errorMsg = new KeyValuePair<String, String>(Data.ArticleNr.ToString(), ListOfErrors[nameof(ArticleData) + nameof(DeleteData)]);
            }

            if (dataObject.GetType() == typeof(CustomerData))
            {
                CustomerData Data = (CustomerData)dataObject;
                if (!HasCustomerDependencies(Data.CustomerNr))
                    DataAccess.Customers.Remove(DataAccess.Customers.First(o => o.CustomerNr == Data.CustomerNr));
                else
                    errorMsg = new KeyValuePair<String, String>(Data.CustomerNr.ToString(), ListOfErrors[nameof(CustomerData) + nameof(DeleteData)]);
            }

            if (dataObject.GetType() == typeof(OrderData))
            {
                OrderData Data = (OrderData)dataObject;
                DataAccess.Positions.RemoveRange(DataAccess.Positions.Where(p => p.Order == Data.OrderNr).ToList());
                DataAccess.Orders.Remove(DataAccess.Orders.First(o => o.OrderNr == Data.OrderNr));
            }

            if (errorMsg.Key == "")
            {
                DataAccess.SaveChanges();
                LoadData();
            }

            return errorMsg;
        }

        private bool HasAddressDependencies(int AddressNr)
        {
            return DataAccess.Customers.Where(c => c.AddressNr == AddressNr).ToList().Count > 0;
        }

        private bool HasClassificationDependencies(int ClassificationNr)
        {
            return DataAccess.Articles.Where(a => a.Classification == ClassificationNr).ToList().Count > 0 ||
                   DataAccess.ArticleClassifications.Where(a => a.Parent == ClassificationNr).ToList().Count > 0;
        }

        private bool HasArticleDependencies(int ArticleNr)
        {
            return DataAccess.Positions.Where(p => p.Article == ArticleNr).ToList().Count > 0;
        }

        private bool HasCustomerDependencies(int CustomerNr)
        {
            return DataAccess.Orders.Where(o => o.CustomerNr == CustomerNr).ToList().Count > 0;
        }


        private List<AccountingData> _accountings;

        public List<AccountingData> Accountings
        {
            get
            {

                return _accountings;
            }
        }

        private List<Address_HistoryData> _address_History;
        public List<Address_HistoryData> Address_History
        {
            get
            {

                return _address_History;
            }
        }

        private List<AddressData> _addresses;
        public List<AddressData> Addresses
        {
            get
            {
                return _addresses;
            }
        }

        private List<ArticleClassification_HistoryData> _articleClassification_History;
        public List<ArticleClassification_HistoryData> ArticleClassification_History
        {
            get
            {

                return _articleClassification_History;
            }
        }

        private List<ArticleClassificationData> _articleClassifications;
        public List<ArticleClassificationData> ArticleClassifications
        {
            get
            {

                return _articleClassifications;
            }
        }

        private List<Article_HistoryData> _article_History;
        public List<Article_HistoryData> Article_History
        {
            get
            {

                return _article_History;
            }
        }

        private List<ArticleData> _articles;
        public List<ArticleData> Articles
        {
            get
            {

                return _articles;
            }
        }

        private List<CurrencyData> _currencies;
        public List<CurrencyData> Currencies
        {
            get
            {
                return _currencies;
            }
        }

        private List<Customer_HistoryData> _customer_History;
        public List<Customer_HistoryData> Customer_History
        {
            get
            {
                return _customer_History;
            }
        }

        private List<CustomerData> _customers;
        public List<CustomerData> Customers
        {
            get
            {

                return _customers;
            }
        }

        private List<CustomerData> _customers_History;
        public List<CustomerData> Customers_History
        {
            get
            {

                return _customers_History;
            }
        }

        private List<Order_HistoryData> _order_History;
        public List<Order_HistoryData> Order_History
        {
            get
            {

                return _order_History;
            }
        }

        private List<OrderData> _orders;
        public List<OrderData> Orders
        {
            get
            {

                return _orders;
            }
        }

        private List<Position_HistoryData> _position_History;
        public List<Position_HistoryData> Position_History
        {
            get
            {
                return _position_History;
            }
        }

        private List<PositionData> _positions;
        public List<PositionData> Positions
        {
            get
            {

                return _positions;
            }
        }

        private List<V_CTE_ArticleClassificationHierarchyData> _v_Classification_Hierarchy;
        public List<V_CTE_ArticleClassificationHierarchyData> V_Classification_Hierarchy
        {
            get
            {

                return _v_Classification_Hierarchy;
            }
        }

        private List<V_YearOverYearReportData> _v_YearOverYearReport;
        public List<V_YearOverYearReportData> V_YearOverYearReport
        {
            get
            {

                return _v_YearOverYearReport;
            }
        }

    }
}
