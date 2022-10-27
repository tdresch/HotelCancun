using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCancun.Models.Dtos
{
    public class ReadBookingDto
    {

        public int Id { private get; set; }

        public string CustomerName { private get; set; }

        public DateTime Checkin { private get; set; }

        public DateTime Checkout { private get; set; }
        public string SearchResult
        {
            get
            {
                if (Id > 0)
                {

                   return $"Booking #{Id} for {CustomerName} : Checkin at {Checkin.Date} - Checkout at {Checkout.Date} ";
                }
                else
                {
                    return $"Booking available : Checkin at {Checkin.Date} - Checkout at {Checkout.Date} ";
                }
            }
        }
    }
}
