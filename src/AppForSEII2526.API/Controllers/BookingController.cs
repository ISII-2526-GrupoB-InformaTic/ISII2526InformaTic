using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookingDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateBooking(BookingForCreateDTO bookingForCreate)
        {
            if (bookingForCreate.BookingItems.Count == 0)
                ModelState.AddModelError("BookingItems", "Error! You must include at least one maintenance for booking");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == bookingForCreate.Name);
            if (user == null)
                ModelState.AddModelError("BookingApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            Booking booking = new Booking(bookingForCreate.DeliveryAddress, bookingForCreate.Surname, DateTime.Today.AddDays(20), 1, bookingForCreate.PaymentMethod,
                new List<BookingItem>(), user);

            foreach (var item in bookingForCreate.BookingItems)
            {
                booking.BookingItems.Add(new BookingItem(item.BookingId, item.Comment, item.MantID, booking,
                    new Maintenance(item.Maintenance.Id, item.Maintenance.Name, item.Maintenance.NumberOfDays, item.Maintenance.Price, null, null)));
            }

            _context.Add(booking);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Booking", "Error! There was an error while saving your booking, please, try again later");
                return Conflict("Error: " + ex.Message);
            }

            var bookingDetail = new BookingDetailDTO(bookingForCreate.Name, bookingForCreate.Surname,
                bookingForCreate.DeliveryAddress, bookingForCreate.PaymentMethod,
                DateTime.Now,
                bookingForCreate.BookingItems);

            return CreatedAtAction("GetBooking", new { id = booking.Id }, bookingDetail);
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
                        .ThenInclude(ris => ris.MaintenanceTypes)
                .Select(r => new BookingDetailDTO(r.clientName, r.clientSurname,
                    r.clientAdress, r.PaymentMethod,
                    r.Date,
                    r.BookingItems
                        .Select(ri => new BookingItemDTO(ri.BookingId,
                                ri.Comment, ri.MantID,
                                new BookingDTO(r.clientName, r.clientSurname, r.Date, r.Id, r.PaymentMethod, r.BookingItems.Select(bi => new BookingItemDTO(bi.BookingId, bi.Comment, bi.MantID, null, null)).ToList(), r.User),
                                new MaintenanceDTO(ri.Maintenance.Id, ri.Maintenance.Name, ri.Maintenance.NumberOfDays, ri.Maintenance.Price, null)))
                        .ToList()))
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                _logger.LogError($"Error: Booking with id {id} does not exist");
                return NotFound();
            }

            return Ok(booking);
        }

    }
}
