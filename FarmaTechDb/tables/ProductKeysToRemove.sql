CREATE TABLE [dbo].[ProductKeysToRemove]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductId] INT NOT NULL,
	[FormulationKeyId] INT NOT NULL,
	Constraint [FK_ProductKeysToRemove_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
	Constraint [FK_ProductKeysToRemove_FormulationKeys] FOREIGN KEY ([FormulationKeyId]) REFERENCES [dbo].[FormulationKeys]([Id]),
)
