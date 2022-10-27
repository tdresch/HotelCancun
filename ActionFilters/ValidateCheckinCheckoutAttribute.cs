using HotelCancun.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HotelCancun.API.ActionFilters
{
    public class ValidateCheckinCheckoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var bookingDto = context.ActionArguments["bookingDto"] as BookingDto;


            if (bookingDto.Checkout == null || bookingDto.Checkin == null)
            {
                BadRequestResponse(context, new ErrorResponse { Code = 2, Message = "Invalid Data: Both Checkin and Checkout must be filled and be in YYYY-MM-DD format." });
            }
            else
            if (bookingDto.Checkin?.Date < DateTime.Today.Date)
            {
                BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "Invalid Data: Checkin must be bigger than Today." });
            }
            else
            if (bookingDto.Checkin?.Date >= bookingDto.Checkout?.Date)
            {
                BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "Invalid Data: Checkin must be bigger than Checkout." });
            }
            else
            if ((bookingDto.Checkout?.Date - bookingDto.Checkin?.Date)?.Days > 3)
            {
                BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "Invalid booking: Your stay can’t be longer than 3 days." });
            }
            else
            if ((bookingDto.Checkin?.Date - DateTime.Today)?.Days > 30)
            {
                BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "Invalid booking: Your booking can’t be more than 30 days in advance." });
            }

            base.OnActionExecuting(context);
        }

        private void BadRequestResponse(ActionExecutingContext context, ErrorResponse error)
        {
            context.Result = new BadRequestObjectResult(error);
        }

    }
}
