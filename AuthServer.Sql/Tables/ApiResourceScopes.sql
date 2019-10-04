CREATE TABLE [dbo].[ApiResourceScopes]
(
	[ResourceName] VARCHAR(20) NOT NULL, 
    [ScopeName] VARCHAR(50) NOT NULL, 
    [DisplayName] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NULL, 
    [Required] BIT NOT NULL DEFAULT 1, 
    [Emphasize] BIT NOT NULL DEFAULT 0, 
    [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_ResourceScopes] PRIMARY KEY ([ResourceName], [ScopeName]), 
    CONSTRAINT [FK_ResourceScopes_Resources] FOREIGN KEY ([ResourceName]) REFERENCES [dbo].[ApiResources]([Name])
)
