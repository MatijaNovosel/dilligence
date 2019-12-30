using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class SidebarContentFileDTO : BaseDTO
    {
        public string Naziv { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int? SidebarContentId { get; set; }
    }
}
