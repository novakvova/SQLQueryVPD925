IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblCustomers]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblCustomers
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    Name nvarchar(255),
    [Address] nvarchar(255),
    City nvarchar(255),
    Country nvarchar(255),
)'
