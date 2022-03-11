USE auftragsverwaltung;  
GO  
CREATE PROCEDURE CreateYOYReport
AS   
DECLARE @MyCursor CURSOR;
DECLARE @DateStart DATE;
DECLARE @DateEnd DATE;
BEGIN
    SET @MyCursor = CURSOR FOR
		With 
		CTE_QuarterOfAYear ([QuartersAgo], [Date]) AS (
			SELECT 1 as QuartersAgo, GETDATE() as [Date]
			UNION ALL 
			SELECT QuartersAgo + 1, 
				DATEADD(MONTH, -3, CTE_QuarterOfAYear.[Date])
			FROM CTE_QuarterOfAYear
			WHERE QuartersAgo <= 12
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

