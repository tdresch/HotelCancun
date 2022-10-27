using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCancun.Models.Dtos
{
    public class FilterBookingDtoSample : IExamplesProvider<FilterBookingDto>
    {
        public FilterBookingDto GetExamples()
        {
            return new FilterBookingDto
            {
                Id = 1,
                CustomerName = "Some Name",
                Checkin = DateTime.Now.Date,
                Checkout = DateTime.Now.AddDays(3).Date

            };
        }
    }
}
