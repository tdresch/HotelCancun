using HotelCancun.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelCancun.Service
{
    public interface IBookingService
    {
        Task<ReadBookingDto> GetBooking(int bookingId);
        Task<int> InsertBooking(BookingDto createBookingDto);
        Task UpdateBooking(int bookingId, BookingDto changeBookingDto);
        Task DeleteBooking(int bookingId);
        Task<List<ReadBookingDto>> SearchBooking(FilterBookingDto filterDto);
    }
}
