using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
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
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public KorisnikController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Korisnik
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<KorisnikDTO>>>> GetKorisnik(int skip = 0, int? take = null)
        {
            List<Korisnik> korisnici = await _context.Korisnik.ToListAsync();
            return new ResponseDataWrapper<List<KorisnikDTO>>(
                _mapper.Map<List<Korisnik>, 
                List<KorisnikDTO>>(
                    korisnici
                    .Skip(skip)
                    .Take(take ?? korisnici.Count)
                    .ToList()
                )
            );
        }

        // GET: api/Korisnik/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<KorisnikDTO>> GetKorisnik(int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return(Unauthorized());
            }
          
            var korisnik = await _context.Korisnik.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return _mapper.Map<Korisnik, KorisnikDTO>(korisnik);
        }
    }
}
