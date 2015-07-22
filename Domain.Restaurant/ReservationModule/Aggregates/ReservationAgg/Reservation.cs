using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork.Extensions;

namespace Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg
{
    /// <summary>
    /// Reservation Aggregate Root
    /// </summary>
    public class Reservation : AggregateRoot
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        public int GuestsCount { get; set; }

        public bool IsDeleted { get; set;}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Expression<Func<Reservation, int>> propertyExpression = e => e.GuestsCount;

            var result = this.ValidationResults(base.Validate(validationContext))
                .NotNullOrEmpty(e => e.Name)
                .Validate(e => e.GuestsCount, value => value > 0, "Invalid guests number")
                .Validate(e => e.ReservationDateTime, value => value.CompareTo(DateTime.Now) >= 0, "A reservation cannot be made in the past")
                .Execute();

            return result;
        }
    }
}
