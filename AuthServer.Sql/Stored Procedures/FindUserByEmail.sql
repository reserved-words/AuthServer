CREATE PROCEDURE [dbo].[FindUserByEmail]
	@Email VARCHAR(100)
AS
BEGIN

	SELECT 
		[SubjectId],
		[Username],
		[Password]
	FROM
		[dbo].[Users]
	WHERE
		[Email] = @Email

	SELECT 
		[UserClaims].[Type],
		[UserClaims].[Value]
	FROM
		[dbo].[Users]
		INNER JOIN [dbo].[UserClaims] ON [UserClaims].[SubjectId] = [Users].[SubjectId]
	WHERE
		[Email] = @Email

END
