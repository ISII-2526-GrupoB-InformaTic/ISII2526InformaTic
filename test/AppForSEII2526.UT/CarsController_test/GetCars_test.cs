using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCars_test: AppForSEII25264SqliteUT
   {

        //Se definen los datos de prueba que seran reutilizados entre varias pruebas (ok, bad rquest) por  eso se definen antes
        public GetCars_test()
        {

            var modelo = new List<Model>()
            {
                new Model(){ Name="Model X" },
                new Model(){ Name="Mustang" },
                new Model(){ Name="Civic" },
                new Model(){ Name="Corolla" },
                new Model(){ Name="Camry" }

            };

            var coche = new List<Car>()
            {

                new Car(){ Model = modelo[0], Color="Red", Description="Electric SUV", PurchasingPrice=80000, QuantityForPurchasing = 50 },
                new Car(){ Model = modelo[1], Color="Blue", Description="Sporty Coupe", PurchasingPrice=55000, QuantityForPurchasing =30 },
                new Car(){ Model = modelo[2], Color="Black", Description="Compact Sedan", PurchasingPrice=25000, QuantityForPurchasing =80 },
                new Car(){ Model = modelo[3], Color="White", Description="Reliable Sedan", PurchasingPrice=24000, QuantityForPurchasing = 70 },
                new Car(){ Model = modelo[4], Color="Silver", Description="Mid-size Sedan", PurchasingPrice=30000, QuantityForPurchasing = 40 }

            };

            ApplicationUser user = new ApplicationUser("1", "Elena", "Navarro Martinez", "elena@uclm.es","Calle Callejon");

            var Purchase = new Purchase("Elena Navarro", PaymentMethod.TarjetaDeCredito, DateTime.Now, 55000, 1, new List<PurchaseItem>(), user);
            var purchaseItem = new PurchaseItem(1, 1, 30, coche[1], Purchase);

            _context.Add(user);
            _context.Add(modelo);
            _context.Add(coche);
            _context.Add(purchaseItem);
            _context.Add(Purchase);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> GetCars_TestData()
        {
            //Datos de prueba para el metodo GetCars
            yield return new object[]
            {
                /*
                new List<CarForPurchasingDTO>()
                {
                    new CarForPurchasingDTO(){ Id=1, ModelName="Model X", Color="Red", Description="Electric SUV", PurchasingPrice=80000, QuantityForPurchasing = 50 },
                    new CarForPurchasingDTO(){ Id=2, ModelName="Mustang", Color="Blue", Description="Sporty Coupe", PurchasingPrice=55000, QuantityForPurchasing =30 },
                    new CarForPurchasingDTO(){ Id=3, ModelName="Civic", Color="Black", Description="Compact Sedan", PurchasingPrice=25000, QuantityForPurchasing =80 },
                    new CarForPurchasingDTO(){ Id=4, ModelName="Corolla", Color="White", Description="Reliable Sedan", PurchasingPrice=24000, QuantityForPurchasing = 70 },
                    new CarForPurchasingDTO(){ Id=5, ModelName="Camry", Color="Silver", Description="Mid-size Sedan", PurchasingPrice=30000, QuantityForPurchasing = 40 }
                }
                */
            };
        }

    }
}
