using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class GetPurchase_test : AppForSEII25264SqliteUT     // PRUEBAS DE QUE ESTE METODO NOS PERMITE OBTENER LAS COMPRAS REALIZADAS (DETAILS) 
    {

        public GetPurchase_test()
        {
            var models = new List<Model>()
            {
                new Model {Id=1,Name="Toyota R"},
                new Model {Id=2,Name="Toyota A"},
                new Model {Id=3,Name="Toyota V"}
            };
            var cars = new List<Car>()
            {
                new Car {carClass = "coche",Color="rojo",Description="un coche rojo",Manufacturer="Toyota",
                    QuantityForPurchasing= 1, PurchasingPrice=2000,Model= models[0],FuelType="Gasoline"},
                new Car {carClass = "coche",Color="amarillo",Description="un coche amarillo",Manufacturer="Toyota",
                    QuantityForPurchasing= 1, PurchasingPrice=3000,Model= models[1],FuelType="Gasoline"},
                new Car {carClass = "coche",Color="verde",Description="un coche verde",Manufacturer="Toyota",
                    QuantityForPurchasing= 1, PurchasingPrice=1500,Model= models[2],FuelType="Gasoline"}
            };

            ApplicationUser user = new ApplicationUser("1", "Pepe", "Viyuela", "pepeV@uclm.es", "Calle MiCasa Nº7");

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var numDays = (endDate - startDate).Days;

            var purchase = new Purchase("Tony", PaymentMethod.TarjetaDeCredito, startDate, 500000, 1, new List<PurchaseItem>(), user);

            var purchaseItem = new PurchaseItem(1, 1, 10, cars[0], purchase);

            purchase.purchaseItems.Add(purchaseItem);

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(purchase);
            _context.Add(purchaseItem);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            // Act
            var result = await controller.GetPurchase(-1);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;
            var controller = new PurchasesController(_context, logger);

            var startDate = DateTime.Today.AddDays(1);

            var startdateUnspecified = DateTime.SpecifyKind(startDate, DateTimeKind.Unspecified);

            var expectedPurchase = new PurchaseForDetailsDTO(1, "Pepe", "Viyuela", "Calle MiCasa Nº7",PaymentMethod.TarjetaDeCredito, startdateUnspecified, "pepeV@uclm.es" ,new List<PurchaseItemDTO>());
            expectedPurchase.PurchaseItemDTO.Add(new PurchaseItemDTO(1, 1, 10, "Toyota R", "Gasoline", "Toyota", 2000, "Rojo", "un coche rojo"));

            // Act 
            var result = await controller.GetPurchase(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<PurchaseForDetailsDTO>(okResult.Value);
            var eq = expectedPurchase.Equals(purchaseDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedPurchase, purchaseDTOActual);

        }




    }
}
