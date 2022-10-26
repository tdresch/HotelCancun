using AutoMapper;
using HotelCancun.Models;
using HotelCancunAPI.Data;
using HotelCancunAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCancun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private static List<Booking> bookings = new List<Booking>();
        private readonly IMapper _autoMapper;
        private readonly BookingContext _bookingContext;

        public BookingController(BookingContext bookingContext, IMapper autoMapper)
        {
            _bookingContext = bookingContext;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [SwaggerResponse(200, "Insert a new booking", typeof(CreateBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto bookingDto)
        {
            Booking booking = _autoMapper.Map<Booking>(bookingDto);
            bookings.Add(booking);
            return CreatedAtAction(nameof(GetBookingById), new { Id = booking.Id }, booking);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary ="Get all bookings",
            Tags = new[] {"Get and Search"},
            Description ="Get all confirmed bookings",
            OperationId ="get"
            )]
        [SwaggerResponse(200,"Returns search result containing a list of bookings",typeof(ReadBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IEnumerable<ReadBookingDto>> GetAllBookings()
        {
            return bookings.Select(s => _autoMapper.Map<ReadBookingDto>(s)).ToList(); ;
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
        public async Task<IActionResult> GetBookingById(int id)
        {
            Booking booking = bookings.FirstOrDefault(bk => bk.Id == id);
            if (booking != null)
            {
                ReadBookingDto bookingDto = _autoMapper.Map<ReadBookingDto>(booking);
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
        [SwaggerResponse(204, "Update an existing booking", typeof(ChangeBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> ChangeBooking(int id, [FromBody] ChangeBookingDto bookingDto)
        {

            Booking booking = bookings.FirstOrDefault(bk => bk.Id == id);
            if (booking != null)
            {
                _autoMapper.Map(bookingDto, booking);
                return NoContent();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a booking",
            Tags = new[] { "Delete" },
            Description = "Delete an existing booking",
            OperationId = "delete"
            )]
        [SwaggerResponse(204, "Delete an existing booking", typeof(ChangeBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> CancelBooking(int id)
        {
            Booking booking = bookings.FirstOrDefault(bk => bk.Id == id);
            if (booking != null)
            {
                bookings.Remove(booking);
                return NoContent();
            }
            return NotFound();
        }
    }

}
