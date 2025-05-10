CREATE PROCEDURE [dbo].[stp_Product_SelectAll]
	@ProductGroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.[Id],
	       p.[Name],
		   p.[Definition],
		   p.[SdsId]
	FROM [dbo].[Products] AS p
	INNER JOIN [dbo].[ProductGroupProducts] AS pgp
	ON p.Id = pgp.ProductId
	WHERE pgp.ProductGroupId = @ProductGroupId
	      AND p.DeletedAt IS NULL;

	RETURN 0;
END
