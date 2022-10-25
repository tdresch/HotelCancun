using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCancunAPI.Dtos
{
    public class ReadBookingDto
    {

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Checkin date is required")]
        public DateTime Checkin { get; set; }

        [Required(ErrorMessage = "Checkout date is required")]
        public DateTime Checkout { get; set; }
        public string DetailInfo
        {
            get
            {
                return $"Dear {CustomerName}, Booking #{Id} : Checkin at {Checkin.Date} - Checkout at {Checkout.Date} ";
            }
        }
    }
}
