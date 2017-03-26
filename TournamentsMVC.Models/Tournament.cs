using System;
using System.ComponentModel.DataAnnotations;

namespace TournamentsMVC.Models
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public decimal Prize { get; set; }
    }
}