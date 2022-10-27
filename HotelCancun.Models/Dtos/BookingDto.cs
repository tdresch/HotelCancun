using System;

namespace HotelCancun.Models.Dtos
{
    public class BookingDto
    {

        public string CustomerName { get; set; }

        public DateTime? Checkin { get; set; }

        public DateTime? Checkout { get; set; }
    }
}
