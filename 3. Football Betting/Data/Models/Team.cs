using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; } = null!;

        public string LogoUrl { get; set; } = null!;
        [MaxLength(3)]
        public string Initials { get; set; }=null!;

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }

        public virtual Color? PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }

        public virtual Color? SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public virtual Town? Town { get; set; }

        public virtual ICollection<Game> ?AwayGames { get; set; }

        public virtual ICollection<Player>?Players { get; set; }

        public virtual ICollection<Game>? HomeGames { get; set; }

    }
}
