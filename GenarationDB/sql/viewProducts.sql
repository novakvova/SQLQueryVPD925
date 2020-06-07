IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[viewProducts]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[viewProducts]
AS
SELECT        NEWID() AS Id, dbo.tblProducts.Id AS ProductId, dbo.tblProducts.Name, dbo.tblProducts.CategoryId, 
                dbo.tblProducts.Unit, dbo.tblProducts.Price, dbo.tblCategories.Name AS CategoryName, 
                         dbo.tblSuppliers.Name AS SupplierName, dbo.tblSuppliers.Address AS SupplierAddress
FROM            dbo.tblCategories INNER JOIN
                         dbo.tblProducts ON dbo.tblCategories.Id = dbo.tblProducts.CategoryId INNER JOIN
                         dbo.tblSuppliers ON dbo.tblProducts.SupplierId = dbo.tblSuppliers.Id'
