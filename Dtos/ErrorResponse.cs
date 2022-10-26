using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace HotelCancunAPI.Dtos
{
    public class ErrorResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string[] Details { get; set; }

        public ErrorResponse InnerError { get; set; }

        public static ErrorResponse From(Exception e)
        {

            if (e == null)
            {
                return null;
            }
            return new ErrorResponse
            {
                Code = e.HResult,
                Message = e.Message,
                InnerError = From(e.InnerException)
            };
        }

        public static ErrorResponse FromModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(s => s.Errors);

            return new ErrorResponse
            {
                Code = 100,
                Message = "There was an error processing your request.",
                Details = errors.Select(s=> s.ErrorMessage).ToArray()
            };
        }
    }
}
