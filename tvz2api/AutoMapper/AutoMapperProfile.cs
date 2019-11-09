using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tvz2api.Models;
using tvz2api.Models.DTO;

namespace tvz2api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Student
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.Smjer, opts => opts.MapFrom(src => src.Smjer.Naziv));
            // Kolegij
            CreateMap<Kolegij, KolegijDTO>()
                .ForMember(dest => dest.Smjer, opts => opts.MapFrom(src => src.SmjerId));
            CreateMap<Korisnik, UserForListDto>();
        }
    }
}
