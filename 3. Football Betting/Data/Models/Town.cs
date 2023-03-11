using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Town
    {
        public int TownId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public  Country? Country { get; set; }

        public ICollection<Team>? Teams { get; set; }
    }
}
