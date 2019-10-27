
IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = 'auth-app')
BEGIN
	BEGIN TRY  
		CREATE LOGIN [auth-app] FROM WINDOWS WITH DEFAULT_DATABASE = [Auth], DEFAULT_LANGUAGE = [us_english]
	END TRY  
	BEGIN CATCH  
		-- OK - if this fails we're in dev environment  
	END CATCH
END
GO

IF EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = 'auth-app')
BEGIN

	IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = 'auth-app')
	BEGIN
		CREATE USER [auth-app] FOR LOGIN [auth-app] WITH DEFAULT_SCHEMA = dbo
	END

	GRANT CONNECT TO [auth-app]

	GRANT EXECUTE ON [dbo].[FindApiResourceByName] TO [auth-app]
	GRANT EXECUTE ON [dbo].[FindApiResourcesByScope] TO [auth-app]
	GRANT EXECUTE ON [dbo].[FindClientById] TO [auth-app]
	GRANT EXECUTE ON [dbo].[FindProviderById] TO [auth-app]
	GRANT EXECUTE ON [dbo].[FindUserByExternalProvider] TO [auth-app]
	GRANT EXECUTE ON [dbo].[FindUserByUsername] TO [auth-app]
	GRANT EXECUTE ON [dbo].[GetApiResources] TO [auth-app]

END
GO
