CREATE PROCEDURE [dbo].[FindProviderById]
	@Id VARCHAR(20)
AS
BEGIN
	SELECT 
		[Id],
		[ClientId],
		[ClientSecret]
	FROM 
		[dbo].[Providers]
	WHERE 
		[Id] = @Id;
END
GO