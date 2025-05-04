CREATE TABLE [dbo].[ProductGroupTargetPests]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductGroupId] INT NOT NULL,
	[TargetPestId] INT NOT NULL,
	Constraint [FK_ProductGroupTargetPests_ProductGroups] FOREIGN KEY ([ProductGroupId]) REFERENCES [dbo].[ProductGroups]([Id]),
	Constraint [FK_ProductGroupTargetPests_TargetPests] FOREIGN KEY ([TargetPestId]) REFERENCES [dbo].[TargetPests]([Id]),
)
