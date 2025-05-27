CREATE TABLE [dbo].[Formulations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ProductId] INT NOT NULL,
	[ParentId] INT NULL,
	[FormulationKeyId] INT NOT NULL,
	[ValueType] VARCHAR(50) NOT NULL,
	[StringValue] VARCHAR(255) NULL,
	[SortOrder] INT NOT NULL,
	[DeletedAt] DATETIME2 NULL,
	Constraint [FK_Formulations_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]),
	Constraint [FK_Formulations_Parent] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Formulations]([Id]),
	Constraint [FK_Formulations_FormulationKeys] FOREIGN KEY ([FormulationKeyId]) REFERENCES [dbo].[FormulationKeys]([Id]),
)
