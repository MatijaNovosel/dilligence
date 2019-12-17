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
    public class PretplataController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public PretplataController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pretplata/5
        [HttpGet("{studentId}")]
        public async Task<ActionResult<List<int?>>> GetPretplata(int studentId)
        {
            var pretplate = await _context.Pretplata.Where(x => x.StudentId == studentId).ToListAsync();
            List<int?> kolegijIDs = pretplate.Select(x => x.KolegijId).ToList();
            return kolegijIDs;
        }
    }
}
