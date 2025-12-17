using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Purchase
{
    public class SelectCarsForPurchasePO : PageObject
    {
        private By inputModel = By.Id("inputModel");
        private By inputColor = By.Id("inputColor");
        private By buttonSearchCars = By.Id("searchCars");
        private By buttonPurchaseCars = By.Id("PurchaseCarButton");
        private By showPurchaseCart = By.Id("PurchaseCart");
        private By tableofCarsBy = By.Id("TableOfCars");
        public SelectCarsForPurchasePO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void SearchCars(string model, string color)
        {
            WaitForBeingVisible(inputModel);
            WaitForBeingVisible(inputColor);
            _driver.FindElement(inputModel).SendKeys(model);
            _driver.FindElement(inputColor).SendKeys(color);
            _driver.FindElement(buttonSearchCars).Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void SelectCars(List<string> carModels)
        {
            foreach (var carModel in carModels)
            {
                WaitForBeingVisible(By.Id($"carToPurchaseButton_{carModel}"));
                _driver.FindElement(By.Id($"carToPurchaseButton_{carModel}")).Click();
            }
        }
        public void PurchaseCars()
        {
            WaitForBeingClickable(buttonPurchaseCars);
            _driver.FindElement(buttonPurchaseCars).Click();
        }

        public void ModifyPurchaseCart(string model)
        {
            WaitForBeingVisible(showPurchaseCart);
            WaitForBeingVisible(By.Id($"removeCar_{model}"));
            _driver.FindElement(By.Id($"removeCar_{model}")).Click();
        }


        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableofCarsBy);
        }

        public bool CheckPurchaseCart(string model)
        {
            return _driver.FindElement(showPurchaseCart).Text.Contains(model);
        }

        public bool ShowPurchaseCart()
        {
            return !_driver.FindElement(showPurchaseCart).Displayed;
        }
        public bool CheckMessageErrorCarNotAvailable(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, By.Id("DialogOKSaveDelete"));
        }
    }
}
