using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }
    public int? BookingId { get; set; }
    public virtual BookingModel Booking { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
