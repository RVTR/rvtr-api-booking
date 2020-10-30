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

    [Required (ErrorMessage = "Guests object is required")]
    public IEnumerable<GuestModel> Guests { get; set; }

    [Required (ErrorMessage = "Rentals object is required")]
    public IEnumerable<RentalModel> Rentals { get; set; }

    [Required(ErrorMessage = "CheckIn is required")]
    public DateTime CheckIn { get; set; }

    [Required(ErrorMessage = "CheckOut is required")]
    public DateTime CheckOut { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      DateTime todaysDate = DateTime.Now.Date;
      if (Guests == null)
      {
        yield return new ValidationResult("Guests cannot be null.");
      }
      if (Rentals == null)
      {
        yield return new ValidationResult("Rentals cannot be null.");
      }
      if(CheckIn < todaysDate)
      {
        yield return new ValidationResult("CheckIn cannot be earlier than today's date.");
      }
      if (CheckIn >= CheckOut)
      {
        yield return new ValidationResult("CheckIn cannot be earlier than or equal to CheckOut.");
      }
    }
  }
}
