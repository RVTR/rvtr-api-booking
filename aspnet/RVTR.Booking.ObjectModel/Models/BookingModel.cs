using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Booking_ model
  /// </summary>
  public class BookingModel
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
  }
}
