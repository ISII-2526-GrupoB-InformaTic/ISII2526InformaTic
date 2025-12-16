using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.CU_Booking
{
    internal class SelectMaintenancesForBookingPO : PageObject
    {
        private By inputType = By.Id("inputType");
        private By Name = By.Id("Name");
        private By buttonsearchMaintenances = By.Id("searchMaintenances");
        private By buttonbookMaintenances = By.Id("bookMaintenanceButton");
        private By showBookingCart = By.Id("bookingCart");
        private By tableofMaintenances = By.Id("TableOfMaintenances");
        public SelectMaintenancesForBookingPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchMaintenances(string type, string name) {
            WaitForBeingVisible(inputType);
            WaitForBeingVisible(Name);
            _driver.FindElement(inputType).SendKeys(type);
            _driver.FindElement(Name).SendKeys(name);
            _driver.FindElement(buttonsearchMaintenances).Click();
            System.Threading.Thread.Sleep(2000);
        }
        public void SelectMaintenances(List<string> maintenanceNames) {
            foreach (var maintenanceName in maintenanceNames) {
                WaitForBeingVisible(By.Id($"maintenanceToBook_{maintenanceName}"));
                _driver.FindElement(By.Id($"maintenanceToBook_{maintenanceName}")).Click();
            }
        }
        public void BookMaintenances() {
            WaitForBeingClickable(buttonbookMaintenances);
            _driver.FindElement(buttonbookMaintenances).Click();
        }

        public void ModifyBookingCart(string name)
        {
            WaitForBeingVisible(showBookingCart);
            WaitForBeingVisible(By.Id($"removeMaintenance_{name}"));
            _driver.FindElement(By.Id($"removeMaintenance_{name}")).Click();
        }
        public bool CheckListOfMaintenances(List<string[]> expectedMaintenances)
        {
            return CheckBodyTable(expectedMaintenances, tableofMaintenances);
        }

        public bool CheckBookingCart(string name)
        {
            return _driver.FindElement(showBookingCart).Text.Contains(name);
        }

        public bool ShowBookingCart()
        {
            return !_driver.FindElement(showBookingCart).Displayed;
        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, By.Id("DialogOKSaveDelete"));
        }
    }
}
