using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Booking_ model
  /// </summary>
  public class BookingModel : IValidatableObject
  {
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int LodgingId { get; set; }

    [Required]
    public IEnumerable<GuestModel> Guests { get; set; }
    [Required]
    public IEnumerable<RentalModel> Rentals { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
      if (Guests == null)
      {
        yield return new ValidationResult("Guests cannot be null.");
      }
      if (Rentals == null)
      {
        yield return new ValidationResult("Rentals cannot be null.");
      }
      if (CheckIn == null)
      {
        yield return new ValidationResult("CheckIn cannot be null.");
      }
      if (CheckOut == null)
      {
        yield return new ValidationResult("Checkout cannot be null.");
      }
    }
  }
}
