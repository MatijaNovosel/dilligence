using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class UserPrivileges
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PrivilegeId { get; set; }

        public virtual Privileges Privilege { get; set; }
        public virtual Korisnik User { get; set; }
    }
}
