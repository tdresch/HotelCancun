using HotelCancun.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelCancun.DataStore.MySQL
{
    public class BookingContext : DbContext
    {

        public BookingContext(DbContextOptions<BookingContext> opt) : base(opt)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
