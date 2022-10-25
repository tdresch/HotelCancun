using HotelCancun.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelCancunAPI.Data
{
    public class BookingContext : DbContext
    {

        public BookingContext(DbContextOptions<BookingContext> opt) : base(opt)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
