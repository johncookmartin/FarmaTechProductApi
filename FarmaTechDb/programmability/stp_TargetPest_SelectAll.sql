CREATE PROCEDURE [dbo].[stp_TargetPest_SelectAll]
	@ProductGroupId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT tp.[Id], 
		   tp.[TargetPest]
	FROM [dbo].[TargetPests] tp
	LEFT JOIN [dbo].[ProductGroupTargetPests] pgtp
	ON tp.Id = pgtp.TargetPestId
	WHERE (pgtp.ProductGroupId = @ProductGroupId OR @ProductGroupId IS NULL)
	      AND tp.DeletedAt IS NULL

	RETURN 0;
END
