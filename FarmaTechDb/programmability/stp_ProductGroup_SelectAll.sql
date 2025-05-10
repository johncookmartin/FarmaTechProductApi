CREATE PROCEDURE [dbo].[stp_ProductGroup_SelectAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id],
	       [Group],
		   [PhotoId],
		   [SpecialInstructions]
	FROM [dbo].[ProductGroups]
	WHERE DeletedAt IS NULL;

	RETURN 0;
END
