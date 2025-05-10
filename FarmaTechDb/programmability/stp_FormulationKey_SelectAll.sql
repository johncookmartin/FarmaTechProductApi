CREATE PROCEDURE [dbo].[stp_FormulationKey_SelectAll]
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT fk.[Key]
	FROM [dbo].[FormulationKeys] AS fk
	INNER JOIN [dbo].[ProductKeysToRemove] AS pk
	ON fk.Id = pk.FormulationKeyId
	WHERE pk.ProductId = @ProductId
	      AND fk.DeletedAt IS NULL;

	RETURN 0;
END
