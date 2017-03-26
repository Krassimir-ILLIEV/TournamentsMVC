using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentsMVC.Models
{
    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string Name { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}