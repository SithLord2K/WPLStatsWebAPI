using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWPLStats.Models
{
    public class WeekFullInfo
    {
        public int WeekNumber { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public bool WeekWon { get; set; }
        public decimal Average { get; set; }
        public int TeamPlayed { get; set; }
        public string TeamName { get; set; }
        public string DatePlayed { get; set; }
        public bool Home { get; set; }
        public bool Forfeit { get; set; }
        public string Captain { get; set; }
    }
}
