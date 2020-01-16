using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class ConnectSidebarFileDTO
    {
        public List<int> FileIDs { get; set; }
        public int SidebarContentId { get; set; }
    }
}
