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
        public async Task<ActionResult<KolegijDTO>> GetKolegij(int id)
        {
            var kolegij = await _context.Kolegij.FindAsync(id);

            if (kolegij == null)
            {
                return NotFound();
            }

            return _mapper.Map<Kolegij, KolegijDTO>(kolegij);
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> GetKolegij(
          [FromQuery(Name = "smjerIDs[]")] List<SmjerEnum> smjerIDs,
          string name = null,
          int minECTS = 1,
          int maxECTS = 6,
          string isvu = null,
          int skip = 0,
          int? take = null) {
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
    }
}
