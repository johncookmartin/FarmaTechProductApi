CREATE TABLE [dbo].[FormulationKeys]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Key] VARCHAR(50) NOT NULL, 
    [DeletedAt] DATETIME2 NULL,
)
