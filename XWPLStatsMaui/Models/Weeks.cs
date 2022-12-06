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
        public int WeeksWon { get; set; }
        public int WeeksLost { get; set; }
        public int WeeksPlayed { get; set; }
        public decimal WeeksAverage { get; set; }
    }
}
