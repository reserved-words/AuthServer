CREATE TABLE [dbo].[ClientGrantTypes]
(
	[ClientId] VARCHAR(20) NOT NULL , 
    [GrantType] VARCHAR(50) NOT NULL, 
    PRIMARY KEY ([GrantType], [ClientId]), 
    CONSTRAINT [FK_ClientGrantTypes_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients]([ClientId])
)
