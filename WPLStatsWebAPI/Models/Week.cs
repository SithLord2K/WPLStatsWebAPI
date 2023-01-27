using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XWPLStatsWebAPI.Models
{
    public class Week
    {
        public int Id { get; set; }
        public int WeeksWon { get; set; }
        public int WeeksLost { get; set; }
        public int WeeksPlayed { get; set; }
        public decimal WeeksAverage { get; set; }
    }
}