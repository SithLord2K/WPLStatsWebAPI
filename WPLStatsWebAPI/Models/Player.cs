using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XWPLStatsWebAPI.Models
{
    public class Player
    {
        [Required]
        public int EntryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public decimal Average { get; set; }
        public int WeeksNumber { get; set; }
    }
}