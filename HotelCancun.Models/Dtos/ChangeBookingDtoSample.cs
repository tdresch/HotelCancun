using Swashbuckle.AspNetCore.Filters;
using System;

namespace HotelCancun.Models.Dtos
{
    public class ChangeBookingDtoSample : IExamplesProvider<BookingDto>
    {
        public BookingDto GetExamples()
        {
            return new BookingDto
            {
                
                CustomerName = "Change Name Here",
                Checkin = DateTime.Now.Date,
                Checkout = DateTime.Now.AddDays(3).Date

            };
        }
    }
}
