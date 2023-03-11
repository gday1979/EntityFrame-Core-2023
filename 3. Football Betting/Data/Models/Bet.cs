using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public string Prediction { get; set; } = null!;

        public DateTime DateTime { get; set; } 

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public int GameId { get; set; }

        public Game Game { get; set; } = null!;
    }
}
