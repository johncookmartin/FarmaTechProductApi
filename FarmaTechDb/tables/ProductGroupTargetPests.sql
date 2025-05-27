CREATE TABLE [dbo].[ProductGroupTargetPests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ProductGroupId] INT NOT NULL,
	[TargetPestId] INT NOT NULL,
	[DeletedAt] DATETIME2 NULL,
	CONSTRAINT [FK_ProductGroupTargetPests_ProductGroups] FOREIGN KEY ([ProductGroupId]) REFERENCES [dbo].[ProductGroups]([Id]),
	CONSTRAINT [FK_ProductGroupTargetPests_TargetPests] FOREIGN KEY ([TargetPestId]) REFERENCES [dbo].[TargetPests]([Id]),
	CONSTRAINT [UQ_ProductGroupTargetPests_ProductGroupId_TargetPestId_DeletedAt] UNIQUE ([ProductGroupId], [TargetPestId], [DeletedAt])

)
