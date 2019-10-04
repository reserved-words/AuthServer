CREATE TABLE [dbo].[ApiResourceProperties]
(
	[ResourceName] VARCHAR(20) NOT NULL , 
    [Key] VARCHAR(20) NOT NULL, 
    [Value] VARCHAR(1000) NULL, 
    PRIMARY KEY ([Key], [ResourceName]), 
    CONSTRAINT [FK_ResourceProperties_Resources] FOREIGN KEY ([ResourceName]) REFERENCES [dbo].[ApiResources]([Name])
)
