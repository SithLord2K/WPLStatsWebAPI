using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XWPLStats.Models
{
    public class Weeks
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int WeekWin { get; set; }
        public int WeekLoss { get; set; }
        public bool WeekWon { get; set; }
        public int WeeksPlayed { get; set; }
        public decimal WeeksAverage { get; set; }
        public int TeamPlayed { get; set; }
        public bool Home { get; set; }
        public DateTime DatePlayed { get; set; }
        public string Captain { get; set; }
        public List<TeamDetails> PickedTeam { get; internal set; }
    }
}
