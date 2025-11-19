SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'Citroen')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'Seat')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'Ford')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (4, N'Ferrari')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (5, N'Mercedes')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (6, N'Toyota')
SET IDENTITY_INSERT [dbo].[Models] OFF


SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId], [FuelType], [EngDisplacement], [RimSize]) VALUES (4, N'Mondeo', N'Rojo', N'5 puertas', N'Ford Company', N'Segunda Mano', 2, 1, 5600, 800, 3, N'Gasolina', N'', N'')
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId], [FuelType], [EngDisplacement], [RimSize]) VALUES (5, N'F80', N'Azul', N'2 puertas', N'Ferrari Company', N'Alta gamma', 1, 0, 500000, 0, 4, N'Gasolina', N'', N'')
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId], [FuelType], [EngDisplacement], [RimSize]) VALUES (6, N'GLC300', N'Gris', N'4 puertas', N'Mercedes-Benz', N'Primera Mano', 3, 2, 23800, 1000, 5, N'Electrico', N'', N'')
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId], [FuelType], [EngDisplacement], [RimSize]) VALUES (7, N'C-HR', N'Negro', N'4 puertas', N'Toyota', N'Financiacion', 5, 2, 30750, 2000, 6, N'Electrico', N'', N'')
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId], [FuelType], [EngDisplacement], [RimSize]) VALUES (8, N'C3', N'Azul-Marino', N'4 puertas', N'Citroen', N'Primera Mano', 8, 4, 15940, 1500, 1, N'Gasoleo', N'', N'')
SET IDENTITY_INSERT [dbo].[Cars] OFF