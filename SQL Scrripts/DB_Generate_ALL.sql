DROP DATABASE IF EXISTS [auftragsverwaltung]
GO
/****** Object:  Database [auftragsverwaltung]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Table [dbo].[dbo.Address_History]    Script Date: 07.03.2022 07:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dbo.Address_History](
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
/****** Object:  Index [ix_MSSQL_TemporalHistoryFor_1525580473]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_MSSQL_TemporalHistoryFor_1525580473] ON [dbo].[dbo.Address_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 07.03.2022 07:51:03 ******/
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
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[dbo.Address_History] )
)
GO
/****** Object:  Table [dbo].[ArticleClassification_History]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Index [ix_ArticleClassification_History]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_ArticleClassification_History] ON [dbo].[ArticleClassification_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleClassification]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  View [dbo].[CTE_ArticleHierarchy]    Script Date: 07.03.2022 07:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CTE_ArticleHierarchy] WITH SCHEMABINDING AS
With CTE_ArticleHierarchy (
	ClassificationNr, ParentProductID, ClassificationLevel )
AS (
	SELECT	ClassificationNr,
            Parent ,
            0 AS ClassificationLevel
    FROM dbo.ArticleClassification
    WHERE Parent IS NULL
    UNION ALL
    SELECT	ac.ClassificationNr ,
            ac.Parent ,
            ac1.ClassificationLevel + 1
    FROM dbo.ArticleClassification AS ac
    INNER JOIN CTE_ArticleHierarchy AS ac1
		ON ac1.ClassificationNr = ac.Parent
)
SELECT	ClassificationNr ,
        ParentProductID ,
        ClassificationLevel
FROM CTE_ArticleHierarchy;
GO
/****** Object:  Table [dbo].[Article_History]    Script Date: 07.03.2022 07:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article_History](
	[ArticleNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Classification] [int] NOT NULL,
	[PurchasingPrice] [numeric](18, 0) NOT NULL,
	[PPCurrency] [nvarchar](50) NOT NULL,
	[SalesPrice] [numeric](18, 0) NOT NULL,
	[SPCurrency] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Article_History]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_Article_History] ON [dbo].[Article_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 07.03.2022 07:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleNr] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Classification] [int] NOT NULL,
	[PurchasingPrice] [numeric](18, 0) NOT NULL,
	[PPCurrency] [nvarchar](50) NOT NULL,
	[SalesPrice] [numeric](18, 0) NOT NULL,
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
/****** Object:  Table [dbo].[Customer_History]    Script Date: 07.03.2022 07:51:03 ******/
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
	[Password] [BINARY](64) NOT NULL,
	[SysStartTime] [datetime2](7) NOT NULL,
	[SysEndTime] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ix_Customer_History]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_Customer_History] ON [dbo].[Customer_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 07.03.2022 07:51:03 ******/
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
	[Password] [BINARY](64) NOT NULL,
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
/****** Object:  Table [dbo].[Order_History]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Index [ix_Order_History]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_Order_History] ON [dbo].[Order_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Table [dbo].[Position_History]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Index [ix_Position_History]    Script Date: 07.03.2022 07:51:03 ******/
CREATE CLUSTERED INDEX [ix_Position_History] ON [dbo].[Position_History]
(
	[SysEndTime] ASC,
	[SysStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 07.03.2022 07:51:03 ******/
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
/****** Object:  Table [dbo].[Currency]    Script Date: 07.03.2022 07:51:03 ******/
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
INSERT [dbo].[Address] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City]) VALUES (N'CU', 1000000, N'Chäsiwis', N'13', CAST(9245 AS Numeric(4, 0)), N'Oberbüren')
INSERT [dbo].[Address] ([AddressKey], [AddressNr], [Street], [Number], [ZIP], [City]) VALUES (N'CU', 1000001, N'Schorenstrasse', N'15a', CAST(9000 AS Numeric(4, 0)), N'St. Gallen')
GO
INSERT [dbo].[Article] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency]) VALUES (1000000, N'Dualshock 5', N'PS5 Kontroller', 1000003, CAST(54 AS Numeric(18, 0)), N'CHF', CAST(73 AS Numeric(18, 0)), N'CHF')
INSERT [dbo].[Article] ([ArticleNr], [Name], [Designation], [Classification], [PurchasingPrice], [PPCurrency], [SalesPrice], [SPCurrency]) VALUES (1000001, N'Dualshock 4', N'PS4 Kontroller', 1000003, CAST(45 AS Numeric(18, 0)), N'CHF', CAST(64 AS Numeric(18, 0)), N'CHF')
GO
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name]) VALUES (1000000, NULL, N'Elektronik')
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name]) VALUES (1000001, 1000000, N'Unterhaltungselektronik')
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name]) VALUES (1000002, 1000001, N'Gaming')
INSERT [dbo].[ArticleClassification] ([ClassificationNr], [Parent], [Name]) VALUES (1000003, 1000002, N'Hardware')
GO
INSERT [dbo].[Currency] ([CurrencyCode], [Name]) VALUES (N'CHF', N'Schweizer Franken')
INSERT [dbo].[Currency] ([CurrencyCode], [Name]) VALUES (N'EUR', N'Euro')
GO
INSERT [dbo].[Customer] ([CustomerKey], [CustomerNr], [Name], [Address], [AddressNr], [EMail], [Website], [Password]) VALUES (N'CU', 1000000, N'Pablo Bächler', N'CU', 1000000, N'pablo@shintog.ch', N'www.shintog.ch', 0x0F037584C99E7FD4F4F8C59550F8F507)
GO
INSERT [dbo].[Order] ([OrderNr], [Date], [Customer], [CustomerNr]) VALUES (1000000, CAST(N'2022-03-06T00:00:00.000' AS DateTime), N'CU', 1000000)
GO
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount]) VALUES (1, 1000000, 1000000, CAST(4 AS Numeric(18, 0)))
INSERT [dbo].[Position] ([PositionNr], [Order], [Article], [Amount]) VALUES (3, 1000000, 1000000, CAST(3 AS Numeric(18, 0)))
GO
/****** Object:  Index [IX_Article_Classification]    Script Date: 07.03.2022 07:51:05 ******/
CREATE NONCLUSTERED INDEX [IX_Article_Classification] ON [dbo].[Article]
(
	[Classification] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Article_CurrencyPP]    Script Date: 07.03.2022 07:51:05 ******/
CREATE NONCLUSTERED INDEX [IX_Article_CurrencyPP] ON [dbo].[Article]
(
	[PPCurrency] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Article_CurrencySP]    Script Date: 07.03.2022 07:51:05 ******/
CREATE NONCLUSTERED INDEX [IX_Article_CurrencySP] ON [dbo].[Article]
(
	[SPCurrency] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ArticleClassification]    Script Date: 07.03.2022 07:51:05 ******/
CREATE NONCLUSTERED INDEX [IX_ArticleClassification] ON [dbo].[ArticleClassification]
(
	[Parent] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer_Address]    Script Date: 07.03.2022 07:51:05 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_Address] ON [dbo].[Customer]
(
	[Address] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_Customer]    Script Date: 07.03.2022 07:51:05 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Order_Customer] ON [dbo].[Order]
(
	[Customer] ASC
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
ALTER DATABASE [auftragsverwaltung] SET  READ_WRITE 
GO
