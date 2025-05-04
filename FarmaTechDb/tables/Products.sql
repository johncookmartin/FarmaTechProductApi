CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR(255) NOT NULL,
	[Definition] VARCHAR(MAX) NOT NULL,
	[SdsId] INT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[DeletedAt] DATETIME NULL,
	[UpdatedAt] DATETIME NULL,
	Constraint [FK_Products_SDS] FOREIGN KEY ([SdsId]) REFERENCES [dbo].[ProductFiles]([Id]),
)
