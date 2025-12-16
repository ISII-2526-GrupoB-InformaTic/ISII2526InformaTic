using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class DetailRentalPO : PageObject
    {
        public DetailRentalPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckRentalDetail(string nameSurname, string delivery, string paymentmethod, string totalprice)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nameSurname);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);
            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(paymentmethod);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalprice);


            return result;

        }

        public bool CheckListOfCars(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("RentedCars"));
        }
    }
}
