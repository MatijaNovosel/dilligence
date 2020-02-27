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
    public class ExamController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public ExamController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Exam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDTO>> getExam(int id)
        {
            ExamDTO exam = _mapper.Map<Exam, ExamDTO>(await _context.Exam.FirstOrDefaultAsync(x => x.Id == id));
            return exam;
        }
    }
}
