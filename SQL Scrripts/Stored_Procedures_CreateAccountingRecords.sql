USE auftragsverwaltung;  
GO  
CREATE PROCEDURE CreateAccountingRecords
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
					[Article].[SPCurrency]
		)

      FETCH NEXT FROM @MyCursor 
      INTO @OrderDate 
    END; 

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;

