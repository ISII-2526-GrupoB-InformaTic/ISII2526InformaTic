using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Booking
{
    internal class CreateBookingPO : PageObject
    {
        private By _name = By.Id("Name");
        private By _surname = By.Id("Surname");
        private By _address = By.Id("DeliveryAddress");
        private By _paymentMethod = By.Id("PaymentMethod");
        private By _phonenumber = By.Id("ClientPhoneNumber");
        private By _errors = By.Id("ErrorsShown");
        public CreateBookingPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void FillInBookingInfo(string name, string surname, string deliveryAddress, string paymentMethod, string? phoneNumber)
        {
            WaitForBeingVisible(_name);
            _driver.FindElement(_name).SendKeys(name);
            _driver.FindElement(_surname).SendKeys(surname);
            _driver.FindElement(_address).SendKeys(deliveryAddress);
            _driver.FindElement(_phonenumber).SendKeys(phoneNumber);

            //create select element object 
            SelectElement selectElement = new SelectElement(_driver.FindElement(_paymentMethod));

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInElement(string comment, int maintenanceId)
        {
            _driver.FindElement(By.Id("Comment_" + maintenanceId)).Clear();
            _driver.FindElement(By.Id("Comment_" + maintenanceId)).SendKeys(comment);
        }


        public void PressBookMaintenances()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }



        public void PressModifyMaintenances()
        {
            _driver.FindElement(By.Id("ModifyMaintenances")).Click();
        }

        public bool CheckListOfBookingItems(List<string[]> expectedBookingItems)
        {
            return CheckBodyTable(expectedBookingItems, By.Id("TableOfBookingItems"));
        }

        public bool CheckValidationError(string expectedError)
        {

            //return _driver.FindElement(_errors).Text.Contains(expectedError);
            return _driver.PageSource.Contains(expectedError);
        }
    }
}
