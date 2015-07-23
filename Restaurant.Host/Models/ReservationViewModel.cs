using System;
using System.ComponentModel.DataAnnotations;
using Application.Restaurant.Dto;

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

    public static class ReservationViewModelExtensions
    {
        public static ReservationRequest ToReservationRequest(this ReservationViewModel viewModel, string userId)
        {
            var date = viewModel.ReservationDate;
            var reservationDateTime = new DateTime(date.Year, date.Month, date.Day, viewModel.Hours, viewModel.Minutes, 0);

            return new ReservationRequest
            {
                UserId = userId,
                ReservationDateTime = reservationDateTime,
                GuestsCount = viewModel.GuestsCount,
                Name = viewModel.Name
            };
        }
    }
}