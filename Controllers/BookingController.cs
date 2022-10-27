using AutoMapper;
using HotelCancun.API.ActionFilters;
using HotelCancun.Service;
using HotelCancun.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelCancun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly IMapper _autoMapper;
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingContext, IMapper autoMapper)
        {
            _bookingService = bookingContext;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Insert a new booking",
            Tags = new[] { "Insert" },
            Description = "Insert a new booking",
            OperationId = "insert"
            )]
        [SwaggerResponse(201, "Insert a new booking", typeof(BookingDto))]
        [SwaggerRequestExample(typeof(BookingDto), typeof(CreateBookingDtoSample))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        [ValidateCheckinCheckout]
        public async Task<IActionResult> AddBooking([FromBody] BookingDto bookingDto)
        {
            if (ModelState.IsValid)
            {
                var bookingId = await _bookingService.InsertBooking(bookingDto);
                return CreatedAtAction(nameof(FindBookingById), new { Id = bookingId }, bookingDto);
            }
            return BadRequest();
        }

        [HttpPost("search")]
        [SwaggerOperation(
            Summary = "Search using parameters and returns a list of bookings",
            Tags = new[] { "Get and Search" },
            Description = "Search using parameters and returns a list of bookings",
            OperationId = "search"
            )]
        [SwaggerResponse(200, "Returns search result containing a list of bookings", typeof(ReadBookingDto))]
        [SwaggerRequestExample(typeof(FilterBookingDto), typeof(FilterBookingDtoSample))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        [ValidateSearchBody]
        public async Task<IActionResult> FindBookings([FromBody] FilterBookingDto filterDto)
        {
            var result = await _bookingService.SearchBooking(filterDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a booking by ID",
            Tags = new[] { "Get and Search" },
            Description = "Get a booking by ID",
            OperationId = "get"
            )]
        [SwaggerResponse(200, "Returns a booking", typeof(ReadBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> FindBookingById(int id)
        {
            ReadBookingDto bookingDto = await _bookingService.GetBooking(id);
            if (bookingDto != null)
            {
                return Ok(bookingDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update a booking",
            Tags = new[] { "Update" },
            Description = "Update an existing booking",
            OperationId = "update"
            )]
        [SwaggerResponse(204, "Update an existing booking", typeof(BookingDto))]
        [SwaggerRequestExample(typeof(BookingDto), typeof(ChangeBookingDtoSample))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        [ValidateCheckinCheckout]
        public async Task<IActionResult> ChangeBooking(int id, [FromBody] BookingDto bookingDto)
        {
            await _bookingService.UpdateBooking(id, bookingDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a booking",
            Tags = new[] { "Delete" },
            Description = "Delete an existing booking",
            OperationId = "delete"
            )]
        [SwaggerResponse(204, "Delete an existing booking", typeof(BookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await _bookingService.DeleteBooking(id);
            return NoContent();
        }
    }

}
