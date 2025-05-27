CREATE TABLE [dbo].[ProductPhotos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ProductId] INT NOT NULL,
	[PhotoId] INT NOT NULL,
	[DeletedAt] DATETIME2 NULL,
	Constraint [FK_ProductPhotos_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
	Constraint [FK_ProductPhotos_Photos] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[ProductFiles]([Id]),
)
