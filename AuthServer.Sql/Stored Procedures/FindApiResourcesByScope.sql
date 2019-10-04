CREATE PROCEDURE [dbo].[FindApiResourcesByScope]
	@Scopes VARCHAR(100)
AS
BEGIN
	
	SELECT 
		[ApiResources].[Name] [ResourceName],
		[ApiResources].[DisplayName] [ResourceDisplayName],
		[ApiResourceScopes].[ScopeName],
		[ApiResourceScopes].[DisplayName] [ScopeDisplayName], 
		[ApiResourceScopes].[Description] [ScopeDescription],
		[Required] [ScopeRequired], 
		[Emphasize] [ScopeEmphasize], 
		[ShowInDiscoveryDocument] [ScopeShowInDiscoveryDocument]
	FROM 
		[dbo].[ApiResources]
		INNER JOIN [dbo].[ApiResourceScopes] ON [ApiResources].[Name] = [ApiResourceScopes].[ResourceName]
	WHERE 
		CHARINDEX (CONCAT('|',[ApiResourceScopes].[ScopeName],'|'), @Scopes) > 0
	
END
GO