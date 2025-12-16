using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class CURentalCars_UIT : UC_UIT
    {
        private SelectCarsForRentalPO selectCarsForRentalPO;
        private const string carModel1 = "Ford";
        private const string carManufacturer1 = "Ford Company";
        private const string carPrice1 = "800";
        private const int carId1 = 1001;
        private const int quantity1 = 1;

        private const string carModel2 = "Toyota";
        private const string carManufacturer2 = "Toyota";
        private const string carPrice2 = "2000";

        
        public CURentalCars_UIT(ITestOutputHelper output) : base(output)
        {
            selectCarsForRentalPO = new SelectCarsForRentalPO(_driver, _output);
        }

        private void Precondition_perform_login()
        {
            Perform_login("pepeV@uclm.es", "Password1234%");
            System.Threading.Thread.Sleep(2000);
        }

        private void InitialStepsForRentalCars()
        {
            Precondition_perform_login();
            selectCarsForRentalPO.WaitForBeingVisible(By.Id("CreateRental"));
            _driver.FindElement(By.Id("CreateRental")).Click();

        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC2_AF1_UC2_filtering()
        {
            //Arrange
            InitialStepsForRentalCars();
            var expectedCars = new List<string[]> { new string[] { carModel1, carManufacturer1, carPrice1 }, };

            //Act
            selectCarsForRentalPO.SearchCars("Ford", "", "");

            //Assert
            Assert.True(selectCarsForRentalPO.CheckListOfCars(expectedCars));
        }

        [Theory]
        [InlineData(carModel1, carManufacturer1, carPrice1, "Ford", "","")]
        [InlineData(carModel2, carManufacturer2, carPrice2, "", carPrice2, "")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_filteringbyModelAndPrice(string model, string manufacturer,
            string price, string filterModel, string filterMinPrice, string filterMaxPrice)
        {
            //Arrange

            var expectedCars = new List<string[]> { new string[] { model, manufacturer, price }, };
            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars(filterModel, filterMinPrice, filterMaxPrice);

            //Assert            
            Assert.True(selectCarsForRentalPO.CheckListOfCars(expectedCars));

        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF2_NoSelectedCars()
        {
            //Arrange

            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars("", "", "");
            selectCarsForRentalPO.SelectCars(new List<string> { carModel1});
            selectCarsForRentalPO.ModifyRentalCart(carModel1);


            //Assert            
            Assert.True(selectCarsForRentalPO.ShowRentalCart());
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF3_ModifySelectedCars()
        {
            //Arrange

            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars("","","");
            selectCarsForRentalPO.SelectCars(new List<string> { carModel1, carModel2 });
            selectCarsForRentalPO.ModifyRentalCart(carModel2);


            //Assert            
            Assert.False(selectCarsForRentalPO.CheckRentalCart(carModel2));
        }

        [Theory]
        [InlineData("","Viyuela" ,"Calle MiCasa Nº7", "(*) Please, set your Name")]
        [InlineData("Pepe", "", "Calle MiCasa Nº7", "(*) Please, set your Surname")]
        [InlineData("Pepe", "Viyuela", "", "(*) Please, set your address for delivery")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF4_testingErrorsMandatorydata(string name,string surname, string deliveryAddress,
        string expectedMessageError)
        {
            //Arrange

            var createrental = new CreateRentalPO(_driver, _output);

            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars("", "", "");
            selectCarsForRentalPO.SelectCars(new List<string> { carModel1 });
            selectCarsForRentalPO.RentCars();
            createrental.FillInRentalInfo(name,surname, deliveryAddress, "GooglePay");
            createrental.PressRentCars();
            createrental.PressOkModalDialog();
            //Assert
            //the expected error is shown in the view
            Assert.True(createrental.CheckValidationError(expectedMessageError), $"Errors: {expectedMessageError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF5_ModifyRentalItems()
        {
            //Arrange

            var createrental = new CreateRentalPO(_driver, _output);

            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars("", "", "");
            selectCarsForRentalPO.SelectCars(new List<string> { carModel1, carModel2 });
            selectCarsForRentalPO.RentCars();
            createrental.PressModifyRentals();
            //we remove movietitle2 from the rentingcart
            selectCarsForRentalPO.ModifyRentalCart(carModel2);
            selectCarsForRentalPO.RentCars();


            //Assert
            //the list of movies must change
            var expectedRentalItems = new List<string[]> { new string[] { carModel1, carManufacturer1, carPrice1 }, };
            Assert.True(createrental.CheckListOfRentalItems(expectedRentalItems));
        }



        [Theory]
        [InlineData("Pepe","Viyuela", "Calle MiCasa Nº7", "Visa")]
        [InlineData("Pepe", "Viyuela", "Calle MiCasa Nº7", "GooglePay")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_BasicFlow(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            //Arrange
            var days = (DateTime.Today.AddDays(8)-DateTime.Today.AddDays(1)).Days; 
                   
        var createrental = new CreateRentalPO(_driver, _output);
            var detailRental = new DetailRentalPO(_driver, _output);
            var nameSurname = $"{name} " + $"{surname}";
            var price = 800;
            var totalPrice = price * quantity1 * days; 
            //Act
            InitialStepsForRentalCars();

            selectCarsForRentalPO.SearchCars("", "", "");
            selectCarsForRentalPO.SelectCars(new List<string> { carModel1 });
            selectCarsForRentalPO.RentCars();

            createrental.FillInRentalInfo(name,surname, deliveryAddress, paymentMethod);
            createrental.PressRentCars();
            createrental.PressOkModalDialog();


            //Assert
            //the expected error is shown in the view
            Assert.True(detailRental.CheckRentalDetail(nameSurname,
                deliveryAddress, paymentMethod, totalPrice + " €"),
                "Error: detail rental is not as expected");

            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, carManufacturer1, carPrice1 , quantity1.ToString()}, };

            Assert.True(detailRental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected");

        }
    }
}
