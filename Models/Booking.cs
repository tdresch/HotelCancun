using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCancun.Models
{
    public class Booking
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "CustomerName is a required field")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Checkin is a required field")]
        public DateTime Checkin { get; set; }
        [Required(ErrorMessage = "Checkout is a required field")]
        public DateTime Checkout { get; set; }
       
    }
}
