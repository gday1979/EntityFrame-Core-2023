using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.PlayersStatistics = new HashSet<PlayerStatistic>();
            this.Bets = new HashSet<Bet>();
        }
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public Team? HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team? AwayTeam { get; set; }

        public int AwayTeamGoals { get; set; }

        public int HomeTeamGoals { get; set; }

        public DateTime DateTime { get; set; }

        public decimal HomeTeamBetRate { get; set; }

        public decimal AwayTeamBetRate { get; set; }

        public decimal DrawBetRate { get; set; }
        [MaxLength(20)]
        public string Result { get; set; } = null!;

        public ICollection<PlayerStatistic> PlayersStatistics { get; set; }

        public ICollection<Bet>Bets { get; set; }
    }
}
