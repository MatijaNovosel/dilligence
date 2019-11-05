using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tvz2api.Models;

namespace tvz2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KolegijController : ControllerBase
    {
        private readonly tvz2Context _context;

        public KolegijController(tvz2Context context)
        {
            _context = context;
        }

        // GET: api/Kolegij
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kolegij>>> GetKolegij()
        {
            return await _context.Kolegij.ToListAsync();
        }

        // GET: api/Kolegij/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kolegij>> GetKolegij(int id)
        {
            var kolegij = await _context.Kolegij.FindAsync(id);

            if (kolegij == null)
            {
                return NotFound();
            }

            return kolegij;
        }

        private bool KolegijExists(int id)
        {
            return _context.Kolegij.Any(e => e.Id == id);
        }
    }
}
