using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class BookingForCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name and Surname must have at least 10 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name and Surname must have at least 10 characters")]
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Delivery address must have at least 10 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public string? clientPhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, place a Comment")]
         [StringLength(50, MinimumLength = 10, ErrorMessage = "The Comment must be at least 10 digits long")]
        public IList<BookingItemDTO> BookingItems { get; set; }
        public int numberOfDays {
            get { return BookingItems.Sum(ri => ri.Maintenance.NumberOfDays); }
        }
        public int Price
        {
            get { return BookingItems.Sum(ri => ri.Maintenance.Price); }
        }

        public BookingForCreateDTO(string name, string surname, string deliveryAddress, PaymentMethod paymentMethod, string? clientPhoneNumber, IList<BookingItemDTO> bookingItems)
        {
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            this.clientPhoneNumber = clientPhoneNumber;
            BookingItems = bookingItems;
        }

        public BookingForCreateDTO()
        {
            BookingItems = new List<BookingItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is BookingForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   clientPhoneNumber == dTO.clientPhoneNumber &&
                   Price == dTO.Price &&
                   numberOfDays == dTO.numberOfDays &&
                   BookingItems.SequenceEqual( dTO.BookingItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DeliveryAddress, PaymentMethod, clientPhoneNumber,Price,numberOfDays, BookingItems);
        }
    }


}
