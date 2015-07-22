using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Host.Models
{
    public class ReservationViewModel
    {
        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public int Minutes {get; set;}

        [Required]
        public string Name { get; set; }

        [Required]
        public int GuestsCount { get; set; }
    }
}