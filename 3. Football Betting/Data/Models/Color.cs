using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        [MaxLength(15)]
        public string Name { get; set; } = null!;

        public ICollection<Team>? PrimaryKitTeams { get; set; }

        public ICollection<Team>? SecondaryKitTeams { get; set; }

    }
}
