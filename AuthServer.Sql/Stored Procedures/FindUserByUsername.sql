CREATE PROCEDURE [dbo].[FindUserByUsername]
	@Username VARCHAR(15)
AS
BEGIN

	SELECT 
		[SubjectId],
		[Username],
		[Password]
	FROM
		[dbo].[Users]
	WHERE
		[Username] = @Username

	SELECT 
		[UserClaims].[Type],
		[UserClaims].[Value]
	FROM
		[dbo].[Users]
		INNER JOIN [dbo].[UserClaims] ON [UserClaims].[SubjectId] = [Users].[SubjectId]
	WHERE
		[Username] = @Username

END
