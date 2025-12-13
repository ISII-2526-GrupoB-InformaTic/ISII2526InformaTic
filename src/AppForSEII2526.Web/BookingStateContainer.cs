using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class BookingStateContainer
    {//we create an instance of Rental when an instance of RentalStateContainer is created
        public BookingForCreateDTO Booking { get; private set; } = new BookingForCreateDTO()
        {
            BookingItems = new List<BookingItemDTO>()
        };

        //we compute the TotalPrice of the movies we have selected for renting them
        public int TotalPrice
        {
            get
            {
                return Booking.BookingItems.Sum(ri =>ri.Price);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddMaintenance(MaintenanceDTO maintenance)
        {
            //before adding a movie we checked whether it has been already added
            if (!Booking.BookingItems.Any(ri => ri.MaintenanceId == maintenance.Id))
                //we add it if it is not in the list
                Booking.BookingItems.Add(new BookingItemDTO()
                {
                    MaintenanceId = maintenance.Id,
                    MaintName = maintenance.Name,
                    Price = maintenance.Price
                }
            );
            NotifyStateChanged();

        }

        //to delete movies from the list of selected movies
        public void RemoveBookingItem(BookingItemDTO item)
        {
            Booking.BookingItems.Remove(item);

        }

        //we eliminate all the movies from the list
        public void ClearBookingCart()
        {
            Booking.BookingItems.Clear();

        }

        //we have already finished the process of renting, thus, we create a new Rental 
        public void BookingProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Booking = new BookingForCreateDTO()
            {
                BookingItems = new List<BookingItemDTO>()
            };
        }
    }
}
