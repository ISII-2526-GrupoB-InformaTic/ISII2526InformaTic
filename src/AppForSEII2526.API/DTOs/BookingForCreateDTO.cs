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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name and Surname must have at least 3 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name and Surname must have at least 3 characters")]
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Delivery address must have at least 3 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public string? clientPhoneNumber { get; set; }
        public IList<BookingItemDTO> BookingItems { get; set; }

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
                   BookingItems.SequenceEqual( dTO.BookingItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DeliveryAddress, PaymentMethod, clientPhoneNumber, BookingItems);
        }
    }


}
