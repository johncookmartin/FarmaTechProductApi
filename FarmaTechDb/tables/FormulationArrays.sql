﻿CREATE TABLE [dbo].[FormulationArrays]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[FormulationId] INT NOT NULL,
	[Value] VARCHAR(255) NULL,
	[SortOrder] INT NOT NULL, 
    [DeletedAt] DATETIME2 NULL,
)
