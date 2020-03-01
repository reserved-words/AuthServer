CREATE PROCEDURE [dbo].[AddResource]
	@Name NVARCHAR(20),
	@DisplayName NVARCHAR(20),
	@Secret NVARCHAR(64)
AS
BEGIN

	INSERT INTO [dbo].[ApiResources]
           ([Name]
           ,[DisplayName]
           ,[Enabled]
           ,[Description])
     VALUES
           (@Name
           ,@DisplayName
           ,1
           ,NULL)

	INSERT INTO [dbo].[ApiResourceSecrets]
			([ResourceName]
			,[Secret])
		 VALUES
			(@Name
			,@Secret)

	INSERT INTO [dbo].[ApiResourceScopes]
           ([ResourceName]
           ,[ScopeName]
           ,[DisplayName]
           ,[Description]
           ,[Required]
           ,[Emphasize]
           ,[ShowInDiscoveryDocument])
     VALUES
           (@Name
           ,@Name
           ,CONCAT(@DisplayName, ' Scope')
           ,NULL
           ,1
           ,0
           ,1)

END
GO