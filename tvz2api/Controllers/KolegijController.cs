using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tvz2api.Enumerations;
using tvz2api.Helpers;
using tvz2api.Models;
using tvz2api.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace tvz2api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KolegijController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public KolegijController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Kolegij/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KolegijExtendedDTO>> GetKolegij(int id)
        {
            var kolegij = await _context
                .Kolegij
                .Include(x => x.Vijest)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (kolegij == null)
            {
                return NotFound();
            }

            KolegijExtendedDTO kolegijExtended = _mapper.Map<Kolegij, KolegijExtendedDTO>(kolegij);

            kolegijExtended.Studenti = _mapper.Map<List<Student>, List<StudentDTO>>((
                from k in _context.Kolegij
                join sk in _context.StudentKolegij on k.Id equals sk.KolegijId
                join s in _context.Student on sk.StudentId equals s.Id
                where sk.KolegijId == id
                select new Student {
                    Id = s.Id,
                    Email = s.Email,
                    ImagePath = s.ImagePath,
                    Ime = s.Ime,
                    Jmbag = s.Jmbag,
                    Pretplata = s.Pretplata,
                    Prezime = s.Prezime,
                    Smjer = s.Smjer,
                    SmjerId = s.SmjerId,
                    StudentKolegij = s.StudentKolegij
                }).OrderBy(x => x.Prezime).ToList()
            );

            kolegijExtended.Vijesti = _mapper.Map<List<Vijest>, List<VijestDTO>>((
                from v in _context.Vijest
                join k in _context.Kolegij on v.KolegijId equals k.Id
                where k.Id == id
                select new Vijest {
                    Naslov = v.Naslov,
                    Datum = v.Datum,
                    Objavio = v.Objavio,
                    Opis = v.Opis
                }).ToList()
            );

            return kolegijExtended;
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> GetKolegij(
          [FromQuery(Name = "smjerIDs[]")] List<SmjerEnum> smjerIDs,
          string name = null,
          int minECTS = 1,
          int maxECTS = 6,
          string isvu = null,
          int skip = 0,
          int? take = null) 
        {
            var kolegiji = await _context.Kolegij.ToListAsync();
            return new ResponseDataWrapper<List<KolegijDTO>>(
                _mapper.Map<List<Kolegij>,
                List<KolegijDTO>>(
                    kolegiji
                    .Where(x => 
                      smjerIDs.Contains((SmjerEnum)x.SmjerId)
                      && x.Ects >= minECTS 
                      && x.Ects <= maxECTS 
                      && (name == null ? true : x.Naziv.Contains(name))
                    )
                    .Skip(skip)
                    .Take(take ?? kolegiji.Count)
                    .ToList()
                )
            );
        }
        
        [HttpGet("my/{studentId}")]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> GetKolegijByPreplate(int studentId) 
        {
            var kolegiji = await (
              from k in _context.Kolegij
              join p in _context.Pretplata on k.Id equals p.KolegijId
              join s in _context.Student on p.StudentId equals s.Id
              where p.StudentId == studentId
              select new KolegijDTO {
                Ects = k.Ects,
                Isvu = k.Isvu,
                Naziv = k.Naziv,
                Smjer = (SmjerEnum)k.Smjer.Id,
                Url = k.Url
              }).ToListAsync();
            
            return new ResponseDataWrapper<List<KolegijDTO>>(kolegiji);
        }

        [HttpGet("Vijesti/{kolegijId}")]
        public async Task<ActionResult<ResponseDataWrapper<List<VijestDTO>>>> GetVijesti(int kolegijId, int skip = 0, int? take = null) 
        {
            var vijesti = await _context.Vijest.Include(x => x.Objavio).ToListAsync();
            return new ResponseDataWrapper<List<VijestDTO>>(
                _mapper.Map<List<Vijest>,
                List<VijestDTO>>(
                    vijesti
                    .Where(x => x.KolegijId == kolegijId)
                    .Skip(skip)
                    .Take(take ?? vijesti.Count)
                    .ToList()
                )
            ); 
        }

        [HttpGet("Students/{kolegijId}")]
        public async Task<ActionResult<ResponseDataWrapper<List<StudentDTO>>>> GetStudenti(int kolegijId, int skip = 0, int? take = null) 
        {
            var studenti = await (from k in _context.Kolegij
                            join sk in _context.StudentKolegij on k.Id equals sk.KolegijId
                            join s in _context.Student on sk.StudentId equals s.Id
                            where sk.KolegijId == kolegijId
                            select new Student {
                                Id = s.Id,
                                Email = s.Email,
                                ImagePath = s.ImagePath,
                                Ime = s.Ime,
                                Jmbag = s.Jmbag,
                                Pretplata = s.Pretplata,
                                Prezime = s.Prezime,
                                Smjer = s.Smjer,
                                SmjerId = s.SmjerId,
                                StudentKolegij = s.StudentKolegij
                            }).ToListAsync();
            return new ResponseDataWrapper<List<StudentDTO>>(
                _mapper.Map<List<Student>,
                List<StudentDTO>>(
                    studenti
                    .Skip(skip)
                    .Take(take ?? studenti.Count)
                    .ToList()
                )
            ); 
        }
        
        [HttpGet("SidebarContent/{kolegijId}")]
        public async Task<ActionResult<ResponseDataWrapper<List<SidebarContentDTO>>>> GetSidebarContent(int kolegijId) 
        {
            var sidebarContent = await _context.SidebarContent.Include(x => x.SidebarContentFile).ToListAsync();
            return new ResponseDataWrapper<List<SidebarContentDTO>>(
                _mapper.Map<List<SidebarContent>, List<SidebarContentDTO>>(sidebarContent)
            ); 
        }
    }
}
