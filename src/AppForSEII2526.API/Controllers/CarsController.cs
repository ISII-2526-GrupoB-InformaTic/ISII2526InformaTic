using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<CarsController> _logger;

        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ComputeDivision(decimal op1, decimal op2)
        {
            if (op2 == 0)
            {
                _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
                return BadRequest("op2 must be different from 0");
            }
            decimal result = decimal.Round(op1 / op2, 2);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCarsForRenting(string? modelFilter, int? priceMin, int? priceMax)
        {
            IList<CarForRentalDTO> cars = await _context.Cars
                .Include(c => c.Model)
                .Where(c => ((modelFilter == null) || (c.Model.Name.Equals(modelFilter))) &&
                    ((c.RentingPrice <= priceMax) || (priceMax==null)) &&
                    ((c.RentingPrice >= priceMin) || (priceMin==null)))
                .OrderBy(c=> c.Model.Name)  
                .Select(c => new CarForRentalDTO(c.Id,c.Color,c.Manufacturer,c.RentingPrice,c.FuelType,c.Model))
                .ToListAsync();
            return Ok(cars);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForPurchasingDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetCarForPurchase(string? name, string? color)
        {

            IList<CarForPurchasingDTO> cars = await _context.Cars
                .Include(m => m.Model)
                .Where(m => (m.Model.Name.Contains(name) || (name == null)) &&
                (m.Color.Equals(color) || (color == null)))
                .Select(m => new CarForPurchasingDTO(m.Id, m.Model, m.Color, m.FuelType, m.Manufacturer, m.PurchasingPrice))
                .ToListAsync();

            return Ok(cars);

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<MaintenanceDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetMaintenance(string? type, string? name)
        {
            IList<MaintenanceDTO> maintenance = await _context.Maintenances
                .Include(m => m.MaintenanceTypes)
                .Where(m => (type == null || m.MaintenanceTypes.Any(mt => mt.Type != null && mt.Type.Contains(type))) &&
                            (name == null || m.Name.Contains(name)))
                .Select(m => new MaintenanceDTO(
                    m.Id,
                    m.Name,
                    m.NumberOfDays,
                    m.Price,
                    m.MaintenanceTypes.Select(mt => new MaintenanceTypeDTO(mt.Id, mt.Type, m)).ToList()
                ))
                .ToListAsync();

            return Ok(maintenance);
        }
    }
}
