CREATE TABLE [dbo].[TargetPests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[TargetPest] VARCHAR(255) NOT NULL,
	[DeletedAt] DATETIME2 NULL, 
	[CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [UpdatedAt] DATETIME2 NULL, 
    [CreatedBy] VARCHAR(100) NULL, 
    [DeletedBy] VARCHAR(100) NULL, 
    [UpdatedBy] VARCHAR(100) NULL, 
    CONSTRAINT [UQ_TargetPests_TargetPest_DeletedAt] UNIQUE ([TargetPest], [DeletedAt])
)
