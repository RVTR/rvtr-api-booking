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

        public virtual async Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
        {
            var bookings = await _db.Where (b => 
                (checkIn <= b.CheckIn && checkOut >= b.CheckIn) ||
                (checkIn <= b.CheckOut && checkOut >= b.CheckOut) ||
                (checkIn <= b.CheckIn && checkOut >= b.CheckOut) ||
                (checkIn >= b.CheckIn && checkOut <= b.CheckOut)
            ).ToListAsync ();

            return bookings;
        }

        public virtual async Task<IEnumerable<BookingModel>> GetByAccountId(int id)
        {
            return await _db.Where(t => t.AccountId == id).ToListAsync();
        }
    }
}
