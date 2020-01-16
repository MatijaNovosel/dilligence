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
        public async Task<ActionResult<KolegijExtendedDTO>> Get(int id)
        {
            var kolegij = await _context.Kolegij.Include(x => x.Vijest).FirstOrDefaultAsync(x => x.Id == id);
            
            if (kolegij == null)
            {
                return NotFound();
            }

            KolegijExtendedDTO kolegijExtended = _mapper.Map<Kolegij, KolegijExtendedDTO>(kolegij);
            kolegijExtended.Vijesti = _mapper.Map<List<Vijest>, List<VijestDTO>>(_context.Vijest.Where(x => x.KolegijId == id).ToList());
            kolegijExtended.Studenti = _mapper.Map<List<Student>, List<StudentDTO>>(_context.Student.Where(x => x.StudentKolegij.Any(y => y.KolegijId == id)).OrderBy(x => x.Prezime).ToList());

            return kolegijExtended;
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<KolegijDTO>>>> Get(
          [FromQuery(Name = "smjerIDs[]")] List<SmjerEnum> smjerIDs = null,
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
                      (smjerIDs == null || smjerIDs.Count == 0 ? true : smjerIDs.Contains((SmjerEnum)x.SmjerId))
                      && x.Ects >= minECTS 
                      && x.Ects <= maxECTS 
                      && (name == null ? true : x.Naziv.ToLower().Contains(name.ToLower()))
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
            var kolegiji = await _context.Kolegij.Where(x => x.Pretplata.Any(y => y.StudentId == studentId)).ToListAsync();
            return new ResponseDataWrapper<List<KolegijDTO>>(_mapper.Map<List<Kolegij>, List<KolegijDTO>>(kolegiji)); 
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
            var studenti = await _context.Student.Where(x => x.StudentKolegij.Any(y => y.KolegijId == kolegijId)).OrderBy(x => x.Prezime).ToListAsync();
            return new ResponseDataWrapper<List<StudentDTO>>(
                _mapper.Map<List<Student>, List<StudentDTO>>(studenti.Skip(skip).Take(take ?? studenti.Count).ToList())
            ); 
        }
        
        [HttpGet("SidebarContent/{kolegijId}")]
        public async Task<ActionResult<ResponseDataWrapper<List<SidebarContentDTO>>>> GetSidebarContent(int kolegijId) 
        {
            var sidebarContent = _mapper.Map<List<SidebarContent>, List<SidebarContentDTO>>(
                await _context
                .SidebarContent
                .Where(x => x.KolegijId == kolegijId)
                .Include(x => x.SidebarContentFile)
                .ThenInclude(x => x.File)
                .ToListAsync()
            );

            sidebarContent.ForEach(x => x.Files = x.SidebarContentFile.Select(y => y.File));

            return new ResponseDataWrapper<List<SidebarContentDTO>>(sidebarContent); 
        }

        [HttpPost("ConnectSidebarFile")]
        public async Task<IActionResult> ConnectSidebarFile(ConnectSidebarFileDTO model) 
        {
            foreach(var id in model.FileIDs) 
            {
                await _context.SidebarContentFile.AddAsync(new SidebarContentFile {
                    FileId = id,
                    SidebarContentId = model.SidebarContentId
                });
            }

            await _context.SaveChangesAsync();

            return Ok("Connected!");
        }
    }
}
