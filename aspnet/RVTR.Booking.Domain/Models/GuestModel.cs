using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.Domain.Models
{
  public class GuestModel : AEntity, IValidatableObject
  {
    public int? BookingModelId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public bool IsMinor { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
