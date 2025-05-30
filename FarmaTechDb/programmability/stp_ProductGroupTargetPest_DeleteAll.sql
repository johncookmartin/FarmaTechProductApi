CREATE PROCEDURE [dbo].[stp_ProductGroupTargetPest_DeleteAll]
	@ProductGroupId INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM [dbo].[ProductGroupTargetPests]
	WHERE [ProductGroupId] = @ProductGroupId;
	RETURN 0;
END
