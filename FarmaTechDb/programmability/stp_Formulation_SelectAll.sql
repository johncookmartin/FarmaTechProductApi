CREATE PROCEDURE [dbo].[stp_Formulation_SelectAll]
	@SearchId INT,
	@SearchProduct BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Sql NVARCHAR(MAX),
	        @Column NVARCHAR(50);

	SET @Column = CASE WHEN @SearchProduct = 1 THEN 'ProductId' ELSE 'ParentId' END;

	SET @Sql = N'
		SELECT 
			f.ProductId,
			f.ProductId,
			f.ParentId,
			fk.[Key],
			f.ValueType,
			f.StringValue,
			f.SortOrder
		FROM dbo.Formulations f
		INNER JOIN dbo.FormulationKeys fk
			ON f.FormulationKeyId = fk.Id
		WHERE f.' + QUOTENAME(@Column) + ' = @SearchId';

	EXEC sp_executesql @Sql, N'SearchId INT', @SearchId = @SearchId;

	RETURN 0
END