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
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.Smjer, opts => opts.MapFrom(src => src.Smjer.Naziv));
            CreateMap<Zaposlenik, ZaposlenikDTO>()
                .ForMember(dest => dest.Odjel, opts => opts.MapFrom(src => src.Odjel.Naziv))
                .ForMember(dest => dest.VrstaZaposljenja, opts => opts.MapFrom(src => src.VrstaZaposljenja.Naziv));
            CreateMap<Kolegij, KolegijDTO>()
                .ForMember(dest => dest.Smjer, opts => opts.MapFrom(src => src.SmjerId));
            CreateMap<Kolegij, KolegijExtendedDTO>()
                .ForMember(dest => dest.Smjer, opts => opts.MapFrom(src => src.SmjerId));
            CreateMap<Pretplata, PretplataDTO>();
            CreateMap<Korisnik, UserForListDto>();
            CreateMap<Korisnik, KorisnikDTO>();
            CreateMap<Vijest, VijestDTO>()
                .ForMember(dest => dest.Objavio, opts => opts.MapFrom(src => String.Format("{0} {1}", src.Objavio.Ime, src.Objavio.Prezime)));
            CreateMap<SidebarContent, SidebarContentDTO>()
                .ForMember(dest => dest.Files, opts => opts.MapFrom(src => src.SidebarContentFile));
            CreateMap<SidebarContentFile, SidebarContentFileDTO>();
        }
    }
}
