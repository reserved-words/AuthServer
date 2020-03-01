CREATE PROCEDURE [dbo].[AddClient]
	@ID NVARCHAR(20),
	@Name NVARCHAR(30),
	@GrantType NVARCHAR(50),
	@RequireSecret BIT,
	@RedirectUri NVARCHAR(250) = NULL,
	@AllowOfflineAccess BIT = 0,
	@PostLogoutRedirectUri NVARCHAR(250) = NULL,
	@RequireConsent BIT = 1,
	@AllowRememberConsent BIT = 1,
	@ClientUri NVARCHAR(250) = NULL,
	@LogoUri NVARCHAR(250) = NULL,
	@Secret NVARCHAR(64) = NULL
AS
BEGIN

	INSERT INTO [dbo].[Clients]
		([ClientId]
		,[ClientName]
		,[Enabled]
		,[RequireClientSecret]
		,[RedirectUri]
		,[AllowOfflineAccess]
		,[PostLogoutRedirectUri]
		,[EnableLocalLogin]
		,[RequireConsent]
		,[AllowRememberConsent]
		,[ClientUri]
		,[LogoUri])
	VALUES
		(@ID,
		@Name,
		1,
		@RequireSecret,
		@RedirectUri,
		@AllowOfflineAccess,
		@PostLogoutRedirectUri,
		1,
		@RequireConsent,
		@AllowRememberConsent,
		@ClientUri,
		@LogoUri
		)

	IF @Secret IS NOT NULL
	BEGIN
		INSERT INTO [dbo].[ClientSecrets]
			([ClientId]
			,[Secret])
		VALUES 
			(@ID,
			@Secret)
	END

	INSERT INTO [dbo].[ClientGrantTypes]
		([ClientId],
		[GrantType])
	VALUES
		(@ID,
		@GrantType)

END
