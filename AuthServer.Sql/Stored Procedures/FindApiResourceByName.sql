CREATE PROCEDURE [dbo].[FindApiResourceByName]
	@Name VARCHAR(20)
AS
BEGIN
	SELECT [Name], [DisplayName]
	FROM [dbo].[ApiResources]
	WHERE [Name] = @Name

	SELECT [Claim]
	FROM [dbo].[ApiResourceClaims]
	WHERE [ResourceName] = @Name

	SELECT [Key], [Value]
	FROM [dbo].[ApiResourceProperties]
	WHERE [ResourceName] = @Name

	SELECT [ScopeName], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument]
	FROM [dbo].[ApiResourceScopes]
	WHERE [ResourceName] = @Name

	SELECT [ScopeName], [Claim]
	FROM [dbo].[ApiResourceScopeClaims]
	WHERE [ResourceName] = @Name

	SELECT [Secret]
	FROM [dbo].[ApiResourceSecrets]
	WHERE [ResourceName] = @Name

END
GO