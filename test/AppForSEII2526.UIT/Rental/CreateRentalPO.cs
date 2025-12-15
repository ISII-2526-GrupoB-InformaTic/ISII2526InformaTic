using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class CreateRentalPO : PageObject
    {
        private By _name = By.Id("Name");
        private By _surname = By.Id("Surname");
        private By _address= By.Id("DeliveryAddress");
        private By _paymentMethod = By.Id("PaymentMethod");
        private By _errors = By.Id("ErrorsShown");
        public CreateRentalPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void FillInRentalInfo(string name,string surname, string deliveryAddress, string paymentMethod)
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


        public void PressRentCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }



        public void PressModifyRentals()
        {
            _driver.FindElement(By.Id("ModifyRentals")).Click();
        }

        public bool CheckListOfRentalItems(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("TableOfRentalItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            //return _driver.FindElement(_errors).Text.Contains(expectedError);
            return _driver.PageSource.Contains(expectedError);
        }
    }
}
