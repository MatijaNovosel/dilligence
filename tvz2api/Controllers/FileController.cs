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
using System.IO;

namespace tvz2api.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly tvz2Context _context;
        private readonly IMapper _mapper;

        public FileController(tvz2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpPost("Upload")]  
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm(Name = "file")] IFormFile file)  
        {  
            if (file == null || file.Length == 0) 
            {
              return Content("File not selected!");  
            }

            using (var stream = new FileStream(Path.Combine("Files", file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        
            return Ok("File uploaded!");
        }
    }
}
