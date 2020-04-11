CREATE TABLE [dbo].[Ninja_Inventory]
(
	[EquipmentId] INT NOT NULL, 
    [NinjaId] INT NOT NULL,
	PRIMARY KEY (EquipmentId, NinjaId), 
    CONSTRAINT [FK_Ninja_Inventory_ToTable] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment]([Id]), 
    CONSTRAINT [FK_Ninja_Inventory_ToTable_1] FOREIGN KEY ([NinjaId]) REFERENCES [Ninja]([Id])
)
