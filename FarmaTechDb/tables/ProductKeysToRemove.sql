CREATE TABLE [dbo].[ProductKeysToRemove]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ProductId] INT NOT NULL,
	[FormulationKeyId] INT NOT NULL,
	[DeletedAt] DATETIME2 NULL,
	Constraint [FK_ProductKeysToRemove_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
	Constraint [FK_ProductKeysToRemove_FormulationKeys] FOREIGN KEY ([FormulationKeyId]) REFERENCES [dbo].[FormulationKeys]([Id]),
)
