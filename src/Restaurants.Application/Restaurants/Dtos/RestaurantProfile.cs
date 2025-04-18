﻿using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => 
                opt.MapFrom(src => src.Address == null ? null :   src.Address.City))
            .ForMember(d => d.Street, opt => 
                opt.MapFrom(src => src.Address == null ? null :   src.Address.Street))
            .ForMember(d => d.PostalCode, opt => 
                opt.MapFrom(src => src.Address == null ? null :   src.Address.PostalCode))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}