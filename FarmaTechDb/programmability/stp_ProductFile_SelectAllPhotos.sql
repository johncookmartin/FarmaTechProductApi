CREATE PROCEDURE [dbo].[stp_ProductFile_SelectAllPhotos]
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT pf.[Id],
	       pf.[FileType],
		   pf.[BlobPath],
		   pf.[FileUrl],
		   pf.[Description]
	FROM [dbo].[ProductFiles] pf
	INNER JOIN [dbo].[ProductPhotos] pp
	ON pf.Id = pp.PhotoId
	WHERE pp.ProductId = @ProductId
	      AND pf.DeletedAt IS NULL;

	RETURN 0;
END
