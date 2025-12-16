using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Booking
{
    internal class DetailBookingPO : PageObject
    {
        public DetailBookingPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckBookingDetail(string nameSurname, string delivery, string paymentmethod, string totalDays, string totalprice)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nameSurname);
            if(result) _output.WriteLine("TRUEname");
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);
            if (result) _output.WriteLine("TRUEdeli");
            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(paymentmethod);
            if (result) _output.WriteLine("TRUEpay");
            result = result && _driver.FindElement(By.Id("BookingDays")).Text.Contains(totalDays);
            if (result) _output.WriteLine("TRUEday");
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalprice);
            if (result) _output.WriteLine("TRUEprice");


            return result;

        }
        public bool CheckListOfMaintenances(List<string[]> expectedBookingItems)
        {
            return CheckBodyTable(expectedBookingItems, By.Id("BookedMaintenaces"));
        }

    }
}
