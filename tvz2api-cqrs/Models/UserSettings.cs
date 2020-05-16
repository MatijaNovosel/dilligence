using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool? DarkMode { get; set; }
        public string Locale { get; set; }
        public bool? Popups { get; set; }

        public virtual User User { get; set; }
    }
}
