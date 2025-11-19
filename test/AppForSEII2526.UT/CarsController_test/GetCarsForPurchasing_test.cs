using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using NuGet.ContentModel;
using NuGet.Versioning;
using RabbitMQ.Client;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Drawing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarsController_test
{

    public class GetCarsPurchasing_test : AppForSEII25264SqliteUT
    {
        /*
        public GetCarsPurchasing_test()
        {

            var modelo = new List<Model>()
            {
                new Model(1, "Model X", null),
                new Model(2, "Mustang", null),
                new Model(3, "Civic", null),
                new Model( 4, "Corolla", null),
                new Model(5, "Camry" , null)

            };

            var coche = new List<Car>()
            {

                new Car("coche", "Red", "Un coche rojo", "Ford", "", 1, 5000, 2000, 50000, 30000, modelo[0], null, null)
                {
                    FuelType = "Diesel",
                    EngDisplacement = "",
                    RimSize = ""
                },

                new Car("coche","Blue", "Un coche azul", "Citroen", "", 2, 5000, 2000, 60000, 30000, modelo[1], null, null)
                {
                    FuelType = "Gasoleo",
                    EngDisplacement = "",
                    RimSize = ""
                },

            };

            

            ApplicationUser user = new ApplicationUser("1", "Elena", "Navarro Martinez", "elena@uclm.es", "avdaEspaña");

            var Purchase = new Purchase("Elena Navarro", PaymentMethod.TarjetaDeCredito, DateTime.Now, 55000, 1, new List<PurchaseItem>(), user);
            var purchaseItem = new PurchaseItem(1, 1, 30, coche[1], Purchase);

            _context.Add(user);
            _context.AddRange(modelo);  //USAMOS ADDRANGE PORQUE COMO ES UNA LISTA NECESITAMOS SU RANGO DE VALORES
            _context.AddRange(coche);
            _context.Add(purchaseItem);
            _context.Add(Purchase);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> TestCasesFor_GetCarForPurchase_OK()
        {

            
            var modelo = new List<Model>()
            {
                new Model(1, "Model X", new List<Car>()),
                new Model(2, "Mustang", new List<Car>()),
                new Model(3, "Civic", new List<Car>()),
                new Model(4, "Corolla", new List<Car>()),
                new Model(5, "Camry" , new List<Car>())

            };



            var carDTO = new List<CarForPurchasingDTO>()
                {

                    new CarForPurchasingDTO(1, modelo[0] , "Red", "Diesel", "Ford", 50000),

                    new CarForPurchasingDTO(2, modelo[1]  ,"Blue", "Gasoleo", "Citroen", 60000),

                };

            var carDTOsExpected = new List<CarForPurchasingDTO>() { carDTO[0], carDTO[1] }
                .OrderBy(c => c.model.Name).ToList();

            var carDTOsExpected2 = new List<CarForPurchasingDTO>() { carDTO[0] };

            var carDTOsExpected3 = new List<CarForPurchasingDTO>() { carDTO[1] };

            var allTests = new List<object[]>
                {

                    new object[] { null, null, carDTOsExpected },

                    new object[] { "Ford", null, carDTOsExpected2, },

                    new object[] { null, "Blue", carDTOsExpected3, },

                };

            return allTests;

        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarForPurchase_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarForPurchase_OK_test(string? filterName, string? FilterColor, IList<CarForPurchasingDTO> expectedPurchase)
        {

            var controller = new CarsController(_context, null);    //La variable _context es la que conecta con la base de datos, en este caso es la que hemos definido arriba con _context.add

            var result = await controller.GetCarForPurchase(filterName, FilterColor);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var purchaseDTOsActual = Assert.IsType<List<CarForPurchasingDTO>>(okResult.Value);

            Assert.Equal(expectedPurchase, purchaseDTOsActual);

        }


        /*
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCarForPurchase_badrequest_test()
        {

            var mock = new Mock<ILogger<CarsController>>();
            ILogger<CarsController> logger = mock.Object;   //La variable _logger solo se utiliza en esta clase ya que es la que se encarga de enseñarnos los errores si es que hay
            var controller = new CarsController(_context, logger);

            var result = await controller.GetCarForPurchase("Fredericar", "White");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problem = problemDetails.Errors.First().Value[0];

            Assert.Equal("name and color are not available", problem);

        }

        */

    }

}


