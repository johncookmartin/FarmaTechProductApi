CREATE TABLE [dbo].[TargetPests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[TargetPest] VARCHAR(255) NOT NULL,
	[DeletedAt] DATETIME2 NULL, 
	CONSTRAINT [UQ_TargetPests_TargetPest_DeletedAt] UNIQUE ([TargetPest], [DeletedAt])
)
