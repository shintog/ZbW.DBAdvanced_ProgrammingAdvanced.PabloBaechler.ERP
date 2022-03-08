using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.EF6_Data_Access
{
    public partial class AuftragsverwaltungDataAccess : DbContext
    {
        public AuftragsverwaltungDataAccess()
            : base("name=AuftragsverwaltungDataAccess")
        {
        }

        public virtual DbSet<Accounting> Accountings { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleClassification> ArticleClassifications { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Address_History> Address_History { get; set; }
        public virtual DbSet<Article_History> Article_History { get; set; }
        public virtual DbSet<ArticleClassification_History> ArticleClassification_History { get; set; }
        public virtual DbSet<Customer_History> Customer_History { get; set; }
        public virtual DbSet<Order_History> Order_History { get; set; }
        public virtual DbSet<Position_History> Position_History { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounting>()
                .Property(e => e.ZIP)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Address>()
                .Property(e => e.ZIP)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Address1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.PurchasingPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Article>()
                .Property(e => e.SalesPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Positions)
                .WithRequired(e => e.Article1)
                .HasForeignKey(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArticleClassification>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.ArticleClassification)
                .HasForeignKey(e => e.Classification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArticleClassification>()
                .HasMany(e => e.ArticleClassification1)
                .WithOptional(e => e.ArticleClassification2)
                .HasForeignKey(e => e.Parent);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.SPCurrency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Articles1)
                .WithRequired(e => e.Currency1)
                .HasForeignKey(e => e.PPCurrency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Positions)
                .WithRequired(e => e.Order1)
                .HasForeignKey(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Address_History>()
                .Property(e => e.ZIP)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Article_History>()
                .Property(e => e.PurchasingPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Article_History>()
                .Property(e => e.SalesPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_History>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Position_History>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);
            
        }
    }
}
