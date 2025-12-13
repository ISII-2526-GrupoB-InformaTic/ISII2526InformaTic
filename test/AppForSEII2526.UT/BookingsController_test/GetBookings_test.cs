using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;



namespace AppForSEII2526.UT.BookingsController_test
{
    public class GetBooking_test : AppForSEII25264SqliteUT
    {
        public GetBooking_test()
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
            ApplicationUser usuario = new ApplicationUser("1","Alfonso", "Gutierrez", "alfonsogutierrez@gmail.es", "Avenida 1");
            var bookings = new List<Booking>()
            {
                new Booking(DateTime.Today, PaymentMethod.GooglePay ,usuario),
                new Booking(DateTime.Today,PaymentMethod.GooglePay ,usuario),
                new Booking(DateTime.Today,PaymentMethod.GooglePay ,usuario)
            };
            var bookingItems = new List<BookingItem>()
            {
                new BookingItem() { Comment="Facil de contratar y tremendamente efectivo",Booking=bookings[0], Maintenance=maintenances[0],MaintName=maintenances[0].Name,Price=maintenances[0].Price},
                new BookingItem() { Comment = "Dificil y tremendamente efectivo",Booking=bookings[1], Maintenance=maintenances[1],MaintName=maintenances[1].Name,Price=maintenances[1].Price},
                new BookingItem() { Comment = "Intermedio de contratar y tremendamente efectivo",Booking=bookings[2], Maintenance=maintenances[2],MaintName=maintenances[0].Name,Price=maintenances[2].Price}
            };
            _context.AddRange(maintenanceTypes);
            _context.AddRange(maintenances);
            _context.AddRange(bookings);
            _context.AddRange(bookingItems);
            _context.SaveChanges();
        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBooking_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingController>>();
            ILogger<BookingController> logger = mock.Object;
            var controller = new BookingController(_context, logger);
            //Act
            var result = await controller.GetBookings(0);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBooking_NotFoundMenor_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingController>>();
            ILogger<BookingController> logger = mock.Object;
            var controller = new BookingController(_context, logger);
            //Act
            var result = await controller.GetBookings(-1);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBooking_OK_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingController>>();
            ILogger<BookingController> logger = mock.Object;
            var controller = new BookingController(_context, logger);

            ApplicationUser usuario = new ApplicationUser("1","Alfonso", "Gutierrez", "alfonsogutierrez@gmail.es", "Avenida 1");

            var expectedBookingDTO = new BookingDetailDTO(1,"Alfonso", "Gutierrez", "Avenida 1", PaymentMethod.GooglePay,null,DateTime.Today.ToUniversalTime(),100,2, new List<BookingItemDTO>());
            expectedBookingDTO.BookingItems.Add(new BookingItemDTO("Facil de contratar y tremendamente efectivo", 1, 1,"R-512", 100));
            //Act
            var result = await controller.GetBookings(1);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookingDTOActual = Assert.IsType<BookingDetailDTO>(okResult.Value);
            var eq = expectedBookingDTO.Equals(bookingDTOActual);

            Assert.Equal(expectedBookingDTO, bookingDTOActual);
        }
    }
}
