using System;
using System.Collections.Generic;
using System.Linq;

namespace AppForSEII2526.API.DTOs
{
    public class BookingDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DeliveryAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? ClientPhoneNumber { get; set; }
        public DateTime BookingDate { get; set; }

        public IList<BookingItemDTO> BookingItems { get; set; }

        public int NumberOfDays => BookingItems.Sum(bi => bi.Maintenance.NumberOfDays);
        public int Price => BookingItems.Sum(bi => bi.Maintenance.Price);

        public BookingDetailDTO(
            int id, string name, string surname,
            string deliveryAddress, PaymentMethod paymentMethod,
            string? clientPhoneNumber, DateTime bookingDate,
            IList<BookingItemDTO> bookingItemDTOs)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            ClientPhoneNumber = clientPhoneNumber;
            BookingDate = bookingDate.ToUniversalTime();
            BookingItems = bookingItemDTOs;
        }

        public override bool Equals(object? obj)
        {
            return obj is BookingDetailDTO dTO &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   ClientPhoneNumber == dTO.ClientPhoneNumber &&
                   BookingDate == dTO.BookingDate &&
                   BookingItems.SequenceEqual(dTO.BookingItems) &&
                   NumberOfDays == dTO.NumberOfDays &&
                   Price == dTO.Price;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(ClientPhoneNumber);
            hash.Add(BookingDate);
            hash.Add(BookingItems);
            hash.Add(NumberOfDays);
            hash.Add(Price);
            return hash.ToHashCode();
        }
    }
}