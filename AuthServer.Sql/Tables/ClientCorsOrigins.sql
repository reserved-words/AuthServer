CREATE TABLE [dbo].[ClientCorsOrigins]
(
	[ClientId] VARCHAR(20) NOT NULL , 
    [CorsOrigin] VARCHAR(255) NOT NULL, 
    PRIMARY KEY ([CorsOrigin], [ClientId]), 
    CONSTRAINT [FK_ClientCorsOrigins_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients]([ClientId])
)
