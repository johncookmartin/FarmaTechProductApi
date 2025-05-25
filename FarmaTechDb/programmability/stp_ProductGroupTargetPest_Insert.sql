CREATE PROCEDURE [dbo].[stp_ProductGroupTargetPest_Insert]
	@ProductGroupId INT,
	@TargetPestId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ProductGroupTargetPestId INT;

	IF EXISTS(
		SELECT 1 
		FROM [dbo].[ProductGroupTargetPests] 
		WHERE [ProductGroupId] = @ProductGroupId 
		      AND [TargetPestId] = @TargetPestId 
			  AND [DeletedAt] IS NULL)
		BEGIN
			SET @ProductGroupTargetPestId = -1;
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[ProductGroupTargetPests]
			(
				[ProductGroupId],
				[TargetPestId]
			)
			VALUES
			(
				@ProductGroupId,
				@TargetPestId
			);

			SET @ProductGroupTargetPestId = SCOPE_IDENTITY();
		END

	SELECT @ProductGroupTargetPestId AS ProductGroupTargetPestId;

	RETURN 0;
END
