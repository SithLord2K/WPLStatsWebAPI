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

    }
}
