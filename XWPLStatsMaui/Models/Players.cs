using System;
using System.Collections.Generic;
using System.Text;

namespace XWPLStats.Models
{
    public class Players
    {
        public int EntryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int GamesPlayed { get; set; }
        public decimal Average { get; set; }
        public int WeekNumber { get; set; }
    }
}
