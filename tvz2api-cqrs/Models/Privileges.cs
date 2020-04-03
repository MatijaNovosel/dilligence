using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Privileges
    {
        public Privileges()
        {
            UserPrivileges = new HashSet<UserPrivileges>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserPrivileges> UserPrivileges { get; set; }
    }
}
