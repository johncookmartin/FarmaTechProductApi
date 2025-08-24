CREATE TABLE [dbo].[ProductGroups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Group] VARCHAR(255) NOT NULL,
	[PhotoId] INT NULL,
	[SpecialInstructions] VARCHAR(MAX) NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[DeletedAt] DATETIME NULL,
	[UpdatedAt] DATETIME NULL,
	[CreatedBy] VARCHAR(100) NULL, 
    [DeletedBy] VARCHAR(100) NULL, 
    [UpdatedBy] VARCHAR(100) NULL, 
    CONSTRAINT [FK_ProductGroups_Photos] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[ProductFiles]([Id]),
	CONSTRAINT [UQ_ProductGroups_Group_DeletedAt] UNIQUE ([Group], [DeletedAt])
)
