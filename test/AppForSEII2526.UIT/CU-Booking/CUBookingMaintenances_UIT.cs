using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Booking
{
    public class CUBookingMaintenances_UIT : UC_UIT
    {
        private SelectMaintenancesForBookingPO selectMaintenancesForBookingPO;
        private const string maintName1 = "A-312";
        private const string type1 = "Cambio de neumaticos, Cambio de aceite";
        private const string days1 = "4";
        private const string price1 = "100";
        private const int maintId1 = 1;
        private const string comment1 = "la verdad es que esta considerado una obra maestra entre todo lo que es mantenimientear";

        private const string maintName2 = "B-312";
        private const string type2 = "Cambio de neumaticos";
        private const string days2 = "8";
        private const string price2 = "150";
        public CUBookingMaintenances_UIT(ITestOutputHelper output) : base(output)
        {
            selectMaintenancesForBookingPO = new SelectMaintenancesForBookingPO(_driver, _output);
        }

        private void Precondition_perform_login()
        {
            Perform_login("pepeV@uclm.es", "Password1234%");
            System.Threading.Thread.Sleep(2000);
        }

        private void InitialStepsForBookingMaintenances()
        {
            Precondition_perform_login();
            selectMaintenancesForBookingPO.WaitForBeingVisible(By.Id("CreateBooking"));
            _driver.FindElement(By.Id("CreateBooking")).Click();

        }

        [Theory]
        [InlineData(maintName1, type1, days1, price1, "aceite", "A-31")]
        [InlineData(maintName2, type2, days2, price2, "", "B-312")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF0_filteringbyNameAndType(string name, string type, string days,
            string price, string filterType, string filterName)
        {
            //Arrange

            var expectedMaintenances = new List<string[]> { new string[] { name, type, days, price , "Add"}, };
            //Act
            InitialStepsForBookingMaintenances();

            selectMaintenancesForBookingPO.SearchMaintenances(filterType, filterName);

            //Assert            
            Assert.True(selectMaintenancesForBookingPO.CheckListOfMaintenances(expectedMaintenances));

        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF1_NoSelectedMaintenances()
        {
            //Arrange
            //Act
            InitialStepsForBookingMaintenances();
            selectMaintenancesForBookingPO.SearchMaintenances("", "");
            selectMaintenancesForBookingPO.SelectMaintenances(new List<string> { maintName1 });
            selectMaintenancesForBookingPO.ModifyBookingCart(maintName1);


            //Assert            
            Assert.True(selectMaintenancesForBookingPO.ShowBookingCart());
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF2_ModifySelectedMaintenances()
        {
            //Arrange
            //Act
            InitialStepsForBookingMaintenances();


            selectMaintenancesForBookingPO.SearchMaintenances("", "");
            selectMaintenancesForBookingPO.SelectMaintenances(new List<string> { maintName1, maintName2 });
            selectMaintenancesForBookingPO.ModifyBookingCart(maintName2);


            //Assert            
            Assert.False(selectMaintenancesForBookingPO.CheckBookingCart(maintName2));
        }
        [Theory]
        [InlineData("", "Viyuela", "Calle MiCasa Nº7","967967967", "The Name field is required.")]
        [InlineData("Pepe", "", "Calle MiCasa Nº7","", "The Surname field is required.")]
        [InlineData("Pepe", "Viyuela", "","967967967", "The DeliveryAddress field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF3_testingErrorsMandatorydata(string name, string surname, string deliveryAddress, string phonenumber,
        string expectedMessageError)
        {
            //Arrange

            var createbooking = new CreateBookingPO(_driver, _output);

            //Act
            InitialStepsForBookingMaintenances();

            selectMaintenancesForBookingPO.SearchMaintenances("", "");
            selectMaintenancesForBookingPO.SelectMaintenances(new List<string> { maintName1 });
            selectMaintenancesForBookingPO.BookMaintenances();
            createbooking.FillInBookingInfo(name, surname, deliveryAddress, "CreditCard",phonenumber);
            createbooking.FillInElement(comment1.ToString(), maintId1);
            createbooking.PressBookMaintenances();

            //Assert
            //the expected error is shown in the view
            Assert.True(createbooking.CheckValidationError(expectedMessageError), $"Errors: {expectedMessageError}");
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF4_ModifyBookingItems()
        {
            //Arrange

            var createbooking = new CreateBookingPO(_driver, _output);

            //Act
            InitialStepsForBookingMaintenances();

            selectMaintenancesForBookingPO.SearchMaintenances("", "");
            selectMaintenancesForBookingPO.SelectMaintenances(new List<string> { maintName1, maintName2 });
            selectMaintenancesForBookingPO.BookMaintenances();
            createbooking.PressModifyMaintenances();
            //we remove movietitle2 from the rentingcart
            selectMaintenancesForBookingPO.ModifyBookingCart(maintName2);
            selectMaintenancesForBookingPO.BookMaintenances();

            //Assert
            //the list of movies must change
            var expectedBookingItems = new List<string[]> { new string[] { maintName1, price1, days1 }, };
            Assert.True(createbooking.CheckListOfBookingItems(expectedBookingItems));
        }
        [Theory]
        [InlineData("Pepe", "Viyuela", "Calle MiCasa Nº7", "Visa","967967967")]
        [InlineData("Pepe", "Viyuela", "Calle MiCasa Nº7", "GooglePay","")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_BasicFlow(string name, string surname, string deliveryAddress, string paymentMethod, string phonenumber)
        {
            //Arrange
            var days = (DateTime.Today.AddDays(8) - DateTime.Today.AddDays(1)).Days;

            var createbooking = new CreateBookingPO(_driver, _output);
            var detailbooking = new DetailBookingPO(_driver, _output);
            var nameSurname = $"{name} " + $"{surname}";
            var totaldays= "4";
            var totalprice = 100;
            //Act
            InitialStepsForBookingMaintenances();

            selectMaintenancesForBookingPO.SearchMaintenances("", "");
            selectMaintenancesForBookingPO.SelectMaintenances(new List<string> { maintName1 });
            selectMaintenancesForBookingPO.BookMaintenances();

            createbooking.FillInBookingInfo(name, surname, deliveryAddress, paymentMethod, phonenumber);
            createbooking.FillInElement(comment1.ToString(), maintId1);
            createbooking.PressBookMaintenances();
            createbooking.PressOkModalDialog();


            //Assert
            Assert.True(detailbooking.CheckBookingDetail(nameSurname,
                deliveryAddress, paymentMethod,totaldays, totalprice + " €"));

            var expectedBookingItems = new List<string[]>
                    { new string[] { maintName1, price1+"€", days1, comment1.ToString()} };

            Assert.True(detailbooking.CheckListOfMaintenances(expectedBookingItems));

        }

    }
}
