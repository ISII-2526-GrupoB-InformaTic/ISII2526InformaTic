using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class SelectCarsForRentalPO : PageObject
    {
        private By inputModel = By.Id("inputModel");
        private By inputMinPrice = By.Id("inputminPrice");
        private By inputMaxPrice = By.Id("inputmaxPrice");
        private By buttonSearchCars = By.Id("searchCars");
        private By buttonRentCars = By.Id("rentCarButton");
        private By showRentalCart = By.Id("rentalCart");
        private By tableofCarsBy = By.Id("TableOfCars");
        public SelectCarsForRentalPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void SearchCars(string model, string minPrice, string maxPrice)
        {
            WaitForBeingVisible(inputModel);
            WaitForBeingVisible(inputMinPrice);
            WaitForBeingVisible(inputMaxPrice);
            _driver.FindElement(inputModel).SendKeys(model);
            _driver.FindElement(inputMinPrice).SendKeys(minPrice);
            _driver.FindElement(inputMaxPrice).SendKeys(maxPrice);
            _driver.FindElement(buttonSearchCars).Click();
        }

        public void SelectCars(List<string> carModels)
        {
            foreach (var carModel in carModels)
            {
                WaitForBeingVisible(By.Id($"carToRentButton_{carModel}"));
                _driver.FindElement(By.Id($"carToRentButton_{carModel}")).Click();
            }
        }
        public void RentCars()
        {
            WaitForBeingClickable(buttonRentCars);
            _driver.FindElement(buttonRentCars).Click();
        }

        public void ModifyRentalCart(string model)
        {
            WaitForBeingVisible(showRentalCart);
            WaitForBeingVisible(By.Id($"removeCar_{model}"));
            _driver.FindElement(By.Id($"removeCar_{model}")).Click();
        }


        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableofCarsBy);
        }

        public bool CheckRentalCart(string model)
        {
            return _driver.FindElement(showRentalCart).Text.Contains(model);
        }

        public bool ShowRentalCart()
        {
            return !_driver.FindElement(showRentalCart).Displayed;
        }
        public bool CheckMessageErrorCarNotAvailable(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError,By.Id("DialogOKSaveDelete"));
        }
    }
}
