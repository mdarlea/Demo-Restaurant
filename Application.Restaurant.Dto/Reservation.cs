using System;

namespace Application.Restaurant.Dto
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ReservationDateTime { get; set; }
        
        public int GuestsCount { get; set; }
    }
}
