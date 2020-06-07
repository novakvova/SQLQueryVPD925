IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblEmployees]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblEmployees
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    LastName nvarchar(40),
    FirstName nvarchar(40),
    BirthDate date,
)'
