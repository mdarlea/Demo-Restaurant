using System;
using AutoMapper;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Application.Seedwork.Extensions;

namespace Application.Restaurant.TypeMapping.Profiles
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            MapperExtensions.CreateActionResultMap<Reservation, Dto.ReservationResult>();

            Mapper.CreateMap<Reservation, Dto.Reservation>();
        }
    }
}
