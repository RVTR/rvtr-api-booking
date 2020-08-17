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

    public IEnumerable<GuestModel> Guests { get; set; }

    public IEnumerable<RentalModel> Rentals { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime Checkout { get; set; }

    /// <summary>
    /// Represents the _Booking_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
