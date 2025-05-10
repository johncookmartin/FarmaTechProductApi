CREATE TABLE [dbo].[ProductGroupProducts]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductGroupId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[DeletedAt] DATETIME2 NULL,
	Constraint [FK_ProductGroupProducts_ProductGroups] FOREIGN KEY ([ProductGroupId]) REFERENCES [dbo].[ProductGroups]([Id]),
	Constraint [FK_ProductGroupProducts_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
)
