CREATE TABLE [dbo].[ApiResources]
(
	[Name] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [DisplayName] VARCHAR(20) NOT NULL, 
    [Enabled] BIT NOT NULL, 
    [Description] VARCHAR(100) NULL
)
