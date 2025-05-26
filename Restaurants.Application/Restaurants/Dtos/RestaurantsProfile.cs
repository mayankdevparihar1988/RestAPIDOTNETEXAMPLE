using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantsProfile: Profile
    {
        public RestaurantsProfile() {

            // CreateMap<SRC, DEST>
            CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City =  src.City,
                    ZipCode = src.PostalCode,
                    Street = src.Street
                }));

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.ZipCode))
                // .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.State))
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes))
                ;



        }
    }
}
