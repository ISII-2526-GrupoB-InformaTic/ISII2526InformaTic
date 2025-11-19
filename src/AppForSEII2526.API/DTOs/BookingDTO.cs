using System;
using System.Collections.Generic;
using System.Linq;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.DTOs
{
    public class BookingDTO
    {
        public BookingDTO()
        {

        }
        public BookingDTO(DateTime Date, int Id, PaymentMethod PaymentMethod, ApplicationUser usuario) : base()
        {
            this.Date = Date;
            this.Id = Id;
            this.PaymentMethod = PaymentMethod;
            this.User = usuario;
            clientAdress = usuario.DeliveryAddress;
            clientName = usuario.UserName;
            clientSurname = usuario.Surname;
            clientPhoneNumber = usuario.PhoneNumber;
        }
        public BookingDTO(DateTime Date, int Id, PaymentMethod PaymentMethod, IList<BookingItemDTO> BookingItems, ApplicationUser usuario) : base()
        {
            this.Date = Date;
            this.Id = Id;
            this.PaymentMethod = PaymentMethod;
            this.BookingItems = BookingItems;
            this.User = usuario;
            clientAdress = usuario.DeliveryAddress;
            clientName = usuario.UserName;
            clientSurname = usuario.Surname;
            clientPhoneNumber = usuario.PhoneNumber;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "La calle debe tener menos de 30 caracteres y mas de 4.", MinimumLength = 4)]
        public string clientAdress { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientName { get; set; }
        [StringLength(12, ErrorMessage = "El telefono movil debe tener entre 9 y 12 digitos", MinimumLength = 9)]
        public string? clientPhoneNumber { get; set; }
        [StringLength(10, ErrorMessage = "El apellido debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientSurname { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IList<BookingItemDTO> BookingItems { get; set; } = new List<BookingItemDTO>();

        public ApplicationUser User { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingDTO dTO &&
                   Id == dTO.Id &&
                   clientAdress == dTO.clientAdress &&
                   clientName == dTO.clientName &&
                   clientPhoneNumber == dTO.clientPhoneNumber &&
                   clientSurname == dTO.clientSurname &&
                   Date == dTO.Date &&
                   PaymentMethod == dTO.PaymentMethod &&
                   BookingItems.SequenceEqual( dTO.BookingItems) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, dTO.User);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(clientAdress);
            hash.Add(clientName);
            hash.Add(clientPhoneNumber);
            hash.Add(clientSurname);
            hash.Add(Date);
            hash.Add(PaymentMethod);
            hash.Add(BookingItems);
            hash.Add(User);
            return hash.ToHashCode();
        }
    }
}