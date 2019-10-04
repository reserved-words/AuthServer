CREATE TABLE [dbo].[ApiResourceClaims]
(
	[ResourceName] VARCHAR(20) NOT NULL, 
    [Claim] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ResourceClaims] PRIMARY KEY ([ResourceName], [Claim]), 
    CONSTRAINT [FK_ResourceClaims_Resources] FOREIGN KEY ([ResourceName]) REFERENCES [dbo].[ApiResources]([Name])
)
