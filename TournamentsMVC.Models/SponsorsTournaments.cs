namespace TournamentsMVC.Models
{
    public class SponsorsTournaments
    {
        public decimal Sum { get; set; }

        public int SponsorId { get; set; }

        public virtual Sponsor Sponsor { get; set; }
    }
}