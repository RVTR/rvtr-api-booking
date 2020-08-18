using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVTR.Booking.DataContext.Repositories
{
    public class BookingRepository : Repository<BookingModel>
    {
        public BookingRepository(BookingContext bookingContext): base(bookingContext) {}

        bool _DatesIntersect (DateTime checkIn, DateTime checkOut, DateTime bookingCheckIn, DateTime bookingCheckOut)
        {
            bool intersectFront = checkIn <= bookingCheckIn && checkOut >= bookingCheckIn;
            bool intersectBack = checkIn <= bookingCheckOut && checkOut >= bookingCheckOut;
            bool intersectOuter = checkIn <= bookingCheckIn && checkOut >= bookingCheckOut;
            bool intersectInner = checkIn >= bookingCheckIn && checkOut <= bookingCheckOut;

            return intersectFront || intersectBack || intersectInner || intersectOuter;
        }

        public virtual async Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
        {
            var bookings = await _db.Where (b => _DatesIntersect (checkIn, checkOut, b.CheckIn, b.CheckOut)).ToListAsync ();
            return bookings;
        }

        public virtual async Task<IEnumerable<BookingModel>> GetByAccountId(int id)
        { 
            return await _db.Where(t => t.AccountId == id).ToListAsync();
        }
    }
}
