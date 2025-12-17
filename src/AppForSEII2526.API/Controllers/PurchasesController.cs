using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase   //ESTAMOS UTILIZANDO EL PURCHASE CONTROLLER PORQUE ESTAMOS OBTENIENDO LAS COMPRAS REALIZADAS
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<PurchasesController> _logger;


        public PurchasesController(ApplicationDbContext context, ILogger<PurchasesController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseForDetailsDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreatePurchase(PurchaseForCreateDTO purchaseForCreate)   //ESTAMOS CREANDO UNA NUEVA COMPRA A TRAVES DE ESTE METODO (POST)
        {

            if (purchaseForCreate.Name == null || purchaseForCreate.Surname == null || purchaseForCreate.DeliveryAddress == null)
            {
                ModelState.AddModelError("Purchase", "Error! Faltan datos obligatorios");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            if(purchaseForCreate.PurchaseDate < DateTime.Today)
            {
                ModelState.AddModelError("Purchase", "Error! No puedes comprarlo antes de hoy");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == purchaseForCreate.username);

            if (user == null)
            {

                ModelState.AddModelError("User", "Usuario no encontrado");

            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            var carModels = purchaseForCreate.PurchaseItemDTO?
                .Select(pi => pi.Car)
                .ToList<String>();

            var cars = _context.Cars
                .Include(c => c.Model)
                .Include(c => c.PurchaseItems)
                .ThenInclude(pi => pi.purchase)
                .ThenInclude(c => c.purchaseItems)
                .ThenInclude(pi => pi.car)
                .Where(c => c.Model != null && carModels.Contains(c.Model.Name))
                .ToList();

            Purchase purchase = new Purchase(
                purchaseForCreate.Name,
                purchaseForCreate.PaymentMethod,
                DateTime.Today,
                purchaseForCreate.Price,
                0,
                new List<PurchaseItem>(),
                user
            );

            purchase.TotalPrice = 0;

            foreach (var item in purchaseForCreate.PurchaseItemDTO)
            {
                var car = cars.FirstOrDefault(c => c.Model.Name == item.Car); //ESTA ES LA DE LA BASE DE DATOS

                if (car == null || item.CarId != car.Id)
                {
                    ModelState.AddModelError("Purchase", $"Error! El coche no existe");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                if (car.QuantityForPurchasing < item.Quantity)
                {
                    ModelState.AddModelError("Purchase", $"Error! No hay suficiente cantidad para comprar");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                PurchaseItem purchaseItem = new PurchaseItem(
                    car.Id,
                    purchase.Id,
                    item.Quantity,
                    car,
                    purchase
                );
                purchase.purchaseItems.Add(purchaseItem);
                purchase.TotalPrice += car.PurchasingPrice;

            }

            purchase.TotalPrice += purchase.purchaseItems.Sum(pi => pi.car.PurchasingPrice * pi.Quantity);

            if (ModelState.ErrorCount > 0)
            {

                return BadRequest(new ValidationProblemDetails(ModelState));

            }

            _context.Add(purchase);

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {

                _logger.LogError(ex.Message);
                ModelState.AddModelError("Purchase", $"Error al crear la compra, por favor, intentalo de nuevo");
                return Conflict("Error" + ex.Message);

            }

            var purchaseDetails = new PurchaseForDetailsDTO(purchase.Id, purchaseForCreate.Name, purchaseForCreate.Surname,
                purchaseForCreate.DeliveryAddress, purchase.PaymentMethod, purchase.PurchaseDate, purchaseForCreate.username, purchaseForCreate.PurchaseItemDTO);

            _logger.LogInformation("Creada las compras a realizar");

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchaseDetails);

            // Lógica para crear la compra utilizando los datos del DTO
            // Aquí deberías agregar la lógica para guardar la compra en la base de datos
            return CreatedAtAction(nameof(CreatePurchase), new { id = 1 }, purchaseForCreate); // Retorna un ejemplo de respuesta creada

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseForDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPurchase(int id)   //ESTE METODO NOS PERMITE OBTENER LAS COMPRAS REALIZADAS  (DETAILS)
        {

            if (_context.Purchases == null)
            {

                _logger.LogError("Error: Purchases table does not exist");
                return NotFound("No hay compras realizadas");

            }

            if(id < 0)
            {
                _logger.LogError($"Error: El id {id} introducido no es correcto");
                return NotFound();
            }

            var purchases = await _context.Purchases
                .Where(p => p.Id == id)
                .Include(p => p.purchaseItems)
                .ThenInclude(pi => pi.car)
                .ThenInclude(c => c.Model)
                .Select(p => new PurchaseForDetailsDTO(
                    p.Id,
                    p.User.Name,
                    p.User.Surname,
                    p.User.DeliveryAddress,
                    p.PaymentMethod,
                    p.PurchaseDate,
                    p.User.UserName,
                    p.purchaseItems
                        .Select(pi => new PurchaseItemDTO
                        {
                            CarId = pi.car.Id,
                            Color = pi.car.Color,
                            Price = pi.car.PurchasingPrice,
                            Car = pi.car.Model.Name,
                            Descripcion = pi.car.Description,
                            PurchaseId = pi.PurchaseId,
                            Quantity = pi.Quantity
                        })
                        .ToList<PurchaseItemDTO>()
                ))
                .FirstOrDefaultAsync();

            if (purchases == null)
            {

                _logger.LogError($"Error: Purchase with id {id} does not exist");
                return NotFound();

            }

            return Ok(purchases);

        }


    }

}
