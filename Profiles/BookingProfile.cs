using AutoMapper;
using HotelCancun.Models;
using HotelCancunAPI.Dtos;

namespace FilmesAPI.Profiles
{
    public class BookingProfile : Profile
    {

        public BookingProfile()
        {
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Booking,ReadBookingDto>();
            CreateMap<ChangeBookingDto, Booking>();
        }
    }
}
