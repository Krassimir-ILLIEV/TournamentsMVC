using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentsMVC.Models;

namespace TournamentsMVC.Areas.Admin.Models
{
    public class AddPlayerViewModel
    {
        [Required]
        [MinLength(2)]
        [StringLength(50)] //[Display(FirstName = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string NickName { get; set; }

        //public string Picture { get; set; }
        [Display(Name = "Player photo")]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Required]
        [MinLength(8)]
        [StringLength(50)]
        public string Email { get; set; }

        public double? Rating { get; set; }

        public int? Votes { get; set; }

        public bool? IsCoach { get; set; }

        public string CV { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}