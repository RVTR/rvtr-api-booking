using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.Domain.Models
{
  /// <summary>
  /// Represents the _Booking_ model
  /// </summary>
  public class BookingModel : IValidatableObject
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "AccountID is required")]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Rentals object is required")]
    public int LodgingId { get; set; }

    public IEnumerable<GuestModel> Guests { get; set; }

    public IEnumerable<RentalModel> Rentals { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }
    public string BookingNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      DateTime todaysDate = DateTime.Now.Date;

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
