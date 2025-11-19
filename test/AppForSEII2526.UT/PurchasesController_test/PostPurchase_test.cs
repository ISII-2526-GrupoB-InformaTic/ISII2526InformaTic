using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class PostPurchase_test : AppForSEII25264SqliteUT
    {
        /*

        private const string _userName = "elena.navarro@uclm.es";
        private const string _customerName = "Elena ";
        private const string _customerSurName = "Navarro Martínez";
        private const string _deliveryAddress = "Avda. España s/n, Albacete 02071";

        private const string _carModel1 = "Model X";
        private const string _carColor1 = "Rojo";
        private const string _carModel2 = "Mondeo";
        private const string _carColor2 = "Azul";

        public PostPurchase_test()
        {

            var modelo = new List<Model>()
            {
                new Model(1,_carModel1, null),
                new Model(2,_carModel2,null)

            };

            var coche = new List<Car>()
            {

                new Car("coche", _carColor1, "Un coche rojo", "Ford", "", 1, 5000, 2000, 50000, 30000, modelo[0], null, null),
                new Car("coche",_carColor2, "Un coche azul", "Citroen", "", 2, 5000, 2000, 60000, 30000, modelo[1], null, null)

            };

            ApplicationUser user = new ApplicationUser("1", _customerName, _customerSurName,_userName, _deliveryAddress);

            var purchase = new Purchase(_customerName, PaymentMethod.TarjetaDeCredito, DateTime.Now,
                            50000, 1, new List<PurchaseItem>(), user);


            purchase.purchaseItems.Add(new PurchaseItem(coche[0].Id, purchase.Id, 
                                       coche[0].QuantityForPurchasing, coche[0], purchase));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(modelo);
            _context.AddRange(coche);
            _context.Add(purchase);
            _context.SaveChanges();

        }


      /*  public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {

            var purchaseNoItem = new PurchaseForCreateDTO(_carColor1, "", 50000, 
                                 _customerName, _customerSurName, _deliveryAddress, 
                                 PaymentMethod.TarjetaDeCredito,
                                 new List<PurchaseItemDTO>());

            var purchaseItems = new List<PurchaseItemDTO>() {new PurchaseItemDTO(1, 1, 25000)};

        }
      */

    }
}
