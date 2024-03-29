﻿using AutoMapper;
using HotelCancun.Models.Entities;
using HotelCancun.Models.Dtos;

namespace HotelCancunAPI.Translator
{
    public class BookingProfile : Profile
    {

        public BookingProfile()
        {
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, ReadBookingDto>();
            CreateMap<BookingDto, Booking>();
        }
    }
}
