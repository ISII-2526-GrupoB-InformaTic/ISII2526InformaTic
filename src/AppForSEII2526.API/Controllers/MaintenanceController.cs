using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(ApplicationDbContext context, ILogger<MaintenanceController> logger)
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
        [ProducesResponseType(typeof(IList<MaintenanceDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetMaintenance(string? type, string? name)
        {
            if ((type != null && type.Length <= 2) ||(name != null && name.Length <= 2))
            {
                ModelState.AddModelError("busquedaPequeña", "debes rellenar al menos un campo con mas de 2 caracteres");
                _logger.LogError($"Name y Type son demasiado pequeños");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            IList<MaintenanceDTO> maintenance = await _context.Maintenances
                .Include(m => m.MaintenanceTypes)
                .Where(m => (type == null || m.MaintenanceTypes.Any(mt => mt.Type != null && mt.Type.Contains(type))) &&
                            (name == null || m.Name.Contains(name)))
                .OrderBy(m => m.Name)
                .Select(m => new MaintenanceDTO(
                    m.Id,
                    m.Name,
                    m.NumberOfDays,
                    m.Price,
                    m.MaintenanceTypes.OrderBy(mt => mt.Id).Select(mt => new MaintenanceTypeDTO(mt.Id, mt.Type, null)).ToList()
                ))
                .ToListAsync();

            return Ok(maintenance);
        }
    }
}
