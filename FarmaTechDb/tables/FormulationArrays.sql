CREATE TABLE [dbo].[FormulationArrays]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[FormulationId] INT NOT NULL,
	[Value] VARCHAR(255) NULL,
	[SortOrder] INT NOT NULL, 
    [DeletedAt] DATETIME2 NULL,
)
