using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Domain.Interfaces;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Context.Repositories
{
  public class BookingRepository : Repository<BookingModel>, IBookingRepository
  {
    public BookingRepository(BookingContext bookingContext) : base(bookingContext) { }

    /// <summary>
    /// Find bookings where the checkIn/checkOut date range is
    /// intersected in any way by the specified checkIn/checkOut range
    /// </summary>
    public virtual async Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
    {
      var bookings = await Db.Where(b =>
        (checkIn <= b.CheckIn && checkOut >= b.CheckIn) || // Intersects left
        (checkIn <= b.CheckOut && checkOut >= b.CheckOut) || // Intersects right
        (checkIn <= b.CheckIn && checkOut >= b.CheckOut) || // Intersects inner
        (checkIn >= b.CheckIn && checkOut <= b.CheckOut) // Intersects outer
      )
      .Include(x => x.Rentals)
      .Include(x => x.Guests)
      .ToListAsync();

      return bookings;
    }

    /// <summary>
    /// Selects all booking models and .incldudes the attached rental and guest lists
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<BookingModel>> SelectAsync(string input)
    {
      //SEARCH FOR ALL Bookings with lodgingnumber == input
      int parsed = -1;
      bool isParsable = Int32.TryParse(input, out parsed);

      IEnumerable<BookingModel> bookingsByBookingNumberAndEmail =
      await Db
      .Include(x => x.Rentals)
      .Include(x => x.Guests)
      .Where(x => x.LodgingId == parsed || x.AccountEmail == input)
      .ToListAsync();

      if(bookingsByBookingNumberAndEmail.Count() >= 1)
      {
        return bookingsByBookingNumberAndEmail;
      }


      return null;
    }

    public virtual async Task<IEnumerable<BookingModel>> GetByAccountEmail(string email)
    {

      return await Db.Where(t => t.AccountEmail == email).ToListAsync();
    }

  }
}
