using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapper.CreateMap<Customer, CustomerDto>(); // is not working
            CreateMap<Customer, CustomerDto>().ForMember(m => m.Id, opt => opt.Ignore()); 
            CreateMap<CustomerDto, Customer>();

            CreateMap<Movie, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>();
        }
    }
}
