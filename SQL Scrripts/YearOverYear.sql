With CTE_YearoverYear (
	Category, [Value] )
AS (
	SELECT 'Anzahl Aufträge' as Category, 
			COUNT([Order].[OrderNr]) as [Value]
	FROM [Order]
	UNION ALL
	SELECT 'Anzahl verwaltete Artikel' as Category, 
			COUNT([Article].[ArticleNr]) as [Value]
	FROM [Article]
	UNION ALL
	SELECT  *
	FROM (SELECT 'Durchschnittliche Anzahl Artikel pro Auftrag' as Category, AVG([Position].[Amount]) as [Value] FROM [Order] JOIN [Position] ON ([Position].[Order] = [Order].[OrderNr]) GROUP BY [Order].[OrderNr]) t
	UNION ALL
	SELECT  *
	FROM (SELECT 'Umsatz pro Kunde (CHF)' as Category, sum([Article].[SalesPrice]) as [Value] FROM [Order] join [Position] on ([Position].[Order] = [Order].[OrderNr]) join [Article] on ([Article].[ArticleNr] = [Position].[Article]) GROUP BY [Order].[CustomerNr]) t
	UNION ALL
	SELECT  *
	FROM (SELECT 'Gesamtumsatz' as Category,  sum([Article].[SalesPrice]) as [Value] FROM [Order] join [Position] on ([Position].[Order] = [Order].[OrderNr]) join [Article] on ([Article].[ArticleNr] = [Position].[Article])) t
)
SELECT * FROM   
(
	SELECT 
        'Q1 2019' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2019-01-01 00:00:00' AND '2019-03-31 23:59:59'
	UNION ALL
	SELECT 
        'Q2 2019' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2019-04-01 00:00:00' AND '2019-06-30 23:59:59'
	UNION ALL
	SELECT 
        'Q3 2019' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2019-07-01 00:00:00' AND '2019-09-30 23:59:59'
	UNION ALL
	SELECT 
        'Q4 2019' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2019-10-01 00:00:00' AND '2019-12-31 23:59:59'
	UNION ALL
	SELECT 
        'Q1 2020' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2020-01-01 00:00:00' AND '2020-03-31 23:59:59'
	UNION ALL
	SELECT 
        'Q2 2020' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2020-04-01 00:00:00' AND '2020-06-30 23:59:59'
	UNION ALL
	SELECT 
        'Q3 2020' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2020-07-01 00:00:00' AND '2020-09-30 23:59:59'
	UNION ALL
	SELECT 
        'Q4 2020' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2020-10-01 00:00:00' AND '2020-12-31 23:59:59'
	UNION ALL
	SELECT 
        'Q1 2021' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2021-01-01 00:00:00' AND '2021-03-31 23:59:59'
	UNION ALL
	SELECT 
        'Q2 2021' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2021-04-01 00:00:00' AND '2021-06-30 23:59:59'
	UNION ALL
	SELECT 
        'Q3 2021' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2021-07-01 00:00:00' AND '2021-09-30 23:59:59'
	UNION ALL
	SELECT 
        'Q4 2021' AS category_name, 
        [Value],
		Category
    FROM CTE_YearoverYear FOR SYSTEM_TIME BETWEEN '2021-10-01 00:00:00' AND '2021-12-31 23:59:59'
) t 
PIVOT(
    SUM([Value]) 
    FOR category_name IN (
        [Q1 2019], 
        [Q1 2020], 
        [YOY Q1 2019 - 2020],  
        [Q1 2021], 
        [YOY Q1 2020 - 2021], 
        [Q2 2019], 
        [Q2 2020], 
        [YOY Q2 2019 - 2020],  
        [Q2 2021], 
        [YOY Q2 2020 - 2021], 
        [Q3 2019], 
        [Q3 2020], 
        [YOY Q3 2019 - 2020],  
        [Q3 2021], 
        [YOY Q3 2020 - 2021], 
        [Q4 2019], 
        [Q4 2020], 
        [YOY Q4 2019 - 2020],  
        [Q4 2021], 
        [YOY Q4 2020 - 2021])
) AS pivot_table;