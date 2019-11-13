BEGIN
	DECLARE @UserName NVARCHAR(20) = 'IIS APPPOOL\Auth',
			@Sql NVARCHAR(200)

	IF NOT EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = @UserName)
	BEGIN
		BEGIN TRY  
			SET @Sql = N'CREATE LOGIN [' + @UserName + '] FROM WINDOWS WITH DEFAULT_DATABASE = [Auth], DEFAULT_LANGUAGE = [us_english]'
			EXECUTE sp_executesql @Sql
		END TRY  
		BEGIN CATCH  
			-- OK - if this fails we're in dev environment  
		END CATCH
	END

	IF EXISTS (SELECT LoginName FROM SYSLOGINS WHERE NAME = @UserName)
	BEGIN

		IF NOT EXISTS (SELECT [Name] FROM SYSUSERS WHERE [Name] = @UserName)
		BEGIN
			SET @Sql = N'CREATE USER [' + @UserName + '] FOR LOGIN [' + @UserName + '] WITH DEFAULT_SCHEMA = dbo'
			EXECUTE sp_executesql @Sql
		END

		SET @Sql = N'GRANT CONNECT TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql

		SET @Sql = N'GRANT EXECUTE ON [dbo].[AddUserExternalProvider] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindApiResourceByName] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindApiResourcesByScope] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindClientById] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindProviderById] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindUserByEmail] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindUserByExternalProvider] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[FindUserByUsername] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql
		SET @Sql = N'GRANT EXECUTE ON [dbo].[GetApiResources] TO [' + @UserName + ']'
		EXECUTE sp_executesql @Sql

	END
END
GO
