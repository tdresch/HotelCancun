using HotelCancunAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HotelCancun.API.ActionFilters
{
    public class ValidateSearchBodyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("filterDto", out var dtoAsObject);

            if (dtoAsObject != null)
            {
                var searchDto = dtoAsObject as FilterBookingDto;
                if (searchDto.Checkout == null && searchDto.Checkin == null && string.IsNullOrEmpty(searchDto.CustomerName) && searchDto.Id == null)
                {
                    BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "At least one parameter must be provided." });
                }
                else
                if ((searchDto.Checkin != null && searchDto.Checkout == null) || (searchDto.Checkout != null && searchDto.Checkin == null))
                {
                    BadRequestResponse(context, new ErrorResponse { Code = 2, Message = "Invalid Data: Both Checkin and Checkout must be filled and be in YYYY-MM-DD format." });
                }
                else
                if (searchDto.Checkin?.Date >= searchDto.Checkout?.Date)
                {
                    BadRequestResponse(context, new ErrorResponse { Code = 1, Message = "Invalid Data: Checkin must be bigger that Checkout." });
                }
            }

            base.OnActionExecuting(context);
        }

        private void BadRequestResponse(ActionExecutingContext context, ErrorResponse error)
        {
            context.Result = new BadRequestObjectResult(error);
        }

    }
}
