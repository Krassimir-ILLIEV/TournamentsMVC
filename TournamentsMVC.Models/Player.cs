using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentsMVC.Models
{
    public class Player 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string NickName { get; set; }

        public string Picture { get; set; }

        [Required]
        [MinLength(8)]
        [StringLength(50)]
        public string Email { get; set; }

        public double? Rating { get; set; }

        public int? Votes { get; set; }

        public bool? IsCoach { get; set; }

        public string CV { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}