MERGE INTO dbo.Equipment AS TARGET
USING(values
	(1, 'Iron Helmet', 200, 'Head', 2, 0, -1),
	(2, 'Gold Helmet', 500, 'Head', 5, 0, 3),
	(3, 'Diamond Helmet', 1000, 'Head', 10, 0, -5),
	(4, 'Iron Shoulders', 200, 'Shoulders', 2, 0, 0),
	(5, 'Gold Shoulders', 500, 'Shoulders', 5, 0, 0),
	(6, 'Diamond Shoulders', 1000, 'Shoulders', 10, 2, 0),
	(7, 'Iron Chest', 500, 'Chest', 2, 0, -2),
	(8, 'Gold Chest', 800, 'Chest', 5, 0, -5),
	(9, 'Diamond Chest', 1000, 'Chest', 10, 0, -10),
	(10, 'Gucci Belt', 300, 'Belt', 0, 5, 5),
	(11, 'Gold Belt', 200, 'Belt', 2, 2, 0),
	(12, 'Leather Belt', 200, 'Belt', 1, 4, 4),
	(13, 'Iron Legs', 500, 'Legs', 2, 0, 0),
	(14, 'Gold Legs', 800, 'Legs', 5, 0, 0),
	(15, 'Diamond Legs', 1000, 'Legs', 8, 0, 0),
	(16, 'Flip flops', 100, 'Boots', 0, 10, 0),
	(17, 'Gucci Boots', 200, 'Boots', 0, 5, 5),
	(18, 'Iron Boots', 100, 'Boots', 4, 0, -1)
) AS SOURCE (Id, Name, Price, Category, Strength, Intel, Agility)
ON TARGET.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Name, Price, Category, Strength, Intel, Agility)
	VALUES (Id, Name, Price, Category, Strength, Intel, Agility)
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name,
		Price = Source.Price,
		Category = Source.Category,
		Strength = Source.Strength,
		Intel = Source.Intel,
		Agility = Source.Agility;