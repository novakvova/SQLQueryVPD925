IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblCategories]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblCategories
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    Name nvarchar(50) UNIQUE,
    [Description] nvarchar(100),
);'




