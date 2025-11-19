using System;
using System.Collections.Generic;
using System.Linq;

namespace AppForSEII2526.API.DTOs
{
    public class BookingDetailDTO : BookingForCreateDTO
    {
        public BookingDetailDTO(int id,string name, string surname, string deliveryAddress, PaymentMethod paymentMethod, string? clientPhoneNumber, DateTime bookingDate, IList<BookingItemDTO> bookingItemDTOs):base(name, surname, deliveryAddress, paymentMethod, clientPhoneNumber, bookingItemDTOs)
        {
            Id = id;
            BookingItems = bookingItemDTOs;
            BookingDate = bookingDate.ToUniversalTime();
        }
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingDetailDTO dTO &&
                   base.Equals(obj) &&
                   Price == dTO.Price &&
                     Id == dTO.Id &&
                     BookingDate.ToUniversalTime() == dTO.BookingDate.ToUniversalTime();

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id, BookingDate);
        }
    }
}