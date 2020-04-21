using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
    public partial class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? ImageFileId { get; set; }
    }
}
