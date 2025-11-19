SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'Citroen')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'Seat')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'Ford')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (4, N'Ferrari')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (5, N'Mercedes')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (6, N'Toyota')
SET IDENTITY_INSERT [dbo].[Models] OFF



SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1001, N'Mondeo', N'Rojo', N'5 puertas', N'Ford Company', N'Segunda mano', 2, 1, 5600, 800, 3)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1002, N'F80', N'Azul', N'2 puertas', N'Ferrari Company', N'Alta gamma', 1, 0, 500000, 0, 4)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1008, N'GLC300', N'Gris', N'4 puertas', N'Mercedes-Benz', N'Primera mano', 3, 2, 23800, 1000, 5)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1009, N'C-HR', N'Negro', N'4 puertas', N'Toyota', N'Financiacion', 5, 2, 30750, 2000, 6)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1010, N'C3', N'Azul-Marino', N'4 puertas', N'Citroen', N'Primera mano', 8, 4, 15940, 1500, 1)
SET IDENTITY_INSERT [dbo].[Cars] OFF

Set identity_insert [dbo].[Rentals] ON
insert into [dbo].[Rentals] ([Id],[EndDate],[StartDate],[RentingDate],[TotalPrice],[DeliveryCarDealer],[PaymentMethod],[ApplicationUserId]) Values (101,'20-11-2025','25-11-2025','19-11-2025',0,N'Tony',1,1)
insert into [dbo].[Rentals] ([Id],[EndDate],[StartDate],[RentingDate],[TotalPrice],[DeliveryCarDealer],[PaymentMethod],[ApplicationUserId]) Values (102,'20-11-2025','25-11-2025','19-11-2025',0,N'Tony',1,2)
insert into [dbo].[Rentals] ([Id],[EndDate],[StartDate],[RentingDate],[TotalPrice],[DeliveryCarDealer],[PaymentMethod],[ApplicationUserId]) Values (103,'20-11-2025','25-11-2025','19-11-2025',0,N'Tony',1,3)
Set identity_insert [dbo].[Rentals] OFF

Set identity_insert [dbo].[RentalItems] ON
insert into [dbo].[RentalItems] ([CarId],[RentalId],[Quantity],[UserId]) Values (1001,101,1,1)
insert into [dbo].[RentalItems] ([Id],[EndDate],[StartDate],[RentingDate],[TotalPrice],[DeliveryCarDealer],[PaymentMethod],[ApplicationUserId]) Values (102,'20-11-2025','25-11-2025','19-11-2025',0,N'Tony',1,2)
insert into [dbo].[RentalItems] ([Id],[EndDate],[StartDate],[RentingDate],[TotalPrice],[DeliveryCarDealer],[PaymentMethod],[ApplicationUserId]) Values (103,'20-11-2025','25-11-2025','19-11-2025',0,N'Tony',1,3)
Set identity_insert [dbo].[RentalItems] OFF