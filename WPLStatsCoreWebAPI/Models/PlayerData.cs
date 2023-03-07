using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WPLStatsCoreWebAPI.Models
{
    [PrimaryKey(nameof(PlayerId), nameof(WeekNumber))]
    public class PlayerData
    {
        public int PlayerId { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int WeekNumber { get; set; }
    }
}
