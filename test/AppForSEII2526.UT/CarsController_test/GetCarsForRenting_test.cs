using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForRenting_test : AppForSEII25264SqliteUT
    {
        public GetCarsForRenting_test() {
            //Arrange
            var models = new List<Model>()
            {
                new Model {Name="Toyota R"},
                new Model {Name="Toyota A"},
                new Model {Name = "Toyota V"}
            }; 
            var cars = new List<Car>()
            {
                new Car {carClass = "coche",Color="rojo",Description="un coche rojo",Manufacturer="Toyota",
                    ReviewItems="???",QuantityForRenting= 1, RentingPrice=2000,Model= models[0] },
                new Car {carClass = "coche",Color="amarillo",Description="un coche amarillo",Manufacturer="Toyota",
                    ReviewItems="???",QuantityForRenting= 1, RentingPrice=3000,Model= models[1] },
                new Car {carClass = "coche",Color="verde",Description="un coche verde",Manufacturer="Toyota",
                    ReviewItems="???",QuantityForRenting= 1, RentingPrice=1500,Model= models[2] }
            };

            ApplicationUser user = new ApplicationUser(1, "Pepe", "Viyuela", "pepeV@uclm.es");

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var numDays = (endDate - startDate).Days;

            var rental = new Rental(endDate,startDate, DateTime.Now, (cars[1].RentingPrice * numDays), "Tony", new List<RentalItem>(), PaymentMethod.TarjetaDeCredito);

            var rentalItem = new RentalItem(1, 5, 1, cars[0], rental);

            rental.RentalItems.Add(rentalItem);

        }
    }
}
