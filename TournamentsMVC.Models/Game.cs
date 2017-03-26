using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentsMVC.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Result { get; set;}  //perhaps some other representation 2 fields with getters, tostring adds ':'
        public string Place { get; set; } //where the game took place
        public int HostId { get; set; }
        public virtual Team Host { get; set; }
        public int GuestId { get; set; }
        public virtual Team Guest { get; set; }
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}