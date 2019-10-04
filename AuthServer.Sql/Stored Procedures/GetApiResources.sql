CREATE PROCEDURE [dbo].[GetApiResources]
AS
BEGIN
	SELECT 
		[Name],
		[DisplayName]
	FROM 
		[dbo].[ApiResources]
END
GO