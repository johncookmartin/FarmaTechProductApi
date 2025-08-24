CREATE PROCEDURE [dbo].[stp_ProductGroup_Update]
	@Id INT,
	@Group VARCHAR(255),
	@SpecialInstructions VARCHAR(MAX) = NULL,
	@PhotoId INT = NULL,
	@User VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[ProductGroups]
	SET 
		[Group] = @Group,
		[SpecialInstructions] = @SpecialInstructions,
		[PhotoId] = @PhotoId,
		[UpdatedAt] = GETDATE(),
		[UpdatedBy] = @User
	WHERE 
		[Id] = @Id;

	SELECT @@ROWCOUNT AS [RowsAffected];

	RETURN 0;
END
