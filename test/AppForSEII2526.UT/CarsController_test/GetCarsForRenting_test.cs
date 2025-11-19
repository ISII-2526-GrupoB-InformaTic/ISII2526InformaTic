using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForRenting_test : AppForSEII25264SqliteUT
    {
        public GetCarsForRenting_test() {
            //Arrange
            var models = new List<Model>()
            {
                new Model(1,"Toyota R"),
                new Model(2,"Toyota A"),
                new Model(3,"Toyota V")
            };
            var cars = new List<Car>()
            {
                new Car {carClass = "coche",Color="rojo",Description="un coche rojo",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=2000,Model= models[0],FuelType="Gasoline" },
                new Car {carClass = "coche",Color="amarillo",Description="un coche amarillo",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=3000,Model= models[1],FuelType="Gasoline" },
                new Car {carClass = "coche",Color="verde",Description="un coche verde",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=1500,Model= models[2],FuelType="Gasoline" }
            };

            ApplicationUser user = new ApplicationUser("1", "Pepe", "Viyuela", "pepeV@uclm.es", "Calle MiCasa Nº7");


            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var numDays = (endDate - startDate).Days;

            var rental = new Rental(endDate,startDate, DateTime.Now, (cars[0].RentingPrice * numDays), "Tony", new List<RentalItem>(), PaymentMethod.TarjetaDeCredito);

            var rentalItem = new RentalItem(1, 1, 1, cars[0], rental,user);

            rental.RentalItems.Add(rentalItem);

            _context.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.Add(rentalItem);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_GetCarsForRental_OK()
        {
            var models = new List<Model>()
            {
                new Model(1,"Toyota R"),
                new Model(2,"Toyota A"),
                new Model(3,"Toyota V")
            };


            var carDTOs = new List<CarForRentalDTO>() {
                new CarForRentalDTO(1,"un coche rojo","rojo","Toyota",2000,1,"Gasoline",models[0]),
                new CarForRentalDTO(2,"un coche amarillo","amarillo","Toyota",3000,1,"Gasoline",models[1]),
                new CarForRentalDTO(3,"un coche verde","verde","Toyota",1500,1,"Gasoline",models[2]),
            };
            
            /*
            models[0].Cars = new List<Car> {
                new Car(1,"un coche rojo","rojo","Toyota",2000,1,"Gasoline"),
            };
            models[1].Cars = new List<Car> {
                new Car(2,"un coche amarillo","amarillo","Toyota",3000,1,"Gasoline"),
            };
            models[2].Cars = new List<Car> {
                new Car(3,"un coche verde","verde","Toyota",1500,1,"Gasoline"),
            };
            */

            var carDTOsTC1 = new List<CarForRentalDTO>() { carDTOs[0], carDTOs[1], carDTOs[2] }
                    .OrderBy(c => c.Model.Name).ToList();


            var carDTOsTC2 = new List<CarForRentalDTO>() { carDTOs[1] };
            var carDTOsTC3 = new List<CarForRentalDTO>() { carDTOs[0],carDTOs[2] }
                .OrderBy(c => c.Model.Name).ToList();


            var allTests = new List<object[]>
            {             
                new object[] {null, null, null,  carDTOsTC1,  },
                new object[] {"Toyota A", null, null,  carDTOsTC2, },
                new object[] {null, 1000, 2000,  carDTOsTC3, },
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForRental_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForRental_OK_test(string? modelFilter, int? priceMin, int? priceMax, IList<CarForRentalDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCarsForRenting(modelFilter,priceMin,priceMax);
            
            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var carDTOsActual = Assert.IsType<List<CarForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }


        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCarsForRental_badrequest_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CarsController>>();
            ILogger<CarsController> logger = mock.Object;
            var controller = new CarsController(_context, logger);

            // Act
            var result = await controller.GetCarsForRenting("BMW",null,null);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problem = problemDetails.Errors.First().Value[0];

            Assert.Equal("No cars could be found meeting that criteria", problem);
        }
    }
}
