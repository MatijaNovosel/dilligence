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

namespace tvz2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaposlenikController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public ZaposlenikController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Zaposlenik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZaposlenikDTO>> GetZaposlenik(int id)
        {
            var zaposlenik = await _context.Zaposlenik.Include(x => x.Odjel).Include(x => x.VrstaZaposljenja).FirstOrDefaultAsync(x => x.Id == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }

            return _mapper.Map<Zaposlenik, ZaposlenikDTO>(zaposlenik);
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<ZaposlenikDTO>>>> GetZaposlenici(
          string name = null,
          string surname = null,
          int? vrstaZaposljenja = null,
          int? odjel = null,
          int skip = 0,
          int? take = null) 
        {
            var zaposlenici = await _context.Zaposlenik.Include(x => x.VrstaZaposljenja).Include(x => x.Odjel).ToListAsync();
            return new ResponseDataWrapper<List<ZaposlenikDTO>>(
                _mapper.Map<List<Zaposlenik>,
                List<ZaposlenikDTO>>(
                    zaposlenici
                    .Where(x => 
                      (name == null ? true : x.Ime.Contains(name))
                      && (surname == null ? true : x.Prezime.Contains(surname))
                      && (vrstaZaposljenja == null ? true : x.VrstaZaposljenjaId == vrstaZaposljenja)
                      && (odjel == null ? true : x.OdjelId == odjel)
                    )
                    .Skip(skip)
                    .Take(take ?? zaposlenici.Count)
                    .ToList()
                )
            );
        }
    }
}
