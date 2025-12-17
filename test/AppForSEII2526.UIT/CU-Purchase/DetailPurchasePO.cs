using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Purchase
{
    public class DetailPurchasePO : PageObject
    {
        public DetailPurchasePO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckPurchaseDetail(string nameSurname, string delivery, string totalprice)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nameSurname);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalprice);


            return result;

        }

        public bool CheckListOfCars(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("PurchasedCars"));
        }
    }
}
