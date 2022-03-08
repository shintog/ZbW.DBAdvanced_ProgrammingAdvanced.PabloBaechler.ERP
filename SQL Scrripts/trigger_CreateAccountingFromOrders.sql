CREATE TRIGGER CreateAccountingFromOrders
ON [Order]
FOR UPDATE
AS
BEGIN
	EXEC CreateAccountingRecords;
END;
GO
CREATE TRIGGER CreateAccountingFromPositions
ON [Position]
FOR UPDATE
AS
BEGIN
	EXEC CreateAccountingRecords;
END;
GO
