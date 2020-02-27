using System;

namespace tvz2api.Models.DTO
{
    public class UserForListDto 
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
    }
}