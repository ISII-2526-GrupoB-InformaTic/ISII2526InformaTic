SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'Citroen')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'Seat')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'Ford')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (4, N'Ferrari')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (5, N'Mercedes')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (6, N'Toyota')
SET IDENTITY_INSERT [dbo].[Models] OFF


INSERT INTO [dbo].[AspNetUsers] ([Id],[Name], [Surname],[Email], [DeliveryAddress],[EmailConfirmed],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount]) VALUES ('1',N'Juan', N'Perez', N'JuanPerez@gmail.com', N'Calle Falsa 123',1,1,1,0,0)
INSERT INTO [dbo].[AspNetUsers] ([Id],[Name], [Surname],[Email], [DeliveryAddress],[EmailConfirmed],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount]) VALUES ('2',N'Maria', N'Lopez',N'MariaLopez@gmail.com', N'Avenida Siempre Viva 456',1,1,1,0,0)



SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1001, N'Mondeo', N'Rojo', N'5 puertas', N'Ford Company', N'Segunda mano', 2, 1, 5600, 800, 3)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1002, N'F80', N'Azul', N'2 puertas', N'Ferrari Company', N'Alta gamma', 1, 0, 500000, 0, 4)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1008, N'GLC300', N'Gris', N'4 puertas', N'Mercedes-Benz', N'Primera mano', 3, 2, 23800, 1000, 5)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1009, N'C-HR', N'Negro', N'4 puertas', N'Toyota', N'Financiacion', 5, 2, 30750, 2000, 6)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1010, N'C3', N'Azul-Marino', N'4 puertas', N'Citroen', N'Primera mano', 8, 4, 15940, 1500, 1)
SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1001, N'Mondeo', N'Rojo', N'5 puertas', N'Ford Company', N'Segunda mano', 2, 1, 5600, 800, 3)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1002, N'F80', N'Azul', N'2 puertas', N'Ferrari Company', N'Alta gamma', 1, 0, 500000, 0, 4)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1008, N'GLC300', N'Gris', N'4 puertas', N'Mercedes-Benz', N'Primera mano', 3, 2, 23800, 1000, 5)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1009, N'C-HR', N'Negro', N'4 puertas', N'Toyota', N'Financiacion', 5, 2, 30750, 2000, 6)
INSERT INTO [dbo].[Cars] ([Id], [carClass], [Color], [Description], [Manufacturer], [ReviewItems], [QuantityForPurchasing], [QuantityForRenting], [PurchasingPrice], [RentingPrice], [ModelId]) VALUES (1010, N'C3', N'Azul-Marino', N'4 puertas', N'Citroen', N'Primera mano', 8, 4, 15940, 1500, 1)
SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Maintenances] ON
INSERT INTO [dbo].[Maintenances] ([Id],[Name],[NumberOfDays],[Price]) VALUES (1,N'A-312',4,100)
INSERT INTO [dbo].[Maintenances] ([Id],[Name],[NumberOfDays],[Price]) VALUES (2,N'B-312',8,150)
INSERT INTO [dbo].[Maintenances] ([Id],[Name],[NumberOfDays],[Price]) VALUES (3,N'C-312',6,300)
SET IDENTITY_INSERT [dbo].[Maintenances] OFF

SET IDENTITY_INSERT [dbo].[MaintenanceTypes] ON
INSERT INTO [dbo].[MaintenanceTypes] ([Id],[MaintenanceId],[Type]) VALUES (1,1,N'Cambio de neumaticos')
INSERT INTO [dbo].[MaintenanceTypes] ([Id],[MaintenanceId],[Type]) VALUES (2,1,N'Cambio de aceite')
INSERT INTO [dbo].[MaintenanceTypes] ([Id],[MaintenanceId],[Type]) VALUES (3,2,N'Cambio de neumaticos')
INSERT INTO [dbo].[MaintenanceTypes] ([Id],[MaintenanceId],[Type]) VALUES (4,3,N'Cambio de aceite')
SET IDENTITY_INSERT [dbo].[MaintenanceTypes] OFF

SET IDENTITY_INSERT [dbo].[Bookings] ON
INSERT INTO [dbo].[Bookings] ([Id],[UserId],[Date],[PaymentMethod],[Price],[numberOfDays],[clientName],[clientSurname],[clientAdress]) VALUES (1,'1',CAST(N'2023-10-01T10:00:00' AS DateTime),2,200,2,N'Juan', N'Perez', N'Calle Falsa 123')
INSERT INTO [dbo].[Bookings] ([Id],[UserId],[Date],[PaymentMethod],[Price],[numberOfDays],[clientName],[clientSurname],[clientAdress]) VALUES (2,'2',CAST(N'2023-11-15T14:30:00' AS DateTime),0,300,6,N'Maria',N'Lopez', N'Avenida Siempre Viva 456')
SET IDENTITY_INSERT [dbo].[Bookings] OFF


INSERT INTO [dbo].[BookingItems] ([BookingId],[MaintenanceId],[Comment]) Values (1,1,N'Muy buen servicio')
INSERT INTO [dbo].[BookingItems] ([BookingId],[MaintenanceId],[Comment]) Values (2,2,N'Rapido y eficiente')


