using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Model
{
    public partial class SidebarContentFile
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }
        [StringLength(255)]
        public string Path { get; set; }
        [Column("SidebarContentID")]
        public int? SidebarContentId { get; set; }

        [ForeignKey("SidebarContentId")]
        [InverseProperty("SidebarContentFile")]
        public virtual SidebarContent SidebarContent { get; set; }
    }
}
