using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Purchase
{
    public class CreatePurchasePO : PageObject
    {
        private By _name = By.Id("Name");
        private By _surname = By.Id("Surname");
        private By _address = By.Id("DeliveryAddress");
        private By _paymentMethod = By.Id("PaymentMethod");
        private By _submit = By.Id("Submit");

        public CreatePurchasePO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void FillInPurchaseInfo(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            WaitForBeingVisible(_name);
            _driver.FindElement(_name).SendKeys(name);
            _driver.FindElement(_surname).SendKeys(surname);
            _driver.FindElement(_address).SendKeys(deliveryAddress);

            //create select element object 
            SelectElement selectElement = new SelectElement(_driver.FindElement(_paymentMethod));

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInQuantity(int Quantity, int carId)
        {
            _driver.FindElement(By.Id("Quantity_" + carId)).Clear();
            _driver.FindElement(By.Id("Quantity_" + carId)).SendKeys(Quantity.ToString());
        }


        public void PressPurchaseCars()
        {
            _driver.FindElement(_submit).Click();
        }



        public void PressModifyPurchases()
        {
            _driver.FindElement(By.Id("ModifyPurchases")).Click();
        }

        public bool CheckListOfPurchaseItems(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("TableOfPurchaseItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            //return _driver.FindElement(_errors).Text.Contains(expectedError);
            return _driver.PageSource.Contains(expectedError);
        }
    }
}

