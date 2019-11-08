using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Model
{
    public partial class SidebarContent
    {
        public SidebarContent()
        {
            SidebarContentFile = new HashSet<SidebarContentFile>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naslov { get; set; }
        [Column("KolegijID")]
        public int? KolegijId { get; set; }

        [ForeignKey("KolegijId")]
        [InverseProperty("SidebarContent")]
        public virtual Kolegij Kolegij { get; set; }
        [InverseProperty("SidebarContent")]
        public virtual ICollection<SidebarContentFile> SidebarContentFile { get; set; }
    }
}
