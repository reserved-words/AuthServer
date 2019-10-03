CREATE TABLE [dbo].[ClientProperties]
(
	[ClientId] VARCHAR(20) NOT NULL , 
    [Key] VARCHAR(20) NOT NULL, 
    [Value] VARCHAR(1000) NULL, 
    PRIMARY KEY ([Key], [ClientId]), 
    CONSTRAINT [FK_ClientProperties_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients]([ClientId])
)
