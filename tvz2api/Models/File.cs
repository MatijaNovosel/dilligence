using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class File
    {
        public File()
        {
            SidebarContentFile = new HashSet<SidebarContentFile>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        public virtual ICollection<SidebarContentFile> SidebarContentFile { get; set; }
    }
}
