using System;
using System.Collections.Generic;
using System.Text;

namespace XWPLStats.Models
{
    public class TeamStats
    {
        public int TotalGamesWon { get; set; }
        public int TotalGamesLost { get; set; }
        public int TotalGamesPlayed { get; set; }
        public decimal TotalAverage { get; set; }
        public int WeeksPlayed { get; set; }
    }
}
