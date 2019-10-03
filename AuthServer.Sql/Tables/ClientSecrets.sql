CREATE TABLE [dbo].[ClientSecrets]
(
	[ClientId] VARCHAR(20) NOT NULL , 
    [Secret] VARCHAR(64) NOT NULL, 
    PRIMARY KEY ([ClientId], [Secret]), 
    CONSTRAINT [FK_ClientSecrets_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients]([ClientId])
)
