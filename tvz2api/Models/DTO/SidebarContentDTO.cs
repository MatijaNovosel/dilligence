using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class SidebarContentDTO : BaseDTO
    {
        public string Naslov { get; set; }
        public int? KolegijId { get; set; }
        public IEnumerable<SidebarContentFile> Files { get; set; }
    }
}
