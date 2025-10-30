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

    }
}
