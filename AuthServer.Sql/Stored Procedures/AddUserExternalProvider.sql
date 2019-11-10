CREATE PROCEDURE [dbo].[AddUserExternalProvider]
	@SubjectId VARCHAR(5),
	@ProviderId VARCHAR(20),
	@ProviderUserId VARCHAR(100)
AS
BEGIN

	INSERT INTO [dbo].[UserExternalProviders] (
		[SubjectId],
		[ProviderId],
		[ProviderUserId]
	)
	VALUES (
		@SubjectId,
		@ProviderId,
		@ProviderUserId
	)

END
