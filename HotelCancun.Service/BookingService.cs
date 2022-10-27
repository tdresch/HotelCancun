using AutoMapper;
using HotelCancun.Models.Entities;
using HotelCancun.DataStore.MySQL;
using HotelCancun.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCancun.Service
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext _bookingContext;
        private readonly IMapper _autoMapper;

        public BookingService(BookingContext context, IMapper autoMapper)
        {
            _bookingContext = context;
            _autoMapper = autoMapper;
        }

        public async Task DeleteBooking(int bookingId)
        {
            Booking booking = await _bookingContext.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _bookingContext.Remove(booking);
                await _bookingContext.SaveChangesAsync();
            }
        }

        public async Task<ReadBookingDto> GetBooking(int bookingId)
        {
            Booking booking = await _bookingContext.Bookings.FindAsync(bookingId);

            if (booking != null)
            {
                return _autoMapper.Map<ReadBookingDto>(booking);
            }

            return null;
        }

        public async Task<int> InsertBooking(BookingDto createBookingDto)
        {
            if (IsPeriodBooked(createBookingDto.Checkin.Value, createBookingDto.Checkout.Value))
            {
                throw new Exception(" Booking not available. It's already booked to someone.");
            }

            Booking booking = _autoMapper.Map<Booking>(createBookingDto);
            _bookingContext.Add(booking);
            await _bookingContext.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<List<ReadBookingDto>> SearchBooking(FilterBookingDto filterDto)
        {
            List<ReadBookingDto> bookings = new List<ReadBookingDto>();

            if (filterDto != null)
            {



                if (filterDto.Checkin != null && filterDto.Checkout != null)
                {
                    if (!IsPeriodBooked(filterDto.Checkin.Value, filterDto.Checkout.Value))
                    {
                        bookings.Add(new ReadBookingDto { Checkin = filterDto.Checkin.Value, Checkout = filterDto.Checkin.Value });
                    }
                    else
                    {
                        bookings = await _bookingContext.Bookings.Where(w => w.Checkin.Date >= filterDto.Checkin.Value && w.Checkin.Date < filterDto.Checkout.Value).Select(s => _autoMapper.Map<ReadBookingDto>(s)).ToListAsync();
                    }
                }
                else
                {
                    IQueryable<Booking> query = _bookingContext.Bookings.AsQueryable();
                    if (filterDto.Id != null)
                    {
                        query = query.Where(booking => booking.Id == filterDto.Id).AsQueryable();
                    }
                    else
                    if (!string.IsNullOrEmpty(filterDto.CustomerName))
                    {
                        query = query.Where(booking => booking.CustomerName.Contains(filterDto.CustomerName)).AsQueryable();
                    }
                    bookings = await query.Select(s => _autoMapper.Map<ReadBookingDto>(s)).ToListAsync();
                }


            }

            return bookings;
        }

        public async Task UpdateBooking(int bookingId, BookingDto changeBookingDto)
        {
            if (IsPeriodBooked(changeBookingDto.Checkin.Value, changeBookingDto.Checkout.Value, bookingId))
            {
                throw new Exception(" Booking not available. It's already booked for someone.");
            }
            Booking booking = await _bookingContext.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _autoMapper.Map(changeBookingDto, booking);
                _bookingContext.Update(booking);
                await _bookingContext.SaveChangesAsync();
            }
        }

        private bool IsPeriodBooked(DateTime checkin, DateTime checkout, int bookingId = 0)
        {
            return _bookingContext.Bookings.Where(bk => bk.Id != bookingId &&
                                                 ((bk.Checkin >= checkin && bk.Checkin < checkout)  ||
                                                  (checkout > bk.Checkin && checkout < bk.Checkout) ||
                                                  (bk.Checkin <= checkin && bk.Checkout >= checkout))).Any();

        }

    }
}
