using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCancun.Models.Dtos
{
    public class FilterBookingDto
    {

        public int? Id { get; set; }
        public string CustomerName { get; set; }

        public DateTime? Checkin { get; set; }

        public DateTime? Checkout { get; set; }
       
    }
}
