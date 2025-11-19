using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.MaintenanceController_test
{
    public class GetMaintenance_test : AppForSEII25264SqliteUT
    {
        public GetMaintenance_test()
        {
            var maintenanceTypes = new List<MaintenanceType>() {
                new MaintenanceType ("Cambio de aceite"),
                new MaintenanceType ("Ajustar frenos"),
                new MaintenanceType ("Rotura de luna"),
                new MaintenanceType ("Cambio de ruedas"),
                new MaintenanceType ("Desabollar")
            };
            var maintenances = new List<Maintenance>()
            {
                new Maintenance() {Id=1, Name="R-512", NumberOfDays=2, Price=100, MaintenanceTypes=new List<MaintenanceType>{ maintenanceTypes[0], maintenanceTypes[1]} },
                new Maintenance() {Id=2, Name="A-321", NumberOfDays=4, Price=200, MaintenanceTypes=new List<MaintenanceType>{ maintenanceTypes[2], maintenanceTypes[3]} },
                new Maintenance() {Id=3, Name="H-123", NumberOfDays=6, Price=300, MaintenanceTypes=new List<MaintenanceType>{ maintenanceTypes[4]} }
            };
            ApplicationUser usuario = new ApplicationUser("1", "Alfonso", "Gutierrez", "alfonsogutierrez@gmail.es", "Avenida 1");
            var bookings = new List<Booking>()
            {
                new Booking(DateTime.Today, PaymentMethod.GooglePay ,usuario),
                new Booking(DateTime.Today,PaymentMethod.GooglePay ,usuario),
                new Booking(DateTime.Today,PaymentMethod.GooglePay ,usuario)
            };
            var bookingItems = new List<BookingItem>()
            {
                new BookingItem() { Comment="Facil",Booking=bookings[0], Maintenance=maintenances[0]},
                new BookingItem() { Comment = "Dificil",Booking=bookings[1], Maintenance=maintenances[1]},
                new BookingItem() { Comment = "Intermedio",Booking=bookings[2], Maintenance=maintenances[2]}
            };

            _context.AddRange(maintenanceTypes);
            _context.AddRange(maintenances);
            _context.AddRange(bookings);
            _context.AddRange(bookingItems);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_GetMaintenances_OK()
        {
            var maintenanceTypesDTOs = new List<MaintenanceTypeDTO>()
            {
                new MaintenanceTypeDTO(1,"Cambio de aceite"),
                new MaintenanceTypeDTO(2,"Ajustar frenos"),
                new MaintenanceTypeDTO(3,"Rotura de luna"),
                new MaintenanceTypeDTO(4,"Cambio de ruedas"),
                new MaintenanceTypeDTO(5,"Desabollar")
            };
            var maintenanceTypesDTOs1 = new List<MaintenanceTypeDTO>()
            {
                new MaintenanceTypeDTO(1,"Cambio de aceite"),
                new MaintenanceTypeDTO(2,"Ajustar frenos")
            };
            var maintenanceTypesDTOs2 = new List<MaintenanceTypeDTO>()
            {
                new MaintenanceTypeDTO(3,"Rotura de luna"),
                new MaintenanceTypeDTO(4,"Cambio de ruedas")
            };
            var maintenanceTypesDTOs3 = new List<MaintenanceTypeDTO>()
            {
                new MaintenanceTypeDTO(5,"Desabollar")
            };
            var maintenanceDTOs = new List<MaintenanceDTO>()
            {
                new MaintenanceDTO(1,"R-512",2,100,maintenanceTypesDTOs1),
                new MaintenanceDTO(2,"A-321",4,200,maintenanceTypesDTOs2),
                new MaintenanceDTO(3,"H-123",6,300,maintenanceTypesDTOs3)
            };
            var maintenanceDTOsOrderedByName = maintenanceDTOs.OrderBy(m => m.Name).ToList();

            var allTests = new List<object[]>
            {
                new object[] { null, null, maintenanceDTOsOrderedByName },
                new object[] { null, "R-512", new List<MaintenanceDTO> { maintenanceDTOs[0] } },
                new object[] { "Cambio de aceite", null, new List<MaintenanceDTO> { maintenanceDTOs[0] } },
                new object[] { null, "A-321", new List<MaintenanceDTO> { maintenanceDTOs[1] } },
                new object[] { null, "H-123", new List<MaintenanceDTO> { maintenanceDTOs[2] } },
            };
            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_GetMaintenances_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMaintenances_OK(string? typeFilter, string? nameFilter, List<MaintenanceDTO> expectedMaintenances)
        {
            var mockLogger = new Mock<ILogger<MaintenanceController>>();
            var controller = new MaintenanceController(_context, mockLogger.Object);
            var result = await controller.GetMaintenance(typeFilter, nameFilter);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var maintenanceDTOsActual= Assert.IsType<List<MaintenanceDTO>>(okResult.Value);
            Assert.Equal(expectedMaintenances, maintenanceDTOsActual);
        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMaintenances_badrequest_test() { 
            //Arrange
            var mockLogger = new Mock<ILogger<MaintenanceController>>();
            var controller = new MaintenanceController(_context, mockLogger.Object);
            //Act
            var result = await controller.GetMaintenance("ah", null);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}