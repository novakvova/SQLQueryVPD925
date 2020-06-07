IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblProducts]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblProducts
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    Name nvarchar(40),
    SupplierId int foreign key references tblSuppliers(Id),
    CategoryId int foreign key references tblCategories(Id),
    Unit nvarchar(40),
    Price money,
)'