using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class PostRentals_test : AppForSEII25264SqliteUT
    {
        private const string _email = "elena.navarro@uclm.es";
        private const string _Name = "Pepe";
        private const string _Surname = "Viyuela";
        private const string _deliveryAddress = "Calle MiCasa Nº7";

        private const string _model1Name = "Toyota R";
        private const string _model2Name = "Toyota A";
        private const string _model3Name = "Toyota V";

        public PostRentals_test()
        {

            var models = new List<Model>()
            {
                new Model {Id=1,Name=_model1Name},
                new Model {Id=2,Name=_model2Name},
                new Model {Id=3,Name=_model3Name}
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

            ApplicationUser user = new ApplicationUser("1", _Name, _Surname, _email,_deliveryAddress);

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var numDays = (endDate - startDate).Days;

            var rental = new Rental(endDate, startDate, DateTime.Today, (cars[1].RentingPrice * numDays), "Tony", new List<RentalItem>(), PaymentMethod.TarjetaDeCredito);

            var rentalItem = new RentalItem(1, 5, 1, cars[0], rental, user);

            rental.RentalItems.Add(rentalItem);

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.Add(rentalItem);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            var rentalNoITem = new RentalForCreateDTO(_Name, _Surname, _deliveryAddress, PaymentMethod.TarjetaDeCredito,
                DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), new List<RentalItemDTO>());

            var rentalItems = new List<RentalItemDTO>() { new RentalItemDTO(1,5,1) };

            var rentalFromBeforeToday = new RentalForCreateDTO(_Name, _Surname,_deliveryAddress, PaymentMethod.TarjetaDeCredito,
                DateTime.Today, DateTime.Today.AddDays(5), rentalItems);

            var rentalToBeforeFrom = new RentalForCreateDTO(_Name, _Surname, _deliveryAddress, PaymentMethod.TarjetaDeCredito,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), rentalItems);

            var RentalApplicationUser = new RentalForCreateDTO("Jose", _Surname, _deliveryAddress, PaymentMethod.TarjetaDeCredito, 
                DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), rentalItems);

            var rentalCarNotAvailable = new RentalForCreateDTO(_Name, _Surname, _deliveryAddress, PaymentMethod.TarjetaDeCredito,
                DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), new List<RentalItemDTO>() { new RentalItemDTO(1,5,1) });


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { rentalNoITem, "Error! You must include at least one car to be rented",  },
                new object[] { rentalFromBeforeToday, "Error! Your rental date must start later than today", },
                new object[] { rentalToBeforeFrom, "Error! Your rental must end later than it starts", },
                new object[] { RentalApplicationUser, "Error! User is not registered", },
                new object[] { rentalCarNotAvailable, "Error! That car is not available for renting at this moment", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreateRental_Error_test(RentalForCreateDTO rentalDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateRental_Success_test()
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

            var rentalDTO = new RentalForCreateDTO(_Name, _Surname,_deliveryAddress, PaymentMethod.TarjetaDeCredito,
                startdateUnspecified, enddateUnspecified, new List<RentalItemDTO>() { new RentalItemDTO(2,1,2) });

            var expectedrentalDetailDTO = new RentalDetailDTO(_Name,_Surname,_deliveryAddress,PaymentMethod.TarjetaDeCredito,
                startdateUnspecified,enddateUnspecified,rentingdateUnspecified,new List<RentalItemDTO>() { new RentalItemDTO(2,1,2) });

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<RentalDetailDTO>(createdResult.Value);

            Assert.Equal(expectedrentalDetailDTO, actualRentalDetailDTO);

        }
    }
}
