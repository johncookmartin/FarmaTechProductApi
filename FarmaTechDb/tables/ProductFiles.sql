CREATE TABLE [dbo].[ProductFiles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[FileType] VARCHAR(50) NOT NULL,
	[BlobPath] VARCHAR(255) NOT NULL,
	[FileUrl] VARCHAR(255) NOT NULL,
	[Description] VARCHAR(255) NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[DeletedAt] DATETIME NULL,
	[UpdatedAt] DATETIME NULL,
	[CreatedBy] VARCHAR(100) NULL, 
    [DeletedBy] VARCHAR(100) NULL, 
    [UpdatedBy] VARCHAR(100) NULL, 
    CONSTRAINT [UQ_ProductFiles_BlobPath_FileUrl_DeletedAt] UNIQUE ([BlobPath], [FileUrl], [DeletedAt])
)
