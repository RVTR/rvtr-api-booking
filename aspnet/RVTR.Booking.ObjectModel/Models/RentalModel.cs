using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }
    public int? BookingId { get; set; }
    [Required]
    public virtual BookingModel Booking { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
      if (Booking == null)
      {
        yield return new ValidationResult("Guests cannot be null.");
      }
    }
  }
}
