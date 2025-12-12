using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class PostPurchase_test : AppForSEII25264SqliteUT
    {
        

        private const string _userName = "elena.navarro@uclm.es";
        private const string _customerName = "Elena ";
        private const string _customerSurName = "Navarro Martínez";
        private const string _deliveryAddress = "Avda. España s/n, Albacete 02071";

        private const string _carModel1 = "Model X";
        private const string _carColor1 = "Rojo";
        private const string _carModel2 = "Mondeo";
        private const string _carColor2 = "Azul";

        public PostPurchase_test()
        {

            var modelo = new List<Model>()
            {
                new Model(1,_carModel1, null),
                new Model(2,_carModel2,null)

            };

            var coche = new List<Car>()
            {

                new Car("coche", _carColor1, "Un coche rojo", "Ford", 1, 5, 2000, 50000, 30000, modelo[0], null, null),
                new Car("coche",_carColor2, "Un coche azul", "Citroen", 2, 4, 2000, 60000, 30000, modelo[1], null, null)

            };

            ApplicationUser user = new ApplicationUser("1", _customerName, _customerSurName,_userName, _deliveryAddress);

            var purchase = new Purchase(_customerName, PaymentMethod.TarjetaDeCredito, DateTime.Now,
                            50000, 1, new List<PurchaseItem>(), user);


            purchase.purchaseItems.Add(new PurchaseItem(coche[0].Id, purchase.Id, 
                                       coche[0].QuantityForPurchasing, coche[0], purchase));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(modelo);
            _context.AddRange(coche);
            _context.Add(purchase);
            _context.SaveChanges();

        }


        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {

            var purchaseItems = new List<PurchaseItemDTO>() {new PurchaseItemDTO(1, 1, 3, _carModel1)};

            var purchaseBeforeToday = new PurchaseForCreateDTO(_customerName, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today.AddDays(-1), purchaseItems);

            var purchaseNotName = new PurchaseForCreateDTO(null, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, purchaseItems);

            var purchaseNotSurName = new PurchaseForCreateDTO(_customerName, null, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, purchaseItems);

            var purchaseNotDelivery= new PurchaseForCreateDTO(_customerName, _customerSurName, null, PaymentMethod.TarjetaDeCredito, DateTime.Today, purchaseItems);

            var purchaseCarNotExist = new PurchaseForCreateDTO(_customerName, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, new List<PurchaseItemDTO>(){ 
                new PurchaseItemDTO(3, 1, 25000, "Toyota") 
            });

            var purchaseQuantityNotEnough = new PurchaseForCreateDTO(_customerName, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, new List<PurchaseItemDTO>(){
                new PurchaseItemDTO(1, 1, 7, _carModel1)
            });

            var allTest = new List<object[]>
            {
                new object[] {purchaseBeforeToday, "Error! No puedes comprarlo antes de hoy", },
                new object[] {purchaseNotName, "Error! Faltan datos obligatorios", },
                new object[] {purchaseNotSurName, "Error! Faltan datos obligatorios", },
                new object[] {purchaseNotDelivery, "Error! Faltan datos obligatorios", },
                new object[] {purchaseCarNotExist, "Error! El coche no existe", },
                new object[] {purchaseQuantityNotEnough, "Error! No hay suficiente cantidad para comprar"}
            };

            return allTest;

        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreatePurchase_Error_test(PurchaseForCreateDTO purchaseDTO, string errorExpected)
        {
            //Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            
            var controller = new PurchasesController(_context, logger);

            //Act
            var result = await controller.CreatePurchase(purchaseDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreatePurchase_Success_test()
        {
            //Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            //Act
            var controller = new PurchasesController(_context, logger);

            var purchaseDTO = new PurchaseForCreateDTO(_customerName, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, new List<PurchaseItemDTO>() { new PurchaseItemDTO(1, 1, 2, _carModel1) });

            var expectedPurchaseDetailDTO = new PurchaseForDetailsDTO(_customerName, _customerSurName, _deliveryAddress, PaymentMethod.TarjetaDeCredito, DateTime.Today, new List<PurchaseItemDTO>() { new PurchaseItemDTO(1, 1, 2, _carModel1) });

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualPurchaseDetailDTO = Assert.IsType<PurchaseForDetailsDTO>(createdResult.Value);

            Assert.Equal(expectedPurchaseDetailDTO, actualPurchaseDetailDTO);
        }


    }
}
