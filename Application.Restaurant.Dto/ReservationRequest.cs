using System;

namespace Application.Restaurant.Dto
{
    public class ReservationRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ReservationDateTime { get; set; }
        
        public int GuestsCount { get; set; }

        public string UserId { get; set; }
    }
}
