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
using System.Net.Http.Headers;

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
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file == null || file.Length == 0) {
              return Content("File not selected!");  
            }
            
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            using (var ms = new MemoryStream())
            {
              file.CopyTo(ms);
              var fileBytes = ms.ToArray();
              _context.SidebarContentFile.Add(new SidebarContentFile
              {
                  Naziv = Path.GetFileName(file.FileName),
                  ContentType = file.ContentType,
                  Data = fileBytes,
                  SidebarContentId = 1
              });
            }
            
            await _context.SaveChangesAsync();
            return Ok("File uploaded");
        }
    }
}
