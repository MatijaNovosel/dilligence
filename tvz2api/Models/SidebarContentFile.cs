using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class SidebarContentFile
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Path { get; set; }
        public int? SidebarContentId { get; set; }

        public virtual SidebarContent SidebarContent { get; set; }
    }
}
