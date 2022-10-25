using System;

namespace HotelCancun.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
       
    }
}
