CREATE TABLE [dbo].[Providers]
(
	[Id] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ClientId] VARCHAR(100) NOT NULL, 
    [ClientSecret] VARCHAR(100) NOT NULL
)
