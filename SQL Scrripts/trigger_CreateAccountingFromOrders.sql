CREATE TRIGGER CreateAccountingFromOrders
ON [Order]
FOR INSERT, UPDATE
AS
BEGIN
	EXEC CreateAccountingRecords;
END;
GO
CREATE TRIGGER CreateAccountingFromPositions
ON [Position]
FOR INSERT, UPDATE
AS
BEGIN
	EXEC CreateAccountingRecords;
END;
GO
