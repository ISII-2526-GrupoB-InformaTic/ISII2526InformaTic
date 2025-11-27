using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(ApplicationDbContext context, ILogger<RentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {
            //any validation defined in PurchaseForCreate is checked before running the method so they don't have to be checked again
            if (rentalForCreate.StartDate <= DateTime.Today)
                ModelState.AddModelError("RentalDateFrom", "Error! Your rental date must start later than today");

            if (rentalForCreate.StartDate >= rentalForCreate.EndDate)
                ModelState.AddModelError("RentalDateFrom&RentalDateTo", "Error! Your rental must end later than it starts");

            if (rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! You must include at least one car to be rented");

            // if (!_context.ApplicationUsers.Any(au=>au.UserName==rentalForCreate.CustomerUserName))
            var user = _context.ApplicationUsers.FirstOrDefault(au => au.Name == rentalForCreate.Name);
            if (user == null)
                ModelState.AddModelError("RentalApplicationUser", "Error! User is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var carModels = rentalForCreate.RentalItems
                .Select(ri => ri.Car).ToList<string>();

            var cars = _context.Cars.Include(c => c.RentalItems)
                .ThenInclude(ri => ri.Rental)
                .Where(m => carModels.Contains(m.Model.Name))

                //we use an anonymous type https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
                .Select(m => new {
                    m.Id,
                    m.Model.Name,
                    m.Manufacturer,
                    m.QuantityForRenting,
                    m.RentingPrice,
                    //we count the number of rentalItems that are within the rental period
                    NumberOfRentedItems = m.RentalItems.Count(ri => ri.Rental.StartDate <= rentalForCreate.EndDate
                            && ri.Rental.EndDate >= rentalForCreate.StartDate)
                })
                .ToList();


            Rental rental = new Rental(rentalForCreate.EndDate, rentalForCreate.StartDate,
                DateTime.Now, rentalForCreate.TotalPrice,"DeliveryCarDealer",
                new List<RentalItem>(), rentalForCreate.PaymentMethod,user);


            rental.TotalPrice = 0;
            var numDays = (rental.EndDate - rental.StartDate).TotalDays;


            foreach (var item in rentalForCreate.RentalItems)
            {
                var car = cars.FirstOrDefault(c => c.Name == item.Car);
                //we must check that there is enough quantity to be rented in the database
                if ((car == null) || (car.NumberOfRentedItems >= car.QuantityForRenting))
                {
                    ModelState.AddModelError("RentalItems", $"Error! That car is not available for renting at this moment");
                }
                else
                {
                    // rental does not exist in the database yet and does not have a valid Id, so we must relate rentalitem to the object rental
                    rental.RentalItems.Add(new RentalItem(car.Id, car.QuantityForRenting, rental.Id,new Car(),rental));
                    item.RentingPrice = car.RentingPrice;
                }
            }
            rental.TotalPrice = (int)rental.RentalItems.Sum(ri => ri.Car.RentingPrice * numDays);


            //if there is any problem because of the available quantity of movies or because the movie does not exist
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(rental);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            //it returns rentalDetail
            var rentalDetail = new RentalDetailDTO(rentalForCreate.Name, rentalForCreate.Surname,
                rentalForCreate.DeliveryAddress, rentalForCreate.PaymentMethod,
                rental.StartDate, rental.EndDate, DateTime.Now,
                rentalForCreate.RentalItems);

            return CreatedAtAction("GetRental", new { id = rental.Id }, rentalDetail);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRental(int id)
        {
            if (_context.Rentals == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }
            if (id < 0)
            {
                _logger.LogError("Error: Id cannot be lower than 0");
                return NotFound();
            }
            var rental = await _context.Rentals
             .Where(r => r.Id == id)
                 .Include(r => r.RentalItems) //join table RentalItems
                    .ThenInclude(ri => ri.Car) //then join table Cars
                        .ThenInclude(car => car.Model) //then join table Model
             .Select(r => new RentalDetailDTO(r.User.Name, r.User.Surname,
                    r.User.DeliveryAddress, r.PaymentMethod,
                    r.StartDate, r.EndDate,r.RentingDate,
                    r.RentalItems
                        .Select(ri => new RentalItemDTO(ri.Car.Id,ri.Quantity,ri.RentalId)).ToList<RentalItemDTO>()))
             .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }
    }

}