using Swashbuckle.AspNetCore.Filters;
using System;

namespace HotelCancun.Models.Dtos
{
    public class CreateBookingDtoSample : IExamplesProvider<BookingDto>
    {
        public BookingDto GetExamples()
        {
            return new BookingDto
            {
                
                CustomerName = "Some Name Here",
                Checkin = DateTime.Now.Date,
                Checkout = DateTime.Now.AddDays(3).Date

            };
        }
    }
}
