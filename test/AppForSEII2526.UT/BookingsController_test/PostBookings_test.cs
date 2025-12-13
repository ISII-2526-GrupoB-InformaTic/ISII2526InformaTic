using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.UT.BookingsController_test
{
    public class PostBookings_test : AppForSEII25264SqliteUT
    {
        private const string correo = "Alejandro.navarro@uclm.es";
        private const string nombre_cliente = "Alejandro";
        private const string apellido_cliente = "Navarro";
        private const string deliveryAddress = "Avda. España s/n, Albacete 02071";

        private const string nombreMantenimiento1 = "R-512";
        private const string tipoMantenimiento1 = "Cambio de aceite";
        private const string tipoMantenimiento2 = "Ajustar frenos";
        private const string nombreMantenimiento2 = "A-321";
        private const string nombreMantenimiento3 = "H-123";

        public PostBookings_test()
        {

            var maintenanceTypes = new List<MaintenanceType>() {
                new MaintenanceType ("Cambio de aceite"),
                new MaintenanceType ("Ajustar frenos"),
            };
            var maintenances = new List<Maintenance>()
            {
                new Maintenance(1,nombreMantenimiento1,2,100,new List<BookingItem>(),new List<MaintenanceType>()),
                new Maintenance(2,nombreMantenimiento1,4,100,new List<BookingItem>(),new List<MaintenanceType>()),
                new Maintenance(3,nombreMantenimiento1,6,100,new List<BookingItem>(),new List<MaintenanceType>()),
            };
            maintenances[0].MaintenanceTypes.Add(maintenanceTypes[0]);
            maintenances[1].MaintenanceTypes.Add(maintenanceTypes[1]);
            maintenances[2].MaintenanceTypes.Add(maintenanceTypes[0]);
            maintenances[2].MaintenanceTypes.Add(maintenanceTypes[1]);
            ApplicationUser usuario = new ApplicationUser("1",nombre_cliente, apellido_cliente, correo, deliveryAddress);
            var booking = new Booking(DateTime.Today, PaymentMethod.GooglePay, new List<BookingItem>(), usuario);
            var bookingItem = new BookingItem("Facil de aplicar y tremendamente economico", booking, maintenances[0]);
            booking.BookingItems.Add(bookingItem);
            _context.ApplicationUsers.Add(usuario);
            _context.AddRange(maintenanceTypes);
            _context.AddRange(maintenances);
            _context.Add(booking);
            _context.Add(bookingItem);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_CreateMaintenance()
        {
            var maintenanceNoItems = new BookingForCreateDTO(nombre_cliente, apellido_cliente, deliveryAddress, PaymentMethod.Paypal, null, new List<BookingItemDTO>());
            var bookingItems = new List<BookingItemDTO>()
            {
                new BookingItemDTO() { Comment="Facil de aplicar y tremendamente economico",MaintenanceId=1 },
                new BookingItemDTO() { Comment="Dificil de aplicar y tremendamente efectivo",MaintenanceId=2 }
            };
            var bookingItemsNoComment = new List<BookingItemDTO>()
            {
                new BookingItemDTO() { Comment="",MaintenanceId=1 },
            };
            var bookingItemsCommentShort = new List<BookingItemDTO>()
            {
                new BookingItemDTO() { Comment="Muy bueno",MaintenanceId=1 },
            };
            var maintenanceNoName = new BookingForCreateDTO("", apellido_cliente, deliveryAddress, PaymentMethod.Paypal, null, bookingItems);
            var maintenanceNoSurname = new BookingForCreateDTO(nombre_cliente, "", deliveryAddress, PaymentMethod.Paypal, null, bookingItems);
            var maintenanceNoAddress = new BookingForCreateDTO(nombre_cliente, apellido_cliente, "", PaymentMethod.Paypal, null, bookingItems);
            var maintenanceInvalidPaymentMethod = new BookingForCreateDTO(nombre_cliente, apellido_cliente, deliveryAddress, (PaymentMethod)999, null, bookingItems);
            var maintenanceNoUser = new BookingForCreateDTO("Maradona", "Huseopos", "El inframundo greco romano", PaymentMethod.Paypal, null, bookingItems);
            var maintenanceNoComment = new BookingForCreateDTO(nombre_cliente, apellido_cliente, deliveryAddress, PaymentMethod.Paypal, null, bookingItemsNoComment);
            var maintenanceNoCommentShort = new BookingForCreateDTO(nombre_cliente, apellido_cliente, deliveryAddress, PaymentMethod.Paypal, null, bookingItemsCommentShort);
            var allTestCases = new List<object[]>
            {
                new object[] { maintenanceNoItems, "Error! You must include at least one maintenance for booking" },
                new object[] { maintenanceNoName, "Error! Name is required" },
                new object[] { maintenanceNoSurname, "Error! Surname is required" },
                new object[] { maintenanceNoAddress, "Error! Delivery address is required" },
                new object[] { maintenanceInvalidPaymentMethod, "Error! A valid payment method is required" },
                new object[] { maintenanceNoComment, "Error! Comment is required for each maintenance (min 20 characters)" },
                new object[] { maintenanceNoCommentShort, "Error! Comment is required for each maintenance (min 20 characters)" },
                new object[] { maintenanceNoUser, "Error! UserName is not registered" }
            };
            return allTestCases;

        }
        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateMaintenance))]
        public async Task CreateBooking_Error_test(BookingForCreateDTO bookingfcDTO, string errorExpected)
        {
            var mock = new Mock<ILogger<BookingController>>();
            ILogger<BookingController> logger = mock.Object;
            var controller = new BookingController(_context, logger);
            var result = await controller.CreateBooking(bookingfcDTO);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var errorActual = problemDetails.Errors.First().Value[0];
            Assert.Equal(errorExpected, errorActual);
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateBooking_OK_test()
        {
            var mock = new Mock<ILogger<BookingController>>();
            ILogger<BookingController> logger = mock.Object;

            var controller = new BookingController(_context, logger);

            var maintenanceTypes = new List<MaintenanceTypeDTO>() {
            new MaintenanceTypeDTO(1, "Cambio de aceite")
    };
            var maintenanceDTO = new MaintenanceDTO(
                1,             
                "R-512",       
                2,              
                100,           
                maintenanceTypes
            );

            var bookingItemDTO = new BookingItemDTO() {
                Comment="Facil de aplicar y tremendamente economico",
                MaintenanceId=maintenanceDTO.Id,
                MaintName=maintenanceDTO.Name,
                Price=maintenanceDTO.Price}
            ;
            var bookingItemList = new List<BookingItemDTO>() { bookingItemDTO };

            var bookingForCreateDTO = new BookingForCreateDTO(
                nombre_cliente,
                apellido_cliente,
                deliveryAddress,
                PaymentMethod.Paypal,
                null,
                new List<BookingItemDTO>() { bookingItemDTO }
            );
            var expectedBookingItemDTO = new BookingItemDTO(
                 "Facil de aplicar y tremendamente economico",
                  bookingID: 2,
                  maintenanceDTO.Id,
                  maintenanceDTO.Name,
                  maintenanceDTO.Price
            );

            var expectedBookingDetailDTO = new BookingDetailDTO(
                2, 
                nombre_cliente,
                apellido_cliente,
                deliveryAddress,
                PaymentMethod.Paypal,
                null,
                DateTime.Today.ToUniversalTime(),
                100,
                2,
                new List<BookingItemDTO>() { expectedBookingItemDTO }
            );


            // Act
            var result = await controller.CreateBooking(bookingForCreateDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualBookingDetailDTO = Assert.IsType<BookingDetailDTO>(createdResult.Value);

            Assert.Equal(expectedBookingDetailDTO, actualBookingDetailDTO);
        }
    }
}