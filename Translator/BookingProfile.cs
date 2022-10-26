using AutoMapper;
using HotelCancun.Models.Entities;
using HotelCancunAPI.Models.Dtos;

namespace HotelCancunAPI.Translator
{
    public class BookingProfile : Profile
    {

        public BookingProfile()
        {
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Booking, ReadBookingDto>();
            CreateMap<ChangeBookingDto, Booking>();
        }
    }
}
