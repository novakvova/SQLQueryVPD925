IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblShippers]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblShippers
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    Name nvarchar(40),
    Phone nvarchar(12),
)'