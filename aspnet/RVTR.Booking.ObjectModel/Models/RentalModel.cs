using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }
    public int? BookingId { get; set; }
    [Required(ErrorMessage = "Guests object is required")]
    public virtual BookingModel Booking { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Booking == null)
      {
        yield return new ValidationResult("Guests cannot be null.");
      }
    }
  }
}
