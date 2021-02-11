using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.Domain.Models
{
  public class RentalModel : AEntity, IValidatableObject
  {
    public int BookingModelId { get; set; }
    public int LodgingRentalId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
