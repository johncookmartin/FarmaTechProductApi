CREATE PROCEDURE [dbo].[stp_ProductFile_Update]
	@Id INT,
	@FileType VARCHAR(50),
	@BlobPath VARCHAR(255),
	@FileUrl VARCHAR(255),
	@Description VARCHAR(255) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[ProductFiles]
	SET 
		[FileType] = @FileType,
		[BlobPath] = @BlobPath,
		[FileUrl] = @FileUrl,
		[Description] = @Description,
		[UpdatedAt] = GETDATE()
	WHERE 
		[Id] = @Id;

	SELECT @@ROWCOUNT AS [RowsAffected];

	RETURN 0;
END
