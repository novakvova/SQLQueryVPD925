IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblOrdersDetails]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblOrdersDetails
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    OrderId int foreign key references tblOrders(Id),
    ProductId int foreign key references tblProducts(Id),
    Price money,
    Quantity int,
)'
