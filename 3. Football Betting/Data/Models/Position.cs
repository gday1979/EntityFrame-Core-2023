using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            this.Players= new HashSet<Player>();    
        }
        public int PositionId { get; set; }
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        public ICollection<Player>Players { get; set; }
    }
}
