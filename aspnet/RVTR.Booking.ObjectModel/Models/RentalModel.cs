using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.ObjectModel.Models
{
  public class RentalModel
  {
    public int Id { get; set; }
    public int? BookingId { get; set; }
    [Required]
    public virtual BookingModel Booking { get; set; }
  }
}
