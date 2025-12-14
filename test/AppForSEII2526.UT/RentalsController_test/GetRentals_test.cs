using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class GetRentals_test : AppForSEII25264SqliteUT
    {
        public GetRentals_test() {
            var models = new List<Model>()
            {
                new Model {Id=1,Name="Toyota R"},
                new Model {Id=2,Name="Toyota A"},
                new Model {Id=3,Name="Toyota V"}
            };
            var cars = new List<Car>()
            {
                new Car {carClass = "coche",Color="rojo",Description="un coche rojo",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=2000,Model= models[0],FuelType="Gasoline"},
                new Car {carClass = "coche",Color="amarillo",Description="un coche amarillo",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=3000,Model= models[1],FuelType="Gasoline"},
                new Car {carClass = "coche",Color="verde",Description="un coche verde",Manufacturer="Toyota",
                    QuantityForRenting= 1, RentingPrice=1500,Model= models[2],FuelType="Gasoline"}
            };

            ApplicationUser user = new ApplicationUser("1", "Pepe", "Viyuela", "pepeV@uclm.es", "Calle MiCasa Nº7");

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var numDays = (endDate - startDate).Days;

            var rental = new Rental(endDate, startDate, DateTime.Today, (cars[1].RentingPrice * numDays), "Tony", new List<RentalItem>(), PaymentMethod.TarjetaDeCredito, user);

            var rentalItem = new RentalItem(1, 5, 1, cars[0], rental);

            rental.RentalItems.Add(rentalItem);

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetRental_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            // Act
            var result = await controller.GetRental(0);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;
            var controller = new RentalsController(_context, logger);

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var rentingDate = DateTime.Today;

            var startdateUnspecified = DateTime.SpecifyKind(startDate, DateTimeKind.Unspecified);
            var enddateUnspecified = DateTime.SpecifyKind(endDate, DateTimeKind.Unspecified);
            var rentingdateUnspecified = DateTime.SpecifyKind(rentingDate, DateTimeKind.Unspecified);

            var expectedRental = new RentalDetailDTO(1,"Pepe","Viyuela", "Calle MiCasa Nº7",PaymentMethod.TarjetaDeCredito,
                startdateUnspecified,enddateUnspecified,rentingdateUnspecified, new List<RentalItemDTO>(),"pepeV@uclm.es");
            expectedRental.RentalItems.Add(new RentalItemDTO(1,5,1,2000,"Toyota R", "Toyota"));

            // Act 
            var result = await controller.GetRental(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rentalDTOActual = Assert.IsType<RentalDetailDTO>(okResult.Value);
            var eq = expectedRental.Equals(rentalDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedRental, rentalDTOActual);

        }

    }
}
