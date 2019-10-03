CREATE PROCEDURE [dbo].[FindClientById]
	@ClientId varchar(20)
AS
BEGIN
	
	SELECT 
		[ClientId], 
		[ClientName], 
		[Enabled], 
		[RedirectUri], 
		[AllowOfflineAccess], 
		[PostLogoutRedirectUri], 
		[EnableLocalLogin], 
		[RequireConsent], 
		[AllowRememberConsent], 
		[ClientUri], 
		[LogoUri]
	FROM
		[dbo].[Clients]
	WHERE 
		[ClientId] = @ClientId

	SELECT [Key], [Value]
	FROM [dbo].[ClientProperties]
	WHERE [ClientId] = @ClientId

	SELECT [Scope]
	FROM [dbo].[ClientScopes]
	WHERE [ClientId] = @ClientId

	SELECT [Secret]
	FROM [dbo].[ClientSecrets]
	WHERE [ClientId] = @ClientId

	SELECT [GrantType]
	FROM [dbo].[ClientGrantTypes]
	WHERE [ClientId] = @ClientId

END
GO