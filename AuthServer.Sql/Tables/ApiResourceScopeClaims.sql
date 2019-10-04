CREATE TABLE [dbo].[ApiResourceScopeClaims]
(
	[ResourceName] VARCHAR(20) NOT NULL, 
    [ScopeName] VARCHAR(50) NOT NULL,
	[Claim] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ApiResourceScopeClaims] PRIMARY KEY ([Claim], [ScopeName], [ResourceName]), 
    CONSTRAINT [FK_ApiResourceScopeClaims_ApiResourceScopes] FOREIGN KEY ([ResourceName], [ScopeName]) REFERENCES [dbo].[ApiResourceScopes]([ResourceName], [ScopeName])
)
