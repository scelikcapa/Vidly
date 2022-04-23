using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();

            // Dto to Domain
            
            Mapper.CreateMap<MovieDto, Movie>();
            Mapper.CreateMap<CustomerDto, Customer>();
            
            //// This code is not need in my case
            //Mapper.CreateMap<MovieDto, Movie>()
            //    .ForMember(c => c.Id, opt => opt.Ignore());
            //Mapper.CreateMap<CustomerDto, Customer>()
            //    .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}