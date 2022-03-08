/****** Object:  View [dbo].[CTE_ArticleHierarchy]    Script Date: 06.03.2022 19:52:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Rechnungswesen] WITH SCHEMABINDING AS
With CTE_ArticlePriceSumPerOrder (
	balance_due, OrderNr)
AS (
	SELECT	sum([Article].[SalesPrice]) as balance_due,
			[OrderNr]
	FROM [dbo].[Order]
	JOIN [dbo].[Position] ON ([Order].[OrderNr] = [Position].[PositionNr])
	JOIN [dbo].[Article] FOR SYSTEM_TIME AS OF [ORDER].[DATE] ON ([Position].[Article] = [Article].[ArticleNr])
	GROUP BY [OrderNr]
)
SELECT	[Customer].[CustomerNr] As CustomerNr,
        [Customer].[Name] As Name,
        Concat([Address].[Street], ', ', [Address].[Number]) As Street,
        [Address].[ZIP] As ZIP,
        [Address].[City] As City,
		'CH' As Country,
        [Order].[Date] As Date,
        [Order].[OrderNr] As OrderNr,
		[CTE_ArticlePriceSumPerOrder].[balance_due] As BalanceDueNet,
		[CTE_ArticlePriceSumPerOrder].[balance_due] * 1.077 As BalanceDueGross
FROM [dbo].[Order]
JOIN [dbo].[Position] ON ([Order].[OrderNr] = [Position].[PositionNr])
JOIN [dbo].[Customer] ON ([Order].[CustomerNr] = [Customer].[CustomerNr])
JOIN  [dbo].[Address] FOR SYSTEM_TIME AS OF [ORDER].[DATE] ON ([Address].[AddressNr] = [Customer].[AddressNr])
JOIN CTE_ArticlePriceSumPerOrder cte ON (cte.[OrderNr] = [Order].[OrderNr]);
GO


