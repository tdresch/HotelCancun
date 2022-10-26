using AutoMapper;
using HotelCancun.Service;
using HotelCancunAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerResponse(201, "Insert a new booking", typeof(CreateBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto bookingDto)
        {
            if (ModelState.IsValid)
            {
                var bookingId = await _bookingService.InsertBooking(bookingDto);
                return CreatedAtAction(nameof(GetBookingById), new { Id = bookingId }, bookingDto);
            }
            return BadRequest();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all bookings",
            Tags = new[] { "Get and Search" },
            Description = "Get all confirmed bookings",
            OperationId = "get"
            )]
        [SwaggerResponse(200, "Returns search result containing a list of bookings", typeof(ReadBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IEnumerable<ReadBookingDto>> GetAllBookings()
        {
            return await _bookingService.SearchBooking(new FilterBookingDto());
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
        [SwaggerResponse(204, "Update an existing booking", typeof(ChangeBookingDto))]
        [SwaggerResponse(400, "Returns an 400 error", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Returns an 404 error", typeof(ErrorResponse))]
        public async Task<IActionResult> ChangeBooking(int id, [FromBody] ChangeBookingDto bookingDto)
        {
            if (ModelState.IsValid)
            {
                await _bookingService.UpdateBooking(id, bookingDto);
                return NoContent();
            }
            return BadRequest();
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
            await _bookingService.DeleteBooking(id);
            return NoContent();
        }
    }

}
