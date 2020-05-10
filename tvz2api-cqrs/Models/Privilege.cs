using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Privilege
    {
        public Privilege()
        {
            UserPrivilege = new HashSet<UserPrivilege>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserPrivilege> UserPrivilege { get; set; }
    }
}
