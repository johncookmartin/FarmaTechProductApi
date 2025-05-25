CREATE PROCEDURE [dbo].[stp_TargetPest_SelectAll]
	@ProductGroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT tp.[Id], 
		   tp.[TargetPest]
	FROM [dbo].[TargetPests] tp
	INNER JOIN [dbo].[ProductGroupTargetPests] pgtp
	ON tp.Id = pgtp.TargetPestId
	WHERE pgtp.ProductGroupId = @ProductGroupId
	      AND tp.DeletedAt IS NULL

	RETURN 0;
END
