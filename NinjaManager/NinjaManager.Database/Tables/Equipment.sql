CREATE TABLE [dbo].[Equipment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(100) NOT NULL, 
    [Price] INT NOT NULL, 
    [Category] NCHAR(100) NOT NULL, 
    [Strength] INT NOT NULL, 
    [Intel] INT NOT NULL, 
    [Agility] INT NOT NULL 
)
