using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets=new HashSet<Bet>();
        }
        public int UserId { get; set; }
        [MaxLength(15)]
        public string Username { get; set; } = null!;
        [MaxLength(20)]
        public string Password { get; set; } = null!;
        [MaxLength(20)]
        public string Email { get; set; } = null!;
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        public decimal Balance { get; set; }

        public ICollection<Bet>Bets { get; set; }
    }
}
