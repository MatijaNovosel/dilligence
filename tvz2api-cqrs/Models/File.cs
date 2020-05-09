using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class File
    {
        public File()
        {
            NotificationFiles = new HashSet<NotificationFiles>();
            SidebarContentFile = new HashSet<SidebarContentFile>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public long? Size { get; set; }

        public virtual ICollection<NotificationFiles> NotificationFiles { get; set; }
        public virtual ICollection<SidebarContentFile> SidebarContentFile { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
