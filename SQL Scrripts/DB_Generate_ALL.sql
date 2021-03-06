/****** Object:  Database [auftragsverwaltung]    Script Date: 13.03.2022 10:02:59 ******/
CREATE DATABASE [auftragsverwaltung]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [auftragsverwaltung] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [auftragsverwaltung] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET ARITHABORT OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [auftragsverwaltung] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [auftragsverwaltung] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [auftragsverwaltung] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [auftragsverwaltung] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [auftragsverwaltung] SET  MULTI_USER 
GO
ALTER DATABASE [auftragsverwaltung] SET ENCRYPTION ON
GO
ALTER DATABASE [auftragsverwaltung] SET QUERY_STORE = ON
GO
ALTER DATABASE [auftragsverwaltung] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  User [tkehl]    Script Date: 13.03.2022 10:02:59 ******/
CREATE USER [tkehl] FOR LOGIN [tkehl] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_ddladmin', @membername = N'tkehl'
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'tkehl'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'tkehl'
GO
/****** Object:  Table [dbo].[YearOverYear]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearOverYear](
	[Category] [nvarchar](50) NOT NULL,
	[Value] [numeric](18, 2) NULL,
	[Quarter] [int] NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_YearOverYear] PRIMARY KEY CLUSTERED 
(
	[Category] ASC,
	[Quarter] ASC,
	[Year] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_YearOverYearReport]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_YearOverYearReport] As
select *
from (
SELECT Category,
	   COALESCE([Value],0) as [Value],	  
	   CONCAT('Q', [Quarter], '_Y', YEAR(GETDATE()) - [Year]) as [Column_Header]
  FROM [YearOverYear]
  UNION ALL
SELECT Category,
	   SUM(COALESCE([Value],0))  OVER (PARTITION BY [Category] ORDER BY [Category] ROWS 2 PRECEDING) as [Value],	  
	   'YOY' as [Column_Header]
  FROM [YearOverYear]
  GROUP BY Category,[Value]
  ) t
PIVOT(
	SUM([Value]) 
	FOR [Column_Header] IN (
		[YOY],
		[Q1_Y3],
		[Q1_Y2],
		[Q1_Y1],
		[Q1_Y0],
		[Q2_Y3],
		[Q2_Y2],
		[Q2_Y1],
		[Q2_Y0],
		[Q3_Y3],
		[Q3_Y2],
		[Q3_Y1],
		[Q3_Y0],
		[Q4_Y3],
		[Q4_Y2],
		[Q4_Y1],
		[Q4_Y0]
	)
) AS pivot_table
GO
/****** Object:  Table [dbo].[ArticleClassification_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleClassification_History](
	[ClassificationNr] [int] NOT NULL,
	[Parent] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_ArticleClassification_History]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_ArticleClassification_History] ON [dbo].[ArticleClassification_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleClassification]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleClassification](
	[ClassificationNr] [int] NOT NULL,
	[Parent] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_ArticleClassification] PRIMARY KEY CLUSTERED 
(
	[ClassificationNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[ArticleClassification_History] )
)
GO
/****** Object:  View [dbo].[V_CTE_ArticleClassificationHierarchy]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_CTE_ArticleClassificationHierarchy] WITH SCHEMABINDING AS
With CTE_ArticleClassificationHierarchy (
	ClassificationNr, [Name], ParentProductID, ClassificationLevel )
AS (
	SELECT	ClassificationNr,
			[Name],
            Parent ,
            0 AS ClassificationLevel
    FROM dbo.ArticleClassification
    WHERE Parent IS NULL
    UNION ALL
    SELECT	ac.ClassificationNr ,
			ac.[Name],
            ac.Parent ,
            ac1.ClassificationLevel + 1
    FROM dbo.ArticleClassification AS ac
    INNER JOIN CTE_ArticleClassificationHierarchy AS ac1
		ON ac1.ClassificationNr = ac.Parent
)
SELECT	ClassificationNr ,
		[Name],
        ParentProductID ,
        ClassificationLevel
FROM CTE_ArticleClassificationHierarchy;
GO
/****** Object:  Table [dbo].[Address_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address_History](
	[AddressKey] [nvarchar](2) NOT NULL,
	[AddressNr] [int] NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[ZIP] [numeric](4, 0) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_MSSQL_TemporalHistoryFor_1525580473]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_MSSQL_TemporalHistoryFor_1525580473] ON [dbo].[Address_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressKey] [nvarchar](2) NOT NULL,
	[AddressNr] [int] NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[ZIP] [numeric](4, 0) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Address_History] )
)
GO
/****** Object:  Table [dbo].[Article_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article_History](
	[ArticleNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Classification] [int] NOT NULL,
	[PurchasingPrice] [numeric](18, 2) NOT NULL,
	[PPCurrency] [nvarchar](50) NOT NULL,
	[SalesPrice] [numeric](18, 2) NOT NULL,
	[SPCurrency] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Article_History]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_Article_History] ON [dbo].[Article_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Classification] [int] NOT NULL,
	[PurchasingPrice] [numeric](18, 2) NOT NULL,
	[PPCurrency] [nvarchar](50) NOT NULL,
	[SalesPrice] [numeric](18, 2) NOT NULL,
	[SPCurrency] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Article_History] )
)
GO
/****** Object:  Table [dbo].[Customer_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_History](
	[CustomerKey] [nvarchar](2) NOT NULL,
	[CustomerNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[AddressNr] [int] NOT NULL,
	[EMail] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Customer_History]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_Customer_History] ON [dbo].[Customer_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerKey] [nvarchar](2) NOT NULL,
	[CustomerNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[AddressNr] [int] NOT NULL,
	[EMail] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Customer_History] )
)
GO
/****** Object:  Table [dbo].[Order_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_History](
	[OrderNr] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Customer] [nvarchar](2) NOT NULL,
	[CustomerNr] [int] NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Order_History]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_Order_History] ON [dbo].[Order_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderNr] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Customer] [nvarchar](2) NOT NULL,
	[CustomerNr] [int] NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Order_History] )
)
GO
/****** Object:  Table [dbo].[Position_History]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position_History](
	[PositionNr] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Article] [int] NOT NULL,
	[Amount] [numeric](18, 0) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Position_History]    Script Date: 13.03.2022 10:02:59 ******/
CREATE CLUSTERED INDEX [ix_Position_History] ON [dbo].[Position_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionNr] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Article] [int] NOT NULL,
	[Amount] [numeric](18, 0) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionNr] ASC,
	[Order] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Position_History] )
)
GO
/****** Object:  Table [dbo].[Accounting]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounting](
	[CustomerNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[ZIP] [numeric](18, 0) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[InvoiceNr] [int] NOT NULL,
	[InvoiceAmountNet] [nvarchar](50) NOT NULL,
	[InvoiceAmountGross] [nvarchar](50) NOT NULL,
	[Currency] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accounting] PRIMARY KEY CLUSTERED 
(
	[CustomerNr] ASC,
	[InvoiceNr] ASC,
	[Currency] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 13.03.2022 10:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[CurrencyCode] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounting] ([CustomerNr], [Name], [Street], [ZIP], [City], [Country], [InvoiceDate], [InvoiceNr], [InvoiceAmountNet], [InvoiceAmountGross], [Currency]) VALUES (1000000, N'Pablo Bächler', N'Chäsiwis', CAST(9245 AS Numeric(18, 0)), N'Oberbüren', N'CH', CAST(N'2022-03-13' AS Date), 1000001, N'1188.00 CHF', N'1279.47600 CHF', N'CHF')
INSERT [dbo].[Accounting] ([CustomerNr], [Name], [Street], [ZIP], [City], [Country], [InvoiceDate], [InvoiceNr], [InvoiceAmountNet], [InvoiceAmountGross], [Currency]) VALUES (1000001, N'Stéphanie Bächler', N'Chäsiwis', CAST(9245 AS Numeric(18, 0)), N'Oberbüren', N'CH', CAST(N'2022-03-17' AS Date), 1000002, N'813.00 CHF', N'875.60100 CHF', N'CHF')
INSERT [dbo].[Accounting] ([CustomerNr], [Name], [Street], [ZIP], [City], [Country], [InvoiceDate], [InvoiceNr], [InvoiceAmountNet], [InvoiceAmountGross], [Currency]) VALUES (1000001, N'Stéphanie Bächler', N'Chäsiwis', CAST(9245 AS Numeric(18, 0)), N'Oberbüren', N'CH', CAST(N'2022-03-14' AS Date), 1000003, N'986.00 CHF', N'1061.92200 CHF', N'CHF')
GO
INSERT [dbo].[Address] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000000, N'Chäsiwis', N'13a', CAST(9245 AS Numeric(4, 0)), N'Oberbüren', CAST(N'2022-03-08T21:20:28.0551947' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Address] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000001, N'Schorenstrasse', N'15a', CAST(9000 AS Numeric(4, 0)), N'St. Gallen', CAST(N'2022-03-08T21:20:28.0708899' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Address] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'Buchental', N'4', CAST(9245 AS Numeric(4, 0)), N'Oberbüren', CAST(N'2022-03-09T15:06:00.6914097' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[Address_History] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'Buchental', N'3', CAST(9245 AS Numeric(4, 0)), N'Oberbüren', CAST(N'2022-03-09T15:05:50.9569609' AS DateTime2), CAST(N'2022-03-09T15:06:00.6914097' AS DateTime2))
INSERT [dbo].[Address_History] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000003, N't', N'1', CAST(1 AS Numeric(4, 0)), N't', CAST(N'2022-03-11T15:49:38.6334716' AS DateTime2), CAST(N'2022-03-11T15:49:45.6178978' AS DateTime2))
INSERT [dbo].[Address_History] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000003, N't2', N'1', CAST(1 AS Numeric(4, 0)), N't', CAST(N'2022-03-11T15:49:45.6178978' AS DateTime2), CAST(N'2022-03-11T15:49:48.4617130' AS DateTime2))
GO
INSERT [dbo].[Article] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000000, N'Dualshock 5', N'PS5 Kontroller', 1000003, CAST(54.00 AS Numeric(18, 2)), N'CHF', CAST(73.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-12T17:16:49.1446805' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Article] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000001, N'Dualshock 4', N'PS4 Kontroller schwarz', 1000003, CAST(46.00 AS Numeric(18, 2)), N'CHF', CAST(64.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-08T21:20:28.2583134' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Article] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'Monster Ham', N'HamHam', 1000008, CAST(0.80 AS Numeric(18, 2)), N'CHF', CAST(2.50 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-12T17:59:15.3376031' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'Monster Energy', N'Hamilton', 1000007, CAST(12.00 AS Numeric(18, 2)), N'CHF', CAST(13.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-08T21:20:28.2739588' AS DateTime2), CAST(N'2022-03-09T09:40:00.7757382' AS DateTime2))
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'Monster Energy Rehab', N'Eistee Energy Drink', 1000007, CAST(5.00 AS Numeric(18, 2)), N'CHF', CAST(7.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-09T15:08:34.1146189' AS DateTime2), CAST(N'2022-03-09T15:08:47.3178601' AS DateTime2))
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'Monster Energy Rehab', N'Eistee Energy Drink', 1000007, CAST(5.05 AS Numeric(18, 2)), N'CHF', CAST(7.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-09T15:08:47.3178601' AS DateTime2), CAST(N'2022-03-09T15:12:19.5539633' AS DateTime2))
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'Monster Energy Rehab', N'Eistee Energy Drink', 1000008, CAST(5.05 AS Numeric(18, 2)), N'CHF', CAST(7.00 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-09T15:12:19.5539633' AS DateTime2), CAST(N'2022-03-11T11:00:01.9702944' AS DateTime2))
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000002, N'test', N'test', 1000006, CAST(1.25 AS Numeric(18, 2)), N'CHF', CAST(1.45 AS Numeric(18, 2)), N'CHF', CAST(N'2022-03-11T15:50:39.4308284' AS DateTime2), CAST(N'2022-03-11T17:09:52.2681227' AS DateTime2))
INSERT [dbo].[Article_History] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency], [SysStartTime], [SysEndTime]) VALUES (1000000, N'Dualshock 5', N'PS5 Kontroller', 1000003, CAST(54.00 AS Numeric(18, 2)), N'EUR', CAST(73.00 AS Numeric(18, 2)), N'EUR', CAST(N'2022-03-08T21:20:28.2583134' AS DateTime2), CAST(N'2022-03-12T17:16:49.1446805' AS DateTime2))
GO
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000001, NULL, N'Unterhaltungselektornik', CAST(N'2022-03-09T10:07:36.9144988' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000002, 1000001, N'Gaming', CAST(N'2022-03-12T18:01:06.8384419' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000003, 1000002, N'Hardware', CAST(N'2022-03-08T21:20:28.4770659' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000004, 1000001, N'Haushalt', CAST(N'2022-03-08T21:20:28.4770659' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000005, 1000004, N'Küche', CAST(N'2022-03-08T21:20:28.4770659' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000006, 1000003, N'Konsolen', CAST(N'2022-03-08T21:20:28.4926749' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000007, NULL, N'Lebensmittel', CAST(N'2022-03-08T21:20:28.4926749' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000008, 1000007, N'Energy Drinks', CAST(N'2022-03-09T15:07:21.5202066' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000009, 1000007, N'Backwaren', CAST(N'2022-03-09T17:59:55.2801470' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000001, 1000000, N'Unterhaltungselektornik', CAST(N'2022-03-08T21:20:28.4614507' AS DateTime2), CAST(N'2022-03-09T10:07:36.9144988' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000000, NULL, N'Elektronik', CAST(N'2022-03-08T21:20:28.4614507' AS DateTime2), CAST(N'2022-03-09T10:07:36.9144988' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000010, 1000007, N'Milchprodukte', CAST(N'2022-03-11T17:10:53.4405734' AS DateTime2), CAST(N'2022-03-11T17:11:03.1750194' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000010, 1000007, N'Milchprodukt', CAST(N'2022-03-11T17:11:03.1750194' AS DateTime2), CAST(N'2022-03-11T17:11:07.1750443' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000002, 1000001, N'gaming', CAST(N'2022-03-08T21:20:28.4614507' AS DateTime2), CAST(N'2022-03-12T18:00:20.0880397' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000002, 1000001, N'Gaming', CAST(N'2022-03-12T18:00:20.0880397' AS DateTime2), CAST(N'2022-03-12T18:00:50.5882887' AS DateTime2))
INSERT [dbo].[ArticleClassification_History] ([ClassificationNr], [Parent], [Name], [SysStartTime], [SysEndTime]) VALUES (1000002, 1000003, N'Gaming', CAST(N'2022-03-12T18:00:50.5882887' AS DateTime2), CAST(N'2022-03-12T18:01:06.8384419' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyCode], [Name]) VALUES (N'CHF', N'Schweizer Franken')
INSERT [dbo].[Currency] ([CurrencyCode], [Name]) VALUES (N'EUR', N'Euro')
GO
INSERT [dbo].[Customer] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000000, N'Pablo Bächler', N'CU', 1000000, N'p@s.ch', N's.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-13T08:25:16.2313613' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Customer] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000001, N'Stéphanie Bächler', N'CU', 1000000, N's@s.ch', N's.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-13T08:25:00.5750201' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'terst', N'CU', 1000002, N't', N't', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T13:39:38.1610370' AS DateTime2), CAST(N'2022-03-11T13:52:34.6361392' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'terst', N'CU', 1000002, N'te', N't', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T13:52:34.6361392' AS DateTime2), CAST(N'2022-03-11T15:39:21.2530698' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'terst', N'CU', 1000002, N'te', N'te', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T15:39:21.2530698' AS DateTime2), CAST(N'2022-03-11T15:41:48.5357458' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'terst', N'CU', 1000002, N't', N't', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T15:46:08.5691797' AS DateTime2), CAST(N'2022-03-11T15:46:12.6004278' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'terst', N'CU', 1000002, N'te', N't', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T15:46:12.6004278' AS DateTime2), CAST(N'2022-03-11T15:46:20.0224110' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000002, N'test', N'CU', 1000002, N't', N't', 0x6A64B8BE19FB12E74FEAB8A7A858F83B000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-11T15:49:03.2737960' AS DateTime2), CAST(N'2022-03-11T15:49:12.1644982' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000000, N'Pablo Bächler', N'CU', 1000000, N'pablo@shintog.ch', N'shintog.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-08T21:20:28.5395383' AS DateTime2), CAST(N'2022-03-12T17:54:29.2573795' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000000, N'Pablo Bächler', N'CU', 1000002, N'pablo@shintog.ch', N'shintog.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-12T17:54:29.2573795' AS DateTime2), CAST(N'2022-03-13T08:24:43.7311815' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000001, N'Stéphanie Bächler', N'CU', 1000000, N's.baechler@shintog.ch', N'www.shintog.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-09T15:06:55.4890853' AS DateTime2), CAST(N'2022-03-13T08:25:00.5750201' AS DateTime2))
INSERT [dbo].[Customer_History] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password], [SysStartTime], [SysEndTime]) VALUES (N'CU', 1000000, N'Pablo Bächler', N'CU', 1000002, N'p@s.ch', N's.ch', 0x4144E195F46DE78A3623DA7364D04F11000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, CAST(N'2022-03-13T08:24:43.7311815' AS DateTime2), CAST(N'2022-03-13T08:25:16.2313613' AS DateTime2))
GO
INSERT [dbo].[Order] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000001, CAST(N'2022-03-13T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-12T08:09:50.3509017' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Order] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000002, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-12T08:09:39.3820650' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Order] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000003, CAST(N'2022-03-14T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-12T08:09:29.3976331' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Order] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000004, CAST(N'2022-03-26T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-12T18:04:15.0750764' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000000, CAST(N'2022-04-06T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-08T21:20:28.5708285' AS DateTime2), CAST(N'2022-03-08T21:52:36.1025137' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000001, CAST(N'2022-03-07T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-08T21:20:28.5708285' AS DateTime2), CAST(N'2022-03-09T18:12:02.0518271' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000002, CAST(N'2022-11-03T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-11T17:28:30.3868674' AS DateTime2), CAST(N'2022-03-11T17:28:51.2776771' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000001, CAST(N'2022-09-03T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-09T18:12:02.0518271' AS DateTime2), CAST(N'2022-03-11T17:29:00.6058730' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000003, CAST(N'2022-12-03T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-11T17:42:30.6746831' AS DateTime2), CAST(N'2022-03-12T08:09:19.1788180' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000003, CAST(N'2022-03-13T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-12T08:09:19.1788180' AS DateTime2), CAST(N'2022-03-12T08:09:29.3976331' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000002, CAST(N'2022-11-03T00:00:00.000' AS DateTime), N'CU', 1000001, CAST(N'2022-03-11T17:35:00.8901009' AS DateTime2), CAST(N'2022-03-12T08:09:39.3820650' AS DateTime2))
INSERT [dbo].[Order_History] ([OrderNr], [Date], [Customer], [CustomerNr], [SysStartTime], [SysEndTime]) VALUES (1000001, CAST(N'2022-11-03T00:00:00.000' AS DateTime), N'CU', 1000000, CAST(N'2022-03-11T17:29:00.6058730' AS DateTime2), CAST(N'2022-03-12T08:09:50.3509017' AS DateTime2))
GO
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000001, 1000001, CAST(6 AS Numeric(18, 0)), CAST(N'2022-03-09T18:12:02.0518271' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000002, 1000000, CAST(5 AS Numeric(18, 0)), CAST(N'2022-03-11T17:35:00.8901009' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000003, 1000000, CAST(5 AS Numeric(18, 0)), CAST(N'2022-03-11T17:42:30.6746831' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000004, 1000000, CAST(500 AS Numeric(18, 0)), CAST(N'2022-03-12T18:04:15.0750764' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000001, 1000000, CAST(4 AS Numeric(18, 0)), CAST(N'2022-03-11T10:59:06.2511503' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000002, 1000001, CAST(7 AS Numeric(18, 0)), CAST(N'2022-03-11T17:35:00.8901009' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000003, 1000001, CAST(4 AS Numeric(18, 0)), CAST(N'2022-03-11T17:42:30.6746831' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (3, 1000001, 1000001, CAST(8 AS Numeric(18, 0)), CAST(N'2022-03-09T17:43:36.4127059' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (3, 1000003, 1000000, CAST(5 AS Numeric(18, 0)), CAST(N'2022-03-11T20:28:33.3991827' AS DateTime2), CAST(N'9999-12-31T23:59:59.9999999' AS DateTime2))
GO
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000000, 1000000, CAST(4 AS Numeric(18, 0)), CAST(N'2022-03-08T21:20:28.6020720' AS DateTime2), CAST(N'2022-03-08T21:52:36.1025137' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (3, 1000000, 1000000, CAST(7 AS Numeric(18, 0)), CAST(N'2022-03-08T21:20:28.6176904' AS DateTime2), CAST(N'2022-03-08T21:52:36.1025137' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000001, 1000001, CAST(9 AS Numeric(18, 0)), CAST(N'2022-03-08T21:20:28.6176904' AS DateTime2), CAST(N'2022-03-09T17:43:23.1782114' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000001, 1000000, CAST(89 AS Numeric(18, 0)), CAST(N'2022-03-08T21:20:28.6176904' AS DateTime2), CAST(N'2022-03-09T17:43:23.1782114' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000001, 1000000, CAST(9 AS Numeric(18, 0)), CAST(N'2022-03-09T17:43:23.1782114' AS DateTime2), CAST(N'2022-03-09T17:43:36.4127059' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000001, 1000001, CAST(9 AS Numeric(18, 0)), CAST(N'2022-03-09T17:43:36.4127059' AS DateTime2), CAST(N'2022-03-09T18:12:02.0518271' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000001, 1000002, CAST(4 AS Numeric(18, 0)), CAST(N'2022-03-09T17:43:23.1782114' AS DateTime2), CAST(N'2022-03-11T10:59:06.2511503' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (1, 1000002, 1000000, CAST(4 AS Numeric(18, 0)), CAST(N'2022-03-11T17:28:30.3868674' AS DateTime2), CAST(N'2022-03-11T17:28:51.2776771' AS DateTime2))
INSERT [dbo].[Position_History] ([PositionNr], [Order], [Article], [Amount], [SysStartTime], [SysEndTime]) VALUES (2, 1000002, 1000001, CAST(7 AS Numeric(18, 0)), CAST(N'2022-03-11T17:28:30.3868674' AS DateTime2), CAST(N'2022-03-11T17:28:51.2776771' AS DateTime2))
GO
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 1, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 1, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 1, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 2, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 2, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 2, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 3, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 3, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 3, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 4, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 4, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl Aufträge', CAST(0.00 AS Numeric(18, 2)), 4, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 1, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 1, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 1, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 2, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 2, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 2, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 3, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 3, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 3, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 4, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 4, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Anzahl verwaltete Artikel', CAST(0.00 AS Numeric(18, 2)), 4, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 1, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 1, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 1, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 2, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 2, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 2, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 3, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 3, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 3, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 4, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 4, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Durchschnittliche Anzahl Artikel pro Auftrag', NULL, 4, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 1, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 1, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 1, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 2, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 2, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 2, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 3, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 3, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 3, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 4, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 4, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Gesamtumsatz', NULL, 4, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 1, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 1, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 1, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 2, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 2, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 2, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 3, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 3, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 3, 2021)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 4, 2019)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 4, 2020)
INSERT [dbo].[YearOverYear] ([Category], [Value], [Quarter], [Year]) VALUES (N'Umsatz pro Kunde (CHF)', NULL, 4, 2021)
GO
/****** Object:  Index [IX_Article_Classification]    Script Date: 13.03.2022 10:03:01 ******/
CREATE NONCLUSTERED INDEX [IX_Article_Classification] ON [dbo].[Article]
(
	[Classification] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Article_CurrencyPP]    Script Date: 13.03.2022 10:03:01 ******/
CREATE NONCLUSTERED INDEX [IX_Article_CurrencyPP] ON [dbo].[Article]
(
	[PPCurrency] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Article_CurrencySP]    Script Date: 13.03.2022 10:03:01 ******/
CREATE NONCLUSTERED INDEX [IX_Article_CurrencySP] ON [dbo].[Article]
(
	[SPCurrency] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ArticleClassification]    Script Date: 13.03.2022 10:03:01 ******/
CREATE NONCLUSTERED INDEX [IX_ArticleClassification] ON [dbo].[ArticleClassification]
(
	[Parent] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_Customer]    Script Date: 13.03.2022 10:03:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Order_Customer] ON [dbo].[Order]
(
	[Customer] ASC,
	[OrderNr] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ArticleClassification] FOREIGN KEY([Classification])
REFERENCES [dbo].[ArticleClassification] ([ClassificationNr])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_ArticleClassification]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ArticleSP] FOREIGN KEY([SPCurrency])
REFERENCES [dbo].[Currency] ([CurrencyCode])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_ArticleSP]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_CurrencyPP] FOREIGN KEY([PPCurrency])
REFERENCES [dbo].[Currency] ([CurrencyCode])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_CurrencyPP]
GO
ALTER TABLE [dbo].[ArticleClassification]  WITH CHECK ADD  CONSTRAINT [FK_ArticleClassification_ArticleClassification] FOREIGN KEY([Parent])
REFERENCES [dbo].[ArticleClassification] ([ClassificationNr])
GO
ALTER TABLE [dbo].[ArticleClassification] CHECK CONSTRAINT [FK_ArticleClassification_ArticleClassification]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address] FOREIGN KEY([AddressNr])
REFERENCES [dbo].[Address] ([AddressNr])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerNr])
REFERENCES [dbo].[Customer] ([CustomerNr])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Article] FOREIGN KEY([Article])
REFERENCES [dbo].[Article] ([ArticleNr])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Article]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Order] FOREIGN KEY([Order])
REFERENCES [dbo].[Order] ([OrderNr])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Order]
GO
/****** Object:  StoredProcedure [dbo].[CreateAccountingRecords]    Script Date: 13.03.2022 10:03:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateAccountingRecords]
AS   
DECLARE @MyCursor CURSOR;
DECLARE @OrderDate DATE;
BEGIN
    SET @MyCursor = CURSOR FOR
		select [Order].[Date] from dbo.[Order] GROUP BY [ORDER].[DATE] ORDER BY [ORDER].[DATE] ASC

	DELETE FROM [Accounting];

    OPEN @MyCursor 
    FETCH NEXT FROM @MyCursor 
    INTO @OrderDate

    WHILE @@FETCH_STATUS = 0
    BEGIN 
		INSERT INTO [Accounting]
			   ([CustomerNr]
			   ,[Name]
			   ,[Street]
			   ,[ZIP]
			   ,[City]
			   ,[Country]
			   ,[InvoiceDate]
			   ,[InvoiceNr]
			   ,[InvoiceAmountNet]
			   ,[InvoiceAmountGross]
			   ,[Currency])
		 (
			SELECT  [Customer].[CustomerNr],
				[Customer].[Name],
				[Address].[Street],
				[Address].[ZIP],
				[Address].[City],
				'CH',
				[Order].[Date],
				[Order].[OrderNr],
				concat(sum([Article].[SalesPrice] * [Position].[Amount]), ' ', [Article].[SPCurrency]),
				concat(sum([Article].[SalesPrice] * [Position].[Amount] * 1.077), ' ', [Article].[SPCurrency]),
				[Article].[SPCurrency]
			FROM [Order] FOR SYSTEM_TIME AS OF @OrderDate
			JOIN [Position] FOR SYSTEM_TIME AS OF @OrderDate ON ([Position].[Order] = [Order].[OrderNr])
			JOIN [Article] FOR SYSTEM_TIME AS OF @OrderDate ON ([Article].[ArticleNr] = [Position].[Article])
			JOIN [Customer] FOR SYSTEM_TIME AS OF @OrderDate ON ([Customer].[CustomerNr] = [Order].[CustomerNr])
			JOIN [Address] FOR SYSTEM_TIME AS OF @OrderDate ON ([Address].[AddressNr] = [Customer].[AddressNr])
			WHERE [Order].[Date] = @OrderDate
 			GROUP BY [Customer].[CustomerNr],
					[Customer].[Name],
					[Address].[Street],
					[Address].[ZIP],
					[Address].[City],
					[Order].[Date],
					[Order].[OrderNr],
					[Position].[Order],
					[Article].[SPCurrency]
		)

      FETCH NEXT FROM @MyCursor 
      INTO @OrderDate 
    END; 

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;

GO
/****** Object:  StoredProcedure [dbo].[CreateYOYReport]    Script Date: 13.03.2022 10:03:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateYOYReport]
AS   
DECLARE @MyCursor CURSOR;
DECLARE @DateStart DATE;
DECLARE @DateEnd DATE;
BEGIN
    SET @MyCursor = CURSOR FOR
		With 
		CTE_QuarterOfAYear ([QuartersAgo], [Date]) AS (
			SELECT 1 as QuartersAgo, DATEADD(MONTH, -3,GETDATE()) as [Date]
			UNION ALL 
			SELECT QuartersAgo + 1, 
				DATEADD(MONTH, -3, CTE_QuarterOfAYear.[Date])
			FROM CTE_QuarterOfAYear
			WHERE QuartersAgo < 12
		)
		select DATEADD(qq, DATEDIFF(qq, 0, [Date]), 0), DATEADD (dd, -1, DATEADD(qq, DATEDIFF(qq, 0, [Date]) +1, 0)) from CTE_QuarterOfAYear;

	DELETE FROM [YearOverYear];

    OPEN @MyCursor 
    FETCH NEXT FROM @MyCursor 
    INTO @DateStart,  @DateEnd 

	WHILE @@FETCH_STATUS = 0
    BEGIN 
		With 
		CTE_YearoverYear (Category, [Value], [Quarter], [YEAR]) AS (
			SELECT 'Anzahl Aufträge' as Category, 
					COUNT([Order].[OrderNr]) as [Value],
					DATEPART(QUARTER, @DateStart) as [Quarter],
					YEAR(@DateStart) as [Year]
			FROM [Order]
			WHERE [Order].[Date] BETWEEN  @DateStart AND @DateEnd
			UNION ALL
			SELECT 'Anzahl verwaltete Artikel' as Category, 
					COUNT([Article].[ArticleNr]) as [Value],
					DATEPART(QUARTER, @DateStart) as [Quarter],
					YEAR(@DateStart) as [Year]
			FROM [Article] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd
			UNION ALL
			SELECT 'Durchschnittliche Anzahl Artikel pro Auftrag' as Category,
					AVG([Position].[Amount]) as [Value],		
					DATEPART(QUARTER, @DateStart) as [Quarter],
					YEAR(@DateStart) as [Year]
			FROM [Order]
			JOIN [Position] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd ON ([Position].[Order] = [Order].[OrderNr]) 
			WHERE [Order].[Date] BETWEEN  @DateStart AND @DateEnd
			UNION ALL
			SELECT 'Umsatz pro Kunde (CHF)' as Category, 
					sum([Article].[SalesPrice] * [Position].[Amount]) as [Value],
					DATEPART(QUARTER, @DateStart) as [Quarter],
					YEAR(@DateStart) as [Year]
			FROM [Order]
			JOIN [Position] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd ON ([Position].[Order] = [Order].[OrderNr]) 
			join [Article] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd on ([Article].[ArticleNr] = [Position].[Article])		
			WHERE [Order].[Date] BETWEEN  @DateStart AND @DateEnd	
			UNION ALL
			SELECT 'Gesamtumsatz' as Category,  
					sum([Article].[SalesPrice] * [Position].[Amount]) as [Value],	
					DATEPART(QUARTER, @DateStart) as [Quarter],
					YEAR(@DateStart) as [Year]
			FROM [Order]
			JOIN [Position] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd ON ([Position].[Order] = [Order].[OrderNr]) 
			join [Article] FOR SYSTEM_TIME  BETWEEN  @DateStart AND @DateEnd on ([Article].[ArticleNr] = [Position].[Article])
			WHERE [Order].[Date] BETWEEN  @DateStart AND @DateEnd
		)
		INSERT INTO YearOverYear 
			SELECT 
				Category,
				[Value],
				[Quarter],
				[Year]
			FROM CTE_YearoverYear;

	  FETCH NEXT FROM @MyCursor
      INTO  @DateStart,  @DateEnd  
    END; 

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;

GO
ALTER DATABASE [auftragsverwaltung] SET  READ_WRITE 
GO
