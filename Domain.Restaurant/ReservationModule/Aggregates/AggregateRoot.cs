using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Extensions;

namespace Domain.Restaurant.ReservationModule.Aggregates
{
    public abstract class AggregateRoot : Entity, IValidatableObject
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults().NotNullOrEmpty(e => e.CreatedBy).Execute();
        }
    }
}
