
IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = 'test-vm\auth-app')
BEGIN
	BEGIN TRY  
		CREATE LOGIN [test-vm\auth-app] FROM WINDOWS WITH DEFAULT_DATABASE = [Auth], DEFAULT_LANGUAGE = [us_english]
	END TRY  
	BEGIN CATCH  
		-- OK - if this fails we're in dev environment  
	END CATCH
END
GO

IF EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = 'test-vm\auth-app')
BEGIN

	IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = 'test-vm\auth-app')
	BEGIN
		CREATE USER [test-vm\auth-app] FOR LOGIN [test-vm\auth-app] WITH DEFAULT_SCHEMA = dbo
	END

	GRANT CONNECT TO [test-vm\auth-app]

	GRANT EXECUTE ON [dbo].[FindApiResourceByName] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[FindApiResourcesByScope] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[FindClientById] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[FindProviderById] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[FindUserByExternalProvider] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[FindUserByUsername] TO [test-vm\auth-app]
	GRANT EXECUTE ON [dbo].[GetApiResources] TO [test-vm\auth-app]

END
GO
