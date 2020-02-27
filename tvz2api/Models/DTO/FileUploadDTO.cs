using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tvz2api.Models.DTO
{
    public class FileUploadDTO
    {
        public IFormFile File { get; set; }
    }
}
