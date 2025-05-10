CREATE PROCEDURE [dbo].[stp_ProductFile_Select]
	@ProductFileId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id],
	       [FileType],
		   [BlobPath],
		   [FileUrl],
		   [Description]
	FROM [dbo].[ProductFiles]
	WHERE Id = @ProductFileId
	      AND DeletedAt IS NULL;

	RETURN 0;
END
