CREATE PROCEDURE [dbo].[FindUserByExternalProvider]
	@ProviderId VARCHAR(20),
	@ProviderUserId VARCHAR(100)
AS
BEGIN

	SELECT 
		[Users].[SubjectId],
		[Users].[Username],
		[Users].[Password]
	FROM
		[dbo].[Users]
		INNER JOIN [dbo].[UserExternalProviders] ON [UserExternalProviders].[SubjectId] = [Users].[SubjectId] 
	WHERE
		[ProviderId] = @ProviderId
		AND [ProviderUserId] = @ProviderUserId

	SELECT 
		[UserClaims].[Type],
		[UserClaims].[Value]
	FROM
		[dbo].[Users]
		INNER JOIN [dbo].[UserExternalProviders] ON [UserExternalProviders].[SubjectId] = [Users].[SubjectId]
		INNER JOIN [dbo].[UserClaims] ON [UserClaims].[SubjectId] = [Users].[SubjectId]
	WHERE
		[ProviderId] = @ProviderId
		AND [ProviderUserId] = @ProviderUserId

END
