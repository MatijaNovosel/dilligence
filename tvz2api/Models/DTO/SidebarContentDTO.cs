using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class SidebarContentDTO
    {
        public string Naslov { get; set; }
        public int? KolegijId { get; set; }
        public ICollection<SidebarContentFileDTO> SidebarContentFile { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
