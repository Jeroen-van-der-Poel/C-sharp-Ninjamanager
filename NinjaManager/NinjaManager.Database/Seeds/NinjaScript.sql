MERGE INTO dbo.Ninja AS TARGET
USING(values
	(1, 'Jackie Chan', 5000)
) AS SOURCE (Id, Name, Gold_amount)
ON TARGET.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Name, Gold_amount)
	VALUES (Id, Name, Gold_amount)
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name,
		Gold_Amount = Source.Gold_amount;