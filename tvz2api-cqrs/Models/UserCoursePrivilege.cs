using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class UserCoursePrivilege
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PrivilegeId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual User User { get; set; }
    }
}
