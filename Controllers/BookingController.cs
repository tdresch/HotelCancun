using AutoMapper;
using HotelCancun.Models;
using HotelCancunAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCancun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private static List<Booking> bookings = new List<Booking>();
        private readonly IMapper _autoMapper;

        public BookingController(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody] CreateBookingDto bookingDto)
        {
            Booking booking = _autoMapper.Map<Booking>(bookingDto);
            bookings.Add(booking);
            return CreatedAtAction(nameof(GetBookingById), new { Id = booking.Id }, booking);
        }

        [HttpGet]
        public IEnumerable<ReadBookingDto> GetAllBookings()
        {
            return bookings.Select(s => _autoMapper.Map<ReadBookingDto>(s)).ToList(); ;
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
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
        public IActionResult ChangeBooking(int id, [FromBody] ChangeBookingDto bookingDto)
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
        public IActionResult CancelBooking(int id)
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
