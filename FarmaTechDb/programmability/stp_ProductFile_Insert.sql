CREATE PROCEDURE [dbo].[stp_ProductFile_Insert]
	@FileType VARCHAR(50),
	@BlobPath VARCHAR(255),
	@FileUrl VARCHAR(255),
	@Description VARCHAR(255),
	@User VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ProductFileId INT;

	IF EXISTS(SELECT 1 FROM [dbo].[ProductFiles] WHERE [BlobPath] = @BlobPath AND [FileUrl] = @FileUrl AND [DeletedAt] IS NULL)
		BEGIN
			SELECT TOP 1 @ProductFileId = [Id]
			FROM [dbo].[ProductFiles]
			WHERE [BlobPath] = @BlobPath
				AND [FileUrl] = @FileUrl
				AND [DeletedAt] IS NULL;
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[ProductFiles]
			(
				[FileType],
				[BlobPath],
				[FileUrl],
				[Description],
				[CreatedAt],
				[CreatedBy]
			)
			VALUES
			(
				@FileType,
				@BlobPath,
				@FileUrl,
				@Description,
				CURRENT_TIMESTAMP,
				@User
			);

			SET @ProductFileId = SCOPE_IDENTITY();
		END

	SELECT @ProductFileId AS ProductFileId;

	RETURN 0;
END
