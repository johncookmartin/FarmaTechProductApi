CREATE PROCEDURE [dbo].[stp_Formulation_SelectAll]
	@SearchId INT,
	@SearchProduct BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

	SELECT f.[Id] AS 'Id',
	       f.[ProductId] AS 'ProductId',
		   f.[ParentId] AS 'ParentId',
		   fk.[Key] AS 'Key',
		   f.[ValueType] AS 'ValueType',
		   f.[StringValue] AS 'StringValue',
		   f.[SortOrder] AS 'SortOrder'
	FROM dbo.Formulations f
	INNER JOIN dbo.FormulationKeys fk
	ON f.FormulationKeyId = fk.Id
	WHERE CASE WHEN @SearchProduct = 1 THEN f.ProductId ELSE f.ParentId END = @SearchId AND
		  CASE WHEN @SearchProduct = 1 THEN f.ParentId ELSE NULL END IS NULL AND
		  f.DeletedAt IS NULL;

	RETURN 0
END