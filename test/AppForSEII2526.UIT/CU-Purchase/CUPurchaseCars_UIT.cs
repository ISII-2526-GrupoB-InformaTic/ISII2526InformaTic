using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Purchase
{
    public class CUPurchaseCars_UIT : UC_UIT
    {
        private SelectCarsForPurchasePO selectCarsForPurchasePO;
        private const string carModel1 = "Ford";
        private const string carColor1 = "Rojo";
        private const string Fueltype1 = "Gasolina";
        private const string carManufacturer1 = "Ford Company";
        private const string Description1 = "5 puertas";
        private const string carPrice1 = "5600";
        private const int carId1 = 1001;
        private const int quantity1 = 1;

        private const string carModel2 = "Toyota";
        private const string carColor2 = "Negro";
        private const string carManufacturer2 = "Toyota";
        private const string Description2 = "4 puertas";
        private const string Fueltype2 = "Electrico";
        private const string carPrice2 = "30750";


        public CUPurchaseCars_UIT(ITestOutputHelper output) : base(output)
        {
            selectCarsForPurchasePO = new SelectCarsForPurchasePO(_driver, _output);
        }

        private void Precondition_perform_login()
        {
            Perform_login("pepeV@uclm.es", "Password1234%");
            System.Threading.Thread.Sleep(2000);
        }

        private void InitialStepsForPurchaseCars()
        {
            Precondition_perform_login();
            selectCarsForPurchasePO.WaitForBeingVisible(By.Id("CreatePurchase"));
            _driver.FindElement(By.Id("CreatePurchase")).Click();

        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC1_AF1_UC1_filtering()
        {
            //Arrange
            InitialStepsForPurchaseCars();
            var expectedCars = new List<string[]> { new string[] { carModel1, carColor1, Fueltype1, carManufacturer1, carPrice1 }, };

            //Act
            selectCarsForPurchasePO.SearchCars("Ford", "");

            //Assert
            Assert.True(selectCarsForPurchasePO.CheckListOfCars(expectedCars));
        }

        [Theory]
        [InlineData(carModel1, carColor1, Fueltype1, carManufacturer1, carPrice1, "Ford", "")]
        [InlineData(carModel2, carColor2, Fueltype2, carManufacturer2, carPrice2, "", carColor2)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF1_filteringbyModelAndColor(string model, string color, string fueltype, string manufacturer,
            string price, string filterModel, string FilterColor)
        {
            //Arrange

            var expectedCars = new List<string[]> { new string[] { model, color, fueltype, manufacturer, price }, };
            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars(filterModel, FilterColor);

            //Assert            
            Assert.True(selectCarsForPurchasePO.CheckListOfCars(expectedCars));

        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF2_NoSelectedCars()
        {
            //Arrange

            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars("", "");
            selectCarsForPurchasePO.SelectCars(new List<string> { carModel1 });
            selectCarsForPurchasePO.ModifyPurchaseCart(carModel1);


            //Assert            
            Assert.True(selectCarsForPurchasePO.ShowPurchaseCart());
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF3_ModifySelectedCars()
        {
            //Arrange

            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars("", "");
            selectCarsForPurchasePO.SelectCars(new List<string> { carModel1, carModel2 });
            selectCarsForPurchasePO.ModifyPurchaseCart(carModel2);


            //Assert            
            Assert.False(selectCarsForPurchasePO.CheckPurchaseCart(carModel2));
        }

        [Theory]
        [InlineData("", "Viyuela", "Calle MiCasa Nº7", "The Name field is required.")]
        [InlineData("Pepe", "", "Calle MiCasa Nº7", "The Surname field is required.")]
        [InlineData("Pepe", "Viyuela", "", "The DeliveryAddress field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF4_testingErrorsMandatorydata(string name, string surname, string deliveryAddress,
        string expectedMessageError)
        {
            //Arrange

            var createpurchase = new CreatePurchasePO(_driver, _output);

            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars("", "");
            selectCarsForPurchasePO.SelectCars(new List<string> { carModel1 });
            selectCarsForPurchasePO.PurchaseCars();
            createpurchase.FillInPurchaseInfo(name, surname, deliveryAddress, "GooglePay");
            createpurchase.PressPurchaseCars();
            //Assert
            //the expected error is shown in the view
            Assert.True(createpurchase.CheckValidationError(expectedMessageError), $"Errors: {expectedMessageError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF5_ModifyPurchaseItems()
        {
            //Arrange

            var createpurchase = new CreatePurchasePO(_driver, _output);

            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars("", "");
            selectCarsForPurchasePO.SelectCars(new List<string> { carModel1, carModel2 });
            selectCarsForPurchasePO.PurchaseCars();
            createpurchase.PressModifyPurchases();
            //we remove movietitle2 from the rentingcart
            selectCarsForPurchasePO.ModifyPurchaseCart(carModel2);
            selectCarsForPurchasePO.PurchaseCars();


            //Assert
            //the list of movies must change
            var expectedPurchaseItems = new List<string[]> { new string[] { carModel1,carColor1, Description1, carPrice1 }, };
            Assert.True(createpurchase.CheckListOfPurchaseItems(expectedPurchaseItems));
        }



        [Theory]
        [InlineData("Pepe", "Viyuela", "Calle MiCasa Nº7", "Visa")]
        [InlineData("Pepe", "Viyuela", "Calle MiCasa Nº7", "GooglePay")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_BasicFlow(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            //Arrange

            var createpurchase = new CreatePurchasePO(_driver, _output);
            var detailPurchase = new DetailPurchasePO(_driver, _output);
            var nameSurname = $"{name} " + $"{surname}";
            var price = 5600;
            var totalPrice = price * quantity1;
            //Act
            InitialStepsForPurchaseCars();

            selectCarsForPurchasePO.SearchCars("", "");
            selectCarsForPurchasePO.SelectCars(new List<string> { carModel1 });
            selectCarsForPurchasePO.PurchaseCars();

            createpurchase.FillInPurchaseInfo(name, surname, deliveryAddress, paymentMethod);
            createpurchase.FillInQuantity(quantity1, carId1);
            createpurchase.PressPurchaseCars();
            createpurchase.PressOkModalDialog();


            //Assert
            //the expected error is shown in the view
            Assert.True(detailPurchase.CheckPurchaseDetail(nameSurname,
                deliveryAddress, totalPrice + " €"),
                "Error: detail purchase is not as expected");

            var expectedPurchaseItems = new List<string[]>
                    { new string[] { carModel1, carPrice1, carColor1 , quantity1.ToString()}, };

            Assert.True(detailPurchase.CheckListOfCars(expectedPurchaseItems),
                "Error: rental items are not as expected");

        }
    }
}

