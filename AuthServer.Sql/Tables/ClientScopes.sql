CREATE TABLE [dbo].[ClientScopes]
(
	[ClientId] VARCHAR(20) NOT NULL , 
    [Scope] VARCHAR(20) NOT NULL, 
    PRIMARY KEY ([Scope], [ClientId]), 
    CONSTRAINT [FK_ClientScopes_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients]([ClientId])
)
