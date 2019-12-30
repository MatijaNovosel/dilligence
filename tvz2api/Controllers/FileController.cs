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
          
            byte[] bytes = null;
            
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
              string contentAsString = reader.ReadToEnd();
              bytes = Convert.FromBase64String(contentAsString);
            }
            
            _context.SidebarContentFile.Add(new SidebarContentFile
            {
                Naziv = Path.GetFileName(file.FileName),
                ContentType = file.ContentType,
                Data = bytes,
                SidebarContentId = 1
            });
            
            await _context.SaveChangesAsync();
            return Ok("File uploaded");
        }
        
        [HttpPost("Download")]
        public async Task<IActionResult> Download(int? id) 
        {
            SidebarContentFile file = await _context.SidebarContentFile.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(File(Convert.ToBase64String(file.Data), file.ContentType, file.Naziv));
        }
    }
}
