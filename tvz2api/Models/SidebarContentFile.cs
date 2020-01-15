using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class SidebarContentFile
    {
        public int Id { get; set; }
        public int? SidebarContentId { get; set; }
        public int? FileId { get; set; }

        public virtual File File { get; set; }
        public virtual SidebarContent SidebarContent { get; set; }
    }
}
