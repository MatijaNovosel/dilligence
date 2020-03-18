using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Message = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int FirstParticipantId { get; set; }
        public int SecondParticipantId { get; set; }

        public virtual Korisnik FirstParticipant { get; set; }
        public virtual Korisnik SecondParticipant { get; set; }
        public virtual ICollection<Message> Message { get; set; }
    }
}
