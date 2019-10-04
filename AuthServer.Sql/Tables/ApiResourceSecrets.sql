CREATE TABLE [dbo].[ApiResourceSecrets]
(
	[ResourceName] VARCHAR(20) NOT NULL, 
    [Secret] VARCHAR(64) NOT NULL, 
    CONSTRAINT [PK_ResourceSecrets] PRIMARY KEY ([ResourceName], [Secret]), 
    CONSTRAINT [FK_ResourceSecrets_Resources] FOREIGN KEY ([ResourceName]) REFERENCES [dbo].[ApiResources]([Name])
)
