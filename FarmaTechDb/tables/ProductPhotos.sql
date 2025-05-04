CREATE TABLE [dbo].[ProductPhotos]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductId] INT NOT NULL,
	[PhotoId] INT NOT NULL,
	Constraint [FK_ProductPhotos_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
	Constraint [FK_ProductPhotos_Photos] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[ProductFiles]([Id]),
)
