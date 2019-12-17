using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tvz2api.Helpers;
using tvz2api.Models;
using tvz2api.Models.DTO;

namespace tvz2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public StudentController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Student.Include(x => x.Smjer).FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return _mapper.Map<Student, StudentDTO>(student);
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<ResponseDataWrapper<List<StudentDTO>>>> GetStudent(int skip = 0, int? take = null)
        {
            List<Student> studenti = await _context.Student.Include(x => x.Smjer).ToListAsync();
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
    }
}
