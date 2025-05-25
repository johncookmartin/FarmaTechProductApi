CREATE PROCEDURE [dbo].[stp_ProductGroup_Insert]
	@Group VARCHAR(255),
	@SpecialInstructions VARCHAR(MAX),
	@PhotoId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ProductGroupId INT;

	IF EXISTS(SELECT 1 FROM [dbo].[ProductGroups] WHERE [Group] = @Group AND [DeletedAt] IS NULL)
		BEGIN
			SET @ProductGroupId = -1;
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[ProductGroups]  
			(
				[Group],
				[PhotoId],
				[SpecialInstructions],
				[CreatedAt]
			)
			VALUES
			(
				@Group,
				@PhotoId,
				@SpecialInstructions,
				CURRENT_TIMESTAMP
			);

			SET @ProductGroupId = SCOPE_IDENTITY();
		END

	SELECT @ProductGroupId AS ProductGroupId;	

	RETURN 0;
END
