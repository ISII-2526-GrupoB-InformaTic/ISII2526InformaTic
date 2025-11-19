using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingController> _logger;

        public BookingController(ApplicationDbContext context, ILogger<BookingController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookingDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetBookings(int id)
        {
            if (_context.Bookings == null)
            {
                _logger.LogError("Error: Booking table does not exist");
                return NotFound();
            }

            var booking = await _context.Bookings
                .Where(r => r.Id == id)
                .Include(r => r.BookingItems)
                    .ThenInclude(ri => ri.Maintenance)
                        .ThenInclude(maintenance => maintenance.MaintenanceTypes)
                .Select(r => new BookingDetailDTO(r.Id,r.clientName, r.clientSurname,
                    r.clientAdress, (PaymentMethod)r.PaymentMethod, r.clientPhoneNumber,
                    r.Date,
                    r.BookingItems
                        .Select(ri => new BookingItemDTO(
                                ri.Comment,new BookingDTO(ri.Booking.Date,ri.Booking.Id,ri.Booking.PaymentMethod,ri.Booking.User),new MaintenanceDTO(ri.Maintenance.Id,ri.Maintenance.Name,ri.Maintenance.NumberOfDays,ri.Maintenance.Price,ri.Maintenance.MaintenanceTypes.Select(mt=>new MaintenanceTypeDTO(mt.Id,mt.Type)).ToList())))
                        .ToList()))
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                _logger.LogError($"Error: Booking with id {id} does not exist");
                return NotFound();
            }

            return Ok(booking);
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookingDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateBooking(BookingForCreateDTO bookingForCreate)
        {

            if (bookingForCreate.BookingItems == null || bookingForCreate.BookingItems.Count == 0)
                ModelState.AddModelError("BookingItems", "Error! You must include at least one maintenance for booking");


            if (string.IsNullOrWhiteSpace(bookingForCreate.Name))
                ModelState.AddModelError(nameof(bookingForCreate.Name), "Error! Name is required");

            if (string.IsNullOrWhiteSpace(bookingForCreate.Surname))
                ModelState.AddModelError(nameof(bookingForCreate.Surname), "Error! Surname is required");

            if (string.IsNullOrWhiteSpace(bookingForCreate.DeliveryAddress))
                ModelState.AddModelError(nameof(bookingForCreate.DeliveryAddress), "Error! Delivery address is required");


            if (!Enum.IsDefined(typeof(PaymentMethod), bookingForCreate.PaymentMethod))
                ModelState.AddModelError(nameof(bookingForCreate.PaymentMethod), "Error! A valid payment method is required");

            if (bookingForCreate.BookingItems != null)
            {
                for (int i = 0; i < bookingForCreate.BookingItems.Count; i++)
                {
                    var item = bookingForCreate.BookingItems[i];
                    if (string.IsNullOrWhiteSpace(item.Comment) || item.Comment.Length < 20 || item.Comment.Length > 200)
                    {
                        ModelState.AddModelError($"BookingItems[{i}].Comment", "Error! Comment is required for each maintenance (min 20 characters)");
                    }
                }
            }

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.Name == bookingForCreate.Name);
            if (user == null)
                ModelState.AddModelError("BookingApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            Booking booking = new Booking(DateTime.Today, bookingForCreate.PaymentMethod, new List<BookingItem>(), user);
            booking.Price = bookingForCreate.Price;
            booking.numberOfDays = bookingForCreate.numberOfDays;
            foreach (var itemDto in bookingForCreate.BookingItems)
            {
                var maintenance = await _context.Maintenances
                    .FirstOrDefaultAsync(m => m.Id == itemDto.MantID);

                if (maintenance == null)
                {
                    ModelState.AddModelError("BookingItems",
                        $"The maintenance with id {itemDto.MantID} does not exist");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                var bookingItem = new BookingItem
                {
                    Comment = itemDto.Comment,
                    Booking = booking,              
                    Maintenance = maintenance,       
                    MantID = maintenance.Id
                };

                booking.BookingItems.Add(bookingItem);
            }
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            _context.Add(booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Booking", $"Error! There was an error while saving your booking, plese, try again later");
                return Conflict("Error" + ex.Message);

            }
            var bookingDetail= new BookingDetailDTO(booking.Id, booking.clientName, booking.clientSurname,
                    booking.clientAdress, (PaymentMethod)booking.PaymentMethod, booking.clientPhoneNumber,
                    booking.Date,
                    booking.BookingItems
                        .Select(ri => new BookingItemDTO(
                                ri.Comment,new BookingDTO(ri.Booking.Date,ri.Booking.Id,ri.Booking.PaymentMethod,ri.Booking.User),new MaintenanceDTO(ri.Maintenance.Id,ri.Maintenance.Name,ri.Maintenance.NumberOfDays,ri.Maintenance.Price,ri.Maintenance.MaintenanceTypes.Select(mt=>new MaintenanceTypeDTO(mt.Id,mt.Type)).ToList())))
                        .ToList());
            return CreatedAtAction("GetBooking", new { id = booking.Id }, bookingDetail);
        }
    }
}