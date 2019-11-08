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

        // GET: api/Kolegij
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> GetKolegij(int skip = 0, int? take = null)
        {
            List<Kolegij> kolegiji = await _context.Kolegij.ToListAsync();
            return new ResponseDataWrapper<List<KolegijDTO>>(
                _mapper.Map<List<Kolegij>, 
                List<KolegijDTO>>(
                    kolegiji
                    .Skip(skip)
                    .Take(take ?? kolegiji.Count)
                    .ToList()
                )
            );
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

        /// <summary>
        /// API endpoint for getting a List of KolegijDTO objects filtered by an array of smjerIDs.
        /// </summary>
        /// <param name="smjerIDs"> An array of integer values that represent the IDs of individual Smjer table entries. </param>
        /// <param name="skip"> The number of objects in the list that will be skipped. </param>
        /// <param name="take"> The number of objects in the list that the endpoint will return. Can be NULL, in that case it returns everything. </param>
        /// <returns> A ResponseDataWrapper object with the type of List<KolegijDTO> </returns>
        // GET: api/Kolegij/bySmjerID
        [HttpGet("bySmjerID")]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> GetKolegijBySmjerID([FromQuery] List<SmjerEnum> smjerIDs, int skip = 0, int? take = null)
        {
            List<Kolegij> kolegiji = await _context.Kolegij.ToListAsync();
            return new ResponseDataWrapper<List<KolegijDTO>>(
                _mapper.Map<List<Kolegij>,
                List<KolegijDTO>>(
                    kolegiji
                    .Where(x => smjerIDs.Contains(SmjerEnum.(x.SmjerId)))
                    .Skip(skip)
                    .Take(take ?? kolegiji.Count)
                    .ToList()
                )
            );
        }
    }
}
